// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using PCLCrypto;
using System.Text;

namespace System
{
    /// <summary>
    /// Byte array extension methods
    /// </summary>
    public static class BufferExtensions
    {
        /// <summary>
        /// Computes the MD5 hash of a given byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeMD5Hash(this byte[] buffer)
        {
            var hashAlgorithm = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Md5);

            return hashAlgorithm.HashData(buffer);
        }

        /// <summary>
        /// Computes the SHA1 hash of a given byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA1Hash(this byte[] buffer)
        {
            var hashAlgorithm = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);

            return hashAlgorithm.HashData(buffer);
        }

        /// <summary>
        /// Converts a given byte array to a base-64 string
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToBase64String(this byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Convert a byte array to an hex string
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] buffer)
        {
            StringBuilder hex = new StringBuilder();

            for (int i = 0; i < buffer.Length; i++)
            {
                hex.Append(buffer[i].ToString("x2"));
            }

            return hex.ToString();
        }
    }
}
