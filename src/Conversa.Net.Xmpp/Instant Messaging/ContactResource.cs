// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Capabilities;
using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Registry;
using DevExpress.Mvvm;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Represents a contact resource
    /// </summary>
    public sealed class ContactResource
        : BindableBase
    {
        private XmppAddress        address;
        private EntityCapabilities capabilities;
        private string             avatarHash;

        /// <summary>
        /// Gets or sets the resource address
        /// </summary>
        /// <value>The resource id.</value>
        public XmppAddress Address
        {
            get { return this.address; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is online
        /// </summary>
        public bool IsOnline
        {
            get { return this.Presence.ShowAs == ShowType.Online; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is away
        /// </summary>
        public bool IsAway
        {
            get { return this.Presence.ShowAs == ShowType.Away; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is busy
        /// </summary>
        public bool IsBusy
        {
            get { return this.Presence.ShowAs == ShowType.Busy; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is extended away
        /// </summary>
        public bool IsExtendedAway
        {
            get { return this.Presence.ShowAs == ShowType.ExtendedAway; }
        }

        /// <summary>
        /// Gets a value indicating whether the presence status is offline
        /// </summary>
        public bool IsOffline
        {
            get { return this.Presence.ShowAs == ShowType.Offline; }
        }

        /// <summary>
        /// Gets a value indicating whether entity capabilities are supported
        /// </summary>
        public bool SupportsEntityCapabilities
        {
            get { return (this.capabilities != null); }
        }

        /// <summary>
        /// Gets a value that indicates whether user tunes are supported
        /// </summary>
        public bool SupportsUserTune
        {
            get { return this.capabilities.SupportsFeature(XmppFeatures.UserTune); }
        }

        /// <summary>
        /// Gets a value that indicates whether user moods are supported
        /// </summary>
        public bool SupportsUserMood
        {
            get { return this.capabilities.SupportsFeature(XmppFeatures.UserMood); }
        }

        /// <summary>
        /// Gets a value that indicates whether simple communications blocking is supported
        /// </summary>
        public bool SupportsBlocking
        {
            get { return this.capabilities.SupportsFeature(XmppFeatures.Blocking); }
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports MUC
        /// </summary>
        public bool SupportsConference
        {
            get { return this.capabilities.SupportsFeature(XmppFeatures.MultiUserChat); }
        }

        /// <summary>
        /// Gets a value that indicates if the contact supports chat state notifications
        /// </summary>
        public bool SupportsChatStateNotifications
        {
            get { return this.capabilities.SupportsFeature(XmppFeatures.ChatStateNotifications); }
        }

        /// <summary>
        /// Gets a value indicating whether last activity queries are supported
        /// </summary>
        public bool SupportsLastActivity
        {
            get { return this.capabilities.SupportsFeature(XmppFeatures.LastActivity); }
        }

        /// <summary>
        /// Gets the resource presence.
        /// </summary>
        public ContactResourcePresence Presence
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the last activity time (UTC)
        /// </summary>
        public ulong? LastActivity
        {
            get { return GetProperty(() => LastActivity); }
            private set {  SetProperty(() => LastActivity, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactResource"/> class.
        /// </summary>
        internal ContactResource(XmppAddress address, Presence initialPresence, bool isDefaultResource = false)
        {
            this.address  = address;
            this.Presence = new ContactResourcePresence(this);

            if (!isDefaultResource && initialPresence.Capabilities != null)
            {
                this.capabilities = new EntityCapabilities(this.Address, initialPresence.Capabilities.DiscoveryNode);

                this.capabilities
                    .CapsChangedStream
                    .Take(1)
                    .Subscribe(async caps => await OnCapabilitiesChangedAsync().ConfigureAwait(false));
            }

            this.UpdatePresence(initialPresence);
        }

        public override string ToString()
        {
            return this.address;
        }

        public async Task DiscoverCapabilitiesAsync()
        {
            await this.capabilities.DiscoverAsync().ConfigureAwait(false);
        }

        internal void Update(Presence presence)
        {
            this.UpdatePresence(presence);

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

        private async Task OnCapabilitiesChangedAsync()
        {
            this.RaisePropertiesChanged<bool,bool,bool,bool,bool,bool>(
                () => SupportsUserTune
              , () => SupportsBlocking
              , () => SupportsConference
              , () => SupportsChatStateNotifications
              , () => SupportsLastActivity
              , () => SupportsUserMood);

            if (this.SupportsLastActivity)
            {
                var transport = XmppTransportManager.GetTransport();
                var iq        = new InfoQuery
                {
                    From         = transport.UserAddress
                  , To           = this.Address
                  , Type         = InfoQueryType.Get
                  , LastActivity = new Xmpp.LastActivity.LastActivity()
                };

                await transport.SendAsync(iq, response => OnLastActivityResponse(response)).ConfigureAwait(false);
            }
        }

        private void OnLastActivityResponse(InfoQuery response)
        {
            if (response.LastActivity != null && response.LastActivity.SecondsSpecified)
            {
                this.LastActivity = response.LastActivity.Seconds;
            }
        }

        private void UpdateVCardAvatarAsync(VCardAvatar vcard)
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

        private void UpdatePresence(Presence presence)
        {
            this.Presence.Update(presence);
            this.RaisePropertyChanged(() => Presence);

            if (presence.LastActivity != null && presence.LastActivity.SecondsSpecified)
            {
                this.LastActivity = presence.LastActivity.Seconds;
            }
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
    }
}
