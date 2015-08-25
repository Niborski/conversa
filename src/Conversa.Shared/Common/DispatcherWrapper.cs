// Licensed under the The Apache License Version 2.0, January 2004
// ---------------------------------------------------------------
// https://github.com/Windows-XAML/Template10
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Conversa.Common
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-DispatcherWrapper
    public class DispatcherWrapper
    {
        public DispatcherWrapper(CoreDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        private CoreDispatcher dispatcher;

        public async Task DispatchAsync(Action action)
        {
            if (dispatcher.HasThreadAccess) { action(); }
            else
            {
                var tcs = new TaskCompletionSource<object>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    try { action(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                });
                await tcs.Task;
            }
        }

        public async Task DispatchAsync(Func<Task> func)
        {
            if (dispatcher.HasThreadAccess) { await func?.Invoke(); }
            else
            {
                var tcs = new TaskCompletionSource<object>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try { await func(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                });
                await tcs.Task;
            }
        }

        public async Task<T> DispatchAsync<T>(Func<T> func)
        {
            if (dispatcher.HasThreadAccess) { return func(); }
            else
            {
                var tcs = new TaskCompletionSource<T>();
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    try { tcs.TrySetResult(func()); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                });
                await tcs.Task;
                return tcs.Task.Result;
            }
        }

        public void Dispatch(Action action)
        {
            if (dispatcher.HasThreadAccess) { action(); }
            else
            {
                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask().Wait();
            }
        }
    }
}
