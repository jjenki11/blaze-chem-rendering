//
// System.Web.SessionState.SessionInProcHandler
//
// Authors:
//	Stefan G�rling (stefan@gorling.se)
//	Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// (C) 2003 Stefan G�rling
// Copyright (c) 2004 Novell, Inc. (http://www.novell.com)
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
using System.IO;
using System.Collections;
using System.Web.Caching;

namespace System.Web.SessionState
{
	class SessionInProcHandler : ISessionHandler
	{
		SessionConfig config;
		CacheItemRemovedCallback removedCB;
		
		public void Dispose () { }

		public void Init (SessionStateModule module, HttpApplication context, SessionConfig config)
		{
			removedCB = new CacheItemRemovedCallback (module.OnSessionRemoved);
			this.config = config;
		}

		public void UpdateHandler (HttpContext context, SessionStateModule module)
		{
			HttpSessionState session = context.Session;
			if (session == null || session.IsReadOnly || !session._abandoned)
				return;

			HttpRuntime.Cache.Remove ("@@@InProc@" + session.SessionID);
		}

		public HttpSessionState UpdateContext (HttpContext context, SessionStateModule module,
							bool required, bool read_only, ref bool isNew)
		{
			if (!required)
				return null;

			Cache cache = HttpRuntime.Cache;
			HttpSessionState state = null;
			string id = SessionId.Lookup (context.Request, config.CookieLess);
			
			if (id != null) {
				state = (HttpSessionState) cache ["@@@InProc@" + id];
				if (state != null)
					return state;
			}

			// Create a new session.
			string sessionID = SessionId.Create (module.Rng);
			state = new HttpSessionState (sessionID, // unique identifier
						new SessionDictionary(), // dictionary
						HttpApplicationFactory.ApplicationState.SessionObjects,
						config.Timeout, //lifetime before death.
						true, //new session
						false, // is cookieless
						SessionStateMode.InProc,
						read_only); //readonly

			TimeSpan timeout = new TimeSpan (0, config.Timeout, 0);
			cache.InsertPrivate ("@@@InProc@" + sessionID, state, null, DateTime.Now + timeout,
					timeout, CacheItemPriority.AboveNormal, removedCB);

			isNew = true;
			return state;
		}
	}
}

