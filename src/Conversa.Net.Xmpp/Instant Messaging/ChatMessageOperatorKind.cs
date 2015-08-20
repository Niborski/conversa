namespace Conversa.Net.Xmpp.InstantMessaging
{
    /// <summary>
    /// Specifies the message operator type.
    /// </summary>
    public enum ChatMessageOperatorKind : int
    {
        /// <summary>
        /// The value hasn't been set.
        /// </summary>
        Unspecified = 0	
        /// <summary>
        /// SMS message
        /// </summary>
      , Sms = 1	
        /// <summary>
        /// MMS message
        /// </summary>
      , Mms = 2	
        /// <summary>
        /// RCS message
        /// </summary>
      , Rcs = 3	        
    }
}
