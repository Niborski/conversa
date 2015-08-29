// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public enum ChatMessageValidationStatus : int
    {
        Valid                   = 0
      , NoRecipients            = 1        
      , InvalidData             = 2
      , MessageTooLarge         = 3	
      , TooManyRecipients       = 4	
      , TransportInactive       = 5	
      , TransportNotFound       = 6	
      , TooManyAttachments      = 7	
      , InvalidRecipients       = 8	
      , InvalidBody             = 9	
      , InvalidOther            = 10	
      , ValidWithLargeMessage   = 11
    }
}
