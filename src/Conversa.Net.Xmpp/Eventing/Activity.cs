// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive.Linq;

namespace Conversa.Net.Xmpp.Eventing
{
    /// <summary>
    /// XMPP Activity
    /// </summary>
    public sealed class Activity
        : IEnumerable<Event>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private XmppTransport                  client;
        private ObservableCollection<Event>	activities;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppSession"/> class
        /// </summary>
        internal Activity(XmppTransport client)
        {
            this.client     = client;
            this.activities	= new ObservableCollection<Event>();

            this.client
                .StateChanged
                .Where(state => state == XmppTransportState.Open)
                .Subscribe(state => OnConnected());

            this.client
                .StateChanged
                .Where(state => state == XmppTransportState.Closing)
                .Subscribe(state => OnDisconnecting());
        }

        /// <summary>
        /// Clears the activity list
        /// </summary>
        public void Clear()
        {
        	this.activities.Clear();

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        IEnumerator<Event> IEnumerable<Event>.GetEnumerator()
        {
            return this.activities.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return this.activities.GetEnumerator();
        }

        private void OnConnected()
        {
            this.client
                .MessageStream
                .Where(m => m.Type == MessageType.Headline || m.Type == MessageType.Normal)
                .Subscribe(message => { this.OnMessageReceived(message); });

            this.activities.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);
        }

        private void OnDisconnecting()
        {
            this.activities.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnCollectionChanged);
        }

        private void OnMessageReceived(Message message)
        {
            switch (message.Type)
            {
                case MessageType.Normal:
                    this.activities.Add(new MessageEvent(message));
                    break;

                case MessageType.Headline:
                    this.activities.Add(new MessageEvent(message));
                    break;
            }
        }

        private void OnEventMessage(Message message)
        {
#warning TODO: Reimplement
//            // Add the activity based on the source event
//            if (XmppEvent.IsActivityEvent(message.Event))
//            {
//                XmppEvent xmppevent = XmppEvent.Create(this.session.Roster[message.From.BareIdentifier], message.Event);

//#warning TODO: see what to do when it is null
//                if (xmppevent != null)
//                {
//#warning TODO: This needs to be changed
//                    if (xmppevent is XmppUserTuneEvent && ((XmppUserTuneEvent)xmppevent).IsEmpty)
//                    {
//                        // And empty tune means no info available or that the user
//                        // cancelled the tune notifications ??
//                    }
//                    else
//                    {
//                        this.activities.Add(xmppevent);
//                    }
//                }
//            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, e);
            }
        }
    }
}
