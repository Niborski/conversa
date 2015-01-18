// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    /// <summary>
    /// Represents a <see cref="XmppService"/> item.
    /// </summary>
    public sealed class XmppServiceItem
        : XmppServiceDiscoveryObject
    {
        internal XmppServiceItem(XmppClient client, string address)
            : base(client, address)
        {
        }
    }
}
