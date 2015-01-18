// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Blocking
{
    using System.Xml.Serialization;

    /// <summary>
    /// Blocking Command
    /// </summary>
    /// <remarks>
    /// XEP-0191: Blocking Command
    /// </remarks>
    [XmlTypeAttribute(Namespace = "urn:xmpp:blocking:errors")]
    [XmlRootAttribute("blocked", Namespace = "urn:xmpp:blocking:errors", IsNullable = false)]
    public partial class BlockingErrors
    {
        public BlockingErrors()
        {
        }
    }
}
