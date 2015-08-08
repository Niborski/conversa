// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Xml;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;

namespace Conversa.Net.Xmpp.Tests
{
    [TestClass]
    public class RosterSerializationTest
    {
        [TestMethod]
        public void DeserializeRosterRequest()
        {
            var xml = @"<iq from='juliet@example.com/balcony'
                            id='bv1bs71f'
                            type='get'
                            xmlns=""jabber:client"">
                          <query xmlns='jabber:iq:roster'/>
                        </iq>";

            var infoQuery = XmppSerializer.Deserialize<InfoQuery>("iq", xml);

            Assert.IsNotNull(infoQuery);
            Assert.AreEqual("juliet@example.com/balcony", infoQuery.From);
            Assert.AreEqual(InfoQueryType.Get, infoQuery.Type);
            Assert.AreNotEqual(null, infoQuery.Roster);
        }

        [TestMethod]
        public void SerializeRosterRequest()
        {
            var exp = @"<iq from=""juliet@example.com/balcony""
                            id=""bv1bs71f""
                            type=""get""
                            xmlns=""jabber:client"">
                          <query xmlns=""jabber:iq:roster""/>
                        </iq>";

            var query = new InfoQuery
            {
                From = "juliet@example.com/balcony"
              , Id   = "bv1bs71f"
              , Type = InfoQueryType.Get
            };

            query.Roster = new Roster();

            var buffer = XmppSerializer.Serialize(query);
            var xml    = XmppEncoding.Utf8.GetString(buffer, 0, buffer.Length);

            Assert.IsTrue(exp.CultureAwareCompare(xml));
        }
    }
}
