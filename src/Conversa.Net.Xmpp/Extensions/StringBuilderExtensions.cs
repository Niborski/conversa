// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace System.Text
{
    /// <summary>
    /// System.Text.StringBuilder extension methods
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Computes the SHA1 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA1Hash(this StringBuilder value)
        {
            return Encoding.UTF8.GetBytes(value.ToString()).ComputeSHA1Hash();
        }
    }
}
