// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.LastActivity
{
    using System.Xml.Serialization;

    /// <summary>
    /// Last Activity
    /// </summary>
    /// <remarks>
    /// XEP-0012 Last Activity
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:last")]
    [XmlRootAttribute("query", Namespace = "jabber:iq:last", IsNullable = false)]
    public partial class LastActivity
    {
        [XmlTextAttribute]
        public string Value
        {
            get;
            set;
        }

        [XmlAttribute("seconds")]
        public ulong Seconds
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool SecondsSpecified
        {
            get;
            set;
        }

        public LastActivity()
        {
        }
    }
}
