//
// SignedXmlTest.cs - NUnit Test Cases for SignedXml
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// (C) 2002, 2003 Motus Technologies Inc. (http://www.motus.com)
// Copyright (C) 2004-2005 Novell, Inc (http://www.novell.com)
//

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

using NUnit.Framework;

namespace MonoTests.System.Security.Cryptography.Xml {

	public class SignedXmlEx : SignedXml {

		// required to test protected GetPublicKey in SignedXml
		public AsymmetricAlgorithm PublicGetPublicKey () 
		{
			return base.GetPublicKey ();
		}
	}

	[TestFixture]
	public class SignedXmlTest : Assertion {

		private const string signature = "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><Reference URI=\"#MyObjectId\"><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><DigestValue>CTnnhjxUQHJmD+t1MjVXrOW+MCA=</DigestValue></Reference></SignedInfo><SignatureValue>dbFt6Zw3vR+Xh7LbM/vuifyFA7gPh/NlDM2Glz/SJBsveISieuTBpZlk/zavAeuXR/Nu0Ztt4OP4tCOg09a2RNlrTP0dhkeEfL1jTzpnVaLHuQbCiwOWCgbRif7Xt7N12FuiHYb3BltP/YyXS4E12NxlGlqnDiFA1v/mkK5+C1o=</SignatureValue><KeyInfo><KeyValue xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><RSAKeyValue><Modulus>hEfTJNa2idz2u+fSYDDG4Lx/xuk4aBbvOPVNqgc1l9Y8t7Pt+ZyF+kkF3uUl8Y0700BFGAsprnhwrWENK+PGdtvM5796ZKxCCa0ooKkofiT4355HqK26hpV8dvj38vq/rkJe1jHZgkTKa+c/0vjcYZOI/RT/IZv9JfXxVWLuLxk=</Modulus><Exponent>EQ==</Exponent></RSAKeyValue></KeyValue></KeyInfo><Object Id=\"MyObjectId\" xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><ObjectListTag xmlns=\"\" /></Object></Signature>";

		[Test]
		public void StaticValues () 
		{
			AssertEquals ("XmlDsigCanonicalizationUrl", "http://www.w3.org/TR/2001/REC-xml-c14n-20010315", SignedXml.XmlDsigCanonicalizationUrl);
			AssertEquals ("XmlDsigCanonicalizationWithCommentsUrl", "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", SignedXml.XmlDsigCanonicalizationWithCommentsUrl);
			AssertEquals ("XmlDsigDSAUrl", "http://www.w3.org/2000/09/xmldsig#dsa-sha1", SignedXml.XmlDsigDSAUrl);
			AssertEquals ("XmlDsigHMACSHA1Url", "http://www.w3.org/2000/09/xmldsig#hmac-sha1", SignedXml.XmlDsigHMACSHA1Url);
			AssertEquals ("XmlDsigMinimalCanonicalizationUrl", "http://www.w3.org/2000/09/xmldsig#minimal", SignedXml.XmlDsigMinimalCanonicalizationUrl);
			AssertEquals ("XmlDsigNamespaceUrl", "http://www.w3.org/2000/09/xmldsig#", SignedXml.XmlDsigNamespaceUrl);
			AssertEquals ("XmlDsigRSASHA1Url", "http://www.w3.org/2000/09/xmldsig#rsa-sha1", SignedXml.XmlDsigRSASHA1Url);
			AssertEquals ("XmlDsigSHA1Url", "http://www.w3.org/2000/09/xmldsig#sha1", SignedXml.XmlDsigSHA1Url);
		}

		[Test]
		public void Constructor_Empty () 
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (signature);
			XmlNodeList xnl = doc.GetElementsByTagName ("Signature", SignedXml.XmlDsigNamespaceUrl);
			XmlElement xel = (XmlElement) xnl [0];

			SignedXml sx = new SignedXml (doc);
			sx.LoadXml (xel);
			Assert ("CheckSignature", sx.CheckSignature ());
		}

		[Test]
		public void Constructor_XmlDocument () 
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (signature);
			XmlNodeList xnl = doc.GetElementsByTagName ("Signature", SignedXml.XmlDsigNamespaceUrl);
			XmlElement xel = (XmlElement) xnl [0];

			SignedXml sx = new SignedXml (doc);
			sx.LoadXml (doc.DocumentElement);
			Assert ("CheckSignature", sx.CheckSignature ());
		}

		[Test]
