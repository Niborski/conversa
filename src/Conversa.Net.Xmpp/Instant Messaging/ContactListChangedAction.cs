// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Describes the action that caused a change in the contact list.
    /// </summary>
    public enum ContactListChangedAction
    {        
        /// <summary>
        /// One contact were added to the contact list.
        /// </summary>
        Add,        
        /// <summary>
        /// One contact was removed from the contact list.
        /// </summary>
        Remove,        
        /// <summary>
        /// One contact was updated in the contact list.
        /// </summary>
        Update,
        /// <summary>
        /// The content of the contact list changed dramatically.
        /// </summary>
        Reset
    }
}
