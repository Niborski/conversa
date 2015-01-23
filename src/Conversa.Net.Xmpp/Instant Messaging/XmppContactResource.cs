// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Caps;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
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
        private XmppContact            contact;
        private XmppAddress			   address;
        private XmppPresence           presence;
        private XmppEntityCapabilities capabilities;
        private string				   avatarHash;
        private System.IO.Stream       avatar;

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
        }

        /// <summary>
        /// Gets the original avatar image
        /// </summary>
        public System.IO.Stream Avatar
        {
            get { return this.avatar; }
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
            this.capabilities = new XmppEntityCapabilities(client, this.Address);
        }

        public override string ToString()
        {
            return this.address;
        }

        internal async Task UpdateAsync(Presence presence)
        {
            this.Presence.Update(presence);

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
            if (this.contact.Subscription == RosterSubscriptionType.Both
             || this.contact.Subscription == RosterSubscriptionType.To)
            {
                return;
            }

            var iq = new InfoQuery
            {
                From      = this.Client.UserAddress
              , To        = this.Address
              , Type      = InfoQueryType.Get
              , VCardData = new VCardData()
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        private void DisposeAvatarStream()
        {
            if (this.avatar != null)
            {
                this.avatar.Dispose();
                this.avatar = null;
            }
        }

        protected override void OnResponseMessage(InfoQuery response)
        {
            if (response.VCardData != null)
            {
                this.OnVCardMessage(response.VCardData);
            }

            base.OnResponseMessage(response);
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
