//
// DataSetReadXmlTest.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//

//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
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
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;

namespace MonoTests.System.Data
{
	[TestFixture]
	public class DataSetReadXmlTest : DataSetAssertion
	{

		const string xml1 = "";
		const string xml2 = "<root/>";
		const string xml3 = "<root></root>";
		const string xml4 = "<root>   </root>";
		const string xml5 = "<root>test</root>";
		const string xml6 = "<root><test>1</test></root>";
		const string xml7 = "<root><test>1</test><test2>a</test2></root>";
		const string xml8 = "<dataset><table><col1>foo</col1><col2>bar</col2></table></dataset>";
		const string xml29 = @"<PersonalSite><License Name='Sum Wang' Email='sumwang@somewhere.net' Mode='Trial' StartDate='01/01/2004' Serial='aaa' /></PersonalSite>";

		const string diff1 = @"<diffgr:diffgram xmlns:msdata='urn:schemas-microsoft-com:xml-msdata' xmlns:diffgr='urn:schemas-microsoft-com:xml-diffgram-v1'>
  <NewDataSet>
    <Table1 diffgr:id='Table11' msdata:rowOrder='0' diffgr:hasChanges='inserted'>
      <Column1_1>ppp</Column1_1>
      <Column1_2>www</Column1_2>
      <Column1_3>xxx</Column1_3>
    </Table1>
  </NewDataSet>
</diffgr:diffgram>";
		const string diff2 = diff1 + xml8;

		const string schema1 = @"<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'>
	<xs:element name='Root'>
		<xs:complexType>
			<xs:sequence>
				<xs:element name='Child' type='xs:string' />
			</xs:sequence>
		</xs:complexType>
	</xs:element>
</xs:schema>";
		const string schema2 = schema1 + xml8;

		[Test]
		public void ReadSimpleAuto ()
		{
			DataSet ds;

			// empty XML
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyString", xml1,
				XmlReadMode.Auto, XmlReadMode.Auto,
				"NewDataSet", 0);

			// simple element
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyElement", xml2,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 0);

			// simple element2
			ds = new DataSet ();
			AssertReadXml (ds, "StartEndTag", xml3,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 0);

			// whitespace in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "Whitespace", xml4,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 0);

			// text in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 0);

			// simple table pattern:
			// root becomes a table and test becomes a column.
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"NewDataSet", 1);
			AssertDataTable ("xml6", ds.Tables [0], "root", 1, 1, 0, 0, 0, 0);

			// simple table with 2 columns:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable2", xml7,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"NewDataSet", 1);
			AssertDataTable ("xml7", ds.Tables [0], "root", 2, 1, 0, 0, 0, 0);

