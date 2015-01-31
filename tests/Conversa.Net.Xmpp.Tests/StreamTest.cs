// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Xml;
using System;
using System.Text;
using Xunit;

namespace Conversa.Net.Xmpp.Test
{
    public class StreamTest
    {
        [Fact]
        public void DeserializeWithMessage()
        {
            var xml = @"<stream:stream
                            from=""juliet@im.example.com""
                            to=""im.example.com""
                            version=""1.0""
                            xml:lang=""en""
                            xmlns:stream=""http://etherx.jabber.org/streams"">
                          <message xmlns=""jabber:client"">
                            <body>foo</body>
                           </message>
                        </stream:stream>";

            var stream = XmppSerializer.Deserialize<Stream>("stream:stream", xml);

            Assert.NotNull(stream);
            Assert.Equal("en", stream.Lang);
            Assert.Equal("1.0", stream.Version);
            Assert.Equal("juliet@im.example.com", stream.From);
            Assert.Equal("im.example.com", stream.To);
            Assert.NotNull(stream.Items);
            Assert.Equal(1, stream.Items.Count);
            Assert.True(stream.Items[0] is Message);

            var message = stream.Items[0] as Message;

            Assert.True(message.Body != null);
            Assert.Equal("foo", message.Body.Value);
        }

        [Fact]
        public void SerializeWithMessage()
        {
            var exp = @"<stream:stream
                            from=""juliet@im.example.com""
                            to=""im.example.com""
                            version=""1.0""
                            xml:lang=""en""
                            xmlns:stream=""http://etherx.jabber.org/streams"">
                          <message xmlns=""jabber:client"">
                            <body>foo</body>
                           </message>
                        </stream:stream>";

            var msg = new Message { Id = null, Body = new MessageBody { Value = "foo" } };
            var stream = new Stream 
            {
                From    = "juliet@im.example.com"
              , To      = "im.example.com"
              , Version = "1.0"
              , Lang    = "en" 
            };

            stream.Items.Add(msg);

            var buffer = XmppSerializer.Serialize(stream);
            var xml    = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            Assert.True(exp.CultureAwareCompare(xml));
        }
    }
}
