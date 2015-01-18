// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    /// <summary>
    /// XMPP Service Feature
    /// </summary>
    [XmlTypeAttribute(Namespace = "")]
    [XmlRootAttribute("feature", Namespace = "", IsNullable = false)]
    public sealed class XmppServiceFeature
    {
        private string name;

        /// <summary>
        /// Gets the feature name
        /// </summary>
        [XmlAttributeAttribute("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppServiceFeature">XmppServiceFeature</see> class.
        /// </summary>
        internal XmppServiceFeature()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppServiceFeature">XmppServiceFeature</see> class.
        /// </summary>
        /// <param name="name"></param>
        internal XmppServiceFeature(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
