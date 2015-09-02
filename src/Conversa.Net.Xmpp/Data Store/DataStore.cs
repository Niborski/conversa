using Conversa.Net.Xmpp.InstantMessaging;

namespace Conversa.Net.Xmpp.DataStore
{
    public static class DataStore
    {
        public static void Create(bool recreateTables = false)
        {
            DataSource.CreateTable<ChatMessage>(recreateTables);
            DataSource.CreateTable<ChatMessageAttachment>(recreateTables);
            DataSource.CreateTable<ChatConversationThreadingInfo>(recreateTables);
            DataSource.CreateTable<ChatRecipientDeliveryInfo>(recreateTables);
            DataSource.CreateTable<ChatConversation>(recreateTables);
        }
    }
}
