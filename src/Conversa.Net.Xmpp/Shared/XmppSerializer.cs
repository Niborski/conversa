// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;

namespace Conversa.Net.Xmpp.Shared
{
    /// <summary>
    /// Serializer class for XMPP stanzas
    /// </summary>
    public sealed class XmppSerializer
    {
        private static readonly List<XmppSerializer> Serializers;
        private static readonly XmlReaderSettings    XmlReaderSettings;
        private static readonly XmlWriterSettings    XmlWriterSettings;

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] Serialize(object value)
        {
            return Serialize(value, null);
        }

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        public static byte[] Serialize(object value, string prefix)
        {
            return GetSerializer(value.GetType()).SerializeObject(value, prefix);
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string nodeName, string xml)
            where T : class
        {
            return Deserialize(nodeName, xml) as T;
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static object Deserialize(string nodeName, string xml)
        {
            var serializer = GetSerializer(nodeName);

            if (serializer != null)
            {
                return serializer.Deserialize(xml);
            }

            return null;
        }

        static string ReadResource(Assembly assembly, string resource)
        {
            string xml;
            
            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                using (var reader = new StreamReader(stream))
                {
                    xml = reader.ReadToEnd();
                }
            }

            return xml;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        static XmppSerializer()
        {
            var watch = Stopwatch.StartNew();

            var resource = "Conversa.Net.Xmpp.Shared.Serializers.xml";
            var document = new XmlDocument();

            document.LoadXml(ReadResource(typeof(XmppSerializer).GetTypeInfo().Assembly, resource));

            var list = document.SelectNodes("/serializers/serializer");

            Serializers = new List<XmppSerializer>();

            foreach (IXmlNode serializer in list)
            {
                var node = serializer.SelectSingleNode("namespace");
    
                string ename  = serializer.Attributes.Single(a => a.NodeName == "elementname").NodeValue.ToString();
                string prefix = node.SelectSingleNode("prefix").InnerText;
                string nsName = node.SelectSingleNode("namespace").InnerText;
                Type   type   = Type.GetType(serializer.SelectSingleNode("serializertype").InnerText);
    
                Serializers.Add(new XmppSerializer(ename, prefix, nsName, type));
            }

            XmlReaderSettings = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment
            };

            XmlWriterSettings = new XmlWriterSettings
            {
                ConformanceLevel   = ConformanceLevel.Auto
              , Encoding           = XmppEncoding.Utf8
              , Indent             = false
              , NamespaceHandling  = NamespaceHandling.Default
              , OmitXmlDeclaration = true
            };

            watch.Stop();
            Debug.WriteLine(String.Format("XmppSerializer static constructor elapsed time: {0}", watch.Elapsed.ToString()));
        }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        static XmppSerializer GetSerializer(string elementName)
        {
            return Serializers.SingleOrDefault(x => x.ElementName == elementName);
        }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        static XmppSerializer GetSerializer(Type type)
        {
            return Serializers.SingleOrDefault(s => s.SerializerType == type);
        }

        private readonly string					 elementName;
        private readonly string                  prefix;
        private readonly string                  defaultNamespace;
        private readonly Type                    serializerType;
        private readonly XmlSerializerNamespaces namespaces;
        private readonly XmlSerializer           serializer;
        private readonly XmlNameTable            nameTable;
        private readonly XmlNamespaceManager     nsMgr;
        private readonly XmlParserContext        context;

        /// <summary>
        /// Gets the name of the element.
        /// </summary>
        /// <value>The name of the element.</value>
        public string ElementName
        {
            get { return this.elementName; }
        }

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix
        {
            get { return this.prefix; }
        }

        /// <summary>
        /// Gets the default namespace.
        /// </summary>
        /// <value>The default namespace.</value>
        public string DefaultNamespace
        {
            get { return this.defaultNamespace; }
        }

        /// <summary>
        /// Gets the type of the serializer.
        /// </summary>
        /// <value>The type of the serializer.</value>
        public Type SerializerType
        {
            get { return this.serializerType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppSerializer"/> class.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="defaultNamespace">The default namespace.</param>
        /// <param name="serializerType">Type of the serializer.</param>
        private XmppSerializer(string elementName, string prefix, string defaultNamespace, Type serializerType)
        {
            this.elementName	  = elementName;
            this.serializerType	  = serializerType;
            this.prefix			  = prefix;
            this.defaultNamespace = defaultNamespace;
            this.serializer       = new XmlSerializer(serializerType, defaultNamespace);
            this.nameTable		  = new NameTable();
            this.nsMgr			  = new XmlNamespaceManager(this.nameTable);
            this.context		  = new XmlParserContext(this.nameTable, this.nsMgr, null, XmlSpace.None, XmppEncoding.Utf8);
            this.namespaces		  = new XmlSerializerNamespaces();

            this.namespaces.Add(prefix, defaultNamespace);

            foreach (XmlQualifiedName name in this.namespaces.ToArray())
            {
                this.nsMgr.AddNamespace(name.Name, name.Namespace);
            }
        }

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        private byte[] SerializeObject(object value, string prefix)
        {
            using (var buffer = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(buffer, XmlWriterSettings))
                {
                    this.serializer.Serialize(writer, value, this.namespaces);

                    return buffer.ToArray();
                }
            }
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        private object Deserialize(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(reader, XmlReaderSettings, this.context))
                {
                    return this.serializer.Deserialize(xmlReader);
                }
            }
        }
    }
}
