using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      , ReceiveRetryNeeded    =  13	
    }
}
