// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Storage;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.Caps
{
    /// <summary>
    /// Entity capabilities store
    /// </summary>
    public sealed class XmppCapabilitiesStorage
        : SecureFileStorage<XmppEntityCapabilities>
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(XmppEntityCapabilities));

        public XmppCapabilitiesStorage(StorageFolderType folderType, string filename)
            : this(folderType, filename, DataProtectionType.User)
        {
        }

        public XmppCapabilitiesStorage(StorageFolderType folderType, string filename, DataProtectionType protectionType)
            : base(folderType, filename, protectionType)
        {
        }

        protected override XmppEntityCapabilities OnDataLoaded(string data)
        {
            var result   = default(XmppEntityCapabilities);
            var settings = new XmlReaderSettings
            {
                CloseInput       = true
              , ConformanceLevel = ConformanceLevel.Fragment
            };

            using (var reader = new StringReader(data))
            {
                using (var xmlReader = XmlReader.Create(reader, settings))
                {
                    result = (XmppEntityCapabilities)Serializer.Deserialize(xmlReader);
                }
            }

            return result;
        }

        protected override string OnSerializeData(XmppEntityCapabilities data)
        {
            var settings = new XmlWriterSettings
            {
                ConformanceLevel   = ConformanceLevel.Auto
              , Encoding           = System.Text.Encoding.UTF8
              , Indent             = false
              , NamespaceHandling  = NamespaceHandling.Default
              , OmitXmlDeclaration = true
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    Serializer.Serialize(writer, data);

                    byte[] buffer = stream.ToArray();

                    return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
