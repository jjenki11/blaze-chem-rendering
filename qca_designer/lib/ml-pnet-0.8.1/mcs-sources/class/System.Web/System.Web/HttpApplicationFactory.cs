//
// System.Web.HttpApplicationFactory
//
// Author:
// 	Patrik Torstensson (ptorsten@hotmail.com)
//	Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// (c) 2002,2003 Ximian, Inc. (http://www.ximian.com)
// (c) Copyright 2004 Novell, Inc. (http://www.novell.com)
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.SessionState;
#if !TARGET_J2EE
using System.Web.Compilation;
#else
using System.Web.Configuration;
using vmw.common;
#endif

namespace System.Web {
	class HttpApplicationFactory {
		private string _appFilename;
		private Type _appType;

		private bool _appInitialized;
		private bool _appFiredEnd;

		private Stack _appFreePublicList;
		private int _appFreePublicInstances;

		FileSystemWatcher appFileWatcher;
		FileSystemWatcher binWatcher;
		
		static private int _appMaxFreePublicInstances = 32;

		private HttpApplicationState _state;

		static IHttpHandler custApplication;

#if TARGET_J2EE
		static private HttpApplicationFactory s_Factory {
					get {
						HttpApplicationFactory factory = (HttpApplicationFactory)AppDomain.CurrentDomain.GetData("HttpApplicationFactory");
						if (factory == null) {
							factory = new HttpApplicationFactory();
							AppDomain.CurrentDomain.SetData("HttpApplicationFactory", factory);
						}
						return factory;
					}
		}
#else
		static private HttpApplicationFactory s_Factory = new HttpApplicationFactory();
#endif
		public HttpApplicationFactory() {
			_appInitialized = false;
			_appFiredEnd = false;

			_appFreePublicList = new Stack();
			_appFreePublicInstances = 0;
		}

		static private string GetAppFilename (HttpContext context)
		{
			string physicalAppPath = context.Request.PhysicalApplicationPath;
			string appFilePath = Path.Combine (physicalAppPath, "Global.asax");
			if (File.Exists (appFilePath))
				return appFilePath;

			return Path.Combine (physicalAppPath, "global.asax");
		}
#if TARGET_J2EE
		void CompileApp(HttpContext context)
		{

			try
			{
				String url = IAppDomainConfig.WAR_ROOT_SYMBOL+"/global.asax";
				_appType = System.Web.GH.PageMapper.GetObjectType(url);
			}
			catch (Exception e)
			{
				_appType = typeof (System.Web.HttpApplication);
			}
			_state = new HttpApplicationState ();

		}
#endif

#if !TARGET_J2EE
		void CompileApp (HttpContext context)
		{
			if (File.Exists (_appFilename)) {
				_appType = ApplicationFileParser.GetCompiledApplicationType (_appFilename, context);
				if (_appType == null) {
					string msg = String.Format ("Error compiling application file ({0}).", _appFilename);
					throw new ApplicationException (msg);
				}

				appFileWatcher = CreateWatcher (_appFilename, new FileSystemEventHandler (OnAppFileChanged));
			} else {
				_appType = typeof (System.Web.HttpApplication);
				_state = new HttpApplicationState ();
			}
		}
#endif

		static bool IsEventHandler (MethodInfo m)
		{
			if (m.ReturnType != typeof (void))
				return false;

			ParameterInfo [] pi = m.GetParameters ();
			int length = pi.Length;
			if (length == 0)
				return true;

			if (length != 2)
				return false;

			if (pi [0].ParameterType != typeof (object) ||
			    pi [1].ParameterType != typeof (EventArgs))
				return false;
			
			return true;
		}

		static void AddEvent (MethodInfo method, Hashtable appTypeEventHandlers)
		{
			string name = method.Name.Replace ("_On", "_");
			if (appTypeEventHandlers [name] == null) {
				appTypeEventHandlers [name] = method;
				return;
			}
			
			ArrayList list;
			if (appTypeEventHandlers [name] is MethodInfo)
				list = new ArrayList ();
			else
				list = appTypeEventHandlers [name] as ArrayList;

			list.Add (method);
		}
		
