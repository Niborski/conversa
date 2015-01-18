// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Caps;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Registry;
using Conversa.Net.Xmpp.ServiceDiscovery;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Client capabilities (XEP-0115)
    /// </summary>
    public sealed class XmppClientCapabilities
        : XmppMessageProcessor
    {
        private const string DefaultHashAlgorithmName = "sha-1";

        private const string Uri      = "http://Conversa.codeplex.com";
        private const string Category = "client";
        private const string Type     = "pc";
        private const string Name     = "babel im";
        private const string Version  = "0.1";

        private XmppEntityCapabilities caps;

        /// <summary>
        /// Gets the service discovery name
        /// </summary>
        public string ServiceDiscoveryName
        {
            get { return Name + " " + Version; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppClientCapabilities"/> class.
        /// </summary>
        internal XmppClientCapabilities(XmppClient client)
            : base(client)
        {
            this.caps = new XmppEntityCapabilities();

            // Supported features
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.ServiceDiscoveryInfo));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.ServiceDiscoveryItems));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.EntityCapabilities));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.BidirectionalStreamsOverSynchronousHTTP));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.ChatStateNotifications));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.MultiUserChat));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.MultiUserChatUser));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.UserMood));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.UserMoodWithNotify));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.UserTune));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.UserTuneWithNotify));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.XmppPing));
            this.caps.Features.Add(new XmppServiceFeature(XmppFeatures.DnsSrvLookups));
        }

        public async Task AdvertiseCapabilitiesAsync()
        {
            var presence = Presence.Create();

            presence.Items.Add(this.GetEntityCapabilities());

            await this.SendMessageAsync(presence).ConfigureAwait(false);
        }

        protected override void OnResponseMessage(Presence presence)
        {
        }

#warning TODO : Add Subscription
        protected override void OnResponseMessage(InfoQuery message)
        {
            // Answers to Entity Capabilities and service discovery info requests
            if (message.Type == InfoQueryType.Get)
            {
                if (message.ServiceInfo != null)
                {
                    var query   = message.ServiceInfo;
                    var service = new ServiceInfo()
                    {
                        Node = ((!String.IsNullOrEmpty(query.Node)) ? this.caps.DiscoveryInfoNode : null)
                    };

                    foreach (XmppServiceIdentity identity in this.caps.Identities)
                    {
                        var supportedIdentity = new ServiceIdentity
                        {
                            Name        = identity.Name,
                            Category    = identity.Category.ToString().ToLower(),
                            Type        = identity.Type
                        };

                        service.Identities.Add(supportedIdentity);
                    }

                    foreach (XmppServiceFeature supportedFeature in this.caps.Features)
                    {
                        ServiceFeature feature = new ServiceFeature
                        {
                            Name = supportedFeature.Name
                        };

                        service.Features.Add(feature);
                    }

                    var response = InfoQuery.Create()
                                            .ResponseTo(message.Id)
                                            .ToAddress(message.From);

                    response.ServiceInfo = service;


                    this.Client.SendAsync(response);
                }
            }
        }

        private EntityCapabilities GetEntityCapabilities()
        {
            var capabilities = new EntityCapabilities
            {
                HashAlgorithmName  = DefaultHashAlgorithmName
              , Node               = this.caps.Node
              , VerificationString = this.BuildVerificationString()
            };

            // Update the Verification String
            this.caps.VerificationString = capabilities.VerificationString;

            return capabilities;
        }

        /// <summary>
        /// Builds the verification string.
        /// </summary>
        /// <returns>The encoded verification string</returns>
        /// <remarks>
        /// 5.1 In order to help prevent poisoning of entity capabilities information,
        /// the value of the verification string MUST be generated according to the following method.
        ///
        /// 1. Initialize an empty string S.
        /// 2. Sort the service discovery identities [15] by category and then by type and then by xml:lang (if it exists),
        /// formatted as CATEGORY '/' [TYPE] '/' [LANG] '/' [NAME]. [16] Note that each slash is included even if the TYPE, LANG,
        /// or NAME is not included.
        /// 3. For each identity, append the 'category/type/lang/name' to S, followed by the '<' character.
        /// 4. Sort the supported service discovery features. [17]
        /// 5. For each feature, append the feature to S, followed by the '<' character.
        /// 6. If the service discovery information response includes XEP-0128 data forms,
        ///    sort the forms by the FORM_TYPE (i.e., by the XML character data of the <value/> element).
        /// 7. For each extended service discovery information form:
        ///     1. Append the XML character data of the FORM_TYPE field's <value/> element, followed by the '<' character.
        ///     2. Sort the fields by the value of the "var" attribute.
        ///     3. For each field:
        ///         1. Append the value of the "var" attribute, followed by the '<' character.
        ///         2. Sort values by the XML character data of the <value/> element.
        ///         3. For each <value/> element, append the XML character data, followed by the '<' character.
        /// 8. Ensure that S is encoded according to the UTF-8 encoding (RFC 3269 [18]).
        /// 9. Compute the verification string by hashing S using the algorithm specified in the 'hash' attribute
        ///    (e.g., SHA-1 as defined in RFC 3174 [19]). The hashed data MUST be generated with binary output and
        ///    encoded using Base64 as specified in Section 4 of RFC 4648 [20] (note: the Base64 output MUST NOT
        ///    include whitespace and MUST set padding bits to zero). [21]
        ///
        /// 5.2 Simple Generation Example
        ///
        /// Consider an entity whose category is "client", whose service discovery type is "pc", whose service discovery name is "Exodus 0.9.1", and whose supported features are "http://jabber.org/protocol/disco#info", "http://jabber.org/protocol/disco#items", and "http://jabber.org/protocol/muc". Using the SHA-1 algorithm, the verification string would be generated as follows (note: line breaks in the verification string are included only for the purposes of readability):
        /// 1. S = ''
        /// 2. Only one identity: "client/pc"
        /// 3. S = 'client/pc//Exodus 0.9.1<'
        /// 4. Sort the features: "http://jabber.org/protocol/caps", "http://jabber.org/protocol/disco#info", "http://jabber.org/protocol/disco#items", "http://jabber.org/protocol/muc".
        /// 5. S = 'client/pc//Exodus 0.9.1<http://jabber.org/protocol/caps<http://jabber.org/protocol/disco#info<
        ///    http://jabber.org/protocol/disco#items<http://jabber.org/protocol/muc<
        /// 6. ver = QgayPKawpkPSDYmwT/WM94uAlu0=
        /// </remarks>
        private string BuildVerificationString()
        {
            var builder = new StringBuilder();

            foreach (XmppServiceIdentity identity in this.caps.Identities)
            {
                builder.AppendFormat("{0}/{1}//{2}<", identity.Category, identity.Type, this.ServiceDiscoveryName);
            }

            foreach (XmppServiceFeature supportedFeature in this.caps.Features)
            {
                builder.AppendFormat("{0}<", supportedFeature.Name);
            }

            return builder.ComputeSHA1Hash().ToBase64String();
        }
    }
}
