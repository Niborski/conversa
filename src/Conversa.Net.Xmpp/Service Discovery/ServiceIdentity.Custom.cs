// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    public partial class ServiceIdentity
    {
        public XmppServiceCategory GetCategory()
        {
            switch (this.Category)
            {
                case "account":
                    return XmppServiceCategory.Account;

                case "auth":
                    return XmppServiceCategory.Auth;

                case "automation":
                    return XmppServiceCategory.Automation;

                case "client":
                    return XmppServiceCategory.Client;

                case "collaboration":
                    return XmppServiceCategory.Collaboration;

                case "component":
                    return XmppServiceCategory.Component;

                case "conference":
                    return XmppServiceCategory.Conference;

                case "directory":
                    return XmppServiceCategory.Directory;

                case "gateway":
                    return XmppServiceCategory.Gateway;

                case "headline":
                    return XmppServiceCategory.Headline;

                case "hierarchy":
                    return XmppServiceCategory.Hierarchy;

                case "proxy":
                    return XmppServiceCategory.Proxy;

                case "pubsub":
                    return XmppServiceCategory.Pubsub;

                case "server":
                    return XmppServiceCategory.Server;

                case "store":
                    return XmppServiceCategory.Store;

                default:
                    return XmppServiceCategory.Unknown;
            }
        }

    }
}
