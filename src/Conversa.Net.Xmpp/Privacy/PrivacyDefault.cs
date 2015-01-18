// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Privacy
{
    using System.Xml.Serialization;

    /// <summary>
    /// Privacy Lists
    /// </summary>
    /// <remarks>
    /// XEP-0016: Privacy Lists
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:privacy")]
    [XmlRootAttribute("default", Namespace = "jabber:iq:privacy", IsNullable = false)]
    public partial class PrivacyDefault
    {
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        [XmlTextAttribute(DataType = "NMTOKEN")]
        public string Value
        {
            get;
            set;
        }

        public PrivacyDefault()
        {
        }
    }
}
