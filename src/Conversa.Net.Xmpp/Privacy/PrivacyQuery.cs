// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Privacy
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Privacy Lists
    /// </summary>
    /// <remarks>
    /// XEP-0016: Privacy Lists
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "jabber:iq:privacy")]
    [XmlRootAttribute("query", Namespace = "jabber:iq:privacy", IsNullable = false)]
    public partial class PrivacyQuery
    {
        [XmlElementAttribute("active")]
        public PrivacyActive Active
        {
            get;
            set;
        }

        [XmlElementAttribute("default")]
        public PrivacyDefault Default
        {
            get;
            set;
        }

        [XmlElementAttribute("list")]
        public List<PrivacyList> Items
        {
            get;
            set;
        }

        public PrivacyQuery()
        {
            this.Items   = new List<PrivacyList>();
            this.Default = new PrivacyDefault();
            this.Active  = new PrivacyActive();
        }
    }
}