#if NET_2_0
		[Ignore ("2.0 throws a NullReferenceException - reported as FDBK25892")]
		// http://lab.msdn.microsoft.com/ProductFeedback/viewfeedback.aspx?feedbackid=02dd9730-d1ad-4170-8c82-36858c55fbe2
#endif
		[ExpectedException (typeof (ArgumentNullException))]
		public void Constructor_XmlDocument_Null () 
		{
			XmlDocument doc = null;
			SignedXml sx = new SignedXml (doc);
		}

		[Test]
		public void Constructor_XmlElement () 
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (signature);
			XmlNodeList xnl = doc.GetElementsByTagName ("Signature", SignedXml.XmlDsigNamespaceUrl);
			XmlElement xel = (XmlElement) xnl [0];

			SignedXml sx = new SignedXml (doc.DocumentElement);
			sx.LoadXml (xel);
			Assert ("CheckSignature", sx.CheckSignature ());
		}

		[Test]
#if !NET_2_0
		[ExpectedException (typeof (CryptographicException))]
#endif
		public void Constructor_XmlElement_WithoutLoadXml () 
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (signature);
			XmlNodeList xnl = doc.GetElementsByTagName ("Signature", SignedXml.XmlDsigNamespaceUrl);
			XmlElement xel = (XmlElement) xnl [0];

			SignedXml sx = new SignedXml (doc.DocumentElement);
			Assert ("!CheckSignature", !sx.CheckSignature ());
			// SignedXml (XmlElement) != SignedXml () + LoadXml (XmlElement)
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Constructor_XmlElement_Null () 
		{
			XmlElement xel = null;
			SignedXml sx = new SignedXml (xel);
		}

		// sample from MSDN (url)
		public SignedXml MSDNSample () 
		{
			// Create example data to sign.
			XmlDocument document = new XmlDocument ();
			XmlNode node = document.CreateNode (XmlNodeType.Element, "", "MyElement", "samples");
			node.InnerText = "This is some text";
			document.AppendChild (node);
	 
			// Create the SignedXml message.
			SignedXml signedXml = new SignedXml ();
	 
			// Create a data object to hold the data to sign.
			DataObject dataObject = new DataObject ();
			dataObject.Data = document.ChildNodes;
			dataObject.Id = "MyObjectId";

			// Add the data object to the signature.
			signedXml.AddObject (dataObject);
	 
			// Create a reference to be able to package everything into the
			// message.
			Reference reference = new Reference ();
			reference.Uri = "#MyObjectId";
	 
			// Add it to the message.
			signedXml.AddReference (reference);

			return signedXml;
		}

		[Test]
		public void AsymmetricRSASignature () 
		{
			SignedXml signedXml = MSDNSample ();

			RSA key = RSA.Create ();
			signedXml.SigningKey = key;

			// Add a KeyInfo.
			KeyInfo keyInfo = new KeyInfo ();
			keyInfo.AddClause (new RSAKeyValue (key));
			signedXml.KeyInfo = keyInfo;

			AssertEquals ("KeyInfo", 1, signedXml.KeyInfo.Count);
			AssertNull ("SignatureLength", signedXml.SignatureLength);
			AssertNull ("SignatureMethod", signedXml.SignatureMethod);
			AssertNull ("SignatureValue", signedXml.SignatureValue);
			AssertNull ("SigningKeyName", signedXml.SigningKeyName);

			// Compute the signature.
			signedXml.ComputeSignature ();

			AssertNull ("SigningKeyName", signedXml.SigningKeyName);
			AssertEquals ("SignatureMethod", SignedXml.XmlDsigRSASHA1Url, signedXml.SignatureMethod);
			AssertEquals ("SignatureValue", 128, signedXml.SignatureValue.Length);
			AssertNull ("SigningKeyName", signedXml.SigningKeyName);

			// Get the XML representation of the signature.
			XmlElement xmlSignature = signedXml.GetXml ();

			// LAMESPEC: we must reload the signature or it won't work
			// MS framework throw a "malformed element"
			SignedXml vrfy = new SignedXml ();
			vrfy.LoadXml (xmlSignature);

			// assert that we can verify our own signature
			Assert ("RSA-Compute/Verify", vrfy.CheckSignature ());
		}

		[Test]
		public void AsymmetricDSASignature () 
		{
			SignedXml signedXml = MSDNSample ();

			DSA key = DSA.Create ();
			signedXml.SigningKey = key;
	 
			// Add a KeyInfo.
			KeyInfo keyInfo = new KeyInfo ();
			keyInfo.AddClause (new DSAKeyValue (key));
			signedXml.KeyInfo = keyInfo;

			AssertEquals ("KeyInfo", 1, signedXml.KeyInfo.Count);
			AssertNull ("SignatureLength", signedXml.SignatureLength);
			AssertNull ("SignatureMethod", signedXml.SignatureMethod);
			AssertNull ("SignatureValue", signedXml.SignatureValue);
			AssertNull ("SigningKeyName", signedXml.SigningKeyName);

			// Compute the signature.
			signedXml.ComputeSignature ();

			AssertNull ("SignatureLength", signedXml.SignatureLength);
			AssertEquals ("SignatureMethod", SignedXml.XmlDsigDSAUrl, signedXml.SignatureMethod);
			AssertEquals ("SignatureValue", 40, signedXml.SignatureValue.Length);
			AssertNull ("SigningKeyName", signedXml.SigningKeyName);

			// Get the XML representation of the signature.
			XmlElement xmlSignature = signedXml.GetXml ();

			// LAMESPEC: we must reload the signature or it won't work
			// MS framework throw a "malformed element"
			SignedXml vrfy = new SignedXml ();
			vrfy.LoadXml (xmlSignature);

			// assert that we can verify our own signature
			Assert ("DSA-Compute/Verify", vrfy.CheckSignature ());
		}

		[Test]
		public void SymmetricHMACSHA1Signature () 
		{
			SignedXml signedXml = MSDNSample ();

			// Compute the signature.
			byte[] secretkey = Encoding.Default.GetBytes ("password");
			HMACSHA1 hmac = new HMACSHA1 (secretkey);
#if NET_2_0
			AssertEquals ("KeyInfo", 0, signedXml.KeyInfo.Count);
#else
			AssertNull ("KeyInfo", signedXml.KeyInfo);
#endif
			AssertNull ("SignatureLength", signedXml.SignatureLength);
			AssertNull ("SignatureMethod", signedXml.SignatureMethod);
			AssertNull ("SignatureValue", signedXml.SignatureValue);
			AssertNull ("SigningKeyName", signedXml.SigningKeyName);

			signedXml.ComputeSignature (hmac);

#if NET_2_0
			AssertEquals ("KeyInfo", 0, signedXml.KeyInfo.Count);
#else
			AssertNull ("KeyInfo", signedXml.KeyInfo);
#endif
			AssertNull ("SignatureLength", signedXml.SignatureLength);
			AssertEquals ("SignatureMethod", SignedXml.XmlDsigHMACSHA1Url, signedXml.SignatureMethod);
			AssertEquals ("SignatureValue", 20, signedXml.SignatureValue.Length);
			AssertNull ("SigningKeyName", signedXml.SigningKeyName);

			// Get the XML representation of the signature.
			XmlElement xmlSignature = signedXml.GetXml ();

			// LAMESPEC: we must reload the signature or it won't work
			// MS framework throw a "malformed element"
			SignedXml vrfy = new SignedXml ();
			vrfy.LoadXml (xmlSignature);

			// assert that we can verify our own signature
			Assert ("HMACSHA1-Compute/Verify", vrfy.CheckSignature (hmac));
		}

		[Test]
		[ExpectedException (typeof (CryptographicException))]
		public void SymmetricMACTripleDESSignature () 
		{
			SignedXml signedXml = MSDNSample ();
			// Compute the signature.
			byte[] secretkey = Encoding.Default.GetBytes ("password");
			MACTripleDES hmac = new MACTripleDES (secretkey);
			signedXml.ComputeSignature (hmac);
		}

		// Using empty constructor
		// LAMESPEC: The two other constructors don't seems to apply in verifying signatures
		[Test]
		public void AsymmetricRSAVerify () 
		{
			string value = "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><Reference URI=\"#MyObjectId\"><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><DigestValue>/Vvq6sXEVbtZC8GwNtLQnGOy/VI=</DigestValue></Reference></SignedInfo><SignatureValue>A6XuE8Cy9iOffRXaW9b0+dUcMUJQnlmwLsiqtQnADbCtZXnXAaeJ6nGnQ4Mm0IGi0AJc7/2CoJReXl7iW4hltmFguG1e3nl0VxCyCTHKGOCo1u8R3K+B1rTaenFbSxs42EM7/D9KETsPlzfYfis36yM3PqatiCUOsoMsAiMGzlc=</SignatureValue><KeyInfo><KeyValue xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><RSAKeyValue><Modulus>tI8QYIpbG/m6JLyvP+S3X8mzcaAIayxomyTimSh9UCpEucRnGvLw0P73uStNpiF7wltTZA1HEsv+Ha39dY/0j/Wiy3RAodGDRNuKQao1wu34aNybZ673brbsbHFUfw/o7nlKD2xO84fbajBZmKtBBDy63NHt+QL+grSrREPfCTM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue></KeyValue></KeyInfo><Object Id=\"MyObjectId\"><MyElement xmlns=\"samples\">This is some text</MyElement></Object></Signature>";
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (value);

			SignedXml v1 = new SignedXml ();
			v1.LoadXml (doc.DocumentElement);
			Assert ("RSA-CheckSignature()", v1.CheckSignature ());

			SignedXml v2 = new SignedXml ();
			v2.LoadXml (doc.DocumentElement);
			AsymmetricAlgorithm key = null;
			bool vrfy = v2.CheckSignatureReturningKey (out key);
			Assert ("RSA-CheckSignatureReturningKey()", vrfy);

			SignedXml v3 = new SignedXml ();
			v3.LoadXml (doc.DocumentElement);
			Assert ("RSA-CheckSignature(key)", v3.CheckSignature (key));
		}

		// Using empty constructor
		// LAMESPEC: The two other constructors don't seems to apply in verifying signatures
		[Test]
		public void AsymmetricDSAVerify () 
		{
			string value = "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#dsa-sha1\" /><Reference URI=\"#MyObjectId\"><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><DigestValue>/Vvq6sXEVbtZC8GwNtLQnGOy/VI=</DigestValue></Reference></SignedInfo><SignatureValue>BYz/qRGjGsN1yMFPxWa3awUZm1y4I/IxOQroMxkOteRGgk1HIwhRYw==</SignatureValue><KeyInfo><KeyValue xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><DSAKeyValue><P>iglVaZ+LsSL8Y0aDXmFMBwva3xHqIypr3l/LtqBH9ziV2Sh1M4JVasAiKqytWIWt/s/Uk8Ckf2tO2Ww1vsNi1NL+Kg9T7FE52sn380/rF0miwGkZeidzm74OWhykb3J+wCTXaIwOzAWI1yN7FoeoN7wzF12jjlSXAXeqPMlViqk=</P><Q>u4sowiJMHilNRojtdmIuQY2YnB8=</Q><G>SdnN7d+wn1n+HH4Hr8MIryIRYgcXdbZ5TH7jAnuWc1koqRc1AZfcYAZ6RDf+orx6Lzn055FTFiN+1NHQfGUtXJCWW0zz0FVV1NJux7WRj8vGTldjJ5ef0oCenkpwDjcIxWsZgVobve4GPoyN1sAc1scnkJB59oupibklmF4y72A=</G><Y>XejzS8Z51yfl0zbYnxSYYbHqreSLjNCoGPB/KjM1TOyV5sMjz0StKtGrFWryTWc7EgvFY7kUth4e04VKf9HbK8z/FifHTXj8+Tszbjzw8GfInnBwLN+vJgbpnjtypmiI5Bm2nLiRbfkdAHP+OrKtr/EauM9GQfYuaxm3/Vj8B84=</Y><J>vGwGg9wqwwWP9xsoPoXu6kHArJtadiNKe9azBiUx5Ob883gd5wlKfEcGuKkBmBySGbgwxyOsIBovd9Kk48hF01ymfQzAAuHR0EdJECSsTsTTKVTLQNBU32O+PRbLYpv4E8kt6rNL83JLJCBY</J><Seed>sqzn8J6fd2gtEyq6YOqiUSHgPE8=</Seed><PgenCounter>sQ==</PgenCounter></DSAKeyValue></KeyValue></KeyInfo><Object Id=\"MyObjectId\"><MyElement xmlns=\"samples\">This is some text</MyElement></Object></Signature>";
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (value);

			SignedXml v1 = new SignedXml ();
			v1.LoadXml (doc.DocumentElement);
			Assert ("DSA-CheckSignature()", v1.CheckSignature ());

			SignedXml v2 = new SignedXml ();
			v2.LoadXml (doc.DocumentElement);
			AsymmetricAlgorithm key = null;
			bool vrfy = v2.CheckSignatureReturningKey (out key);
			Assert ("DSA-CheckSignatureReturningKey()", vrfy);

			SignedXml v3 = new SignedXml ();
			v3.LoadXml (doc.DocumentElement);
			Assert ("DSA-CheckSignature(key)", v3.CheckSignature (key));
		}

		[Test]
		public void SymmetricHMACSHA1Verify () 
		{
			string value = "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#hmac-sha1\" /><Reference URI=\"#MyObjectId\"><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><DigestValue>/Vvq6sXEVbtZC8GwNtLQnGOy/VI=</DigestValue></Reference></SignedInfo><SignatureValue>e2RxYr5yGbvTqZLCFcgA2RAC0yE=</SignatureValue><Object Id=\"MyObjectId\"><MyElement xmlns=\"samples\">This is some text</MyElement></Object></Signature>";
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (value);

			SignedXml v1 = new SignedXml ();
			v1.LoadXml (doc.DocumentElement);

			byte[] secretkey = Encoding.Default.GetBytes ("password");
			HMACSHA1 hmac = new HMACSHA1 (secretkey);

			Assert ("HMACSHA1-CheckSignature(key)", v1.CheckSignature (hmac));
		}

		[Test]
		// adapted from http://bugzilla.ximian.com/show_bug.cgi?id=52084
		public void GetIdElement () 
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (signature);

			SignedXml v1 = new SignedXml ();
			v1.LoadXml (doc.DocumentElement);
			Assert ("CheckSignature", v1.CheckSignature ());

			XmlElement xel = v1.GetIdElement (doc, "MyObjectId");
			Assert ("GetIdElement", xel.InnerXml.StartsWith ("<ObjectListTag"));
		}

		[Test]
		public void GetPublicKey () 
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (signature);

			SignedXmlEx sxe = new SignedXmlEx ();
			sxe.LoadXml (doc.DocumentElement);
			
			AsymmetricAlgorithm aa1 = sxe.PublicGetPublicKey ();
			Assert ("First Public Key is RSA", (aa1 is RSA));
			
			AsymmetricAlgorithm aa2 = sxe.PublicGetPublicKey ();
			AssertNull ("Second Public Key is null", aa2);
		}
