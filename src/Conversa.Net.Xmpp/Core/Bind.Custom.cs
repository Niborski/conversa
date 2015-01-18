// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Resource Binding
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    public partial class Bind
    {
        public static Bind WithAddress(string address)
        {
            return new Bind { Jid = address };
        }

        public static Bind WithResource(string resource)
        {
            return new Bind { Resource = resource };
        }
    }
}
