using Conversa.Net.Xmpp.Client;

namespace Conversa.Net.Xmpp.Tests
{
    internal class ConnectionStringHelper
    {
        internal static XmppConnectionString GetDefaultConnectionString()
        {
            var csb = new XmppConnectionStringBuilder
            {
                HostName    = "jabber.at"
              , ServiceName = "5222"
              , UserId      = "conversa@jabber.at"
              , Password    = "6431238Kla"
              , Resource    = "test"
            };

            return csb.ToConnectionString();
        }
    }
}