			// simple dataset with 1 table:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleDataSet", xml8,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"dataset", 1);
			AssertDataTable ("xml8", ds.Tables [0], "table", 2, 1, 0, 0, 0, 0);
		}

		[Test]
		public void ReadSimpleDiffgram ()
		{
			DataSet ds;

			// empty XML
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyString", xml1,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			// simple element
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyElement", xml2,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			// simple element2
			ds = new DataSet ();
			AssertReadXml (ds, "StartEndTag", xml3,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			// whitespace in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "Whitespace", xml4,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			// text in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			// simple table pattern:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			// simple table with 2 columns:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable2", xml7,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			// simple dataset with 1 table:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleDataSet", xml8,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);
		}

		[Test]
		public void ReadSimpleFragment ()
		{
			DataSet ds;

			// empty XML
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyString", xml1,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// simple element
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyElement", xml2,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// simple element2
			ds = new DataSet ();
			AssertReadXml (ds, "StartEndTag", xml3,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// whitespace in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "Whitespace", xml4,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// text in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// simple table pattern:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// simple table with 2 columns:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable2", xml7,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// simple dataset with 1 table:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleDataSet", xml8,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);
		}

		[Test]
		public void ReadSimpleIgnoreSchema ()
		{
			DataSet ds;

			// empty XML
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyString", xml1,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			// simple element
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyElement", xml2,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			// simple element2
			ds = new DataSet ();
			AssertReadXml (ds, "StartEndTag", xml3,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			// whitespace in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "Whitespace", xml4,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			// text in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			// simple table pattern:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			// simple table with 2 columns:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable2", xml7,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			// simple dataset with 1 table:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleDataSet", xml8,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);
		}

		[Test]
		public void ReadSimpleInferSchema ()
		{
			DataSet ds;

			// empty XML
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyString", xml1,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"NewDataSet", 0);

			// simple element
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyElement", xml2,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"root", 0);

			// simple element2
			ds = new DataSet ();
			AssertReadXml (ds, "StartEndTag", xml3,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"root", 0);

			// whitespace in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "Whitespace", xml4,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"root", 0);

			// text in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"root", 0);

			// simple table pattern:
			// root becomes a table and test becomes a column.
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"NewDataSet", 1);
			AssertDataTable ("xml6", ds.Tables [0], "root", 1, 1, 0, 0, 0, 0);

			// simple table with 2 columns:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable2", xml7,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"NewDataSet", 1);
			AssertDataTable ("xml7", ds.Tables [0], "root", 2, 1, 0, 0, 0, 0);

			// simple dataset with 1 table:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleDataSet", xml8,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"dataset", 1);
			AssertDataTable ("xml8", ds.Tables [0], "table", 2, 1, 0, 0, 0, 0);
		}

		[Test]
		public void ReadSimpleReadSchema ()
		{
			DataSet ds;

			// empty XML
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyString", xml1,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// simple element
			ds = new DataSet ();
			AssertReadXml (ds, "EmptyElement", xml2,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// simple element2
			ds = new DataSet ();
			AssertReadXml (ds, "StartEndTag", xml3,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// whitespace in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "Whitespace", xml4,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// text in simple element
			ds = new DataSet ();
			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// simple table pattern:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// simple table with 2 columns:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleTable2", xml7,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// simple dataset with 1 table:
			ds = new DataSet ();
			AssertReadXml (ds, "SimpleDataSet", xml8,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);
		}

		[Test]
		public void TestSimpleDiffXmlAll ()
		{
			DataSet ds;

			// ignored
			ds = new DataSet ();
			AssertReadXml (ds, "Fragment", diff1,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			ds = new DataSet ();
			AssertReadXml (ds, "IgnoreSchema", diff1,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			ds = new DataSet ();
			AssertReadXml (ds, "InferSchema", diff1,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"NewDataSet", 0);

			ds = new DataSet ();
			AssertReadXml (ds, "ReadSchema", diff1,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0);

			// Auto, DiffGram ... treated as DiffGram
			ds = new DataSet ();
			AssertReadXml (ds, "Auto", diff1,
				XmlReadMode.Auto, XmlReadMode.DiffGram,
				"NewDataSet", 0);

			ds = new DataSet ();
			AssertReadXml (ds, "DiffGram", diff1,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0);
		}

		[Test]
		public void TestSimpleDiffPlusContentAll ()
		{
			DataSet ds;

			// Fragment ... skipped
			ds = new DataSet ();
			AssertReadXml (ds, "Fragment", diff2,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 0);

			// others ... kept 
			ds = new DataSet ();
			AssertReadXml (ds, "IgnoreSchema", diff2,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0, ReadState.Interactive);

			ds = new DataSet ();
			AssertReadXml (ds, "InferSchema", diff2,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"NewDataSet", 0, ReadState.Interactive);

			ds = new DataSet ();
			AssertReadXml (ds, "ReadSchema", diff2,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 0, ReadState.Interactive);

			// Auto, DiffGram ... treated as DiffGram
			ds = new DataSet ();
			AssertReadXml (ds, "Auto", diff2,
				XmlReadMode.Auto, XmlReadMode.DiffGram,
				"NewDataSet", 0, ReadState.Interactive);

			ds = new DataSet ();
			AssertReadXml (ds, "DiffGram", diff2,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 0, ReadState.Interactive);
		}

		[Test]
		public void TestSimpleSchemaXmlAll ()
		{
			DataSet ds;

			// ignored
			ds = new DataSet ();
			AssertReadXml (ds, "IgnoreSchema", schema1,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0);

			ds = new DataSet ();
			AssertReadXml (ds, "InferSchema", schema1,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"NewDataSet", 0);

			// misc ... consume schema
			ds = new DataSet ();
			AssertReadXml (ds, "Fragment", schema1,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 1);
			AssertDataTable ("fragment", ds.Tables [0], "Root", 1, 0, 0, 0, 0, 0);

			ds = new DataSet ();
			AssertReadXml (ds, "ReadSchema", schema1,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 1);
			AssertDataTable ("readschema", ds.Tables [0], "Root", 1, 0, 0, 0, 0, 0);

			ds = new DataSet ();
			AssertReadXml (ds, "Auto", schema1,
				XmlReadMode.Auto, XmlReadMode.ReadSchema,
				"NewDataSet", 1);
			AssertDataTable ("auto", ds.Tables [0], "Root", 1, 0, 0, 0, 0, 0);

			ds = new DataSet ();
			AssertReadXml (ds, "DiffGram", schema1,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 1);
		}

		[Test]
		public void TestSimpleSchemaPlusContentAll ()
		{
			DataSet ds;

			// ignored
			ds = new DataSet ();
			AssertReadXml (ds, "IgnoreSchema", schema2,
				XmlReadMode.IgnoreSchema, XmlReadMode.IgnoreSchema,
				"NewDataSet", 0, ReadState.Interactive);

			ds = new DataSet ();
			AssertReadXml (ds, "InferSchema", schema2,
				XmlReadMode.InferSchema, XmlReadMode.InferSchema,
				"NewDataSet", 0, ReadState.Interactive);

			// Fragment ... consumed both
			ds = new DataSet ();
			AssertReadXml (ds, "Fragment", schema2,
				XmlReadMode.Fragment, XmlReadMode.Fragment,
				"NewDataSet", 1);
			AssertDataTable ("fragment", ds.Tables [0], "Root", 1, 0, 0, 0, 0, 0);

			// rest ... treated as schema
			ds = new DataSet ();
			AssertReadXml (ds, "Auto", schema2,
				XmlReadMode.Auto, XmlReadMode.ReadSchema,
				"NewDataSet", 1, ReadState.Interactive);
			AssertDataTable ("auto", ds.Tables [0], "Root", 1, 0, 0, 0, 0, 0);

			ds = new DataSet ();
			AssertReadXml (ds, "DiffGram", schema2,
				XmlReadMode.DiffGram, XmlReadMode.DiffGram,
				"NewDataSet", 1, ReadState.Interactive);
			AssertDataTable ("diffgram", ds.Tables [0], "Root", 1, 0, 0, 0, 0, 0);

			ds = new DataSet ();
			AssertReadXml (ds, "ReadSchema", schema2,
				XmlReadMode.ReadSchema, XmlReadMode.ReadSchema,
				"NewDataSet", 1, ReadState.Interactive);
		}

		[Test]
		public void SequentialRead1 ()
		{
			// simple element -> simple table
			DataSet ds = new DataSet ();

			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 0);

			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 1); // not NewDataSet unlike standalone load
			AssertDataTable ("seq1", ds.Tables [0], "root", 1, 1, 0, 0, 0, 0);
		}

		[Test]
		public void SequentialRead2 ()
		{
			// simple element -> simple dataset
			DataSet ds = new DataSet ();

			AssertReadXml (ds, "SingleText", xml5,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 0);

			AssertReadXml (ds, "SimpleTable2", xml7,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"root", 1); // dataset name will not be overwritten
			AssertDataTable ("#1", ds.Tables [0], "root", 2, 1, 0, 0, 0, 0);

			// simple table -> simple dataset
			ds = new DataSet ();

			AssertReadXml (ds, "SimpleTable", xml6,
				XmlReadMode.Auto, XmlReadMode.InferSchema,
				"NewDataSet", 1);
			AssertDataTable ("#2", ds.Tables [0], "root", 1, 1, 0, 0, 0, 0);

			// Return value became IgnoreSchema, since there is
			// already schema information in the dataset.
			// Columns are kept 1 as old table holds.
			// Rows are up to 2 because of accumulative read.
			AssertReadXml (ds, "SimpleTable2-2", xml7,
				XmlReadMode.Auto, XmlReadMode.IgnoreSchema,
				"NewDataSet", 1);
			AssertDataTable ("#3", ds.Tables [0], "root", 1, 2, 0, 0, 0, 0);

		}

		[Test] // based on bug case
		public void ReadComplexElementDocument ()
		{
			DataSet ds = new DataSet ();
			ds.ReadXml (new StringReader (xml29));
		}

		[Test]
		public void IgnoreSchemaShouldFillData ()
		{
			// no such dataset
			string xml1 = "<set><tab><col>test</col></tab></set>";
			// no wrapper element
			string xml2 = "<tab><col>test</col></tab>";
			// no such table
			string xml3 = "<tar><col>test</col></tar>";
			DataSet ds = new DataSet ();
			DataTable dt = new DataTable ("tab");
			ds.Tables.Add (dt);
			dt.Columns.Add ("col");
			ds.ReadXml (new StringReader (xml1), XmlReadMode.IgnoreSchema);
			AssertDataSet ("ds", ds, "NewDataSet", 1, 0);
			AssertEquals ("wrapper element", 1, dt.Rows.Count);
			dt.Clear ();

			ds.ReadXml (new StringReader (xml2), XmlReadMode.IgnoreSchema);
			AssertEquals ("no wrapper element", 1, dt.Rows.Count);
			dt.Clear ();

			ds.ReadXml (new StringReader (xml3), XmlReadMode.IgnoreSchema);
			AssertEquals ("no such table", 0, dt.Rows.Count);
		}

		// bug #60118
		[Test]
		public void NameConflictDSAndTable ()
		{
			string xml = @"<PriceListDetails> 
	<PriceListList>    
		<Id>1</Id>
	</PriceListList>
	<PriceListDetails> 
		<Id>1</Id>
		<Status>0</Status>
	</PriceListDetails>
</PriceListDetails>";

			DataSet ds = new DataSet ();
			ds.ReadXml (new StringReader (xml));
			AssertNotNull (ds.Tables ["PriceListDetails"]);
		}
	}
}