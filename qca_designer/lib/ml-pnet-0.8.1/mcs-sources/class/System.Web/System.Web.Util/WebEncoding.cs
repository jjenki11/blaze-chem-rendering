//
// System.Web.Util.WebEncoding
//
// Authors:
//	Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// (C) 2002 Ximian, Inc (http://www.ximian.com)
// (C) 2003 Novell, Inc (http://www.novell.com)
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

using System.Text;
using System.Web.Configuration;

namespace System.Web.Util
{
	internal class WebEncoding
	{
		static public Encoding FileEncoding {
			get {
				GlobalizationConfiguration gc = GlobalizationConfiguration.GetInstance (null);
				if (gc == null)
					return Encoding.Default;

				return gc.FileEncoding;
			}
		}

		static public Encoding ResponseEncoding {
			get {
				GlobalizationConfiguration gc = GlobalizationConfiguration.GetInstance (null);
				if (gc == null)
					return Encoding.Default;

				return gc.ResponseEncoding;
			}
		}

		static public Encoding RequestEncoding {
			get {
				GlobalizationConfiguration gc = GlobalizationConfiguration.GetInstance (null);
				if (gc == null)
					return Encoding.Default;

				return gc.RequestEncoding;
			}
		}
	}
}

