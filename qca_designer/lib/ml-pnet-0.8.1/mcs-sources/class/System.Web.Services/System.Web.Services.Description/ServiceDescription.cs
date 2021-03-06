// 
// System.Web.Services.Description.ServiceDescription.cs
//
// Author:
//   Tim Coleman (tim@timcoleman.com)
//   Lluis Sanchez Gual (lluis@ximian.com)
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

using System.IO;
using System.Collections;
using System.Reflection;
using System.Web.Services;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	[XmlFormatExtensionPoint ("Extensions")]
	[XmlRoot ("definitions", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
	public sealed class ServiceDescription :
#if NET_2_0
		NamedItem
#else
		DocumentableItem 
#endif
	{
		#region Fields

		public const string Namespace = "http://schemas.xmlsoap.org/wsdl/";

		BindingCollection bindings;
		ServiceDescriptionFormatExtensionCollection extensions;
		ImportCollection imports;
		MessageCollection messages;
#if !NET_2_0
		string name;
#endif
		PortTypeCollection portTypes;
		string retrievalUrl;
		ServiceDescriptionCollection serviceDescriptions;
		ServiceCollection services;
		string targetNamespace;
		Types types;
#if !TARGET_JVM
		static ServiceDescriptionSerializer serializer;
#else
		static ServiceDescriptionSerializer serializer {
			get {
				return (ServiceDescriptionSerializer)AppDomain.CurrentDomain.GetData("ServiceDescriptionSerializer.serializer");
			}
			set {
				AppDomain.CurrentDomain.SetData("ServiceDescriptionSerializer.serializer", value);
			}
		}
#endif

		#endregion // Fields

		#region Constructors

		static ServiceDescription ()
		{
			serializer = new ServiceDescriptionSerializer ();
		}

		public ServiceDescription ()
		{
			bindings = new BindingCollection (this);
			extensions = new ServiceDescriptionFormatExtensionCollection (this);
			imports = new ImportCollection (this);
			messages = new MessageCollection (this);
#if !NET_2_0
			name = String.Empty;		
#endif
			portTypes = new PortTypeCollection (this);

			serviceDescriptions = null;
			services = new ServiceCollection (this);
			targetNamespace = String.Empty;
			types = null;
		}
		
		#endregion // Constructors

		#region Properties

		[XmlElement ("import")]
		public ImportCollection Imports {
			get { return imports; }
		}

		[XmlElement ("types")]
		public Types Types {
			get { return types; }
			set { types = value; }
		}

		[XmlElement ("message")]
		public MessageCollection Messages {
			get { return messages; }
		}

		[XmlElement ("portType")]	
		public PortTypeCollection PortTypes {
			get { return portTypes; }
		}
	
		[XmlElement ("binding")]
		public BindingCollection Bindings {
			get { return bindings; }
		}

		[XmlIgnore]
		public ServiceDescriptionFormatExtensionCollection Extensions { 	
			get { return extensions; }
		}

#if !NET_2_0
		[XmlAttribute ("name", DataType = "NMTOKEN")]	
		public string Name {
			get { return name; }
			set { name = value; }
		}
#endif

		[XmlIgnore]	
		public string RetrievalUrl {
			get { return retrievalUrl; }
			set { retrievalUrl = value; }
		}
	
		[XmlIgnore]	
		public static XmlSerializer Serializer {
			get { return serializer; }
		}

		[XmlIgnore]
		public ServiceDescriptionCollection ServiceDescriptions {
			get { 
				if (serviceDescriptions == null) 
					throw new NullReferenceException ();
				return serviceDescriptions; 
			}
		}

		[XmlElement ("service")]
		public ServiceCollection Services {
			get { return services; }
		}

		[XmlAttribute ("targetNamespace")]
		public string TargetNamespace {
			get { return targetNamespace; }
			set { targetNamespace = value; }
		}

		#endregion // Properties

		#region Methods

		public static bool CanRead (XmlReader reader)
		{
			reader.MoveToContent ();
			return reader.LocalName == "definitions" && 
				reader.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/";
		}

		public static ServiceDescription Read (Stream stream)
		{
			return (ServiceDescription) serializer.Deserialize (stream);
		}

		public static ServiceDescription Read (string fileName)
		{
			return Read (new FileStream (fileName, FileMode.Open));
		}

		public static ServiceDescription Read (TextReader textReader)
		{
			return (ServiceDescription) serializer.Deserialize (textReader);
		}

		public static ServiceDescription Read (XmlReader reader)
		{
			return (ServiceDescription) serializer.Deserialize (reader);
		}

		public void Write (Stream stream)
		{
			serializer.Serialize (stream, this, GetNamespaceList ());
		}

		public void Write (string fileName)
		{
			Write (new FileStream (fileName, FileMode.Create));
		}

		public void Write (TextWriter writer)
		{
			serializer.Serialize (writer, this, GetNamespaceList ());
		}

		public void Write (XmlWriter writer)
		{
			serializer.Serialize (writer, this, GetNamespaceList ());
		}

		internal void SetParent (ServiceDescriptionCollection serviceDescriptions)
		{
			this.serviceDescriptions = serviceDescriptions; 
		}
		
		XmlSerializerNamespaces GetNamespaceList ()
		{
			XmlSerializerNamespaces ns;
			ns = new XmlSerializerNamespaces ();
			ns.Add ("soap", SoapBinding.Namespace);
			ns.Add ("soapenc", "http://schemas.xmlsoap.org/soap/encoding/");
			ns.Add ("s", XmlSchema.Namespace);
			ns.Add ("http", HttpBinding.Namespace);
			ns.Add ("mime", MimeContentBinding.Namespace);
			ns.Add ("tm", MimeTextBinding.Namespace);
			ns.Add ("s0", TargetNamespace);
			
			AddExtensionNamespaces (ns, Extensions);
			
			if (Types != null) AddExtensionNamespaces (ns, Types.Extensions);
			
			foreach (Service ser in Services)
				foreach (Port port in ser.Ports)
					AddExtensionNamespaces (ns, port.Extensions);

			foreach (Binding bin in Bindings)
			{
				AddExtensionNamespaces (ns, bin.Extensions);
				foreach (OperationBinding op in bin.Operations)
				{
					AddExtensionNamespaces (ns, op.Extensions);
					if (op.Input != null) AddExtensionNamespaces (ns, op.Input.Extensions);
					if (op.Output != null) AddExtensionNamespaces (ns, op.Output.Extensions);
				}
			}
			return ns;
		}
		
		void AddExtensionNamespaces (XmlSerializerNamespaces ns, ServiceDescriptionFormatExtensionCollection extensions)
		{
			foreach (ServiceDescriptionFormatExtension ext in extensions)
			{
				ExtensionInfo einf = ExtensionManager.GetFormatExtensionInfo (ext.GetType ());
				foreach (XmlQualifiedName qname in einf.NamespaceDeclarations)
					ns.Add (qname.Name, qname.Namespace);
			}
		}
		
		internal static void WriteExtensions (XmlWriter writer, object ob)
		{
			ServiceDescriptionFormatExtensionCollection extensions = ExtensionManager.GetExtensionPoint (ob);
			if (extensions != null)
			{
				foreach (ServiceDescriptionFormatExtension ext in extensions)
					WriteExtension (writer, ext);
			}
		}
		
		static void WriteExtension (XmlWriter writer, ServiceDescriptionFormatExtension ext)
		{
			Type type = ext.GetType ();
			ExtensionInfo info = ExtensionManager.GetFormatExtensionInfo (type);
			
//				if (prefix != null && prefix != "")
//					Writer.WriteStartElement (prefix, info.ElementName, info.Namespace);
//				else
//					WriteStartElement (info.ElementName, info.Namespace, false);

			XmlSerializerNamespaces ns = new XmlSerializerNamespaces ();
			ns.Add ("","");
			info.Serializer.Serialize (writer, ext, ns);
		}
		
		internal static void ReadExtension (XmlReader reader, object ob)
		{
			ServiceDescriptionFormatExtensionCollection extensions = ExtensionManager.GetExtensionPoint (ob);
			if (extensions != null)
			{
				ExtensionInfo info = ExtensionManager.GetFormatExtensionInfo (reader.LocalName, reader.NamespaceURI);
				if (info != null)
				{
					object extension = info.Serializer.Deserialize (reader);
					extensions.Add ((ServiceDescriptionFormatExtension)extension);
					return;
				}
			}
			reader.Skip ();
		}


		#endregion

		internal class ServiceDescriptionSerializer : XmlSerializer 
		{
			protected override void Serialize (object o, XmlSerializationWriter writer)
			{
				ServiceDescriptionWriterBase xsWriter = writer as ServiceDescriptionWriterBase;
				xsWriter.WriteTree ((ServiceDescription)o);
			}
			
			protected override object Deserialize (XmlSerializationReader reader)
			{
				ServiceDescriptionReaderBase xsReader = reader as ServiceDescriptionReaderBase;
				return xsReader.ReadTree ();
			}
			
			protected override XmlSerializationWriter CreateWriter ()
			{
				return new ServiceDescriptionWriterBase ();
			}
			
			protected override XmlSerializationReader CreateReader ()
			{
				return new ServiceDescriptionReaderBase ();
			}
		}		
	}
}
