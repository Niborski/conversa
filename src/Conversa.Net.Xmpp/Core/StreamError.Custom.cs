// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System;
    using System.Text;

    /// <summary>
    /// XML Streams
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP core
    /// </remarks>
    public partial class StreamError
    {
        public string GetErrorMessage()
        {
            StringBuilder exceptionMessage = new StringBuilder();

            if (this.BadFormat != null)
            {
                exceptionMessage.Append("bad-format");
            }
            else if (this.BadNamespacePrefix != null)
            {
                exceptionMessage.Append("bad-namespace-prefix");
            }
            else if (this.Conflict != null)
            {
                exceptionMessage.Append("conflict");
            }
            else if (this.ConnectionTimeout != null)
            {
                exceptionMessage.Append("connection-timeout");
            }
            else if (this.HostGone != null)
            {
                exceptionMessage.Append("host-gone");
            }
            else if (this.HostUnknown != null)
            {
                exceptionMessage.Append("host-unknown");
            }
            else if (this.ImproperAddressing != null)
            {
                exceptionMessage.Append("improper-addressing");
            }
            else if (this.InternalServerError != null)
            {
                exceptionMessage.Append("internal-server-error");
            }
            else if (this.InvalidFrom != null)
            {
                exceptionMessage.Append("invalid-from");
            }
            else if (this.InvalidID != null)
            {
                exceptionMessage.Append("invalid-id");
            }
            else if (this.InvalidNamespace != null)
            {
                exceptionMessage.Append("invalid-namespace");
            }
            else if (this.InvalidXml != null)
            {
                exceptionMessage.Append("invalid-xml");
            }
            else if (this.NotAuthorized != null)
            {
                exceptionMessage.Append("not-authorized");
            }
            else if (this.PolicyViolation != null)
            {
                exceptionMessage.Append("policy-violation");
            }
            else if (this.RemoteConnectionFailed != null)
            {
                exceptionMessage.Append("remote-connection-failed");
            }
            else if (this.ResourceConstraint != null)
            {
                exceptionMessage.Append("resource-constraint");
            }
            else if (this.RestrictedXml != null)
            {
                exceptionMessage.Append("restricted-xml");
            }
            else if (this.SeeOtherHost != null)
            {
                exceptionMessage.Append("see-other-host");
            }
            else if (this.SystemShutdown != null)
            {
                exceptionMessage.Append("system-shutdown");
            }
            else if (this.UndefinedCondition != null)
            {
                exceptionMessage.Append("undefined-condition");
            }
            else if (this.UnsupportedEncoding != null)
            {
                exceptionMessage.Append("unsupported-encoding");
            }
            else if (this.UnsupportedStanzaType != null)
            {
                exceptionMessage.Append("unsupported-stanza-type");
            }
            else if (this.UnsupportedVersion != null)
            {
                exceptionMessage.Append("unsupported-version");
            }
            else if (this.XmlNotWellFormed != null)
            {
                exceptionMessage.Append("xml-not-well-formed");
            }

            if (this.Text != null && this.Text.Value != null)
            {
                exceptionMessage.AppendFormat("{0}{1}", Environment.NewLine, this.Text.Value);
            }

            return exceptionMessage.ToString();
        }
    }
}
