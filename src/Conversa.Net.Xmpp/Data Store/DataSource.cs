using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.DataStore
{
    public static class DataSource<T>
        where T: class
    {
        static readonly string                         StorePath  = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Data Store\\Conversa.db");
        static readonly SQLiteConnectionString         ConnString = new SQLiteConnectionString(StorePath, false);
        static readonly SQLitePlatformWinRT            Platform   = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
        static readonly Func<SQLiteConnectionWithLock> Factory    = new Func<SQLiteConnectionWithLock> (() => new SQLiteConnectionWithLock(Platform, ConnString));

        public static AsyncTableQuery<T> Query()
        {
            var store = new SQLiteAsyncConnection(Factory);

            return store.Table<T>();
        }

        public static async Task AddOrUpdateAsync(T item)
        {
            var store = new SQLiteAsyncConnection(Factory);

            await store.InsertOrReplaceAsync(item);
        }
    }
}
