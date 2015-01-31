// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Reactive.Disposables;

namespace Conversa.Net.Xmpp.Client
{
    public abstract class Hub
    {
        private ConcurrentDictionary<string, CompositeDisposable> subscriptions;

        protected Hub()
        {
            this.subscriptions = new ConcurrentDictionary<string, CompositeDisposable>();
        }

        protected void AddSubscription(IDisposable onMessage)
        {
            this.AddSubscription(Guid.NewGuid().ToString(), onMessage);
        }

        protected void AddSubscription(string messageId, IDisposable onMessage, IDisposable onError = null)
        {
            var subscription = new CompositeDisposable();

            subscription.Add(onMessage);

            if (onError != null)
            {
                subscription.Add(onError);
            }

            this.subscriptions.TryAdd(messageId, subscription);
        }

        protected void DisposeSubscriptions()
        {
            if (this.subscriptions != null && subscriptions.Count > 0)
            {
                foreach (var pair in this.subscriptions)
                {
                    pair.Value.Dispose();
                }

                this.subscriptions.Clear();
            }
        }

        protected void DisposeSubscription(string messageId)
        {
            if (this.subscriptions.ContainsKey(messageId))
            {
                CompositeDisposable subscription = null;
                this.subscriptions.TryRemove(messageId, out subscription);

                if (subscription != null)
                {
                    subscription.Dispose();
                }
            }
        }
    }
}
