// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// SASL Authentication
    /// </summary>
    /// <remarks>
    /// RFC 6120: XMPP Core
    /// </remarks>
    public partial class SaslMechanisms
    {
		/// <summary>
		/// Gets a value indicating whether SASL auth mechanisms has been informed
		/// </summary>
		[XmlIgnoreAttribute]
		public bool HasMechanisms
        {
            get { return !this.Mechanism.IsEmpty(); }
        }
    }
}
