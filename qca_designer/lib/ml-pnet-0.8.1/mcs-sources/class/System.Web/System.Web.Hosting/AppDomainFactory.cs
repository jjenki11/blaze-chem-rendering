//
// System.Web.Hosting.AppDomainFactory.cs
//
// Authors:
// 	Bob Smith <bob@thestuff.net>
// 	Gonzalo Paniagua (gonzalo@ximian.com)
//
// (C) Bob Smith
// (c) Copyright 2002 Ximian, Inc. (http://www.ximian.com)
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
using System.Security;
using System.Security.Policy;
using System.Text;

namespace System.Web.Hosting
{
        public sealed class AppDomainFactory : IAppDomainFactory
	{
		static int nDomain = 0;
		static string [] domainData = { ".appDomain",
						".appId",
						".appPath",
						".appVPath",
						".appName",
						".domainId"
						};

                public object Create (string module,
				      string typeName,
				      string appId,
				      string appPath,
				      string strUrlOfAppOrigin,
				      int iZone)
		{
			appPath = Path.GetFullPath (appPath);
			if (appPath [appPath.Length - 1] == '\\')
				appPath += '\\';

			StringBuilder sb = new StringBuilder (appId);
			sb.Append ('-');
			lock (domainData){
				sb.Append (nDomain.ToString ());
				nDomain++;
			}

			sb.Append ('-' + DateTime.Now.ToString ());
			string domainId = sb.ToString ();
			sb = null;

			int slash = appId.IndexOf ('/');
			string vPath;
			if (slash == -1)
				vPath = "/";
			else
				vPath = appId.Substring (slash + 1);

			string appName = (appId.GetHashCode () + appPath.GetHashCode ()).ToString ("x");
			AppDomainSetup domainSetup = new AppDomainSetup ();

			PopulateDomainBindings (domainId,
						appId,
						appName,
						appPath,
						vPath,
						domainSetup,
						null);

			// May be adding more assemblies and such to Evidence?
			AppDomain domain = AppDomain.CreateDomain (domainId,
								   AppDomain.CurrentDomain.Evidence,
								   domainSetup);

			string [] settings = new string [6];
			settings [0] = "*";
			settings [1] = appId;
			settings [2] = appPath;
			settings [3] = vPath;
			settings [4] = appName;
			settings [5] = domainId;
			for (int i = 0; i < 6; i++)
				domain.SetData (domainData [i], settings [i]);

			object o = null;
			try {
				o = domain.CreateInstance (module, typeName);
			} catch {
				AppDomain.Unload (domain);
				o = null;
			}

			return o;
		}

		internal static void PopulateDomainBindings (string domainId,
							     string appId,
							     string appName,
							     string appPath,
							     string appVPath,
							     AppDomainSetup setup,
							     IDictionary dict)
		{
			setup.PrivateBinPath = "bin";
			setup.PrivateBinPathProbe = "*";
			setup.ShadowCopyFiles = "true";
			
			//HACK: we should use Uri (appBase).ToString () once Uri works fine.
			string uri = "file://" + appPath;
			if (Path.DirectorySeparatorChar != '/')
				uri = uri.Replace (Path.DirectorySeparatorChar, '/');
				
			setup.ApplicationBase = uri;
			setup.ApplicationName = appName;
			string temp = Path.GetTempPath ();
			setup.DynamicBase = Path.Combine (temp, Environment.UserName + "-temp-aspnet");
			string webConfigName = Path.Combine (appPath, "Web.config");
			if (File.Exists (webConfigName))
				setup.ConfigurationFile = webConfigName;
			else
				setup.ConfigurationFile = Path.Combine (appPath, "web.config");

			if (dict != null) {
				dict.Add (domainData [0], "*");
				dict.Add (domainData [1], appId);
				dict.Add (domainData [2], appPath);
				dict.Add (domainData [3], appVPath);
				dict.Add (domainData [4], appName);
				dict.Add (domainData [5], domainId);
			}
		}
	}
}

