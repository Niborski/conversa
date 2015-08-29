using Conversa.Net.Xmpp.InstantMessaging;

namespace Conversa.Net.Xmpp.DataStore
{
    public static class DataStore
    {
        public static void Create()
        {
            DataSource<ChatMessageAttachment>.CreateTable();
            DataSource<ChatConversationThreadingInfo>.CreateTable();         
            DataSource<ChatRecipientDeliveryInfo>.CreateTable();
            DataSource<ChatMessage>.CreateTable();
        }
    }
}
