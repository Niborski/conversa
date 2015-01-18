// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Presence Stanza
    /// </summary>
    /// <remarks>
    /// RFC 6121:  Instant Messaging and Presence
    /// </remarks>
    public partial class Presence
    {
        public static Presence Create()
        {
            return new Presence { Id = IdentifierGenerator.Generate() };
        }

        public Presence FromAddress(XmppAddress address)
        {
            this.From = address;

            return this;
        }

        public Presence ToAddress(XmppAddress address)
        {
            this.To = address;

            return this;
        }

        public Presence AsSubscribe()
        {
            return this.SetType(PresenceType.Subscribe);
        }

        public Presence AsSubscribed()
        {
            return this.SetType(PresenceType.Subscribed);
        }

        public Presence AsUnavailable()
        {
            return this.SetType(PresenceType.Unavailable)
                       .ShowAs(ShowType.Offline);
        }

        public Presence AsUnsubscribe()
        {
            return this.SetType(PresenceType.Unsubscribe);
        }

        public Presence AsUnsubscribed()
        {
            return this.SetType(PresenceType.Unsubscribed);
        }

        public Presence AsProbe()
        {
            return this.SetType(PresenceType.Probe);
        }

        public Presence ShowAs(ShowType showAs)
        {
            this.Show = showAs;

            return this;
        }

        public Presence WithStatus(string statusMessage)
        {
            this.Status = new Status { Value = statusMessage };

            return this;
        }

        public Presence WithPriority(int priority)
        {
            this.Priority = (sbyte)priority;

            return this;
        }

        private Presence SetType(PresenceType type)
        {
            this.Type          = type;
            this.TypeSpecified = true;

            return this;
        }
    }
}
