// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Security.Cryptography.Certificates;
using Windows.Storage.Streams;

namespace Conversa.Net.Xmpp.Transports
{
    /// <summary>
    /// TCP/IP Transport implementation
    /// </summary>
    internal sealed class TcpTransport
        : BaseTransport
    {
        private StreamSocket                 socket;
        private DataReader                   reader;
        private DataWriter                   writer;
        private StreamParser                 parser;
        private DataReaderLoadOperation      asyncRead;
        private HostName                     hostname;
        private IList<ChainValidationResult> ignorableServerCertificateErrors;

        /// <summary>
        /// Get a vector of SSL server errors to ignore when making an secure connection.
        /// </summary>
        /// <returns>A vector of SSL server errors to ignore.</returns>
        public override IEnumerable<ChainValidationResult> IgnorableServerCertificateErrors
        {
            get { return this.ignorableServerCertificateErrors.AsEnumerable(); }
        }

        /// <summary>
        /// Gets a value indicating whether the current transport supports secure connections
        /// </summary>
        public override bool SupportsSecureConnections
        {
            get { return true; }
        }

        private bool CanRead
        {
            get { return (this.State == TransportState.Open); }
        }

        /// <summary>
        /// Initializes a new instance of the <b>XmppConnection</b> class.
        /// </summary>
        public TcpTransport()
        {
        }

        /// <summary>
        /// Opens the connection
        /// </summary>
        /// <param name="connectionString">The connection string used for authentication.</param>
        public override async Task OpenAsync(XmppConnectionString         connectionString
                                           , IList<ChainValidationResult> ignorableServerCertificateErrors)
        {
            Debug.WriteLine("TRANSPORT => Opening connection against hostname " + connectionString.HostName);

            this.PublishStateChange(TransportState.Opening);

            // Update the list of ignorable server SSL errors
            if (!ignorableServerCertificateErrors.IsEmpty())
            {
                this.ignorableServerCertificateErrors = ignorableServerCertificateErrors;
            }
            else
            {
                this.ignorableServerCertificateErrors = new List<ChainValidationResult>
                {
                     ChainValidationResult.InvalidName
                };
            }

            // Connection string
            this.ConnectionString = connectionString;

            // Connect to the server
            await this.ConnectAsync().ConfigureAwait(false);

            this.PublishStateChange(TransportState.Open);

            // Initialize XMPP Stream
            await this.ResetStreamAsync().ConfigureAwait(false);
        }

        public override async Task UpgradeToSslAsync()
        {
            Debug.WriteLine("TRANSPORT => Upgrading connection to TLS v1.2");

            // Update transport state
            this.PublishStateChange(TransportState.UpgradingToSsl);

            // Try to stop current read operation
            this.StopAsyncReads();

            // Update ignorable SSL error list
            this.socket.Control.IgnorableServerCertificateErrors.Clear();

            foreach (var ignorableError in this.ignorableServerCertificateErrors)
            {
                this.socket.Control.IgnorableServerCertificateErrors.Add(ignorableError);
            }

            // Validation HostName
            var validationHostName = new HostName(this.ConnectionString.UserAddress.DomainName);

            // Try to upgrade to SSL
            await this.socket.UpgradeToSslAsync(SocketProtectionLevel.Tls12, validationHostName);

            // Update transport state
            this.PublishStateChange(TransportState.Open);

            // Reset XMPP stream
            await this.ResetStreamAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Writes an string value to the output transport.
        /// </summary>
        /// <param name="value">The value</param>
        public override async Task SendAsync(string value)
        {
            Debug.WriteLine("CLIENT -> " + value);

            this.writer.WriteString(value);

            await this.writer.StoreAsync();
            await this.writer.FlushAsync();
        }

        /// <summary>
        /// Writes an byte array value to the output transport.
        /// </summary>
        /// <param name="value">The value</param>
        public override async Task SendAsync(byte[] value)
        {
            Debug.WriteLine("CLIENT -> " + XmppEncoding.Utf8.GetString(value, 0, value.Length));

            this.writer.WriteBytes(value);

            await this.writer.StoreAsync();
            await this.writer.FlushAsync();
        }

        /// <summary>
        /// Resets the transport and sends the XMPP stream initialization
        /// </summary>
        public override async Task ResetStreamAsync()
        {
            // Serialization can't be used in this case
            string xml = String.Format(XmppCodes.StreamFormat
                                     , XmppCodes.StreamNamespace
                                     , XmppCodes.StreamURI
                                     , this.ConnectionString.UserAddress.DomainName
                                     , XmppCodes.StreamVersion);

            // Stop current read operation
            this.StopAsyncReads();

            // Cleanup the stream
            this.parser.Reset(true);

            // (Re)Start reads
            this.BeginReadAsync();

            // (Re)Start the XMPP Stream
            await this.SendAsync(xml).ConfigureAwait(false);
        }

        public override void Close()
        {
            try
            {
                this.PublishStateChange(TransportState.Closing);
                this.StopAsyncReads();

                if (this.reader != null)
                {
                    this.reader.Dispose();
                    this.reader = null;
                }
                if (this.writer != null)
                {
                    this.writer.Dispose();
                    this.writer = null;
                }
                if (this.socket != null)
                {
                    this.socket.Dispose();
                    this.socket = null;
                }
                if (this.parser != null)
                {
                    this.parser.Dispose();
                    this.parser = null;
                }

                if (this.ignorableServerCertificateErrors != null)
                {
                    this.ignorableServerCertificateErrors.Clear();
                    this.ignorableServerCertificateErrors = null;
                }
            }
            catch
            {
            }
            finally
            {
                this.PublishStateChange(TransportState.Closed);
            }
        }

        private async Task ConnectAsync()
        {
            // Hostname
            this.hostname = new HostName(this.ConnectionString.HostName);

            // Remote service name ( DNS SRV )
            var remoteServiceName = XmppCodes.XmppSrvRecordPrefix + "." + this.ConnectionString.HostName;

            // Network Socket
            this.socket = new StreamSocket();

            // Socket configuration

            // Controls the size, in bytes, of the send buffer to be used for sending data on a StreamSocket object.
            this.socket.Control.OutboundBufferSizeInBytes = this.ConnectionString.PacketSize;

            // Indicates whether keep-alive packets are sent to the remote destination on a StreamSocket object.
            // this.socket.Control.KeepAlive = false;

            // Indicates whether Nagle's algorithm is used on a StreamSocket object
            // this.socket.Control.NoDelay = false;

            // The quality of service on a StreamSocket object.
            // this.socket.Control.QualityOfService = SocketQualityOfService.LowLatency;

            // Make the socket to connect to the Server
            // 1. First try to connect agains the remote service name with DNS SRV
            // 2. If it can't connect try using the service name given in the connection string ( tipically the port number )
            // https://view.officeapps.live.com/op/view.aspx?src=http%3a%2f%2fvideo.ch9.ms%2fbuild%2f2011%2fslides%2fPLAT-580T_Thaler.pptx
            bool connected = await this.ConnectAsync(remoteServiceName).ConfigureAwait(false);

            if (!connected)
            {
                connected = await this.ConnectAsync(this.ConnectionString.ServiceName).ConfigureAwait(false);
            }

            if (!connected)
            {
                this.socket.Dispose();
                this.socket = null;
            }

            // Create streams for reading & writing to the socket
            this.reader = new DataReader(this.socket.InputStream);
            this.writer = new DataWriter(this.socket.OutputStream);

            // Set encodings
            this.reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            this.writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;

            // Set byte order
            // this.writer.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;

            // Allow partial reads
            this.reader.InputStreamOptions = InputStreamOptions.Partial;

            // Create the XMPP stream parser instance
            this.parser = new StreamParser();
        }

        private async Task<bool> ConnectAsync(string remoteServiceName)
        {
            try
            {
                await this.socket.ConnectAsync(this.hostname, remoteServiceName);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BeginReadAsync()
        {
            if (!this.CanRead)
            {
                Debug.WriteLine("The transport state doesn't allow reads");
                return;
            }
            if (this.asyncRead != null && this.asyncRead.Status == AsyncStatus.Started)
            {
                Debug.WriteLine("There is a running read operation");
                return;
            }

            this.asyncRead           = this.reader.LoadAsync(this.ConnectionString.PacketSize);
            this.asyncRead.Completed = new AsyncOperationCompletedHandler<uint>(this.ReadBytes);
        }

        private void ReadBytes(IAsyncOperation<uint> asyncInfo, AsyncStatus asyncStatus)
        {
            if (asyncStatus == AsyncStatus.Error || asyncStatus == AsyncStatus.Canceled || !this.CanRead)
            {
                return;
            }

            while (this.reader.UnconsumedBufferLength > 0)
            {
                var buffer = new byte[this.reader.UnconsumedBufferLength];

                this.reader.ReadBytes(buffer);
                this.parser.WriteBytes(buffer);
            }

            while (this.parser != null && !this.parser.EOF)
            {
                var message = this.parser.ReadNextNode();

                if (message != null)
                {
                    this.PublishMessage(message);
                }
            }

            this.BeginReadAsync();
        }

        private void StopAsyncReads()
        {
            if (this.asyncRead != null)
            {
                this.asyncRead.Close();
                this.asyncRead = null;
            }
        }
    }
}
