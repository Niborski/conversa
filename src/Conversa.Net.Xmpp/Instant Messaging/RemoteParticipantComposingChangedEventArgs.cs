// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using Windows.Foundation.Metadata;

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Provides data to the RemoteParticipantComposingChanged event.
    /// </summary>
    [MarshalingBehavior(MarshalingType.Agile)]
    [Threading(ThreadingModel.Both)]
    public sealed class RemoteParticipantComposingChangedEventData
        : EventArgs
    {
        /// <summary>
        /// Gets a Boolean value indicating if the remote participant is currently composing a message.
        /// </summary>
        public bool IsComposing
        {
            get;
        }

        /// <summary>
        /// Gets the address of the remote chat participant.
        /// </summary>
        public XmppAddress ParticipantAddress
        {
            get;
        }

        /// <summary>
        /// Gets the ID for the message transport.
        /// </summary>
        public string TransportId
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteParticipantComposingChangedEventData"/> class.
        /// </summary>
        public RemoteParticipantComposingChangedEventData(XmppAddress participantAddress
                                                        , bool        isComposing
                                                        , string      transportId = null)
        {
            this.IsComposing        = isComposing;
            this.ParticipantAddress = participantAddress;
            this.TransportId        = transportId;
        }
    }
}
