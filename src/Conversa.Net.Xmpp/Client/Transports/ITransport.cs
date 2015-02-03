// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Xml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;

namespace Conversa.Net.Xmpp.Client.Transports
{
    /// <summary>
    /// Interface for transport implementations
    /// </summary>
    internal interface ITransport
        : IDisposable
    {
        /// <summary>
        /// Occurs when a new XMPP message is received
        /// </summary>
        IObservable<StreamElement> MessageStream
        {
            get;
        }

        /// <summary>
        /// Occurs when the transport state has changed
        /// </summary>
        IObservable<TransportState> StateChanged
        {
            get;
        }

        /// <summary>
        /// Gets the transport state
        /// </summary>
        TransportState State
        {
            get;
        }

        /// <summary>
        /// XMPP server Host name
        /// </summary>
        /// <remarks>
        /// It may return the connection string host name or the one resolved by DNS SRV records lookups
        /// </remarks>
        string HostName
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the current transport supports secure connections
        /// </summary>
        bool SupportsSecureConnections
        {
            get;
        }

        /// <summary>
        /// Get a vector of SSL server errors to ignore when making an secure connection.
        /// </summary>
        /// <returns>A vector of SSL server errors to ignore.</returns>
        IEnumerable<ChainValidationResult> IgnorableServerCertificateErrors
        {
            get;
        }

        /// <summary>
        /// Opens the connection to the server
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="ignorableServerCertificateErrors">List of ignorable SSL errors</param>
        /// <returns></returns>
        Task OpenAsync(XmppConnectionString 		connectionString
                     , IList<ChainValidationResult> ignorableServerCertificateErrors);

        /// <summary>
        /// Opens a secure transport connection
        /// </summary>
        Task UpgradeToSslAsync();

        /// <summary>
        /// Writes an string value to the output transport.
        /// </summary>
        /// <param name="value">The value</param>
        Task SendAsync(string value);

        /// <summary>
        /// Writes an byte array value to the output transport.
        /// </summary>
        /// <param name="value">The value</param>
        Task SendAsync(byte[] value);

        /// <summary>
        /// Resets the transport and sends the XMPP stream initialization
        /// </summary>
        Task ResetStreamAsync();

        /// <summary>
        /// Closes the transport connection
        /// </summary>
        void Close();
    }
}
