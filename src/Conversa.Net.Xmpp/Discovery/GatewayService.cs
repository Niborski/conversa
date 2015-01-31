// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.InBandRegistration;
using System;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Discovery
{
    /// <summary>
    /// XMPP Gateway service
    /// </summary>
    public class GatewayService
    {
        private XmppClient  Client;
        private XmppAddress Address;
        private GatewayType type;

        /// <summary>
        /// Gets the gateway type
        /// </summary>
        public GatewayType Type
        {
            get { return type;  }
        }

        /// <summary>
        /// Initiazes a new instance of the <see cref="GatewayService">XmppGatewayService</see> class.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="serviceId"></param>
        public GatewayService(XmppClient client, string address)
        {
            this.Client  = client;
            this.Address = address;
            this.SetGatewayType();
        }

        /// <summary>
        /// Sets the initial presence agains the XMPP Service.
        /// </summary>
        public async Task SetDefaultPresenceAsync()
        {
            // await this.Client.Presence.SetDefaultPresenceAsync(this.Address);
        }

        /// <summary>
        /// Performs the gateway registration process
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async Task RegisterAsync(string username, string password)
        {
            var iq = new InfoQuery
            {
                Type     = InfoQueryType.Set
              , From     = this.Client.UserAddress
              , To       = this.Address
              , Register = new Register { UserName = username, Password = password }
            };

            await this.Client.SendAsync(iq);
        }

        /// <summary>
        /// Performs the gateway unregistration process
        /// </summary>
        public async Task UnregisterAsync()
        {
            var iq = new InfoQuery
            {
                Type     = InfoQueryType.Set
              , From     = this.Client.UserAddress
              , To       = this.Address
              , Register = new Register { Remove = String.Empty }
            };

            await this.Client.SendAsync(iq);
        }

        private void SetGatewayType()
        {
            if (this.Address.BareAddress.StartsWith("aim"))
            {
                this.type = GatewayType.Aim;
            }
            else if (this.Address.BareAddress.StartsWith("facebook"))
            {
                this.type = GatewayType.Facebook;
            }
            else if (this.Address.BareAddress.StartsWith("gadugadu"))
            {
                this.type = GatewayType.GaduGadu;
            }
            else if (this.Address.BareAddress.StartsWith("gtalk"))
            {
                this.type = GatewayType.GTalk;
            }
            else if (this.Address.BareAddress.StartsWith("http-ws"))
            {
                this.type = GatewayType.HttpWs;
            }
            else if (this.Address.BareAddress.StartsWith("icq"))
            {
                this.type = GatewayType.Icq;
            }
            else if (this.Address.BareAddress.StartsWith("lcs"))
            {
                this.type = GatewayType.Lcs;
            }
            else if (this.Address.BareAddress.StartsWith("mrim"))
            {
                this.type = GatewayType.Mrim;
            }
            else if (this.Address.BareAddress.StartsWith("msn"))
            {
                this.type = GatewayType.Msn;
            }
            else if (this.Address.BareAddress.StartsWith("myspaceim"))
            {
                this.type = GatewayType.MySpaceIm;
            }
            else if (this.Address.BareAddress.StartsWith("ocs"))
            {
                this.type = GatewayType.Ocs;
            }
            else if (this.Address.BareAddress.StartsWith("qq"))
            {
                this.type = GatewayType.QQ;
            }
            else if (this.Address.BareAddress.StartsWith("sametime"))
            {
                this.type = GatewayType.Sametime;
            }
            else if (this.Address.BareAddress.StartsWith("simple"))
            {
                this.type = GatewayType.Simple;
            }
            else if (this.Address.BareAddress.StartsWith("skype"))
            {
                this.type = GatewayType.Skype;
            }
            else if (this.Address.BareAddress.StartsWith("sms"))
            {
                this.type = GatewayType.Sms;
            }
            else if (this.Address.BareAddress.StartsWith("smtp"))
            {
                this.type = GatewayType.Smtp;
            }
            else if (this.Address.BareAddress.StartsWith("tlen"))
            {
                this.type = GatewayType.Tlen;
            }
            else if (this.Address.BareAddress.StartsWith("xfire"))
            {
                this.type = GatewayType.Xfire;
            }
            else if (this.Address.BareAddress.StartsWith("xmpp"))
            {
                this.type = GatewayType.Xmpp;
            }
            else if (this.Address.BareAddress.StartsWith("yahoo"))
            {
                this.type = GatewayType.Yahoo;
            }
        }
    }
}