#if NET_2_0
		[Test]
		// [ExpectedException (typeof (ArgumentNullException))]
		public void AddObject_Null () 
		{
			SignedXml sx = new SignedXml ();
			// still no ArgumentNullExceptions for this one
			sx.AddObject (null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void AddReference_Null () 
		{
			SignedXml sx = new SignedXml ();
			sx.AddReference (null);
		}
#else
		[Test]
		public void Add_Null () 
		{
			SignedXml sx = new SignedXml ();
			// no ArgumentNull exceptions for those
			sx.AddObject (null);
			sx.AddReference (null);
		}
#endif
		[Test]
		[ExpectedException (typeof (CryptographicException))]
		public void GetXml_WithoutInfo () 
		{
			SignedXml sx = new SignedXml ();
			XmlElement xel = sx.GetXml ();
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void LoadXml_Null ()
		{
			SignedXml sx = new SignedXml ();
			sx.LoadXml (null);
		}

		[Test]
		public void SigningKeyName () 
		{
			SignedXmlEx sxe = new SignedXmlEx ();
			AssertNull ("SigningKeyName", sxe.SigningKeyName);
			sxe.SigningKeyName = "mono";
			AssertEquals ("SigningKeyName", "mono", sxe.SigningKeyName);
		}

		[Test]
		public void CheckSignatureEmptySafe ()
		{
			SignedXml sx;
			KeyInfoClause kic;
			KeyInfo ki;

			// empty keyinfo passes...
			sx = new SignedXml ();
			sx.KeyInfo = new KeyInfo ();
			Assert (!sx.CheckSignature ());

			// with empty KeyInfoName
			kic = new KeyInfoName ();
			ki = new KeyInfo ();
			ki.AddClause (kic);
			sx.KeyInfo = ki;
			Assert (!sx.CheckSignature ());
		}

		[Test]
#if !NET_2_0
		[ExpectedException (typeof (CryptographicException))]
#endif
		public void CheckSignatureEmpty ()
		{
			SignedXml sx = new SignedXml ();
			Assert (!sx.CheckSignature ());
		}
	}
}
