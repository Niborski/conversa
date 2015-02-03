// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.InstantMessaging;
using Conversa.Net.Xmpp.PublishSubscribe;

namespace Conversa.Net.Xmpp.Eventing
{
    /// <summary>
    /// Represents an XMPP event
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Return a value that iundicates whether a pubsub event is an activity event
        /// </summary>
        /// <param name="xmppevent"></param>
        /// <returns></returns>
        public static bool IsActivityEvent(PubSubEvent xmppevent)
        {
            var items = xmppevent.Item as PubSubEventItems;

            if (items != null && items.Items.Count == 1)
            {
                var item = items.Items[0] as PubSubItem;

                return (item.Item is Tune || item.Item is Mood);
            }

            return false;
        }

        /// <summary>
        /// Creates an xmpp event with the give user and pubsub event
        /// </summary>
        /// <param name="user"></param>
        /// <param name="xmppevent"></param>
        /// <returns></returns>
        public static Event Create(Contact user, PubSubEvent xmppevent)
        {
            var items = xmppevent.Item as PubSubEventItems;

            if (items != null && items.Items.Count == 1)
            {
                var item = items.Items[0] as PubSubItem;

                if (item.Item is Tune)
                {
                    return new UserTuneEvent(user, (Tune)item.Item);
                }
                else if (item.Item is Mood)
                {
                    return new UserMoodEvent(user, (Mood)item.Item);
                }
            }

            return null;
        }
    }
}
