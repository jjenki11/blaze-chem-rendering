//
// Mono.Data.Tds.Protocol.TdsColumnType.cs
//
// Author:
//   Tim Coleman (tim@timcoleman.com)
//
// Copyright (C) Tim Coleman, 2002
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

namespace Mono.Data.Tds.Protocol {
	public enum TdsColumnType {
		Binary = 0x2d,
		Bit = 0x32,
		Char = 0x2f,
		DateTime = 0x3d,
		DateTime4 = 0x3a,
		DateTimeN = 0x6f,
		Decimal = 0x6a,
		Real = 0x3b,
		Float8 = 0x3e,
		FloatN = 0x6d,
		Image = 0x22,
		Int1 = 0x30,
		Int2 = 0x34,
		Int4 = 0x38,
		IntN = 0x26,
		Void = 0x1f,
		Text = 0x23,
		UniqueIdentifier = 0x24, // This is TDS_BLOB in Sybase?
		VarBinary = 0x25,
		VarChar = 0x27,
		Money = 0x3c,
		NText = 0x63,
		NVarChar = 0x67,
		BitN = 0x68,
		Numeric = 0x6c,
		MoneyN = 0x6e,
		Money4 = 0x70,
		NChar = 0xef,
		BigBinary = 0xad,
		BigVarBinary = 0xa5,
		BigVarChar = 0xa7,
		BigChar = 0xaf,
		SmallMoney = 0x7a
	}
}
