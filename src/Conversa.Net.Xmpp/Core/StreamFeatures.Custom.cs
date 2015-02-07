// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// XML Streams
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP core
    /// </remarks>
    public partial class StreamFeatures
    {
        /// <summary>
        /// Gets a value indicating whether secure connection establishment is required
        /// </summary>
        /// <returns><b>true</b> if secure connection is required; otherwise <b>false</b>.</returns>
        [XmlIgnoreAttribute]
        public bool SecureConnectionRequired
        {
            get { return (this.StartTls != null && this.StartTls.RequiredSpecified); }
        }

        /// <summary>
        /// Gets a value indicating whether auth mechanisms has been informed.
        /// </summary>
        /// <returns><b>true</b> if auth mechanisms has been informed; otherwise <b>false</b>.</returns>
        [XmlIgnoreAttribute]
        public bool HasAuthMechanisms
        {
            get { return (this.Mechanisms != null && this.Mechanisms.HasMechanisms); }
        }

        /// <summary>
        /// Gets a value indicating whether resource binding is supported.
        /// </summary>
        /// <returns><b>true</b> if resource binding is supported; otherwise <b>false</b>.</returns>
        [XmlIgnoreAttribute]
        public bool SupportsResourceBinding
        {
            get { return this.Bind != null; }
        }

        /// <summary>
        /// Gets a value indicating whether session establishment is supported.
        /// </summary>
        /// <returns><b>true</b> if session establishment is supported; otherwise <b>false</b>.</returns>
        [XmlIgnoreAttribute]
        public bool SupportsSessions
        {
            get { return this.Session != null; }
        }

        /// <summary>
        /// Gets a value indicating whether stream compression is supported.
        /// </summary>
        /// <returns><b>true</b> if stream compression is supported; otherwise <b>false</b>.</returns>
        [XmlIgnoreAttribute]
        public bool SupportsCompression
        {
            get { return this.Compression != null; }
        }

        /// <summary>
        /// Gets a value indicating whether in-band registration is supported.
        /// </summary>
        /// <returns><b>true</b> if in-band registration is supported; otherwise <b>false</b>.</returns>
        [XmlIgnoreAttribute]
        public bool SupportsInBandRegistration
        {
            get { return this.Register != null; }
        }

        /// <summary>
        /// Gets a value indicating whether entity capabilities is supported.
        /// </summary>
        /// <returns><b>true</b> if entiti capabilities are supported; otherwise <b>false</b>.</returns>
        [XmlIgnoreAttribute]
        public bool SupportsEntityCapabilities
        {
            get { return this.Register != null; }
        }
    }
}
