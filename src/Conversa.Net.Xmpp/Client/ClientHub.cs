// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Reactive.Linq;

namespace Conversa.Net.Xmpp.Client
{
    public abstract class ClientHub
        : Hub
    {
        protected XmppClient Client
        {
            get;
            private set;
        }

        protected ClientHub(XmppClient client)
            : base()
        {
            this.Client = client;

            this.SubscribeToClientState();
        }

        protected virtual void SubscribeToClientState()
        {
            this.AddSubscription(this.Client
                                     .StateChanged
                                     .Where(state => state == XmppClientState.Open)
                                     .Subscribe(state => this.OnConnected()));

            this.AddSubscription(this.Client
                                     .StateChanged
                                     .Where(state => state == XmppClientState.Closed)
                                     .Subscribe(state => this.OnDisconnected()));
        }

        protected virtual void OnConnected()
        {
        }

        protected virtual void OnDisconnected()
        {
            this.DisposeSubscriptions();
        }
    }
}
