// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public enum ChatMessageChangeType : int
    {
        MessageCreated     = 0	
      , MessageModified    = 1	
      , MessageDeleted     = 2	
      , ChangeTrackingLost = 3	
    }
}
