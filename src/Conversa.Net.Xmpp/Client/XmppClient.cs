// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Authentication;
using Conversa.Net.Xmpp.Caps;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.InstantMessaging;
using Conversa.Net.Xmpp.PersonalEventing;
using Conversa.Net.Xmpp.ServiceDiscovery;
using Conversa.Net.Xmpp.Shared;
using Conversa.Net.Xmpp.Storage;
using Conversa.Net.Xmpp.Transports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Represents a connection to a XMPP server
    /// </summary>
    public sealed class XmppClient
        : IDisposable
    {
        // State change subject
        private Subject<XmppClientState> stateChanged;

        // Authentication Subjects
        private Subject<XmppAuthenticationFailure> authenticationFailed;

        // Messaging Subjects
        private Subject<InfoQuery> infoQueryStream;
        private Subject<Message>   messageStream;
        private Subject<Presence>  presenceStream;

        // Private members
        private XmppConnectionString    connectionString;
        private XmppAddress             userAddress;
        private XmppStreamFeatures      streamFeatures;
        private XmppClientState         state;
        private ITransport              transport;
        private IXmppAuthenticator      authenticator;
        private CompositeDisposable     subscriptions;
        private XmppRoster              roster;
        private XmppActivity            activity;
        private XmppClientCapabilities  capabilities;
        private XmppCapabilitiesStorage capabilitiesStorage;
        private XmppServiceDiscovery    serviceDiscovery;
        private XmppPersonalEventing    personalEventing;
        private AvatarStorage           avatarStorage;
        private bool                    isDisposed;

        /// <summary>
        /// Get a vector of SSL server errors to ignore when making an secure connection.
        /// </summary>
        /// <returns>A vector of SSL server errors to ignore.</returns>
        public IList<ChainValidationResult> IgnorableServerCertificateErrors
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when the connection state changes
        /// </summary>
        public IObservable<XmppClientState> StateChanged
        {
            get { return this.stateChanged.AsObservable(); }
        }

        /// <summary>
        /// Occurs when the authentication process fails
        /// </summary>
        public IObservable<XmppAuthenticationFailure> AuthenticationFailed
        {
            get { return this.authenticationFailed.AsObservable(); }
        }

        /// <summary>
        /// Occurs when a new IQ stanza is received.
        /// </summary>
        public IObservable<InfoQuery> InfoQueryStream
        {
            get { return this.infoQueryStream.AsObservable(); }
        }

        /// <summary>
        /// Occurs when a new message stanza is received.
        /// </summary>
        public IObservable<Message> MessageStream
        {
            get { return this.messageStream.AsObservable(); }
        }

        /// <summary>
        /// Occurs when a new presence stanza is received.
        /// </summary>
        public IObservable<Presence> PresenceStream
        {
            get { return this.presenceStream.AsObservable(); }
        }

        /// <summary>
        /// Gets the session <see cref="XmppRoster">Roster</see>
        /// </summary>
        public XmppRoster Roster
        {
            get { return this.roster; }
        }

        /// <summary>
        /// Gets the list of <see cref="XmppActivity">activities</see>
        /// </summary>
        public XmppActivity Activity
        {
            get { return this.activity; }
        }

        /// <summary>
        /// Gets the client capabilities
        /// </summary>
        public XmppClientCapabilities Capabilities
        {
            get { return this.capabilities; }
        }

        /// <summary>
        /// Gets the capabilities storage manager
        /// </summary>
        public XmppCapabilitiesStorage CapabilitiesStorage
        {
            get { return this.capabilitiesStorage; }
        }

        /// <summary>
        /// Gets the <see cref="XmppSession">service discovery </see> instance associated to the session
        /// </summary>
        public XmppServiceDiscovery ServiceDiscovery
        {
            get { return this.serviceDiscovery; }
        }

        /// <summary>
        /// Gets the avatar storage
        /// </summary>
        public AvatarStorage AvatarStorage
        {
            get { return this.avatarStorage; }
        }

        /// <summary>
        /// Gets the <see cref="XmppPersonalEventing">personal eventing</see> instance associated to the session
        /// </summary>
        public XmppPersonalEventing PersonalEventing
        {
            get { return this.personalEventing; }
        }

        /// <summary>
        /// Gets the string used to open the connection.
        /// </summary>
        public XmppConnectionString ConnectionString
        {
            get  { return this.connectionString; }
        }

        /// <summary>
        /// Gets the connection Host name
        /// </summary>
        public string HostName
        {
            get
            {
                if (this.transport == null)
                {
                    return String.Empty;
                }

                return this.transport.HostName;
            }
        }

        /// <summary>
        /// Gets the User ID specified in the Connection String.
        /// </summary>
        public XmppAddress UserAddress
        {
            get { return this.userAddress; }
        }

        /// <summary>
        /// Gets the current state of the connection.
        /// </summary>
        public XmppClientState State
        {
            get { return this.state; }
            private set
            {
                this.state = value;
                this.stateChanged.OnNext(this.state);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppClient"/> class.
        /// </summary>
        public XmppClient(XmppConnectionString connectionString)
        {
            this.connectionString     = connectionString;
            this.stateChanged         = new Subject<XmppClientState>();
            this.authenticationFailed = new Subject<XmppAuthenticationFailure>();
            this.infoQueryStream      = new Subject<InfoQuery>();
            this.messageStream        = new Subject<Message>();
            this.presenceStream       = new Subject<Presence>();
            this.subscriptions        = new CompositeDisposable();
            this.roster               = new XmppRoster(this);
            this.activity             = new XmppActivity(this);
            this.capabilities         = new XmppClientCapabilities(this);
            this.serviceDiscovery     = new XmppServiceDiscovery(this);
            this.personalEventing     = new XmppPersonalEventing(this);
            this.userAddress          = new XmppAddress(this.connectionString.UserAddress.UserName
                                                      , this.connectionString.UserAddress.DomainName
                                                      , this.connectionString.Resource);

            // Avatar storage initialization
            this.avatarStorage = new AvatarStorage();

            // Capabilities Storage initialization
            this.capabilitiesStorage = new XmppCapabilitiesStorage(StorageFolderType.Roaming, "ClientCapabilities.xml");
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="XmppClient"/> is reclaimed by garbage collection.
        /// </summary>
        ~XmppClient()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
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
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    // Release managed resources here
                    this.Close();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                this.connectionString = null;
                this.userAddress      = null;
                this.authenticator    = null;
                this.streamFeatures   = XmppStreamFeatures.None;
                this.state            = XmppClientState.Closed;

                this.CloseTransport();
                this.ReleaseSubscriptions();
                this.ReleaseSubjects();
            }

            this.isDisposed = true;
        }

        /// <summary>
        /// Opens the connection
        /// </summary>
        /// <param name="connectionString">The connection string used for authentication.</param>
        public async Task OpenAsync()
        {
            if (this.State == XmppClientState.Open)
            {
                throw new XmppException("Connection is already open.");
            }

            // Set the initial state
            this.State = XmppClientState.Opening;

            // Connect to the server
            this.transport = new TcpTransport();

            // Event subscriptions
            this.InitializeSubscriptions();

            // Open the connection
            await this.transport
                      .OpenAsync(this.connectionString, this.IgnorableServerCertificateErrors)
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a new message.
        /// </summary>
        public async Task SendAsync<T>()
            where T: class, new()
        {
            await this.SendAsync<T>(new T()).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a new message.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public async Task SendAsync<T>(T message)
            where T: class
        {
            await this.transport.SendAsync(XmppSerializer.Serialize(message)).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a new message.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public async Task SendAsync(object message)
        {
            await this.transport.SendAsync(XmppSerializer.Serialize(message)).ConfigureAwait(false);
        }

        private async void Close()
        {
            if (this.isDisposed || this.State == XmppClientState.Closed || this.State == XmppClientState.Closing)
            {
                return;
            }

            try
            {
                this.State = XmppClientState.Closing;

                // Send the XMPP stream close tag
                await this.CloseStreamAsync().ConfigureAwait(false);

#warning TODO: Wait until the server sends the stream close tag

                // Close the underlying transport
                this.CloseTransport();
            }
            catch
            {
            }
            finally
            {
                this.ReleaseSubscriptions();

                this.transport        = null;
                this.authenticator    = null;
                this.connectionString = null;
                this.userAddress      = null;
                this.streamFeatures   = XmppStreamFeatures.None;
                this.State            = XmppClientState.Closed;

                this.ReleaseSubjects();
            }
        }

        private void CloseTransport()
        {
            if (this.transport != null)
            {
                this.transport.Dispose();
                this.transport = null;
            }
        }

        private async Task CloseStreamAsync()
        {
            await this.transport.SendAsync(XmppCodes.EndStream).ConfigureAwait(false);
        }

        private bool Supports(XmppStreamFeatures feature)
        {
            return ((this.streamFeatures & feature) == feature);
        }

        private void InitializeSubscriptions()
        {
            this.subscriptions.Add(this.transport
                                       .MessageStream
                                       .Subscribe(message => this.OnMessageReceivedAsync(message)));
        }

        private void ReleaseSubscriptions()
        {
            if (this.subscriptions != null)
            {
                this.subscriptions.Dispose();
                this.subscriptions = null;
            }
        }

        private void ReleaseSubjects()
        {
            if (this.authenticationFailed != null)
            {
                this.authenticationFailed.Dispose();
                this.authenticationFailed = null;
            }
            if (this.stateChanged != null)
            {
                this.stateChanged.Dispose();
                this.stateChanged = null;
            }
            if (this.infoQueryStream != null)
            {
                this.infoQueryStream.Dispose();
                this.infoQueryStream = null;
            }
            if (this.messageStream != null)
            {
                this.messageStream.Dispose();
                this.messageStream = null;
            }
            if (this.presenceStream != null)
            {
                this.presenceStream.Dispose();
                this.presenceStream = null;
            }
        }

        private IXmppAuthenticator CreateAuthenticator()
        {
            IXmppAuthenticator authenticator = null;

            if (this.Supports(XmppStreamFeatures.SaslDigestMD5))
            {
                authenticator = new XmppSaslDigestAuthenticator(this.ConnectionString);
            }
            else if (this.Supports(XmppStreamFeatures.SaslPlain))
            {
                authenticator = new XmppSaslPlainAuthenticator(this.ConnectionString);
            }

            return authenticator;
        }

        private async void OnMessageReceivedAsync(XmppStreamElement xmlMessage)
        {
            Debug.WriteLine("SERVER <- " + xmlMessage.ToString());

            if (xmlMessage.OpensXmppStream)
            {
                // Stream opened
            }
            else if (xmlMessage.ClosesXmppStream)
            {
                // Stream closed
            }
            else
            {
                var message = XmppSerializer.Deserialize(xmlMessage.Name, xmlMessage.ToString());

                if (message is InfoQuery || message is Message || message is Presence)
                {
                    await this.OnStanzaAsync(message).ConfigureAwait(false);
                }
                else
                {
                    await this.OnStreamFragmentAsync(message).ConfigureAwait(false);
                }
            }
        }

        private async Task OnStreamFragmentAsync(object fragment)
        {
            if (fragment is StreamError)
            {
                throw new XmppException(fragment as StreamError);
            }
            else if (fragment is StreamFeatures)
            {
                await this.OnNegotiateStreamFeaturesAsync(fragment as StreamFeatures).ConfigureAwait(false);
            }
            else if (fragment is ProceedTls)
            {
                await this.OnUpgradeToSsl().ConfigureAwait(false);
            }
            else if (fragment is SaslChallenge || fragment is SaslResponse)
            {
                await this.SendAsync(this.authenticator.ContinueNegotiationWith(fragment)).ConfigureAwait(false);
            }
            else if (fragment is SaslSuccess)
            {
                await this.OnAuthenticationSuccessAsync().ConfigureAwait(false);
            }
            else if (fragment is SaslFailure)
            {
                this.OnAuthenticationFailed(fragment as SaslFailure);
            }
        }

        private async Task OnStanzaAsync(object stanza)
        {
            if (stanza is Message)
            {
                this.messageStream.OnNext(stanza as Message);
            }
            else if (stanza is Presence)
            {
                this.presenceStream.OnNext(stanza as Presence);
            }
            else if (stanza is InfoQuery)
            {
                await this.OnInfoQueryAsync(stanza as InfoQuery).ConfigureAwait(false);
            }
        }

        private async Task OnInfoQueryAsync(InfoQuery iq)
        {
            if (iq.Bind != null)
            {
                await this.OnBindedResourceAsync(iq).ConfigureAwait(false);
            }
            else if (iq.Ping != null)
            {
                await this.OnPingPongAsync(iq).ConfigureAwait(false);
            }
            else
            {
                this.infoQueryStream.OnNext(iq);
            }
        }

        private async Task OnPingPongAsync(InfoQuery ping)
        {
            if (ping.Type != InfoQueryType.Get)
            {
                return;
            }

            // Send the "pong" response
            await this.SendAsync(ping.AsResponse()).ConfigureAwait(false);
        }

        private async Task OnUpgradeToSsl()
        {
            await this.transport.UpgradeToSslAsync().ConfigureAwait(false);
        }

        private async Task OnNegotiateStreamFeaturesAsync(StreamFeatures features)
        {
            this.streamFeatures = XmppStreamFeatures.None;

            if (features.SecureConnectionRequired)
            {
                this.streamFeatures |= XmppStreamFeatures.SecureConnection;
            }

            if (features.HasAuthMechanisms)
            {
                this.streamFeatures |= this.DiscoverAuthMechanisms(features);
            }

            if (features.SupportsResourceBinding)
            {
                this.streamFeatures |= XmppStreamFeatures.ResourceBinding;
            }

            if (features.SupportsSessions)
            {
                this.streamFeatures |= XmppStreamFeatures.Sessions;
            }

            if (features.SupportsInBandRegistration)
            {
                this.streamFeatures |= XmppStreamFeatures.InBandRegistration;
            }

            await this.NegotiateStreamFeaturesAsync().ConfigureAwait(false);
        }

        private XmppStreamFeatures DiscoverAuthMechanisms(StreamFeatures features)
        {
            var mechanisms = XmppStreamFeatures.None;

            foreach (string mechanism in features.Mechanisms.Mechanism)
            {
                switch (mechanism)
                {
                    case XmppCodes.SaslGoogleXOAuth2Authenticator:
                        mechanisms |= XmppStreamFeatures.SaslXOAuth2;
                        break;

                    case XmppCodes.SaslDigestMD5Mechanism:
                        mechanisms |= XmppStreamFeatures.SaslDigestMD5;
                        break;

                    case XmppCodes.SaslPlainMechanism:
                        mechanisms |= XmppStreamFeatures.SaslPlain;
                        break;
                }
            }

            return mechanisms;
        }

        private async Task NegotiateStreamFeaturesAsync()
        {
            if (this.Supports(XmppStreamFeatures.SecureConnection))
            {
                await this.OpenSecureConnectionAsync().ConfigureAwait(false);
            }
            else if (this.Supports(XmppStreamFeatures.SaslXOAuth2)
                  || this.Supports(XmppStreamFeatures.SaslDigestMD5)
                  || this.Supports(XmppStreamFeatures.SaslPlain))
            {
                await this.OnStartSaslNegotiationAsync().ConfigureAwait(false);
            }
            else if (this.Supports(XmppStreamFeatures.ResourceBinding))
            {
                // Bind resource
                await this.OnBindResourceAsync().ConfigureAwait(false);
            }
            else if (this.Supports(XmppStreamFeatures.Sessions))
            {
                await this.OnRequestSessionAsync().ConfigureAwait(false);
            }
            else
            {
                // No more features for negotiation set state as Open
                this.State = XmppClientState.Open;
            }
        }

        private async Task OpenSecureConnectionAsync()
        {
            await this.SendAsync<StartTls>().ConfigureAwait(false);
        }

        private async Task OnStartSaslNegotiationAsync()
        {
            this.State         = XmppClientState.Authenticating;
            this.authenticator = this.CreateAuthenticator();

            await this.SendAsync(this.authenticator.StartSaslNegotiation()).ConfigureAwait(false);
        }

        private async Task OnAuthenticationSuccessAsync()
        {
            this.State         = XmppClientState.Authenticated;
            this.authenticator = null;

            await this.transport.ResetStreamAsync().ConfigureAwait(false);
        }

        private void OnAuthenticationFailed(SaslFailure failure)
        {
            var errorMessage = "Authentication failed (" + failure.GetErrorMessage() + ")";

            this.authenticationFailed.OnNext(new XmppAuthenticationFailure(errorMessage));

            this.State = XmppClientState.AuthenticationFailure;

            this.Close();
        }

        private async Task OnBindResourceAsync()
        {
            if (!this.Supports(XmppStreamFeatures.ResourceBinding))
            {
                return;
            }

            var iq = new InfoQuery
            {
                Type = InfoQueryType.Set
              , Bind = Bind.WithResource(this.connectionString.Resource)
            };

            await this.SendAsync(iq).ConfigureAwait(false);
        }

        private async Task OnBindedResourceAsync(InfoQuery iq)
        {
            // Update user ID
            this.userAddress = iq.Bind.Jid;

            // Update negotiated features
            this.streamFeatures = this.streamFeatures & (~XmppStreamFeatures.ResourceBinding);

            // Continue feature negotiation
            await this.NegotiateStreamFeaturesAsync().ConfigureAwait(false);
        }

        private async Task OnRequestSessionAsync()
        {
            if (!this.Supports(XmppStreamFeatures.Sessions))
            {
                return;
            }

            var iq = new InfoQuery 
            {
                Type    = InfoQueryType.Set
              , Session = new Session()
            };

            await this.SendAsync(iq).ConfigureAwait(false);

            // Update negotiated features
            this.streamFeatures = this.streamFeatures & (~XmppStreamFeatures.Sessions);

            // Continue feature negotiation
            await this.NegotiateStreamFeaturesAsync().ConfigureAwait(false);
        }
    }
}
