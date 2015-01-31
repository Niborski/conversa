// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Discovery
{
    /// <summary>
    /// Service Discovery
    /// </summary>
    /// <remarks>
    /// XEP-0030: Service Discovery
    /// </remarks>
    public partial class ServiceIdentity
    {
        public ServiceCategory GetCategory()
        {
            switch (this.Category)
            {
                case "account":
                    return ServiceCategory.Account;

                case "auth":
                    return ServiceCategory.Auth;

                case "automation":
                    return ServiceCategory.Automation;

                case "client":
                    return ServiceCategory.Client;

                case "collaboration":
                    return ServiceCategory.Collaboration;

                case "component":
                    return ServiceCategory.Component;

                case "conference":
                    return ServiceCategory.Conference;

                case "directory":
                    return ServiceCategory.Directory;

                case "gateway":
                    return ServiceCategory.Gateway;

                case "headline":
                    return ServiceCategory.Headline;

                case "hierarchy":
                    return ServiceCategory.Hierarchy;

                case "proxy":
                    return ServiceCategory.Proxy;

                case "pubsub":
                    return ServiceCategory.Pubsub;

                case "server":
                    return ServiceCategory.Server;

                case "store":
                    return ServiceCategory.Store;

                default:
                    return ServiceCategory.Unknown;
            }
        }

    }
}
