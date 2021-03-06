//
// HttpWebRequestTest.cs - NUnit Test Cases for System.Net.HttpWebRequest
//
// Authors:
//   Lawrence Pit (loz@cable.a2000.nl)
//   Martin Willemoes Hansen (mwh@sysrq.dk)
//
// (C) 2003 Martin Willemoes Hansen
//

using NUnit.Framework;
using System;
using System.Net;
using System.Collections;

namespace MonoTests.System.Net
{

[TestFixture]
public class HttpWebRequestTest : Assertion
{
        [Test]
		[Category("InetAccess")] 
       public void Sync ()
        {
		HttpWebRequest req = (HttpWebRequest) WebRequest.Create ("http://www.google.com");
		AssertNotNull ("req:If Modified Since: ", req.IfModifiedSince);

		req.UserAgent = "MonoClient v1.0";
		AssertEquals ("req Header 1", "User-Agent", req.Headers.GetKey (0));
		AssertEquals ("req Header 2", "MonoClient v1.0", req.Headers.Get (0));

		HttpWebResponse res = (HttpWebResponse) req.GetResponse ();
		AssertEquals ("res:HttpStatusCode: ", "OK", res.StatusCode.ToString ());
		AssertEquals ("res:HttpStatusDescription: ", "OK", res.StatusDescription);
		
		AssertEquals ("res Header 1", "text/html", res.Headers.Get ("Content-Type"));
		AssertNotNull ("Last Modified: ", res.LastModified);
		
		AssertEquals ("res:", 0, res.Cookies.Count);
			
		res.Close ();
	}
	
        [Test]
        public void Async ()
        {
	}
	
        [Test]
	public void AddRange ()
	{
		HttpWebRequest req = (HttpWebRequest) WebRequest.Create ("http://www.google.com");
		req.AddRange (10);
		req.AddRange (50, 90);
		req.AddRange ("bytes", 100); 
		req.AddRange ("bytes", 100, 120);
		Assertion.AssertEquals ("#1", "bytes=10-,50-90,100-,100-120", req.Headers ["Range"]);
		try {
			req.AddRange ("bits", 2000);
			Assertion.Fail ("#2");
		} catch (InvalidOperationException) {}
	}

        [Test]
	[Category("InetAccess")] 
	public void Cookies1 ()
	{
		// The purpose of this test is to ensure that the cookies we get from a request
		// are stored in both, the CookieCollection in HttpWebResponse and the CookieContainer
		// in HttpWebRequest.
		// If this URL stops sending *one* and only one cookie, replace it.
		string url = "http://www.elmundo.es";
		CookieContainer cookies = new CookieContainer ();
		HttpWebRequest req = (HttpWebRequest) WebRequest.Create (url);
		req.KeepAlive = false;
		req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv; 1.7.6) Gecko/20050317 Firefox/1.0.2";
		req.CookieContainer = cookies;
		Assertion.AssertEquals ("#01", 0, cookies.Count);
		using (HttpWebResponse res = (HttpWebResponse) req.GetResponse()) {
			CookieCollection coll = req.CookieContainer.GetCookies (new Uri (url));
			Assertion.AssertEquals ("#02", 1, coll.Count);
			Assertion.AssertEquals ("#03", 1, res.Cookies.Count);
			Cookie one = coll [0];
			Cookie two = res.Cookies [0];
			Assertion.AssertEquals ("#04", true, object.ReferenceEquals (one, two));
		}
	}

/* Unused code for now, but might be useful for debugging later

	private void WriteHeaders (string label, WebHeaderCollection col) 
	{
		label += "Headers";
		if (col.Count == 0)
			Console.WriteLine (label + "Nothing in web headers collection\n");
		else
			Console.WriteLine (label);
		for (int i = 0; i < col.Count; i++)
			Console.WriteLine ("\t" + col.GetKey (i) + ": " + col.Get (i));
	}
	
	private void WriteCookies (string label, CookieCollection col) 
	{
		label += "Cookies";
		if (col.Count == 0)
			Console.WriteLine (label + "Nothing in cookies collection\n");
		else
			Console.WriteLine (label);
		for (int i = 0; i < col.Count; i++)
			Console.WriteLine ("\t" + col [i]);
	}
*/

}

}

