//
// System.Web.HttpMultipartContentParser
//
// Authors:
//   	Ben Maurer (bmaurer@users.sourceforge.net)
//
// (c) 2003 Ben Maurer
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
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web {
	internal class HttpMultipartContentParser {
		
		const byte HYPHEN = (byte) '-', LF = (byte) '\n', CR = (byte) '\r';
		
		public static MultipartContentElement [] Parse (byte [] data, byte [] boundary, Encoding encoding)
		{
			HttpMultipartContentParser p = new HttpMultipartContentParser (data, boundary, encoding);
			p.ParseIntoElementList();
			
			return (MultipartContentElement []) p.elements.ToArray (typeof (MultipartContentElement));
		}
		
		HttpMultipartContentParser (byte [] data, byte [] boundary, Encoding encoding)
		{
			this.data = data;
			this.boundary = boundary;
			this.enc = encoding;
			this.len = data.Length;
		}

		bool IsAtBoundaryLine ()
		{
			int boundaryLen = boundary.Length;

			if (lineLen != boundaryLen     &&
			    lineLen != boundaryLen + 2)
				return false; 

			
			for (int i = 0; i < boundaryLen; i++)
				if (data [lineStart + i] != boundary [i])
					return false;
				
			if (lineLen == boundaryLen)
				return true; 
			
			if (data [lineStart + boundaryLen    ] != HYPHEN || 
			    data [lineStart + boundaryLen + 1] != HYPHEN)
				return false;
			
			lastBoundaryFound = true;
			return true; 
		}
		
		string GetAttributeFromContentDispositionHeader (string l, int pos, string name)
		{
			string nameEqQuote = name + "=\"";

			int idxVal = l.IndexOf (nameEqQuote, pos);
			if (idxVal < 0)
				return null;
			idxVal += nameEqQuote.Length;
			int idxEndQuote = l.IndexOf ('"', idxVal);
			if (idxEndQuote < 0)
				return null;
			if (idxEndQuote == idxVal)
				return "";
			return l.Substring (idxVal, idxEndQuote - idxVal);
		}

		
		bool IsAtEndOfData ()
		{
			return pos >= len || lastBoundaryFound;
		}
		
		private bool GetNextLine()
		{ 
			int pos = this.pos;
			lineStart = -1;
				
			while (pos < len && !(this.data [pos] == LF || ++pos + 1 == len))
				;
			
			if (pos >= len)
				return false;
			
			lineStart = this.pos;
			lineLen = pos - lineStart;
			this.pos = pos + 1;
			
			if (lineLen > 0 && data [pos - 1] == CR)
				lineLen--;
			
			return true;
		}
		
		void ParseIntoElementList ()
		{ 
			while (!IsAtBoundaryLine () && GetNextLine ())
				;
			
			do {
				partName = partFilename = partContentType = null;
			
				while (this.GetNextLine () && lineLen != 0) {
					
					string line = enc.GetString (data, lineStart, lineLen);
					int colonPos = line.IndexOf(':');
					if (colonPos < 0) continue;
						
					string headerName = line.Substring (0, colonPos);
					if (String.Compare (headerName, "Content-Disposition", true) == 0) {
						partName = GetAttributeFromContentDispositionHeader (line, colonPos + 1, "name");
						partFilename = GetAttributeFromContentDispositionHeader (line, colonPos + 1, "filename");
					} else if (String.Compare (headerName, "Content-Type", true) == 0)
						partContentType = line.Substring (colonPos + 1).Trim ();
				}
				
				if (IsAtEndOfData ()) break;
					
				partDataStart = pos;
				partDataLength = -1;
				
				while (GetNextLine ()) {
					if (!IsAtBoundaryLine ())
						continue;
					
					int end = lineStart - 1;
					if (data [end] == LF) end--;
					if (data [end] == CR) end--;
					
					partDataLength = end - partDataStart + 1;
					break;
				}
				
				if (partDataLength == -1) break;
				
				if (partName != null) {
					 elements.Add (new MultipartContentElement (
						partName, partFilename, partContentType,
						data, partDataStart, partDataLength
					));
				}
			} while (!IsAtEndOfData ());
		}

		byte [] data, boundary;

		ArrayList elements = new ArrayList ();
		Encoding enc;
		bool lastBoundaryFound;
		int len, lineLen, lineStart;

		int partDataStart, partDataLength;
		string partContentType, partName, partFilename;
		int pos;
	}
	
	internal class MultipartContentElement {
		string name, fileName, contentType;
		byte [] data;
		int offset, len;
		
		public MultipartContentElement (string name, string fileName, string contentType, byte [] data, int offset, int len)
		{
			this.name = name;
			this.fileName = fileName;
			this.contentType = contentType;
			this.data = data;
			this.offset = offset;
			this.len = len;
		}
		
		public bool IsFile { get { return fileName != null; } }
		public bool IsFormItem { get { return fileName == null; } }
		public string Name { get { return name; } }
		
		public string GetString (Encoding enc)
		{
			if (len == 0) return "";
			return enc.GetString (data, offset, len);
		}
		
		public HttpPostedFile GetFile ()
		{
			return new HttpPostedFile (fileName, contentType, new HttpRequestStream (data, offset, len));
		}
	}
}