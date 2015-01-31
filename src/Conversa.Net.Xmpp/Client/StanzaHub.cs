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

        protected async virtual Task SendAsync(object request)
        {
            await this.Client.SendAsync(request);
        }

        protected async virtual Task SendAsync(InfoQuery         request
                                             , Action<InfoQuery> onResponse
                                             , Action<InfoQuery> onError)
        {
            var raction = this.Client
                              .InfoQueryStream
                              .Where(response => response.Id == request.Id && !response.IsError)
                              .Subscribe(onResponse);

            var eaction  = this.Client
                               .InfoQueryStream
                               .Where(response => response.Id == request.Id && response.IsError)
                               .Subscribe(onError);

            var daction = this.Client
                              .InfoQueryStream
                              .Where(response => response.Id == request.Id)
                              .Subscribe(response => this.DisposeSubscription(response.Id));

            await this.SendAsync(request, raction, eaction, daction).ConfigureAwait(false);
        }

        protected async virtual Task SendAsync(Presence         request
                                             , Action<Presence> onResponse
                                             , Action<Presence> onError)
        {
            var raction = this.Client
                              .PresenceStream
                              .Where(response => response.Id == request.Id && !response.IsError)
                              .Subscribe(onResponse);

            var eaction  = this.Client
                               .PresenceStream
                               .Where(message => message.Id == request.Id && message.IsError)
                               .Subscribe(onError);

            var daction = this.Client
                              .PresenceStream
                              .Where(response => response.Id == request.Id)
                              .Subscribe(response => this.DisposeSubscription(response.Id));

            await this.SendAsync(request, raction, eaction, daction).ConfigureAwait(false);
        }

        protected async virtual Task SendAsync(Message         request
                                             , Action<Message> onResponse
                                             , Action<Message> onError = null)
        {
            var raction = this.Client
                              .MessageStream
                              .Where(response => response.Id == request.Id && !response.IsError)
                              .Subscribe(onResponse);

            var eaction  = this.Client
                               .MessageStream
                               .Where(message => message.Id == request.Id && message.IsError)
                               .Subscribe(onError);

            var daction = this.Client
                              .MessageStream
                              .Where(response => response.Id == request.Id)
                              .Subscribe(response => this.DisposeSubscription(response.Id));

            await this.SendAsync(request, raction, eaction, daction).ConfigureAwait(false);
        }

        protected async virtual Task SendAsync<T>(T           request
                                                , IDisposable onResponse
                                                , IDisposable onError
                                                , IDisposable dispose)
            where T: class, IStanza
        {
            this.AddSubscription(request.Id, onResponse, onError, dispose);

            await this.Client.SendAsync(request).ConfigureAwait(false);
        }
    }
}
