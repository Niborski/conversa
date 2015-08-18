// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Xml;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;

namespace Conversa.Net.Xmpp.Client.Authentication
{
    /// <summary>
    /// SASL SCRAM-SHA-1 authentication mechanism.
    /// </summary>
    /// <remarks>
    /// References:
    ///     https://tools.ietf.org/html/rfc5802
    /// </remarks>
    internal sealed class SaslScramSha1Mechanism
        : ISaslMechanism
    {
        private static string Normalize(string value)
        {
            return Saslprep.SaslPrep(value);
        }

        private static byte[] GenerateRandomBytes()
        {
            return CryptographicBuffer.GenerateRandom(32).ToArray();
        }

        private XmppConnectionString connectionString;
        private string               clientFirstMessageBare;
        private string               serverSignature;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SaslScramSha1Mechanism"/> class.
        /// </summary>
        public SaslScramSha1Mechanism(XmppConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Starts the SASL negotiation process.
        /// </summary>
        /// <returns>
        /// A SASL auth instance.
        /// </returns>
        public SaslAuth StartSaslNegotiation()
        {
            this.clientFirstMessageBare = "n=" + this.connectionString.UserAddress.UserName + ","
                                        + "r=" + GenerateRandomBytes().ToBase64String();

            return new SaslAuth
            {
                Mechanism = XmppCodes.SaslScramSha1Mechanism
              , Value     = ($"n,,{this.clientFirstMessageBare}").ToBase64String()
            };
        }

        /// <summary>
        /// Process the SASL challenge message.
        /// </summary>
        /// <param name="challenge">The server challenge.</param>
        /// <returns>
        /// The challenge response.
        /// </returns>
        /// <remarks>
        /// SaltedPassword  := Hi(Normalize(password), salt, i)
        /// ClientKey       := HMAC(SaltedPassword, "Client Key")
        /// StoredKey       := H(ClientKey)
        /// AuthMessage     := client-first-message-bare + "," +
        ///                    server-first-message + "," +
        ///                    client-final-message-without-proof
        /// ClientSignature := HMAC(StoredKey, AuthMessage)
        /// ClientProof     := ClientKey XOR ClientSignature
        /// ServerKey       := HMAC(SaltedPassword, "Server Key")
        /// ServerSignature := HMAC(ServerKey, AuthMessage)
        /// </remarks>
        public SaslResponse ProcessChallenge(SaslChallenge challenge)
        {
            var password             = Normalize(this.connectionString.UserPassword);
            var decoded              = Convert.FromBase64String(challenge.Value);
            var serverFirstMessage   = XmppEncoding.Utf8.GetString(decoded, 0, decoded.Length);
            var tokens               = SaslTokenizer.ToDictionary(serverFirstMessage);
            var snonce               = tokens["r"];
            var ssalt                = Convert.FromBase64String(tokens["s"]);
            var ssaltSize            = Convert.ToUInt32(tokens["i"]);
            var clientFinalMessageWP = $"c={XmppEncoding.Utf8.GetBytes("n,,").ToBase64String()},r={snonce}";
            var saltedPassword       = password.Rfc2898DeriveBytes(ssalt, ssaltSize, 20);
            var clientKey            = saltedPassword.ComputeHmacSha1("Client Key");
            var storedKey            = clientKey.ComputeSHA1Hash();
            var authMessage          = $"{this.clientFirstMessageBare},{serverFirstMessage},{clientFinalMessageWP}";
            var clientSignature      = storedKey.ComputeHmacSha1(authMessage);
            var clientProof          = clientKey.Xor(clientSignature);
            var clientFinalMessage   = $"{clientFinalMessageWP},p={clientProof.ToBase64String()}";
            var serverKey            = saltedPassword.ComputeHmacSha1("Server Key");
            this.serverSignature     = serverKey.ComputeHmacSha1(authMessage).ToBase64String();
            
            return new SaslResponse { Value = clientFinalMessage.ToBase64String() };
        }

        /// <summary>
        /// Process the SASL reponse message.
        /// </summary>
        /// <param name="response">The server reponse</param>
        /// <returns>
        /// The client response.
        /// </returns>
        public SaslResponse ProcessResponse(SaslResponse response)
        {
            return null;
        }

        /// <summary>
        /// Verifies the SASL success message if needed.
        /// </summary>
        /// <param name="success">The server success response</param>
        /// <returns>
        ///   <b>true</b> if the reponse has been verified; otherwise <b>false</b>
        /// </returns>
        public bool ProcessSuccess(SaslSuccess success)
        {
            // The server verifies the nonce and the proof, verifies that the
            // authorization identity (if supplied by the client in the first
            // message) is authorized to act as the authentication identity, and,
            // finally, it responds with a "server-final-message", concluding the
            // authentication exchange.
            var decoded            = Convert.FromBase64String(success.Value);
            var serverFinalMessage = XmppEncoding.Utf8.GetString(decoded, 0, decoded.Length);
            var tokens             = SaslTokenizer.ToDictionary(serverFinalMessage);
            var serverSignature    = tokens["v"];

            return (serverSignature == this.serverSignature);
        }
    }
}
