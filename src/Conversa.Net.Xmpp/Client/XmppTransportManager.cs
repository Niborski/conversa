using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Provides methods for managing chat messages.
    /// </summary>
    public sealed class XmppTransportManager
    {
        private static XmppTransport transport;

        /// <summary>
        /// Asynchronously registers the app as a ChatMessageTransport in order to post messages to the ChatMessageStore.
        /// </summary>
        /// <returns>The transport ID for the newly registered ChatMessageTransport.</returns>
        public static IAsyncOperation<string> RegisterTransportAsync()
        {
            transport = new XmppTransport();

            return AsyncInfo.Run(_ => Task.Run<string>(() => { return transport.TransportId; }));
        }

        /// <summary>
        /// Asynchronously gets the ChatMessageTransport.
        /// </summary>
        /// <returns>An asynchronous operation that returns the transport on successful completion.</returns>
        public static IAsyncOperation<XmppTransport> GetTransportAsync()
        {
            return AsyncInfo.Run(_ => Task.Run<XmppTransport>(() => { return transport; }));
        }
    }
}
