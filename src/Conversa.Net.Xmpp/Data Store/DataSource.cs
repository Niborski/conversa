using Conversa.Net.Xmpp.InstantMessaging;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.DataStore
{
    internal static class DataSource
    {
        private static readonly string                         StorePath  = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Data Store\\Conversa.db");
        private static readonly SQLiteConnectionString         ConnString = new SQLiteConnectionString(StorePath, false);
        private static readonly SQLitePlatformWinRT            Platform   = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
        private static readonly Func<SQLiteConnectionWithLock> Factory    = new Func<SQLiteConnectionWithLock> (() => new SQLiteConnectionWithLock(Platform, ConnString));

        static SemaphoreSlim Semaphore = new SemaphoreSlim(1);

        public static void CreateTable<T>()
            where T : class
        {
            using (var db = new SQLiteConnection(Platform, StorePath))
            {
                // Create the tables if they don't exist
                db.CreateTable<T>(SQLite.Net.Interop.CreateFlags.AllImplicit);
            }
        }

        internal static async Task<int> DeleteMessageAsync(string localMessageId)
        {
            var chatMessage = await GetMessageAsync(localMessageId).ConfigureAwait(false);
            int result      = 0;

            if (chatMessage != null)
            {
                result = await DeleteAsync<ChatMessage>(chatMessage).ConfigureAwait(false);
            }

            return result;
        }

        internal static async Task<ChatConversation> GetConversationAsync(string conversationId)
        {
            return await FirstOrDefaultAsync<ChatConversation>(c => c.Id == conversationId);
        }

        internal static async Task<ChatConversation> GetConversationFromThreadingInfoAsync(ChatConversationThreadingInfo threadingInfo)
        {
            throw new NotImplementedException();
        }

        internal static async Task<ChatMessage> GetMessageAsync(string localChatMessageId)
        {
            return await FirstOrDefaultAsync<ChatMessage>(m => m.Id == localChatMessageId).ConfigureAwait(false);
        }

        internal static async Task<ChatMessage> GetMessageByRemoteIdAsync(string remoteId)
        {
            return await FirstOrDefaultAsync<ChatMessage>(m => m.RemoteId == remoteId).ConfigureAwait(false);
        }

        internal static async Task<int> GetUnseenCountAsync()
        {
            return await GetCountAsync<ChatMessage>(m => !m.IsSeen).ConfigureAwait(false);
        }

        internal static async Task MarkMessagesAsSeenAsync()
        {
            var store = new SQLiteAsyncConnection(Factory);
            var items = await store.Table<ChatMessage>().Where(m => !m.IsSeen).ToListAsync().ConfigureAwait(false);

            items.ForEach(m => m.IsSeen = true);
            
            await store.UpdateAllAsync(items).ConfigureAwait(false);
        }

        internal static async Task<List<ChatConversation>> ReadConversationBatchAsync(int count)
        {
            return await ReadBatchAsync<ChatConversation>(count).ConfigureAwait(false);
        }

        internal static async Task<List<ChatMessage>> ReadMessageBatchAsync(int count)
        {
            return await ReadBatchAsync<ChatMessage>(count).ConfigureAwait(false);
        }


        internal static async Task SaveMessageAsync(ChatMessage chatMessage)
        {
            await AddOrUpdateAsync<ChatMessage>(chatMessage).ConfigureAwait(false);
        }

        internal static async Task MarkMessageReadAsync(string localChatMessageId)
        {
            var chatMessage = await GetMessageAsync(localChatMessageId).ConfigureAwait(false);

            if (chatMessage != null)
            {
                chatMessage.IsRead = true;

                await AddOrUpdateAsync(chatMessage).ConfigureAwait(false);
            }
        }

        internal static async Task<int> GetCountAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            var store = new SQLiteAsyncConnection(Factory);

            return await store.Table<T>().Where(predicate).CountAsync().ConfigureAwait(false);
        }

        private static async Task<int> DeleteAsync<T>(T item)
            where T: class
        {
            await Semaphore.WaitAsync();

            var store = new SQLiteAsyncConnection(Factory);
            int count = await store.DeleteAsync<T>(item).ConfigureAwait(false);

            Semaphore.Release();

            return count;
        }

        private static async Task AddOrUpdateAsync<T>(T item)
            where T : class
        {
            await Semaphore.WaitAsync();

            var store = new SQLiteAsyncConnection(Factory);

            await store.InsertOrReplaceWithChildrenAsync(item, recursive: true).ConfigureAwait(false);

            Semaphore.Release();
        }

        private static async Task<List<T>> ReadBatchAsync<T>(int count)
            where T: class
        {
            var store = new SQLiteAsyncConnection(Factory);

            return await store.GetAllWithChildrenAsync<T>().ConfigureAwait(false);
        }

        private static async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate
                                                          , bool                      recursive = false)
            where T: class
        {
            var store = new SQLiteAsyncConnection(Factory);
            var items = await store.GetAllWithChildrenAsync(predicate, recursive).ConfigureAwait(false);

            return items.FirstOrDefault();
        }
    }
}
