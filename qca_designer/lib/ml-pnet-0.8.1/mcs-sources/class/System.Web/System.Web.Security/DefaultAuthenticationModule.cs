//
// System.Web.Security.DefaultAuthenticationModule
//
// Authors:
//	Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// (C) 2002 Ximian, Inc (http://www.ximian.com)
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

using System.Web;
using System.Security.Principal;

namespace System.Web.Security
{
	public sealed class DefaultAuthenticationModule : IHttpModule
	{
		static GenericIdentity defaultIdentity = new GenericIdentity ("", "");

		public event DefaultAuthenticationEventHandler Authenticate;

		public void Dispose ()
		{
		}

		public void Init (HttpApplication app)
		{
			app.DefaultAuthentication += new EventHandler (OnDefaultAuthentication);
		}

		void OnDefaultAuthentication (object sender, EventArgs args)
		{
			HttpApplication app = (HttpApplication) sender;
			HttpContext context = app.Context;

			if (context.User == null && Authenticate != null)
				Authenticate (this, new DefaultAuthenticationEventArgs (context));

			if (context.User == null)
				context.User = new GenericPrincipal (defaultIdentity, new string [0]);

			app.SetPrincipal (context.User);
		}
	}
}

