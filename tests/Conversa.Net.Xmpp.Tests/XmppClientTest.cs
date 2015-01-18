// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Conversa.Net.Xmpp.Test
{
    public class XmppClientTest
    {
        [Fact]
        public void XmppClientAsycTest()
        {
            InternalXmppClientAsycTest().Wait();
        }

        private async Task InternalXmppClientAsycTest()
        {
            var csb = new XmppConnectionStringBuilder
            {
                HostName    = "jabber.at"
              , ServiceName = "5222"
              , UserId      = ""
              , Password    = ""
              , Resource    = "test"
            };

            using (var connection = new XmppClient(csb.ToConnectionString()))
            {
                connection.StateChanged.Subscribe(state => Debug.WriteLine("TEST -> Connection state " + state.ToString()));

                await connection.OpenAsync();

                System.Threading.SpinWait.SpinUntil(() => { return connection.State == XmppClientState.Closing; });
            }
        }
    }
}
