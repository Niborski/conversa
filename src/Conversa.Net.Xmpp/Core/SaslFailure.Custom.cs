// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System;

    /// <summary>
    /// SASL Authentication
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    public partial class SaslFailure
    {
        public string GetErrorMessage()
        {
            var result = String.Empty;

            switch (this.FailureType)
            {
                case SaslFailureType.Aborted:
                    result = "aborted";
                    break;

                case SaslFailureType.AccountDisabled:
                    result = "account-disabled";
                    break;

                case SaslFailureType.CredentialsExpired:
                    result = "credentials-expired";
                    break;

                case SaslFailureType.EncryptionRequired:
                    result = "encryption-required";
                    break;

                case SaslFailureType.IncorrectEncoding:
                    result = "incorrect-encoding";
                    break;

                case SaslFailureType.InvalidAuthzid:
                    result = "invalid-authzid";
                    break;

                case SaslFailureType.InvalidMechanism:
                    result = "invalid-mechanism";
                    break;

                case SaslFailureType.MalformedRequest:
                    result = "malformed-request";
                    break;

                case SaslFailureType.MechanismTooWeak:
                    result = "mechanism-too-weak";
                    break;

                case SaslFailureType.NotAuthorized:
                    result = "not-authorized";
                    break;

                case SaslFailureType.TemporaryAuthFailure:
                    result = "temporary-auth-failure";
                    break;

                case SaslFailureType.TransitionNeeded:
                    result = "transition-needed";
                    break;
            }

            return result + " " + ((String.IsNullOrEmpty(this.Text.Value) ? String.Empty : this.Text.Value));
        }
    }
}
