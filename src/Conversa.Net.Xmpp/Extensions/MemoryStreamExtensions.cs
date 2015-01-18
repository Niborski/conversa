// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace System.IO
{
    /// <summary>
    /// MemoryStream extension methods
    /// </summary>
    public static class MemoryStreamExtensions
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XmppMemoryStream"/> reached EOF.
        /// </summary>
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public static bool EOF(this MemoryStream stream)
        {
            return (stream.Position >= stream.Length);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public static void Clear(this MemoryStream stream)
        {
            stream.SetLength(0);
            stream.Position = 0;
        }
    }
}
