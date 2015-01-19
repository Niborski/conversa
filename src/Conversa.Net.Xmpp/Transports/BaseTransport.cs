// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;

namespace Conversa.Net.Xmpp.Transports
{
    /// <summary>
    /// Base class for transport implementations
    /// </summary>
    internal abstract class BaseTransport
        : ITransport
    {
        private XmppConnectionString 	   connectionString;
        private Subject<XmppStreamElement> messageStream;
		private Subject<TransportState>    stateChanged;
        private string               	   hostName;
        private bool                 	   isDisposed;

        /// <summary>
        /// Occurs when a new message is received
        /// </summary>
        public IObservable<XmppStreamElement> MessageStream
        {
            get { return this.messageStream.AsObservable(); }
        }

        /// <summary>
        /// Occurs when the transport state has changed
        /// </summary>
		public IObservable<TransportState> StateChanged
		{
			get { return this.stateChanged.AsObservable(); }
		}

		/// <summary>
		/// Gets the transport state
		/// </summary>
        public TransportState State
        {
            get;
            private set;
        }

        /// <summary>
        /// XMPP server Host name
        /// </summary>
        public string HostName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.hostName))
                {
                    return this.hostName;
                }

                return this.connectionString.HostName;
            }
            protected set { this.hostName = value; }
        }

        /// <summary>
        /// Get a vector of SSL server errors to ignore when making an secure connection.
        /// </summary>
        /// <returns>A vector of SSL server errors to ignore.</returns>
        public abstract IEnumerable<ChainValidationResult> IgnorableServerCertificateErrors
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the current transport supports secure connections
        /// </summary>
        public abstract bool SupportsSecureConnections
        {
            get;
        }

        protected XmppConnectionString ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        protected bool IsDisposed
        {
            get { return this.isDisposed; }
        }

        protected BaseTransport()
        {
            this.messageStream = new Subject<XmppStreamElement>();
			this.stateChanged  = new Subject<TransportState>();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Conversa.Net.Xmpp.XmppConnection"/> is reclaimed by garbage collection.
        /// </summary>
        ~BaseTransport()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.Close();
                }

                this.connectionString = null;

                if (this.messageStream != null)
                {
                    this.messageStream.Dispose();
                    this.messageStream = null;
                }

                if (this.stateChanged != null)
                {
                    this.stateChanged.Dispose();
                    this.stateChanged = null;
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
            }

            this.isDisposed = true;
        }

        public abstract Task OpenAsync(XmppConnectionString         connectionString
                                     , IList<ChainValidationResult> ignorableServerCertificateErrors);

        public abstract Task UpgradeToSslAsync();

        public abstract Task SendAsync(string value);

        public abstract Task SendAsync(byte[] value);

        public abstract Task ResetStreamAsync();

        public virtual void Close()
        {
            if (this.messageStream != null)
            {
                this.messageStream.Dispose();
                this.messageStream = null;
            }

			if (this.stateChanged != null)
			{
				this.stateChanged.Dispose();
				this.stateChanged = null;
			}

            this.connectionString = null;
        }

        protected void PublishMessage(XmppStreamElement message)
        {
			this.messageStream.OnNext(message);
        }

        protected void PublishStateChange(TransportState state)
        {
			this.State = state;
            this.stateChanged.OnNext(state);
        }
    }
}
