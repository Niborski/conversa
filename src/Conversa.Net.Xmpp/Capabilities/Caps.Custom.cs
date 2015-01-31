// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.Capabilities
{
    /// <summary>
    /// Entity Capabilities
    /// </summary>
    /// <remarks>
    /// XEP-0115: Entity Capabilities
    /// </remarks>
    public partial class Caps
    {
        /// <summary>
        /// Gets the service discovery node
        /// </summary>
        [XmlIgnoreAttribute]
        public string DiscoveryNode
        {
            get { return this.Node + "#" + this.VerificationString; }
        }
    }
}
