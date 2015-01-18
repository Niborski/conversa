// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.EntityTime
{
    using System.Xml.Serialization;

    /// <summary>
    /// Entity Time
    /// </summary>
    /// <remarks>
    /// XEP-0202 Entity Time
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xmpp:time")]
    [XmlRootAttribute("time", Namespace = "urn:xmpp:time", IsNullable = false)]
    public partial class EntityTime
    {
        [XmlElementAttribute("tzo")]
        public string TimeZone
        {
            get;
            set;
        }

        [XmlElementAttribute("utc")]
        public string Utc
        {
            get;
            set;
        }

        public EntityTime()
        {
        }
    }
}
