// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Text;

namespace Conversa.Net.Xmpp.Xml
{
	/// <summary>
	/// XMPP stream  character encoding
	/// </summary>
	public static class XmppEncoding
	{
		public static readonly Encoding Utf8 = new UTF8Encoding(false);
	}
}