		static Hashtable GetApplicationTypeEvents (HttpApplication app)
		{
			Type appType = app.GetType ();
			Hashtable appTypeEventHandlers = new Hashtable ();
			BindingFlags flags = BindingFlags.Public    |
					     BindingFlags.NonPublic | 
					     BindingFlags.Instance |
					     BindingFlags.Static;

			MethodInfo [] methods = appType.GetMethods (flags);
			foreach (MethodInfo m in methods) {
				if (IsEventHandler (m))
					AddEvent (m, appTypeEventHandlers);
			}

			return appTypeEventHandlers;
		}

		static bool FireEvent (string method_name, object target, object [] args)
		{
			Hashtable possibleEvents = GetApplicationTypeEvents ((HttpApplication) target);
			MethodInfo method = possibleEvents [method_name] as MethodInfo;
			if (method == null)
				return false;

			if (method.GetParameters ().Length == 0)
				args = null;

			try {
				method.Invoke (target, args);
			} catch {
				// Ignore any exception here
			}
			return true;
		}

		internal static void FireOnAppStart (HttpApplication app)
		{
			object [] args = new object [] {app, EventArgs.Empty};
			FireEvent ("Application_Start", app, args);
		}

		void FireOnAppEnd ()
		{
			if (_appType == null)
				return; // we didn't even get an application

			HttpApplication app = (HttpApplication) HttpRuntime.CreateInternalObject (_appType);
			AttachEvents (app);
			FireEvent ("Application_End", app, new object [] {this, EventArgs.Empty});
			app.Dispose ();
		}

		FileSystemWatcher CreateWatcher (string file, FileSystemEventHandler hnd)
		{
			FileSystemWatcher watcher = new FileSystemWatcher ();

			watcher.Path = Path.GetFullPath (Path.GetDirectoryName (file));
			watcher.Filter = Path.GetFileName (file);

			watcher.Changed += hnd;
			watcher.Created += hnd;
			watcher.Deleted += hnd;

			watcher.EnableRaisingEvents = true;

			return watcher;
		}

		void OnAppFileChanged (object sender, FileSystemEventArgs args)
		{
			binWatcher.EnableRaisingEvents = false;
			appFileWatcher.EnableRaisingEvents = false;
			HttpRuntime.UnloadAppDomain ();
		}

		private void InitializeFactory (HttpContext context)
		{
			_appFilename = GetAppFilename (context);

			CompileApp (context);

			// Create a application object
			HttpApplication app = (HttpApplication) HttpRuntime.CreateInternalObject (_appType);

			// Startup
			app.Startup(context, HttpApplicationFactory.ApplicationState);

			// Shutdown the application if bin directory changes.
			string binFiles = HttpRuntime.BinDirectory;
			if (Directory.Exists (binFiles))
				binFiles = Path.Combine (binFiles, "*.*");

			binWatcher = CreateWatcher (binFiles, new FileSystemEventHandler (OnAppFileChanged));

			// Fire OnAppStart
			HttpApplicationFactory.FireOnAppStart (app);

			// Recycle our application instance
			RecyclePublicInstance(app);
		}

		private void Dispose() {
			ArrayList torelease = new ArrayList();
			lock (_appFreePublicList) {
				while (_appFreePublicList.Count > 0) {
					torelease.Add(_appFreePublicList.Pop());
					_appFreePublicInstances--;
				}
			}

			if (torelease.Count > 0) {
				foreach (Object obj in torelease) {
					((HttpApplication) obj).Cleanup();
				}
			}

			if (!_appFiredEnd) {
				lock (this) {
					if (!_appFiredEnd) {
						FireOnAppEnd();
						_appFiredEnd = true;
					}
				}
			}
		}

		internal static IHttpHandler GetInstance(HttpContext context)
		{
			if (custApplication != null)
				return custApplication;

			if (!s_Factory._appInitialized) {
				lock (s_Factory) {
					if (!s_Factory._appInitialized) {
						s_Factory.InitializeFactory(context);
						s_Factory._appInitialized = true;
					}
				}
			}

			return s_Factory.GetPublicInstance(context);
		}

		internal static void RecycleInstance(HttpApplication app) {
			if (!s_Factory._appInitialized)
				throw new InvalidOperationException("Factory not intialized");

			s_Factory.RecyclePublicInstance(app);
		}

