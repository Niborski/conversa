// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Caps;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Registry;
using Conversa.Net.Xmpp.ServiceDiscovery;
using Conversa.Net.Xmpp.Storage;
using System;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a contact resource
    /// </summary>
    public sealed class XmppContactResource
        : XmppMessageProcessor
    {
        private XmppContact             contact;
        private XmppAddress			    address;
        private XmppPresence            presence;
        private XmppEntityCapabilities  capabilities;
        private XmppCapabilitiesStorage capsStorage;
        private string				    avatarHash;
        private System.IO.Stream        avatar;

        /// <summary>
        /// Gets or sets the resource address
        /// </summary>
        /// <value>The resource id.</value>
        public XmppAddress Address
        {
            get { return this.address; }
        }

        /// <summary>
        /// Gets or sets the resource presence information.
        /// </summary>
        /// <value>The presence.</value>
        public XmppPresence Presence
        {
            get { return this.presence; }
        }

        /// <summary>
        /// Gets or sets the resource capabilities.
        /// </summary>
        /// <value>The capabilities.</value>
        public XmppEntityCapabilities Capabilities
        {
            get { return this.capabilities; }
            private set { this.capabilities = value; }
        }

        /// <summary>
        /// Gets the original avatar image
        /// </summary>
        public System.IO.Stream Avatar
        {
            get { return this.avatar; }
            private set { this.avatar = value; }
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports MUC
        /// </summary>
        public bool SupportsConference
        {
            get { return this.Capabilities.IsSupported(XmppFeatures.MultiUserChat); }
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports chat state notifications
        /// </summary>
        public bool SupportsChatStateNotifications
        {
            get { return this.Capabilities.IsSupported(XmppFeatures.ChatStateNotifications); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppContactResource"/> class.
        /// </summary>
        internal XmppContactResource(XmppClient client, XmppContact contact, XmppAddress address)
            : base(client)
        {
            this.contact      = contact;
            this.address      = address;
            this.presence	  = new XmppPresence(this.Client, this);
            this.capabilities = new XmppEntityCapabilities();
            this.capsStorage  = new XmppCapabilitiesStorage(StorageFolderType.Roaming, this.Address + "Caps.xml");
        }

        public override string ToString()
        {
            return this.address;
        }

        internal void Update(Presence presence)
        {
            this.Presence.Update(presence);

            if (this.Presence.ShowAs == ShowType.Offline)
            {
                string cachedHash = this.Client.AvatarStorage.GetAvatarHash(this.address.BareAddress);

                // Grab stored images for offline users
                if (!String.IsNullOrEmpty(cachedHash))
                {
                    // Dipose Avatar Streams
                    this.DisposeAvatarStream();

                    // Update the avatar hash and file Paths
                    this.avatarHash = cachedHash;
                    this.Avatar     = this.Client.AvatarStorage.ReadAvatar(this.address.BareAddress);
                }
            }

            foreach (object item in presence.Items)
            {
                if (item is VCardAvatar)
                {
                    this.UpdateVCardAvatar(item as VCardAvatar);
                }
                else if (item is EntityCapabilities)
                {
                    this.UpdateCapabilitiesAsync(item as EntityCapabilities);
                }
            }
        }

        private void UpdateVCardAvatar(VCardAvatar vcard)
        {
            if (vcard.Photo != null && vcard.Photo.Length > 0)
            {
                if (!String.IsNullOrEmpty(vcard.Photo))
                {
                    // Check if we have the avatar cached
                    string cachedHash = this.Client.AvatarStorage.GetAvatarHash(this.address.BareAddress);

                    if (cachedHash == vcard.Photo)
                    {
                        // Dispose Avatar Streams
                        this.DisposeAvatarStream();

                        // Update the avatar hash and file Paths
                        this.avatarHash = vcard.Photo;
                        this.Avatar     = this.Client.AvatarStorage.ReadAvatar(this.address.BareAddress);
                    }
                    else
                    {
                        // Update the avatar hash
                        this.avatarHash = vcard.Photo;

                        // Avatar is not cached request the new avatar information
                        this.RequestAvatarAsync();
                    }
                }
            }
        }

        private async Task UpdateCapabilitiesAsync(EntityCapabilities caps)
        {
            // Request capabilities only if they aren't cached yet for this resource
            // or the verfiication string differs from the one that is cached
            if (this.Capabilities == null || this.Capabilities.VerificationString != caps.VerificationString)
            {
                this.Capabilities.Update(caps);

                // Check if we have the capabilities in the storage
                if (!await this.capsStorage.IsEmptyAsync().ConfigureAwait(false))
                {
                    this.Capabilities = await this.capsStorage.LoadAsync().ConfigureAwait(false);
                }
                else if ((this.contact.Subscription == RosterSubscriptionType.Both
                       || this.contact.Subscription == RosterSubscriptionType.To))
#warning TODO: Review
                        // && (!this.Presence.TypeSpecified || presence.Type == PresenceType.Unavailable)
                {
                    // Discover Entity Capabilities Extension Features
                    await this.DiscoverCapabilitiesAsync().ConfigureAwait(false);
                }
            }
        }

        private async Task DiscoverCapabilitiesAsync()
        {
            var iq = InfoQuery.Create()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.Address)
                              .ForRequest();

            iq.ServiceInfo = new ServiceInfo { Node = this.Capabilities.DiscoveryInfoNode };

            await this.SendMessageAsync(iq);
        }

        private async Task RequestAvatarAsync()
        {
            if (this.contact.Subscription == RosterSubscriptionType.Both
             || this.contact.Subscription == RosterSubscriptionType.To)
            {
                return;
            }

            var iq = InfoQuery.Create()
                              .FromAddress(this.Client.UserAddress)
                              .ToAddress(this.Address)
                              .ForRequest();

            iq.VCardData = new VCardData();

            await this.SendMessageAsync(iq);
        }

        private void DisposeAvatarStream()
        {
            if (this.Avatar != null)
            {
                this.Avatar.Dispose();
                this.Avatar = null;
            }
        }

        protected override void OnResponseMessage(InfoQuery response)
        {
            if (response.VCardData != null)
            {
                this.OnVCardMessage(response.VCardData);
            }
            else if (response.ServiceInfo != null)
            {
                this.OnServiceInfoMessage(response.ServiceInfo);
            }

            base.OnResponseMessage(response);
        }

        private void OnServiceInfoMessage(ServiceInfo service)
        {
            this.Capabilities.Identities.Clear();
            this.Capabilities.Features.Clear();

            // Reponse to our capabilities query
            foreach (var identity in service.Identities)
            {
                this.Capabilities.Identities.Add
                (
                    new XmppServiceIdentity(identity.Name, identity.Category, identity.Type)
                );
            }

            foreach (var supportedFeature in service.Features)
            {
                this.Capabilities.Features.Add(new XmppServiceFeature(supportedFeature.Name));
            }

#warning TODO: Reimplement
            //if (!this.Capabilities.Exists(this.capabilities.Node, this.capabilities.VerificationString))
            //{
            //    this.Client.Capabilities.Update(this.Capabilities);
            //    this.Client.Capabilities.Save();
            //}

            //this.NotifyPropertyChanged(() => Capabilities);
        }

        private void OnVCardMessage(VCardData vCard)
        {
#warning TODO: Implement
//            // Update the Avatar image
//            if (vCard.Photo.Photo != null && vCard.Photo.Photo.Length > 0)
//            {
//                Image avatarImage = null;

//                try
//                {
//                    this.DisposeAvatarStream();

//                    using (MemoryStream avatarStream = new MemoryStream(vCard.Photo.Photo))
//                    {
//                        // En sure it's a valid image
//                        avatarImage = Image.FromStream(avatarStream);

//                        // Save avatar
//                        if (avatarStream != null && avatarStream.Length > 0)
//                        {
//                            this.Client.AvatarStorage.SaveAvatar(this.Address.BareAddress, this.avatarHash, avatarStream);
//                        }
//                    }

//                    this.Avatar = this.Client.AvatarStorage.ReadAvatar(this.Address.BareAddress);
//                }
//                catch
//                {
//#warning TODO: Handle the exception
//                }
//                finally
//                {
//                    if (avatarImage != null)
//                    {
//                        avatarImage.Dispose();
//                        avatarImage = null;
//                    }
//                }
//            }
//            else
//            {
//                this.Client.AvatarStorage.RemoveAvatar(this.Address.BareAddress);
//            }

//            this.Client.AvatarStorage.Save();
        }
    }
}
