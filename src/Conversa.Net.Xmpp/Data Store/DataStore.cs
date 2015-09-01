using Conversa.Net.Xmpp.InstantMessaging;

namespace Conversa.Net.Xmpp.DataStore
{
    public static class DataStore
    {
        public static void Create()
        {
            DataSource.CreateTable<ChatMessage>();
            DataSource.CreateTable<ChatMessageAttachment>();
            DataSource.CreateTable<ChatConversationThreadingInfo>();
            DataSource.CreateTable<ChatRecipientDeliveryInfo>();
            DataSource.CreateTable<ChatConversation>();
        }
    }
}
