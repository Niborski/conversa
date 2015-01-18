// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using PCLCrypto;
using System.IO;
using System.Text;

namespace System
{
    /// <summary>
    /// System.String extension methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Computes the MD5 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeMD5Hash(this string[] values)
        {
            var hashAlgorithm = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Md5);

            using (var stream = new MemoryStream())
            {
                foreach (string value in values)
                {
                    if (value != null)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }

                return hashAlgorithm.HashData(stream.ToArray());
            }
        }

        /// <summary>
        /// Computes the SHA1 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA1Hash(this string value)
        {
            return Encoding.UTF8.GetBytes(value).ComputeSHA1Hash();
        }

        /// <summary>
        /// Computes the SHA1 hash of a given array of strings
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static byte[] ComputeSHA1Hash(this string[] values)
        {
            var hashAlgorithm = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);

            using (var stream = new MemoryStream())
            {
                foreach (string value in values)
                {
                    if (value != null)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }

                return hashAlgorithm.HashData(stream.ToArray());
            }
        }
    }
}
