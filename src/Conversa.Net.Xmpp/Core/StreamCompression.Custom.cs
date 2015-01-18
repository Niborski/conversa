// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Core
{

    /// <summary>
    /// Stream Compression
    /// </summary>
    /// <remarks>
    /// XEP-0138 Stream Compression
    /// </remarks>
    public partial class StreamCompression
    {
        public static StreamCompression Create(string method)
        {
            var compression = new StreamCompression();

            compression.Methods.Add(method);

            return compression;
        }
    }
}
