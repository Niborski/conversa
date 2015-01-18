// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.ServiceDiscovery;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.Caps
{
    /// <summary>
    /// Entity capabilities (XEP-0115)
    /// </summary>
    [XmlTypeAttribute(Namespace = "")]
    [XmlRootAttribute("caps", Namespace = "", IsNullable = false)]
    public class XmppEntityCapabilities
    {
        private string                    node;
        private string                    hashAlgorithmName;
        private string                    verificationString;
        private List<XmppServiceIdentity> identities;
        private List<XmppServiceFeature>  supportedFeatures;

        /// <summary>
        /// Gets or sets the client node
        /// </summary>
        [XmlAttribute("node")]
        public string Node
        {
            get { return this.node; }
            set { this.node = value; }
        }

        /// <summary>
        /// Gets or sets the client version
        /// </summary>
        [XmlAttribute("ver")]
        public string VerificationString
        {
            get { return this.verificationString; }
            set { this.verificationString = value; }
        }

        /// <summary>
        /// Gets or sets the hash algorithm name
        /// </summary>
        [XmlAttribute("hash")]
        public string HashAlgorithmName
        {
            get { return hashAlgorithmName; }
            set { this.hashAlgorithmName = value; }
        }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>The identity.</value>
        [XmlArray("identities")]
        [XmlArrayItem("identity", typeof(XmppServiceIdentity))]
        public List<XmppServiceIdentity> Identities
        {
            get { return this.identities; }
        }

        /// <summary>
        /// Gets the list of supported features
        /// </summary>
        [XmlArray("features")]
        [XmlArrayItem("feature", typeof(XmppServiceFeature))]
        public List<XmppServiceFeature> Features
        {
            get { return this.supportedFeatures; }
        }

        /// <summary>
        /// Gets the discovery info node
        /// </summary>
        [XmlIgnore]
        public string DiscoveryInfoNode
        {
            get { return this.Node + "#" + this.VerificationString; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppEntityCapabilities"/> class.
        /// </summary>
        public XmppEntityCapabilities()
        {
            this.identities        = new List<XmppServiceIdentity>();
            this.supportedFeatures = new List<XmppServiceFeature>();
        }

        public bool IsSupported(string featureName)
        {
            return (this.Features.Count(f => f.Name == featureName) > 0);
        }

        internal void Update(EntityCapabilities capability)
        {
            this.Node               = capability.Node;
            this.HashAlgorithmName  = capability.HashAlgorithmName;
            this.VerificationString = capability.VerificationString;
        }
    }
}
