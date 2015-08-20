// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Capabilities;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a contact resource
    /// </summary>
    public sealed class ContactResource
    {
        private XmppAddress             address;
        private ContactResourcePresence presence;
        private EntityCapabilities      capabilities;
        private string                  avatarHash;
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
        /// Gets or sets the resource capabilities.
        /// </summary>
        /// <value>The capabilities.</value>
        public EntityCapabilities Capabilities
        {
            get { return this.capabilities; }
        }

        /// <summary>
        /// Gets the original avatar image
        /// </summary>
        public System.IO.Stream Avatar
        {
            get { return this.avatar; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is online
        /// </summary>
        public bool IsOnline
        {
            get { return this.presence.ShowAs == ShowType.Online; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is away
        /// </summary>
        public bool IsAway
        {
            get { return this.presence.ShowAs == ShowType.Away; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is busy
        /// </summary>
        public bool IsBusy
        {
            get { return this.presence.ShowAs == ShowType.Busy; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is extended away
        /// </summary>
        public bool IsExtendedAway
        {
            get { return this.presence.ShowAs == ShowType.ExtendedAway; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is offline
        /// </summary>
        public bool IsOffline
        {
            get { return this.presence.ShowAs == ShowType.Offline; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactResource"/> class.
        /// </summary>
        internal ContactResource(XmppAddress address, Presence initialPresence)
        {
            var node = ((initialPresence.Capabilities == null) ? null : initialPresence.Capabilities.DiscoveryNode);

            this.address  = address;
            this.presence = new ContactResourcePresence(this);

            if (node != null)
            {
                this.capabilities = new EntityCapabilities(this.Address, node);
            }

            this.presence.Update(initialPresence);
        }

        public override string ToString()
        {
            return this.address;
        }

        internal async Task UpdateAsync(Presence presence)
        {
            this.presence.Update(presence);

#warning TODO: Implement Avatar Storage
            //if (this.Presence.ShowAs == ShowType.Offline)
            //{
            //    string cachedHash = this.Client.AvatarStorage.GetAvatarHash(this.address.BareAddress);

            //    // Grab stored images for offline users
            //    if (!String.IsNullOrEmpty(cachedHash))
            //    {
            //        // Dipose Avatar Streams
            //        this.DisposeAvatarStream();

            //        // Update the avatar hash and file Paths
            //        this.avatarHash = cachedHash;
            //        this.avatar     = this.Client.AvatarStorage.ReadAvatar(this.address.BareAddress);
            //    }
            //}

            //if (presence.VCardAvatar != null)
            //{
            //    await this.UpdateVCardAvatarAsync(presence.VCardAvatar).ConfigureAwait(false);
            //}
        }

        private async Task UpdateVCardAvatarAsync(VCardAvatar vcard)
        {
#warning TODO: Implement Avatar Storage
            //if (String.IsNullOrEmpty(vcard.Photo) && vcard.Photo.Length == 0)
            //{
            //    return;
            //}

            //// Check if we have the avatar cached
            //string cachedHash = this.Client.AvatarStorage.GetAvatarHash(this.address.BareAddress);

            //if (cachedHash == vcard.Photo)
            //{
            //    // Dispose Avatar Streams
            //    this.DisposeAvatarStream();

            //    // Update the avatar hash and file Paths
            //    this.avatarHash = vcard.Photo;
            //    this.avatar     = this.Client.AvatarStorage.ReadAvatar(this.address.BareAddress);
            //}
            //else
            //{
            //    // Update the avatar hash
            //    this.avatarHash = vcard.Photo;

            //    // Avatar is not cached request the new avatar information
            //    await this.RequestAvatarAsync().ConfigureAwait(false);
            //}
        }

        private async Task RequestAvatarAsync()
        {
            var transport = XmppTransportManager.GetTransport();
            var iq        = new InfoQuery
            {
                From      = transport.UserAddress
              , To        = this.Address
              , Type      = InfoQueryType.Get
              , VCardData = new VCardData()
            };

            await transport.SendAsync(iq, r => this.OnAvatarResponse(r), e => this.OnError(e))
                           .ConfigureAwait(false);
        }

        private void OnAvatarResponse(InfoQuery response)
        {
            this.OnVCardMessage(response.VCardData);
        }

        private void OnError(InfoQuery error)
        {

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

        private void DisposeAvatarStream()
        {
            if (this.avatar != null)
            {
                this.avatar.Dispose();
                this.avatar = null;
            }
        }
    }
}
