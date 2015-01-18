// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Xml.Serialization;

    /// <summary>
    /// SASL Authentication
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    [XmlTypeAttribute(Namespace = "urn:ietf:params:xml:ns:xmpp-sasl", IncludeInSchema = false)]
    public enum SaslFailureType
    {
        /// <remarks/>
        [XmlEnumAttribute("aborted")]
        Aborted,

        /// <remarks/>
        [XmlEnumAttribute("account-disabled")]
        AccountDisabled,

        /// <remarks/>
        [XmlEnumAttribute("credentials-expired")]
        CredentialsExpired,

        /// <remarks/>
        [XmlEnumAttribute("encryption-required")]
        EncryptionRequired,

        /// <remarks/>
        [XmlEnumAttribute("incorrect-encoding")]
        IncorrectEncoding,

        /// <remarks/>
        [XmlEnumAttribute("invalid-authzid")]
        InvalidAuthzid,

        /// <remarks/>
        [XmlEnumAttribute("invalid-mechanism")]
        InvalidMechanism,

        /// <remarks/>
        [XmlEnumAttribute("malformed-request")]
        MalformedRequest,

        /// <remarks/>
        [XmlEnumAttribute("mechanism-too-weak")]
        MechanismTooWeak,

        /// <remarks/>
        [XmlEnumAttribute("not-authorized")]
        NotAuthorized,

        /// <remarks/>
        [XmlEnumAttribute("temporary-auth-failure")]
        TemporaryAuthFailure,

        /// <remarks/>
        [XmlEnumAttribute("transition-needed")]
        TransitionNeeded,
    }
}
