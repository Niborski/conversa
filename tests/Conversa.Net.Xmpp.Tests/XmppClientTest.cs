// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Conversa.Net.Xmpp.Tests
{
    public class XmppClientTest
    {
        [Fact]
        public void OpenConnectionTest()
        {
            InternalOpenConnectionAsync().Wait();
        }

        private async Task InternalOpenConnectionAsync()
        {
            using (var client = new XmppClient(ConnectionStringHelper.GetDefaultConnectionString()))
            {
                client.StateChanged.Subscribe(state => Debug.WriteLine("TEST -> Connection state " + state.ToString()));

                await client.OpenAsync().ConfigureAwait(false);

                System.Threading.SpinWait.SpinUntil(() => { return client.State == XmppClientState.Open; });
            }
        }
    }
}
