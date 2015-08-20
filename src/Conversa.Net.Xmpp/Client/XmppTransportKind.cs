namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Specifies the type of the message transport.
    /// </summary>
    public enum XmppTransportKind
    {
        /// <summary>
        /// Text message
        /// </summary>
        Text = 0	
        /// <summary>
        /// Untriaged message
        /// </summary>
      , Untriaged =	1	
        /// <summary>
        /// Intercepted by the filtering app and marked as blocked
        /// </summary>
      , Blocked = 2	
        /// <summary>
        /// Custom message
        /// </summary>
      , Custom = 3	        
    }
}
