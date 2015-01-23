// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

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
            var sha1 = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var hash = sha1.HashData(CryptographicBuffer.CreateFromByteArray(buffer));

            return hash.ToArray();
        }

        /// <summary>
        /// Computes the SHA1 hash of a given byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA1Hash(this byte[] buffer)
        {
            var sha1 = HashAlgorithmProvider.OpenAlgorithm (HashAlgorithmNames.Sha1);
            var hash = sha1.HashData(buffer.AsBuffer());
            // var hash = sha1.HashData(CryptographicBuffer.CreateFromByteArray(buffer));

            return hash.ToArray();
        }

        /// <summary>
        /// HMAC(key, str)  := Apply the HMAC keyed hash algorithm (defined in [RFC2104])
        /// </summary>
        public static byte[] ComputeHmacSha1(this byte[] keyMaterial, string value)
        {
            var mac         = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            var cryptoKey   = mac.CreateKey(keyMaterial.AsBuffer());
            var strMaterial = CryptographicBuffer.ConvertStringToBinary(value, BinaryStringEncoding.Utf8);

            return CryptographicEngine.Sign(cryptoKey, strMaterial).ToArray();
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

        public static byte[] Xor(this byte[] buffer1, byte[] buffer2)
        {
            var buffer = new byte[buffer1.Length];

            for (int i = 0; i < buffer1.Length; i++)
            {
                buffer[i] = (byte)(buffer1[i] ^ buffer2[i]);
            }

            return buffer;
        }
    }
}
