// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Capabilities;
using Conversa.Net.Xmpp.Client.Authentication;
using Conversa.Net.Xmpp.Client.Transports;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Eventing;
using Conversa.Net.Xmpp.InstantMessaging;
using Conversa.Net.Xmpp.Xml;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Represents a connection to a XMPP server
    /// </summary>
    public sealed class XmppTransport
        : IDisposable
    {
        // State change subject
        private Subject<XmppTransportState> stateChanged;

        // Authentication Subjects
        private Subject<SaslAuthenticationFailure> authenticationFailed;

        // Messaging Subjects
        private Subject<InfoQuery> infoQueryStream;
        private Subject<Message>   messageStream;
        private Subject<Presence>  presenceStream;        

        // Private members
        private XmppConnectionString  connectionString;
        private XmppAddress           userAddress;
        private ServerFeatures        serverFeatures;
        private XmppTransportState    state;
        private ITransport            transport;
        private ISaslMechanism        saslMechanism;
        private ContactList           people;
        private Activity              activity;
        private ClientCapabilities    capabilities;
        private EntityCapabilities    serverCapabilities;
        private PersonalEventing      personalEventing;
        private XmppTransportPresence presence;
        private bool                  isDisposed;

        // Message Subscriptions
        private ConcurrentDictionary<string, CompositeDisposable> subscriptions;

        /// <summary>
        /// Occurs when the connection state changes
        /// </summary>
        public IObservable<XmppTransportState> StateChanged
        {
            get { return this.stateChanged.AsObservable(); }
        }

        /// <summary>
        /// Occurs when the authentication process fails
        /// </summary>
        public IObservable<SaslAuthenticationFailure> AuthenticationFailed
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
        /// Gets the configuration of the message transport.
        /// </summary>
        public XmppTransportConfiguration Configuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating if the transport is active.
        /// </summary>
        public bool IsActive
        {
            get { return this.State != XmppTransportState.Closed; }
        }

        /// <summary>
        /// Gets a value indicating if the app is set as a notification provider.
        /// </summary>
        public bool IsAppSetAsNotificationProvider
        {
            get;
            private set;
        } = false;

        /// <summary>
        /// The friendly name for the transport.
        /// </summary>
        public string TransportFriendlyName
        {
            get { return "XMPP transport"; }
        }

        /// <summary>
        /// The ID of the transport.
        /// </summary>
        public string TransportId
        {
            get { return "3200721B-17E5-4DB6-B390-A79AAC9B19EB"; }
        }

        /// <summary>
        /// Gets the type of the message transport.
        /// </summary>
        public XmppTransportKind TransportKind
        {
            get { return XmppTransportKind.Custom; }
        }        

        /// <summary>
        /// Gets the roster instance associated to the client.
        /// </summary>
        public ContactList Contacts
        {
            get { return this.people; }
        }

        /// <summary>
        /// Gets the user activity instance associated to the client.
        /// </summary>
        public Activity Activity
        {
            get { return this.activity; }
        }

        /// <summary>
        /// Gets the personal eventing instance associated to the client.
        /// </summary>
        public PersonalEventing PersonalEventing
        {
            get { return this.personalEventing; }
        }

        /// <summary>
        /// Gets the presence instance associated to the client.
        /// </summary>
        public XmppTransportPresence Presence
        {
            get { return this.presence; }
        }

        /// <summary>
        /// Gets the server capabilities.
        /// </summary>
        /// <value>The server capabilities.</value>
        public IEntityCapabilitiesInfo ServerCapabilities
        {
            get { return this.serverCapabilities; }
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
        public XmppTransportState State
        {
            get { return this.state; }
            private set
            {
                this.state = value;
                this.stateChanged.OnNext(this.state);
            }
        }

        /// <summary>
        /// Gets or sets the string used to open the connection.
        /// </summary>
        public XmppConnectionString ConnectionString
        {
            get { return connectionString; }
            set
            {
                if (this.state != XmppTransportState.Closed)
                {
                    throw new XmppException("Connection should be closed");
                }
                connectionString = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppTransport"/> class.
        /// </summary>
        internal XmppTransport()
        {
            this.state                = XmppTransportState.Closed;
            this.stateChanged         = new Subject<XmppTransportState>();
            this.authenticationFailed = new Subject<SaslAuthenticationFailure>();
            this.infoQueryStream      = new Subject<InfoQuery>();
            this.messageStream        = new Subject<Message>();
            this.presenceStream       = new Subject<Presence>();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="XmppTransport"/> is reclaimed by garbage collection.
        /// </summary>
        ~XmppTransport()
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
                    this.CloseAsync().GetAwaiter().GetResult();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                this.connectionString   = null;
                this.userAddress        = null;
                this.saslMechanism      = null;
                this.people             = null;
                this.activity           = null;
                this.capabilities       = null;
                this.personalEventing   = null;
                this.presence           = null;
                this.serverCapabilities = null;
                this.serverFeatures     = ServerFeatures.None;
                this.state              = XmppTransportState.Closed;
                this.CloseTransport();
                this.DisposeSubscriptions();
                this.ReleaseSubjects();
            }

            this.isDisposed = true;
        }

        public IAsyncAction RequestSetAsNotificationProviderAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        public async Task OpenAsync()
        {
            if (this.ConnectionString == null)
            {
                throw new XmppException("ConnectionString cannot be null.");
            }
            if (this.State == XmppTransportState.Open)
            {
                throw new XmppException("Connection is already open.");
            }

            // Build user xmpp address
            this.userAddress = this.connectionString.ToXmppAddress();

            // Set the initial state
            this.State = XmppTransportState.Opening;

            // Connect to the server
            this.transport = new TcpTransport();

            // Event subscriptions
            this.InitializeSubscriptions();

            // Open the connection
            await this.transport
                      .OpenAsync(this.connectionString, this.Configuration.IgnorableServerCertificateErrors)
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            if (this.isDisposed || this.State == XmppTransportState.Closed || this.State == XmppTransportState.Closing)
            {
                return;
            }

            try
            {
                await SoftCloseAsync().ConfigureAwait(false);
            }
            catch
            {
            }
            finally
            {
                this.DisposeSubscriptions();

                this.transport          = null;
                this.saslMechanism      = null;
                this.connectionString   = null;
                this.userAddress        = null;
                this.people             = null;
                this.activity           = null;
                this.capabilities       = null;
                this.personalEventing   = null;
                this.presence           = null;
                this.serverCapabilities = null;
                this.serverFeatures     = ServerFeatures.None;

                this.ReleaseSubjects();
            }
        }

        public async Task SendAsync(InfoQuery request, Action<InfoQuery> onResponse = null, Action<InfoQuery> onError = null)
        {
            if (this.State != XmppTransportState.Open)
            {
                return;
            }

            IDisposable raction = null;
            IDisposable eaction = null;

            if (onResponse != null)
            {
                raction = this.InfoQueryStream
                              .Where(response => response.Id == request.Id && !response.IsError)
                              .Subscribe(onResponse);
            }

            if (eaction != null)
            {
                eaction = this.InfoQueryStream
                              .Where(response => response.Id == request.Id && response.IsError)
                              .Subscribe(onError);
            }

            var daction = this.InfoQueryStream
                              .Where(response => response.Id == request.Id)
                              .Subscribe(response => this.DisposeSubscription(response.Id));

            await this.SendAsync(request, raction, eaction, daction).ConfigureAwait(false);
        }

        public async Task SendAsync(Presence request, Action<Presence> onResponse = null, Action<Presence> onError = null)
        {
            if (this.State != XmppTransportState.Open)
            {
                return;
            }

            IDisposable raction = null;
            IDisposable eaction = null;

            if (onResponse != null)
            {
                raction = this.PresenceStream
                              .Where(response => response.Id == request.Id && !response.IsError)
                              .Subscribe(onResponse);
            }

            if (onError != null)
            {
                eaction = this.PresenceStream
                              .Where(message => message.Id == request.Id && message.IsError)
                              .Subscribe(onError);
            }

            var daction = this.PresenceStream
                              .Where(response => response.Id == request.Id)
                              .Subscribe(response => this.DisposeSubscription(response.Id));

            await this.SendAsync(request, raction, eaction, daction).ConfigureAwait(false);
        }

        public async Task SendAsync(Message request, Action<Message> onResponse = null, Action<Message> onError = null)
        {
            if (this.State != XmppTransportState.Open)
            {
                return;
            }

            IDisposable raction = null;
            IDisposable eaction = null;

            if (onResponse != null)
            {
                raction = this.MessageStream
                              .Where(response => response.Id == request.Id && !response.IsError)
                              .Subscribe(onResponse);
            }

            if (onError != null)
            {
                eaction = this.MessageStream
                              .Where(message => message.Id == request.Id && message.IsError)
                              .Subscribe(onError);
            }

            var daction = this.MessageStream
                              .Where(response => response.Id == request.Id)
                              .Subscribe(response => this.DisposeSubscription(response.Id));

            await this.SendAsync(request, raction, eaction, daction).ConfigureAwait(false);
        }

        public async Task SendAsync<T>(T request, IDisposable onResponse = null, IDisposable onError = null, IDisposable dispose = null)
            where T : class, IStanza
        {
            if (this.State != XmppTransportState.Open)
            {
                return;
            }

            this.AddSubscription(request.Id, onResponse, onError, dispose);

            await this.SendAsync(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a new message.
        /// </summary>
        public async Task SendAsync<T>()
            where T : class, new()
        {
            await this.SendAsync<T>(new T()).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a new message.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public async Task SendAsync<T>(T message)
            where T : class
        {
            await this.transport.SendAsync(XmppSerializer.Serialize(message)).ConfigureAwait(false);
        }

        internal void RequestTransportInitialization()
        {
            this.subscriptions      = new ConcurrentDictionary<string, CompositeDisposable>();
            this.people             = new ContactList();
            this.activity           = new Activity();
            this.capabilities       = new ClientCapabilities();
            this.personalEventing   = new PersonalEventing();
            this.presence           = new XmppTransportPresence();
            this.Configuration      = new XmppTransportConfiguration();
            this.serverCapabilities = new EntityCapabilities();
        }

        private void CloseTransport()
        {
            if (this.transport != null)
            {
                this.transport.Dispose();
                this.transport = null;
            }
        }

        private async Task SoftCloseAsync()
        {
            if (this.isDisposed || this.State == XmppTransportState.Closed || this.State == XmppTransportState.Closing)
            {
                return;
            }

            try
            {
                this.State = XmppTransportState.Closing;

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
                this.transport      = null;
                this.saslMechanism  = null;
                this.serverFeatures = ServerFeatures.None;
                this.State          = XmppTransportState.Closed;
            }
        }

        private async Task CloseStreamAsync()
        {
            await this.transport.SendAsync(XmppCodes.EndStream).ConfigureAwait(false);
        }

        private bool Supports(ServerFeatures feature)
        {
            return ((this.serverFeatures & feature) == feature);
        }

        private void InitializeSubscriptions()
        {
            this.AddSubscription(this.transport.StateChanged.Subscribe(state => OnTransportStateChanged(state)));
            this.AddSubscription(this.transport.MessageStream.Subscribe(message => OnMessageReceivedAsync(message)));
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

        private ISaslMechanism CreateSaslMechanism()
        {
            ISaslMechanism mechanism = null;

            if (this.Supports(ServerFeatures.SaslScramSha1))
            {
                mechanism = new SaslScramSha1Mechanism(this.ConnectionString);
            }
            else if (this.Supports(ServerFeatures.SaslDigestMD5))
            {
                mechanism = new SaslDigestMechanism(this.ConnectionString);
            }
            else if (this.Supports(ServerFeatures.SaslPlain))
            {
                mechanism = new SaslPlainMechanism(this.ConnectionString);
            }

            return mechanism;
        }

        private async void OnTransportStateChanged(TransportState state)
        {
            if (state == TransportState.ConnectionFailed)
            {
                await this.SoftCloseAsync().ConfigureAwait(false);
            }
            else if (state == TransportState.Closed)
            {
                if (this.State == XmppTransportState.Opening || this.State == XmppTransportState.Open)
                {
                    await this.SoftCloseAsync().ConfigureAwait(false);
                }
            }
        }

        private async void OnMessageReceivedAsync(StreamElement xmlMessage)
        {
            Debug.WriteLine($"SERVER <- {xmlMessage}");

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
            else if (fragment is SaslChallenge)
            {
                await this.SendAsync(this.saslMechanism.ProcessChallenge(fragment as SaslChallenge)).ConfigureAwait(false);
            }
            else if (fragment is SaslResponse)
            {
                await this.SendAsync(this.saslMechanism.ProcessResponse(fragment as SaslResponse)).ConfigureAwait(false);
            }
            else if (fragment is SaslSuccess)
            {
                await this.OnSaslSuccessAsync(fragment as SaslSuccess).ConfigureAwait(false);
            }
            else if (fragment is SaslFailure)
            {
                this.OnSaslFailure(fragment as SaslFailure);
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

                if (iq.Roster != null && iq.IsResult && this.Presence.IsOffline)
                {
                    await this.Presence.SetInitialPresenceAsync().ConfigureAwait(false);
                }
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
            this.serverFeatures = ServerFeatures.None;

            if (features.SecureConnectionRequired)
            {
                this.serverFeatures |= ServerFeatures.SecureConnection;
            }

            if (features.HasAuthMechanisms)
            {
                this.serverFeatures |= this.DiscoverAuthMechanisms(features);
            }

            if (features.SupportsResourceBinding)
            {
                this.serverFeatures |= ServerFeatures.ResourceBinding;
            }

            if (features.SupportsSessions)
            {
                this.serverFeatures |= ServerFeatures.Sessions;
            }

            if (features.SupportsInBandRegistration)
            {
                this.serverFeatures |= ServerFeatures.InBandRegistration;
            }

            if (features.SupportsEntityCapabilities)
            {
                this.serverFeatures |= ServerFeatures.EntityCapabilities;
                
                this.serverCapabilities.Address              = this.UserAddress.DomainName;
                this.serverCapabilities.ServiceDiscoveryNode = features.EntityCapabilities.DiscoveryNode;
            }

            await this.NegotiateStreamFeaturesAsync().ConfigureAwait(false);
        }

        private ServerFeatures DiscoverAuthMechanisms(StreamFeatures features)
        {
            var mechanisms = ServerFeatures.None;

            foreach (string mechanism in features.Mechanisms.Mechanism)
            {
                switch (mechanism)
                {
                    case XmppCodes.SaslGoogleXOAuth2Authenticator:
                        mechanisms |= ServerFeatures.SaslGoogleXOAuth2;
                        break;

                    case XmppCodes.SaslScramSha1Mechanism:
                        mechanisms |= ServerFeatures.SaslScramSha1;
                        break;

                    case XmppCodes.SaslDigestMD5Mechanism:
                        mechanisms |= ServerFeatures.SaslDigestMD5;
                        break;

                    case XmppCodes.SaslPlainMechanism:
                        mechanisms |= ServerFeatures.SaslPlain;
                        break;
                }
            }

            return mechanisms;
        }

        private async Task NegotiateStreamFeaturesAsync()
        {
            if (this.Supports(ServerFeatures.SecureConnection))
            {
                await this.OpenSecureConnectionAsync().ConfigureAwait(false);
            }
            else if (this.Supports(ServerFeatures.SaslGoogleXOAuth2)
                  || this.Supports(ServerFeatures.SaslScramSha1)
                  || this.Supports(ServerFeatures.SaslDigestMD5)
                  || this.Supports(ServerFeatures.SaslPlain))
            {
                await this.OnStartSaslNegotiationAsync().ConfigureAwait(false);
            }
            else if (this.Supports(ServerFeatures.ResourceBinding))
            {
                // Bind resource
                await this.OnBindResourceAsync().ConfigureAwait(false);
            }
            else if (this.Supports(ServerFeatures.Sessions))
            {
                await this.OnRequestSessionAsync().ConfigureAwait(false);
            }
            else
            {
                // No more features for negotiation set state as Open
                this.State = XmppTransportState.Open;

                // Discover Server Capabilities
                await this.DiscoverServerCapabilitiesAsync().ConfigureAwait(false);
            }
        }

        private async Task OpenSecureConnectionAsync()
        {
            await this.SendAsync<StartTls>().ConfigureAwait(false);
        }

        private async Task OnStartSaslNegotiationAsync()
        {
            this.State         = XmppTransportState.Authenticating;
            this.saslMechanism = this.CreateSaslMechanism();

            await this.SendAsync(this.saslMechanism.StartSaslNegotiation()).ConfigureAwait(false);
        }

        private async Task OnSaslSuccessAsync(SaslSuccess success)
        {
            if (this.saslMechanism.ProcessSuccess(success))
            {
                this.State         = XmppTransportState.Authenticated;
                this.saslMechanism = null;

                await this.transport.ResetStreamAsync().ConfigureAwait(false);
            }
            else
            {
                this.OnSaslFailure("Server reponse cannot be verified.");
            }
        }

        private void OnSaslFailure(SaslFailure failure)
        {
            this.OnSaslFailure(failure.GetErrorMessage());
        }

        private async void OnSaslFailure(string message)
        {
            var errorMessage = $"Authentication failed ({message})";

            this.authenticationFailed.OnNext(new SaslAuthenticationFailure(errorMessage));

            this.State = XmppTransportState.AuthenticationFailure;

            await this.SoftCloseAsync().ConfigureAwait(false);
        }

        private async Task OnBindResourceAsync()
        {
            if (!this.Supports(ServerFeatures.ResourceBinding))
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
            this.serverFeatures = this.serverFeatures & (~ServerFeatures.ResourceBinding);

            // Continue feature negotiation
            await this.NegotiateStreamFeaturesAsync().ConfigureAwait(false);
        }

        private async Task OnRequestSessionAsync()
        {
            if (!this.Supports(ServerFeatures.Sessions))
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
            this.serverFeatures = this.serverFeatures & (~ServerFeatures.Sessions);

            // Continue feature negotiation
            await this.NegotiateStreamFeaturesAsync().ConfigureAwait(false);
        }

        private async Task DiscoverServerCapabilitiesAsync()
        {
            if (!String.IsNullOrEmpty(this.serverCapabilities.ServiceDiscoveryNode))
            {
                await this.serverCapabilities.DiscoverAsync().ConfigureAwait(false);
            }
        }

        private void AddSubscription(IDisposable onMessage)
        {
            var subscription = new CompositeDisposable();

            subscription.Add(onMessage);

            this.subscriptions.TryAdd(IdentifierGenerator.Generate(), subscription);
        }

        private void AddSubscription(string messageId
                                   , IDisposable onResponse
                                   , IDisposable onError
                                   , IDisposable onDispose)
        {
            var subscription = new CompositeDisposable();

            if (onResponse != null)
            {
                subscription.Add(onResponse);
            }
            if (onError != null)
            {
                subscription.Add(onError);
            }
            subscription.Add(onDispose);

            this.subscriptions.TryAdd(messageId, subscription);
        }

        private void DisposeSubscriptions()
        {
            if (!this.subscriptions.IsEmpty)
            {
                foreach (var pair in this.subscriptions)
                {
                    pair.Value.Dispose();
                }

                this.subscriptions.Clear();
            }
        }

        private void DisposeSubscription(string messageId)
        {
            if (this.subscriptions.ContainsKey(messageId))
            {
                CompositeDisposable subscription = null;

                this.subscriptions.TryRemove(messageId, out subscription);

                if (subscription != null)
                {
                    subscription.Dispose();
                }
            }
        }
    }
}