		internal static void AttachEvents (HttpApplication app)
		{
			Hashtable possibleEvents = GetApplicationTypeEvents (app);
			foreach (string key in possibleEvents.Keys) {
				int pos = key.IndexOf ('_');
				if (pos == -1 || key.Length <= pos + 1)
					continue;

				string moduleName = key.Substring (0, pos);
				object target;
				if (moduleName == "Application") {
					target = app;
				} else {
					target = app.Modules [moduleName];
					if (target == null)
						continue;
				}

				string eventName = key.Substring (pos + 1);
				EventInfo evt = target.GetType ().GetEvent (eventName);
				if (evt == null)
					continue;
			
				string usualName = moduleName + "_" + eventName;
				object methodData = possibleEvents [usualName];
				if (methodData == null)
					continue;

				if (methodData is MethodInfo) {
					AddHandler (evt, target, app, (MethodInfo) methodData);
					continue;
				}

				ArrayList list = (ArrayList) methodData;
				foreach (MethodInfo method in list)
					AddHandler (evt, target, app, method);
			}
		}

		static void AddHandler (EventInfo evt, object target, HttpApplication app, MethodInfo method)
		{
			int length = method.GetParameters ().Length;

			if (length == 0) {
				NoParamsInvoker npi = new NoParamsInvoker (app, method.Name);
				evt.AddEventHandler (target, npi.FakeDelegate);
			} else {
				evt.AddEventHandler (target, Delegate.CreateDelegate (
							typeof (EventHandler), app, method.Name));
			}
		}

#if TARGET_J2EE
		internal HttpApplication GetPublicInstance()
		{
			HttpApplication app = null;

			lock (_appFreePublicList)
			{
				if (_appFreePublicInstances > 0)
				{
					app = (HttpApplication) _appFreePublicList.Pop();
					_appFreePublicInstances--;
				}
			}
			return app;
		}
#endif

		private IHttpHandler GetPublicInstance(HttpContext context) {
			HttpApplication app = null;

			lock (_appFreePublicList) {
				if (_appFreePublicInstances > 0) {
					app = (HttpApplication) _appFreePublicList.Pop();
					_appFreePublicInstances--;
				}
			}

			if (app == null) {
				// Create non-public object
				app = (HttpApplication) HttpRuntime.CreateInternalObject(_appType);

				app.Startup(context, HttpApplicationFactory.ApplicationState);
			}

			return (IHttpHandler) app;
		}

		internal void RecyclePublicInstance(HttpApplication app) {
			lock (_appFreePublicList) {
				if (_appFreePublicInstances < _appMaxFreePublicInstances) {
					_appFreePublicList.Push(app);
					_appFreePublicInstances++;

					app = null;
				}
			}
			
			if  (app != null) {
				app.Cleanup();
			}
		}

		static HttpStaticObjectsCollection MakeStaticCollection (ArrayList list)
		{
			if (list == null || list.Count == 0)
				return null;

			HttpStaticObjectsCollection coll = new HttpStaticObjectsCollection ();
			foreach (ObjectTagBuilder tag in list) {
				coll.Add (tag);
			}

			return coll;
		}
		
		static internal HttpApplicationState ApplicationState {
#if TARGET_J2EE
			get {
				if (null == s_Factory._state) {
					HttpStaticObjectsCollection app = null;
					HttpStaticObjectsCollection ses = null;
					s_Factory._state = new HttpApplicationState (app, ses);
				}

				return s_Factory._state;
			}
#else
			get {
				if (null == s_Factory._state) {
					HttpStaticObjectsCollection app = MakeStaticCollection (GlobalAsaxCompiler.ApplicationObjects);
					HttpStaticObjectsCollection ses = MakeStaticCollection (GlobalAsaxCompiler.SessionObjects);
					s_Factory._state = new HttpApplicationState (app, ses);
				}

				return s_Factory._state;
			}
#endif
		}

		internal static void EndApplication() {
			s_Factory.Dispose();
		}

		public static void SetCustomApplication (IHttpHandler customApplication)
		{
			custApplication = customApplication;
		}

		internal Type AppType {
			get { return _appType; }
		}

		internal static void SignalError(Exception exc) {
			// TODO: Raise an error (we probably don't have a HttpContext)
		}
	}
}
