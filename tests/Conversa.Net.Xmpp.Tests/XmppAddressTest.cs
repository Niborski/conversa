// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using Xunit;

namespace Conversa.Net.Xmpp.Test
{
    public class XmppAddressTest
    {
        [Fact]
        public void XmppAddressFromString()
        {
            var         sjid = "alice@wonderland.lit/RabbitHole";
            XmppAddress xjid = sjid;

            Assert.Equal("alice"               , xjid.UserName);
            Assert.Equal("wonderland.lit"      , xjid.DomainName);
            Assert.Equal("RabbitHole"          , xjid.ResourceName);
            Assert.Equal(sjid                  , xjid.Addresss);
            Assert.Equal("alice@wonderland.lit", xjid.BareAddress);
        }

        [Fact]
        public void XmppAddressToString()
        {
            var sjid = "alice@wonderland.lit/RabbitHole";
            var xjid = new XmppAddress("alice", "wonderland.lit", "RabbitHole");
            var ojid = xjid;

            Assert.Equal("alice"               , xjid.UserName);
            Assert.Equal("wonderland.lit"      , xjid.DomainName);
            Assert.Equal("RabbitHole"          , xjid.ResourceName);
            Assert.Equal(sjid                  , xjid.Addresss);
            Assert.Equal(sjid                  , ojid);
            Assert.Equal("alice@wonderland.lit", xjid.BareAddress);
        }

        [Fact]
        public void XmppAddressFromBareIdentifierString()
        {
            var         sjid = "alice@wonderland.lit";
            XmppAddress xjid = sjid;

            Assert.Equal("alice"         , xjid.UserName);
            Assert.Equal("wonderland.lit", xjid.DomainName);
            Assert.Equal(String.Empty    , xjid.ResourceName);
            Assert.Equal(sjid            , xjid.Addresss);
            Assert.Equal(sjid            , xjid.BareAddress);
        }

        [Fact]
        public void XmppAddressToBareIdentifierString()
        {
            var sjid = "alice@wonderland.lit";
            var xjid = new XmppAddress("alice", "wonderland.lit");
            var ojid = xjid;

            Assert.Equal("alice"         , xjid.UserName);
            Assert.Equal("wonderland.lit", xjid.DomainName);
            Assert.Equal(String.Empty    , xjid.ResourceName);
            Assert.Equal(sjid            , xjid.Addresss);
            Assert.Equal(sjid            , ojid);
            Assert.Equal(sjid            , xjid.BareAddress);
        }

        [Fact]
        public void StringEqualityOperator()
        {
            var sjid = "alice@wonderland.lit";
            var xjid = new XmppAddress(sjid);

            Assert.True(sjid == xjid);
        }

        [Fact]
        public void EqualityOperator()
        {
            var sjid  = "alice@wonderland.lit";
            var xjid1 = new XmppAddress(sjid);
            var xjid2 = new XmppAddress(sjid);

            Assert.True(xjid2 == xjid1);
        }
    }
}
