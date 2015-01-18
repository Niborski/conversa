// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.AdvancedMessageProcessing
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Advanced Message Processing
    /// </summary>
    /// <remarks>
    /// XEP-0079: Advanced Message Processing
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/amp")]
    [XmlRootAttribute("unsupported-actions", Namespace = "http://jabber.org/protocol/amp", IsNullable = false)]
    public partial class AmpUnsupportedActions
    {
        [XmlElementAttribute("rule")]
        public List<AmpRule> Rules
        {
            get;
            private set;
        }

        public AmpUnsupportedActions()
        {
            this.Rules = new List<AmpRule>();
        }
    }
}
