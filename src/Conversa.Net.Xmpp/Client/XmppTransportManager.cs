using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.InstantMessaging;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Core;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Provides methods for managing chat messages.
    /// </summary>
    public sealed class XmppTransportManager
    {
        private static object           syncObject = new object();
        private static XmppTransport    transport  = null;
        private static ChatMessageStore store      = null;

        /// <summary>
        /// Registers the app as a ChatMessageTransport in order to post messages to the ChatMessageStore.
        /// </summary>
        /// <returns>The transport ID for the newly registered ChatMessageTransport.</returns>
        public static string RegisterTransport()
        {
            transport = new XmppTransport();

            transport.RequestTransportInitialization();

            CoreApplication.Exiting += async (s, e) => {
                await transport.CloseAsync().ConfigureAwait(false);
                transport = null;
            };

            return transport.TransportId;;
        }

        /// <summary>
        /// Gets the <see cref="XmppTransport"/>.
        /// </summary>
        /// <returns>An asynchronous operation that returns the transport on successful completion.</returns>
        public static XmppTransport GetTransport()
        {
            return transport;
        }

        public static ChatMessageStore RequestStore(XmppAddress address)
        {
            lock (syncObject)
            {
                if (store == null)
                {
                    store = new ChatMessageStore();
                }
            }

            return store;
        }
    }
}
