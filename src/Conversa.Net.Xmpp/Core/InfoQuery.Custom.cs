// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{

    /// <summary>
    /// Info/Query (IQ)
    /// <remarks>
    /// </summary>
    /// RFC 6120: XMPP Core
    /// </remarks>
    public partial class InfoQuery
	{
        public static InfoQuery Create()
        {
            return new InfoQuery
            {
                Id   = IdentifierGenerator.Generate()
              , Type = InfoQueryType.Get
            };
        }

		public InfoQuery ResponseTo(string messageId)
		{
            this.Id   = messageId;
            this.Type = InfoQueryType.Result;

			return this;
		}

        public InfoQuery ForRequest()
        {
            this.Type = InfoQueryType.Get;

            return this;
        }

		public InfoQuery ForUpdate()
		{
            this.Type = InfoQueryType.Set;

			return this;
		}

        public InfoQuery FromAddress(XmppAddress from)
		{
			this.From = from;

			return this;
		}

        public InfoQuery ToAddress(XmppAddress to)
		{
			this.To = to;

			return this;
		}

		public InfoQuery AsReponse()
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
