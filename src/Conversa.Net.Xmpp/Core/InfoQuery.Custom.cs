// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Xml.Serialization;

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Info/Query (IQ)
    /// <remarks>
    /// </summary>
    /// RFC 6120: XMPP Core
    /// </remarks>
    public partial class InfoQuery
        : IStanza
	{
        /// <summary>
        /// Gets a value indicating whether the current instance is a IQ error 
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsError
        {
            get { return this.Type == InfoQueryType.Error; }
        }

        /// <summary>
        /// Gets a value indicating whether the current instance is a result IQ
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsResult
        {
            get { return this.Type == InfoQueryType.Result; }
        }

        /// <summary>
        /// Gets a value indicating whether the current instance is a request IQ
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsRequest
        {
            get { return this.Type == InfoQueryType.Get; }
        }

        /// <summary>
        /// Gets a value indicating whether the current instance is a update IQ
        /// </summary>
        [XmlIgnoreAttribute]
        public bool IsUpdate
        {
            get { return this.Type == InfoQueryType.Set; }
        }
        
        /// <summary>
        /// Returns a new IQ Stanza configured as a response to the current IQ
        /// </summary>
        /// <returns>The IQ response</returns>
		public InfoQuery AsResponse()
		{
            return new InfoQuery
            {
                Id   = this.Id
              , To   = this.From
              , From = this.To
              , Type = InfoQueryType.Result
            };
		}
    }
}
