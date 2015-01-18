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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/amp#errors")]
    [XmlRootAttribute("failed-rules", Namespace = "http://jabber.org/protocol/amp#errors", IsNullable = false)]
    public partial class AmpFailedRules
    {
        [XmlElementAttribute("rule")]
        public List<AmpRule> Rules
        {
            get;
            private set;
        }

        public AmpFailedRules()
        {
            this.Rules = new List<AmpRule>();
        }
    }
}
