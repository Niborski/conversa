// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Tests
{
    [TestClass]
    public class XmppClientTest
    {
        [TestMethod]
        public async Task OpenConnectionTest()
        {
            using (var client = new XmppClient(ConnectionStringHelper.GetDefaultConnectionString()))
            {
                client.StateChanged.Subscribe(state => OnStateChanged(state));

                await client.OpenAsync().ConfigureAwait(false);

                System.Threading.SpinWait.SpinUntil(() => { return client.State == XmppClientState.Open; });
            }
        }

        private void OnStateChanged(XmppClientState state)
        {
            Debug.WriteLine("TEST -> Connection state " + state.ToString());
        }
    }
}
