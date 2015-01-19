// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Client
{
    public abstract class XmppMessageProcessor
    {
        protected XmppClient Client
        {
            get;
            private set;
        }

        private CompositeDisposable                       subscriptions;
        private ConcurrentDictionary<string, IDisposable> respSubscriptions;

        protected XmppMessageProcessor(XmppClient client)
        {
            this.Client            = client;
            this.subscriptions     = new CompositeDisposable();
            this.respSubscriptions = new ConcurrentDictionary<string, IDisposable>();

            this.ClientStateSubscriptions();
        }


        protected virtual void ClientStateSubscriptions()
        {
            this.AddSubscription(this.Client
                                     .StateChanged
                                     .Where(state => state == XmppClientState.Open)
                                     .Subscribe(state => this.OnClientConnected()));

            this.AddSubscription(this.Client
                                     .StateChanged
                                     .Where(state => state == XmppClientState.Closed)
                                     .Subscribe(state => this.OnClientDisconnected()));
        }

        protected async virtual Task SendAsync(InfoQuery infoQuery)
        {
            this.AddSubscription(infoQuery.Id
                               , this.Client
                                     .InfoQueryStream
                                     .Where(message => message.Id   == infoQuery.Id
                                                    && message.Type != InfoQueryType.Error)
                                     .Subscribe(message => this.OnResponseMessage(message)));

            this.AddSubscription(infoQuery.Id
                               , this.Client
                                     .InfoQueryStream
                                     .Where(message => message.Id   == infoQuery.Id
                                                    && message.Type == InfoQueryType.Error)
                                     .Subscribe(message => this.OnErrorMessage(message)));

            await this.Client.SendAsync(infoQuery).ConfigureAwait(false);
        }

        protected async virtual Task SendAsync(Presence presence)
        {
            this.AddSubscription(presence.Id
                               , this.Client
                                     .PresenceStream
                                     .Where(message => message.Id   == presence.Id
                                                    && message.Type != PresenceType.Error)
                                     .Subscribe(messsage => this.OnResponseMessage(messsage)));

            this.AddSubscription(presence.Id
                               , this.Client
                                     .PresenceStream
                                     .Where(message => message.Id   == presence.Id
                                                    && message.Type == PresenceType.Error)
                                     .Subscribe(messsage => this.OnErrorMessage(messsage)));

            await this.Client.SendAsync(presence).ConfigureAwait(false);
        }

        protected virtual void OnClientConnected()
        {
        }

        protected virtual void OnClientDisconnected()
        {
            if (this.respSubscriptions != null && respSubscriptions.Count > 0)
            {
                foreach (var pair in this.respSubscriptions)
                {
                    pair.Value.Dispose();
                }

                this.respSubscriptions.Clear();
            }
            if (this.subscriptions != null)
            {
                this.subscriptions.Dispose();
                this.subscriptions = null;
            }
        }

        protected virtual void OnResponseMessage(InfoQuery response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnErrorMessage(InfoQuery response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnResponseMessage(Presence response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected virtual void OnErrorMessage(Presence response)
        {
            this.DisposeSubscription(response.Id);
        }

        protected void AddSubscription(IDisposable subscription)
        {
            this.subscriptions.Add(subscription);
        }

        protected void AddSubscription(string messageId, IDisposable subscription)
        {
            this.respSubscriptions.TryAdd(messageId, subscription);
        }

        protected void DisposeSubscription(string messageId)
        {
            if (this.respSubscriptions.ContainsKey(messageId))
            {
                IDisposable subscription = null;
                this.respSubscriptions.TryRemove(messageId, out subscription);

                if (subscription != null)
                {
                    subscription.Dispose();
                }
            }
        }
    }
}
