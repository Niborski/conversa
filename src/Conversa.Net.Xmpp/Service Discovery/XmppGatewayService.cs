// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.InBandRegistration;
using System;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.ServiceDiscovery
{
    /// <summary>
    /// XMPP Gateway service
    /// </summary>
    public class XmppGatewayService
        : XmppService
    {
        private XmppGatewayType type;

        /// <summary>
        /// Gets the gateway type
        /// </summary>
        public XmppGatewayType Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// Initiazes a new instance of the <see cref="XmppGatewayService">XmppGatewayService</see> class.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="serviceId"></param>
        public XmppGatewayService(XmppClient client, string  addresss)
            : base(client, addresss)
        {
            this.InferGatewayType();
        }

        /// <summary>
        /// Sets the initial presence agains the XMPP Service.
        /// </summary>
        public Task SetDefaultPresenceAsync()
        {
#warning TODO: Reimplement
            throw new NotImplementedException();
            // await this.Client.Presence.SetDefaultPresence(this.Address);
        }

        /// <summary>
        /// Performs the gateway registration process
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async Task RegisterAsync(string username, string password)
        {
            var iq = InfoQuery.Create()
                              .ForUpdate()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.Address);

            iq.Register = new Register { UserName = username, Password = password };

            await this.Client.SendAsync(iq);
        }

        /// <summary>
        /// Performs the gateway unregistration process
        /// </summary>
        public async Task UnregisterAsync()
        {
            var iq = InfoQuery.Create()
                              .ForUpdate()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.Address);

            iq.Register = new Register { Remove = String.Empty };

            await this.Client.SendAsync(iq);
        }

        private void InferGatewayType()
        {
            if (this.Address.BareAddress.StartsWith("aim"))
            {
                this.type = XmppGatewayType.Aim;
            }
            else if (this.Address.BareAddress.StartsWith("facebook"))
            {
                this.type = XmppGatewayType.Facebook;
            }
            else if (this.Address.BareAddress.StartsWith("gadugadu"))
            {
                this.type = XmppGatewayType.GaduGadu;
            }
            else if (this.Address.BareAddress.StartsWith("gtalk"))
            {
                this.type = XmppGatewayType.GTalk;
            }
            else if (this.Address.BareAddress.StartsWith("http-ws"))
            {
                this.type = XmppGatewayType.HttpWs;
            }
            else if (this.Address.BareAddress.StartsWith("icq"))
            {
                this.type = XmppGatewayType.Icq;
            }
            else if (this.Address.BareAddress.StartsWith("lcs"))
            {
                this.type = XmppGatewayType.Lcs;
            }
            else if (this.Address.BareAddress.StartsWith("mrim"))
            {
                this.type = XmppGatewayType.Mrim;
            }
            else if (this.Address.BareAddress.StartsWith("msn"))
            {
                this.type = XmppGatewayType.Msn;
            }
            else if (this.Address.BareAddress.StartsWith("myspaceim"))
            {
                this.type = XmppGatewayType.MySpaceIm;
            }
            else if (this.Address.BareAddress.StartsWith("ocs"))
            {
                this.type = XmppGatewayType.Ocs;
            }
            else if (this.Address.BareAddress.StartsWith("qq"))
            {
                this.type = XmppGatewayType.QQ;
            }
            else if (this.Address.BareAddress.StartsWith("sametime"))
            {
                this.type = XmppGatewayType.Sametime;
            }
            else if (this.Address.BareAddress.StartsWith("simple"))
            {
                this.type = XmppGatewayType.Simple;
            }
            else if (this.Address.BareAddress.StartsWith("skype"))
            {
                this.type = XmppGatewayType.Skype;
            }
            else if (this.Address.BareAddress.StartsWith("sms"))
            {
                this.type = XmppGatewayType.Sms;
            }
            else if (this.Address.BareAddress.StartsWith("smtp"))
            {
                this.type = XmppGatewayType.Smtp;
            }
            else if (this.Address.BareAddress.StartsWith("tlen"))
            {
                this.type = XmppGatewayType.Tlen;
            }
            else if (this.Address.BareAddress.StartsWith("xfire"))
            {
                this.type = XmppGatewayType.Xfire;
            }
            else if (this.Address.BareAddress.StartsWith("xmpp"))
            {
                this.type = XmppGatewayType.Xmpp;
            }
            else if (this.Address.BareAddress.StartsWith("yahoo"))
            {
                this.type = XmppGatewayType.Yahoo;
            }
        }
    }
}
