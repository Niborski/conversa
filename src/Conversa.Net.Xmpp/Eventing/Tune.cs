// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Eventing
{
    using System.Xml.Serialization;

    /// <summary>
    /// User Tune
    /// </summary>
    /// <remarks>
    /// XEP-0118: User Tune
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/tune")]
    [XmlRootAttribute("tune", Namespace = "http://jabber.org/protocol/tune", IsNullable = false)]
    public partial class Tune
    {
        /// <remarks/>
        [XmlElementAttribute("artist")]
        public string Artist
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("length", DataType = "unsignedShort")]
        public ushort Length
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public bool LengthSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("rating", DataType = "positiveInteger")]
        public string Rating
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("source")]
        public string Source
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("title")]
        public string Title
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("track")]
        public string Track
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("uri", DataType = "anyURI")]
        public string Uri
        {
            get;
            set;
        }

        public Tune()
        {
        }
    }
}
