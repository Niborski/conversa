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
    static class DataSource<T>
        where T: class
    {
        static readonly string                         StorePath  = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Data Store\\Conversa.db");
        static readonly SQLiteConnectionString         ConnString = new SQLiteConnectionString(StorePath, false);
        static readonly SQLitePlatformWinRT            Platform   = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
        static readonly Func<SQLiteConnectionWithLock> Factory    = new Func<SQLiteConnectionWithLock> (() => new SQLiteConnectionWithLock(Platform, ConnString));

        static SemaphoreSlim Semaphore = new SemaphoreSlim(1);

        public static void CreateTable()
        {
            using (var db = new SQLiteConnection(Platform, StorePath))
            {
                // Create the tables if they don't exist
                db.CreateTable<T>(SQLite.Net.Interop.CreateFlags.AllImplicit);
            }
        }

        public static async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>   predicate
                                                      , Expression<Func<T, object>> orderExpr = null
                                                      , bool                        recursive = false)
        {
            var store = new SQLiteAsyncConnection(Factory);
            var items = await store.GetAllWithChildrenAsync(predicate, recursive).ConfigureAwait(false);

            return items.FirstOrDefault();
        }

        public static async Task<List<T>> ReadBatchAsync(int                         count
                                                       , Expression<Func<T, bool>>   predicate = null
                                                       , Expression<Func<T, object>> orderExpr = null)
        {
            var store = new SQLiteAsyncConnection(Factory);
            var query = store.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderExpr != null)
            {
                query = query.OrderByDescending(orderExpr);
            }

            var items = await query.Take(count).ToListAsync().ConfigureAwait(false);

            foreach (T item in items)
            {
                await store.GetChildrenAsync(item).ConfigureAwait(false);
            }

            return items;
        }

        public static async Task AddOrUpdateAsync(T item)
        {
            await Semaphore.WaitAsync();

            var store = new SQLiteAsyncConnection(Factory);

            await store.InsertOrReplaceWithChildrenAsync(item, recursive: true).ConfigureAwait(false);

            Semaphore.Release();
        }
    }
}
