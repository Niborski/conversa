// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Client;
using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Shared;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;

namespace Conversa.Net.Xmpp.Authentication
{
    /// <summary>
    /// SASL SCRAM-SHA-1 authentication mechanism.
    /// </summary>
    /// <remarks>
    /// References:
    ///     http://www.ietf.org/rfc/rfc4616.txt
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
            return CryptographicBuffer.GenerateRandom(40).ToArray();
        }

        public static string ToBase64String(string source)
        {
            return Encoding.UTF8.GetBytes(source).ToBase64String();
        }

        private XmppConnectionString connectionString;
        private string               clientFirstMessageBare;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SaslScramSha1Mechanism"/> class.
        /// </summary>
        public SaslScramSha1Mechanism(XmppConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public SaslAuth StartSaslNegotiation()
        {
            this.clientFirstMessageBare = "n=" + this.connectionString.UserAddress.UserName + ","
                                        + "r=" + GenerateRandomBytes().ToBase64String();

            return new SaslAuth
            {
                Mechanism = XmppCodes.SaslScramSha1Mechanism
              , Value     = ToBase64String("n,," + this.clientFirstMessageBare)
            };
        }

        /// <summary>
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
        /// </summary>
        public SaslResponse ProcessChallenge(SaslChallenge challenge)
        {
            var password             = Normalize(this.connectionString.UserPassword);
            var decoded              = Convert.FromBase64String(challenge.Value);
            var serverFirstMessage   = XmppEncoding.Utf8.GetString(decoded, 0, decoded.Length);
            var tokens               = SaslTokenizer.ToDictionary(serverFirstMessage);
            var snonce               = tokens["r"];
            var ssalt                = Convert.FromBase64String(tokens["s"]);
            var ssaltSize            = Convert.ToUInt32(tokens["i"]);
            var saltedPassword       = password.Rfc2898DeriveBytes(ssalt, ssaltSize, 20);
            var clientKey            = saltedPassword.ComputeHmacSha1("Client Key");
            var storedKey            = clientKey.ComputeSHA1Hash();
            var clientFinalMessageWP = "c=" + XmppEncoding.Utf8.GetBytes("n,,").ToBase64String() + ",r=" + snonce;
            var authMessage          = this.clientFirstMessageBare
                                     + "," + serverFirstMessage
                                     + "," + clientFinalMessageWP;
            var clientSignature      = storedKey.ComputeHmacSha1(authMessage);
            var clientProof          = clientKey.Xor(clientSignature);
            var clientFinalMessage   = clientFinalMessageWP + ",p=" + clientProof.ToBase64String();

            return new SaslResponse { Value = clientFinalMessage };
        }

        public SaslResponse ProcessResponse(SaslResponse response)
        {
            // The server verifies the nonce and the proof, verifies that the
            // authorization identity (if supplied by the client in the first
            // message) is authorized to act as the authentication identity, and,
            // finally, it responds with a "server-final-message", concluding the
            // authentication exchange.

            // S: v=rmF9pqV8S7suAoZWja4dJRkFsKQ=
#warning TODO: Verify server response

            return null;
        }
    }
}
