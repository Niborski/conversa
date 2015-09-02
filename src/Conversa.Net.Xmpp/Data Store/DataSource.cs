using Conversa.Net.Xmpp.InstantMessaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Velox.DB;
using Velox.DB.Sqlite;

namespace Conversa.Net.Xmpp.DataStore
{
    internal static class DataSource
    {
        private static readonly string StorePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Data Store\\Conversa.db");
        private static SemaphoreSlim Semaphore   = new SemaphoreSlim(1);

        public static void CreateTable<T>(bool recreateTable = false)
            where T : class
        {
            using (var provider = new SqliteDataProvider(StorePath))
            {
                using (var context = new Vx.Context(provider))
                {
                    context.CreateTable<T>(recreateTable: recreateTable);
                }
            }
        }

        internal static async Task<bool> DeleteMessageAsync(string localMessageId)
        {
            var  chatMessage = await GetMessageAsync(localMessageId).ConfigureAwait(false);
            bool result      = false;

            if (chatMessage != null)
            {
                result = await DeleteAsync<ChatMessage>(chatMessage).ConfigureAwait(false);
            }

            return result;
        }

        internal static async Task<long> GetCountAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return await Task.Run<long>(() =>
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        return context.DataSet<T>().Where(predicate).Count();
                    }
                }
            }).ConfigureAwait(false);
        }

        internal static async Task<ChatConversation> GetConversationAsync(string conversationId)
        {
            return await FirstOrDefaultAsync<ChatConversation>(c => c.Id == conversationId);
        }

        internal static async Task<ChatConversation> GetConversationFromThreadingInfoAsync(ChatConversationThreadingInfo threadingInfo)
        {
            return await Task.Run<ChatConversation>(() => 
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        var q = from conversation in context.DataSet<ChatConversation>()
                                where conversation.Id == threadingInfo.Id
                                select conversation;

                        return q.FirstOrDefault();
                    }
                }
            }).ConfigureAwait(false);
        }

        internal static async Task<ChatMessage> GetMessageAsync(string localChatMessageId)
        {
            return await FirstOrDefaultAsync<ChatMessage>(m => m.Id == localChatMessageId).ConfigureAwait(false);
        }

        internal static async Task<ChatMessage> GetMessageByRemoteIdAsync(string remoteId)
        {
            return await FirstOrDefaultAsync<ChatMessage>(m => m.RemoteId == remoteId).ConfigureAwait(false);
        }

        internal static async Task<long> GetUnseenCountAsync()
        {
            return await GetCountAsync<ChatMessage>(m => !m.IsSeen).ConfigureAwait(false);
        }

        internal static async Task MarkMessagesAsSeenAsync()
        {
            await Task.Run(() => 
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        var q = from message in context.DataSet<ChatMessage>()
                                where !message.IsSeen
                                select message;

                        foreach (var message in q)
                        {
                            message.IsSeen = true;
                            context.Update(message);
                        }
                    }
                }
            }).ConfigureAwait(false);
        }

        internal static async Task MarkMessageReadAsync(string localChatMessageId)
        {
            var chatMessage = await GetMessageAsync(localChatMessageId).ConfigureAwait(false);

            if (chatMessage != null)
            {
                chatMessage.IsRead = true;

                await UpdateAsync(chatMessage).ConfigureAwait(false);
            }
        }

        internal static async Task<List<ChatConversation>> ReadConversationBatchAsync(int count)
        {
            return await ReadBatchAsync<ChatConversation>(count, c => c.ThreadingInfo).ConfigureAwait(false);
        }

        internal static async Task<List<ChatMessage>> ReadMessageBatchAsync(int count)
        {
            return await ReadBatchAsync<ChatMessage>(count
                                                   , m => m.Attachments
                                                   , m => m.RecipientsDeliveryInfos
                                                   , m => m.ThreadingInfo
                                                   ).ConfigureAwait(false);
        }

        internal static async Task SaveConversationAsync(ChatConversation chatConversation)
        {
            var count = await GetCountAsync<ChatConversation>(m => m.Id == chatConversation.Id).ConfigureAwait(false);

            if (count == 0)
            {
                await AddAsync<ChatConversation>(chatConversation).ConfigureAwait(false);
            }            
            else
            {
                await UpdateAsync<ChatConversation>(chatConversation).ConfigureAwait(false);
            }
        }

        internal static async Task SaveMessageAsync(ChatMessage chatMessage)
        {
            var count = await GetCountAsync<ChatMessage>(m => m.Id == chatMessage.Id).ConfigureAwait(false);

            if (count == 0)
            {
                await AddAsync<ChatMessage>(chatMessage).ConfigureAwait(false);
            }            
            else
            {
                await UpdateAsync<ChatMessage>(chatMessage).ConfigureAwait(false);
            }
        }

        internal static async Task<List<ChatMessage>> SearchMessagesAsync(Expression<Func<ChatMessage, bool>> searchCriteria, int take, int skip)
        {
            return await SearchAsync<ChatMessage>(searchCriteria, take, skip).ConfigureAwait(false);
        }

        private static async Task<bool> DeleteAsync<T>(T item)
            where T: class
        {
            await Semaphore.WaitAsync();

            bool deleted = await Task.Run<bool>(() =>
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        return context.Delete<T>(item);
                    }
                }
            }).ConfigureAwait(false);

            Semaphore.Release();

            return deleted;
        }

        private static async Task AddAsync<T>(T item)
            where T : class
        {
            await Semaphore.WaitAsync();

            await Task.Run(() =>
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        context.Insert<T>(item, true);
                    }
                }
            }).ConfigureAwait(false);

            Semaphore.Release();
        }

        private static async Task UpdateAsync<T>(T item)
            where T : class
        {
            await Semaphore.WaitAsync();

            await Task.Run(() =>
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        context.Update<T>(item, true);
                    }
                }
            }).ConfigureAwait(false);

            Semaphore.Release();
        }

        private static async Task<List<T>> ReadBatchAsync<T>(int count, params Expression<Func<T, object>>[] relationsToLoad)
            where T: class
        {
            return await Task.Run<List<T>>(() =>
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        return context.DataSet<T>().WithRelations(relationsToLoad).Take(count).ToList();
                    }
                }
            }).ConfigureAwait(false);
        }

        private static async Task<List<T>> SearchAsync<T>(Expression<Func<T, bool>> searchCriteria, int take, int skip)
            where T: class
        {
            return await Task.Run<List<T>>(() =>
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        return context.DataSet<T>().Take(take).Skip(skip).ToList();
                    }
                }
            }).ConfigureAwait(false);
        }

        private static async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate
                                                          , bool                      recursive = false)
            where T: class
        {
            return await Task.Run<T>(() =>
            {
                using (var provider = new SqliteDataProvider(StorePath))
                {
                    using (var context = new Vx.Context(provider))
                    {
                        return context.DataSet<T>().FirstOrDefault(predicate);
                    }
                }
            }).ConfigureAwait(false);
        }
    }
}
