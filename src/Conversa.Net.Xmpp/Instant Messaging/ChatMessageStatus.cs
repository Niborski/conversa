// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.InstantMessaging
{
    public enum ChatMessageStatus : int
    {
        Draft                 = 0
      , Sending               = 1
      , Sent                  = 2	
      , SendRetryNeeded       = 3	
      , SendFailed            = 4	
      , Received              = 5	
      , ReceiveDownloadNeeded = 6	
      , ReceiveDownloadFailed = 7	
      , ReceiveDownloading    = 8	
      , Deleted               = 9
      , Declined              = 10	
      , Cancelled             = 11
      , Recalled              = 12
      , ReceiveRetryNeeded    = 13	
    }
}
