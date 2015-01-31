// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Client
{
    public abstract class StanzaHub
        : ClientHub
    {
        protected StanzaHub(XmppClient client)
            : base(client)
        {
        }

        protected async virtual Task SendAsync(InfoQuery infoQuery)
        {
            var onMessage = this.Client
                                .InfoQueryStream
                                .Where(message => message.Id   == infoQuery.Id
                                               && message.Type != InfoQueryType.Error)
                                .Subscribe(message => this.OnStanza(message));

            var onError  = this.Client
                               .InfoQueryStream
                               .Where(message => message.Id   == infoQuery.Id
                                              && message.Type == InfoQueryType.Error)
                               .Subscribe(message => this.OnError(message));

            await this.SendAsync(infoQuery, onMessage, onError).ConfigureAwait(false);
        }

        protected async virtual Task SendAsync(InfoQuery infoQuery, IDisposable onMessage, IDisposable onError)
        {
            this.AddSubscription(infoQuery.Id, onMessage, onError);

            await this.Client.SendAsync(infoQuery).ConfigureAwait(false);
        }

        protected async virtual Task SendAsync(Presence presence)
        {
            var onMessage = this.Client
                                .PresenceStream
                                .Where(message => message.Id    == presence.Id
                                               && (message.Type != PresenceType.Error
                                                || !message.TypeSpecified))
                                .Subscribe(messsage => this.OnStanza(messsage));

            var onError  = this.Client
                               .PresenceStream
                               .Where(message => message.Id   == presence.Id
                                              && message.Type == PresenceType.Error
                                              && message.TypeSpecified)
                               .Subscribe(messsage => this.OnError(messsage));

            await this.SendAsync(presence, onMessage, onError).ConfigureAwait(false);
        }

        protected async virtual Task SendAsync(Presence presence, IDisposable onMessage, IDisposable onError)
        {
            this.AddSubscription(presence.Id, onMessage, onError);

            await this.Client.SendAsync(presence).ConfigureAwait(false);
        }

        protected virtual void OnStanza(InfoQuery response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnError(InfoQuery response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnStanza(Presence response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnError(Presence response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnStanza(Message response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnError(Message response)
        {
            this.DisposeSubscription(response.Id);
        }
    }
}
