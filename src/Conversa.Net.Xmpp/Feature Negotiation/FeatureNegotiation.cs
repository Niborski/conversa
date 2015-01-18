// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.FeatureNegotiation
{
    using Conversa.Net.Xmpp.DataForms;
    using System.Xml.Serialization;

    /// <summary>
    /// Feature Negotiation
    /// </summary>
    /// <remarks>
    /// XEP-0020: Feature Negotiation
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/feature-neg")]
    [XmlRootAttribute("feature", Namespace = "http://jabber.org/protocol/feature-neg", IsNullable = false)]
    public partial class FeatureNegotiation
    {
        [XmlElementAttribute("x", Namespace = "jabber:x:data")]
        public DataForm Form
        {
            get;
            set;
        }

        public FeatureNegotiation()
        {
            this.Form = new DataForm();
        }
    }
}
