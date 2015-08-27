// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;

namespace Conversa.Net.Xmpp.Tests
{
    [TestClass]
    public class XmppAddressTest
    {
        [TestMethod]
        public void XmppAddressFromString()
        {
            var         sjid = "alice@wonderland.lit/RabbitHole";
            XmppAddress xjid = sjid;

            Assert.AreEqual("alice"               , xjid.UserName);
            Assert.AreEqual("wonderland.lit"      , xjid.DomainName);
            Assert.AreEqual("RabbitHole"          , xjid.ResourceName);
            Assert.AreEqual(sjid                  , xjid.Address);
            Assert.AreEqual("alice@wonderland.lit", xjid.BareAddress);
        }

        [TestMethod]
        public void XmppAddressToString()
        {
            var sjid = "alice@wonderland.lit/RabbitHole";
            var xjid = new XmppAddress("alice", "wonderland.lit", "RabbitHole");
            var ojid = xjid;

            Assert.AreEqual("alice"               , xjid.UserName);
            Assert.AreEqual("wonderland.lit"      , xjid.DomainName);
            Assert.AreEqual("RabbitHole"          , xjid.ResourceName);
            Assert.AreEqual(sjid                  , xjid.Address);
            Assert.AreEqual(ojid                  , sjid);
            Assert.AreEqual("alice@wonderland.lit", xjid.BareAddress);
        }               

        [TestMethod]
        public void XmppAddressFromBareIdentifierString()
        {
            var         sjid = "alice@wonderland.lit";
            XmppAddress xjid = sjid;

            Assert.AreEqual("alice"         , xjid.UserName);
            Assert.AreEqual("wonderland.lit", xjid.DomainName);
            Assert.AreEqual(String.Empty    , xjid.ResourceName);
            Assert.AreEqual(sjid            , xjid.Address);
            Assert.AreEqual(sjid            , xjid.BareAddress);
        }

        [TestMethod]
        public void XmppAddressToBareIdentifierString()
        {
            var sjid = "alice@wonderland.lit";
            var xjid = new XmppAddress("alice", "wonderland.lit");
            var ojid = xjid;

            Assert.AreEqual("alice"         , xjid.UserName);
            Assert.AreEqual("wonderland.lit", xjid.DomainName);
            Assert.AreEqual(String.Empty    , xjid.ResourceName);
            Assert.AreEqual(sjid            , xjid.Address);
            Assert.AreEqual(sjid            , ojid);
            Assert.AreEqual(sjid            , xjid.BareAddress);
        }

        [TestMethod]
        public void StringEqualityOperator()
        {
            var sjid = "alice@wonderland.lit";
            var xjid = new XmppAddress(sjid);

            Assert.IsTrue(sjid == xjid);
        }

        [TestMethod]
        public void EqualityOperator()
        {
            var sjid  = "alice@wonderland.lit";
            var xjid1 = new XmppAddress(sjid);
            var xjid2 = new XmppAddress(sjid);

            Assert.IsTrue(xjid2 == xjid1);
        }
    }
}
