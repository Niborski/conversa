// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.InstantMessaging;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Tests
{
    [TestClass]
    public class ContactTest
    {
        [TestMethod]
        public async Task BlockContactTest()
        {
            var waiter = new AutoResetEvent(false);

            using (var client = new XmppClient(ConnectionStringHelper.GetDefaultConnectionString()))
            {
                client.ServerCapabilities
                      .CapsChangedStream
                      .Subscribe(x => waiter.Set());

                await client.OpenAsync();

                waiter.WaitOne();

                var contact = client.Roster.First();

                contact.BlockingStream
                       .Where(x => x == ContactBlockingAction.Blocked)
                       .Subscribe(x => waiter.Set());

                await contact.BlockAsync();

                waiter.WaitOne();
            }
        }

        [TestMethod]
        public async Task UnBlockContactTest()
        {
            var waiter = new AutoResetEvent(false);

            using (var client = new XmppClient(ConnectionStringHelper.GetDefaultConnectionString()))
            {
                client.ServerCapabilities
                      .CapsChangedStream
                      .Subscribe(x => waiter.Set());

                await client.OpenAsync();

                waiter.WaitOne();

                var contact = client.Roster.First();

                contact.BlockingStream
                       .Where(x => x == ContactBlockingAction.Unblocked)
                       .Subscribe(x => waiter.Set());

                await contact.UnBlockAsync();

                waiter.WaitOne();
            }
        }
    }
}