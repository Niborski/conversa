// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Gnu.Inet.Encoding;
using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// XMPP Address (JID)
    /// </summary>
    /// <remarks>http://tools.ietf.org/html/rfc6122</remarks>
    public sealed class XmppAddress 
        : IEquatable<String>, IEquatable<XmppAddress>, IXmlSerializable
    {
        /// <summary>
        /// Regex used to parse XMPP Address (JID) strings
        /// </summary>
        /// <remarks>
        /// jid           = [ localpart "@" ] domainpart [ "/" resourcepart ]
        /// localpart     = 1*(nodepoint)
        ///                 ;
        ///                 ; a "nodepoint" is a UTF-8 encoded Unicode code
        ///                 ; point that satisfies the Nodeprep profile of
        ///                 ; stringprep
        ///                 ;
        /// domainpart    = IP-literal / IPv4address / ifqdn
        ///                 ;
        ///                 ; the "IPv4address" and "IP-literal" rules are
        ///                 ; defined in RFC 3986, and the first-match-wins
        ///                 ; (a.k.a. "greedy") algorithm described in RFC
        ///                 ; 3986 applies to the matching process
        ///                 ;
        ///                 ; note well that reuse of the IP-literal rule
        ///                 ; from RFC 3986 implies that IPv6 addresses are
        ///                 ; enclosed in square brackets (i.e., beginning
        ///                 ; with '[' and ending with ']'), which was not
        ///                 ; the case in RFC 3920
        ///                 ;
        /// ifqdn         = 1*(namepoint)
        ///                 ;
        ///                 ; a "namepoint" is a UTF-8 encoded Unicode
        ///                 ; code point that satisfies the Nameprep
        ///                 ; profile of stringprep
        ///                 ;
        /// resourcepart  = 1*(resourcepoint)
        ///                 ;
        ///                 ; a "resourcepoint" is a UTF-8 encoded Unicode
        ///                 ; code point that satisfies the Resourceprep
        ///                 ; profile of stringprep
        ///                 ;
        /// </remarks>
        private static readonly Regex AddressRegex = new Regex
        (
            @"(?:(?<localpart>[^\@]{1,1023})\@)?"        // optional node
          + @"(?<domainpart>[a-zA-Z0-9\.\-]{1,1023}){1}" // domain
          + @"(?:/(?<resourcepart>.{1,1023}))?"          // optional resource
          , RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.ExplicitCapture
        );

        public static XmppAddress FromString(string address)
        {
            return new XmppAddress(address);
        }

        private string userName;
        private string domainName;
        private string resourceName;
        private string bareAddress;
        private string address;

        public string Address
        {
            get { return this.address; }
        }

        /// <summary>
        /// Gets the Bare Address (Bare JID)
        /// </summary>
        public string BareAddress
        {
            get { return this.bareAddress; }
        }

        /// <summary>
        /// Gets the User Name
        /// </summary>
        public string UserName
        {
            get { return this.userName; }
        }

        /// <summary>
        /// Gets the Domain Name
        /// </summary>
        public string DomainName
        {
            get { return this.domainName; }
        }

        /// <summary>
        /// Gets the Resource Name
        /// </summary>
        public string ResourceName
        {
            get { return this.resourceName; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppAddress"/> class with the given XMPP Address (JID)
        /// </summary>
        /// <param name="address">The XMPP Address (JID)</param>
        public XmppAddress()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppAddress"/> class with the given XMPP Address (JID)
        /// </summary>
        /// <param name="address">The XMPP Address (JID)</param>
        public XmppAddress(string address)
        {
            this.Parse(address);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppAddress"/> class with
        /// the given user name, domain name and resource name.
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <param name="domainName">The domain name</param>
        public XmppAddress(string userName, string domainName)
            : this(userName, domainName, String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppAddress"/> class with
        /// the given user name, domain name and resource name.
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <param name="domainName">The domain name</param>
        /// <param name="resourceName">The resource name</param>
        public XmppAddress(string userName, string domainName, string resourceName)
        {
            this.userName     = Stringprep.NamePrep(userName);
            this.domainName   = Stringprep.NodePrep(domainName);
            this.resourceName = Stringprep.ResourcePrep(resourceName);

            this.Update();
        }

        public static bool operator ==(XmppAddress a, XmppAddress b)
        {
            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.address == b.address;
        }

        public static bool operator !=(XmppAddress a, XmppAddress b)
        {
            return !(a == b);
        }

        public static bool operator ==(XmppAddress a, string b)
        {
            // If one is null, but not both, return false.
            if (((object)a == null) || (b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.address == b;
        }

        public static bool operator !=(XmppAddress a, string b)
        {
            return !(a == b);
        }

        public static bool operator ==(string a, XmppAddress b)
        {
            // If one is null, but not both, return false.
            if ((a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a == b.address;
        }

        public static bool operator !=(string a, XmppAddress b)
        {
            return !(a == b);
        }

        public static implicit operator XmppAddress(string x)
        {
            return new XmppAddress(x);
        }

        public static implicit operator string(XmppAddress x)
        {
            return (((object)x == null) ? null : x.address);
        }

        public bool Equals(string other)
        {
            // If parameter is null return false.
            if (other == null)
            {
                return false;
            }

            return (this.address == other);
        }

        public bool Equals(XmppAddress other)
        {
            // If parameter is null return false.
            if ((object)other == null)
            {
                return false;
            }

            return (this.address == other.address);
        }

        // Override the Object.Equals(object o) method:
        public override bool Equals(object other)
        {
            // If parameter is null return false.
            if ((object)other == null)
            {
                return false;
            }
            if (other is XmppAddress)
            {
                return Equals(other as XmppAddress);
            }
            else if (other is string)
            {
                return Equals(other as string);
            }

            return false;
        }

        // Override the Object.GetHashCode() method:
        public override int GetHashCode()
        {
            return this.address.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return this.address;
        }

        private void Parse(string address)
        {
            Match match = AddressRegex.Match(address);

            this.userName     = String.Empty;
            this.domainName   = String.Empty;
            this.resourceName = String.Empty;

            if (match != null)
            {
                // The Local part is optional
                if (match.Groups["localpart"] != null)
                {
                    this.userName = Stringprep.NamePrep(match.Groups["localpart"].Value);
                }
                // The Domain part is mandatory
                this.domainName = Stringprep.NodePrep(match.Groups["domainpart"].Value);
                // The Resource part is optional
                if (match.Groups["resourcepart"] != null)
                {
                    this.resourceName = Stringprep.ResourcePrep(match.Groups["resourcepart"].Value);
                }
            }

            this.Update();
        }

        private void Update()
        {
            this.address = String.Empty;

            if (!String.IsNullOrEmpty(this.UserName))
            {
                this.address += this.UserName;
            }

            if (this.address.Length > 0)
            {
                this.address += "@";
            }

            this.address    += this.DomainName;
            this.bareAddress = this.address;

            if (!String.IsNullOrEmpty(this.ResourceName))
            {
                this.address += "/" + this.ResourceName;
            }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
