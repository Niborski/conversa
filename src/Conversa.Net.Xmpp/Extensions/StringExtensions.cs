// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Xml;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography.Core;

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
            using (var stream = new MemoryStream())
            {
                foreach (string value in values)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }

                return stream.ToArray().ComputeMD5Hash();
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
            using (var stream = new MemoryStream())
            {
                foreach (string value in values)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }

                return stream.ToArray().ComputeSHA1Hash();
            }
        }

        /// <summary>
        /// password-based key derivation functionality, PBKDF2, by using a pseudo-random number generator based on HMACSHA1.
        /// </summary>
        /// <param name="password">The password used to derive the key.</param>
        /// <param name="salt">The key salt used to derive the key.</param>
        /// <param name="iterations">The number of iterations for the operation.</param>
        /// <returns>The generated pseudo-random key.</returns>
        public static byte[] Rfc2898DeriveBytes(this string password, byte[] salt, uint iterations, uint cb)
        {
            var alg       = KeyDerivationAlgorithmProvider.OpenAlgorithm(KeyDerivationAlgorithmNames.Pbkdf2Sha1);
            var cryptoKey = alg.CreateKey(XmppEncoding.Utf8.GetBytes(password).AsBuffer());
            var keyParams = KeyDerivationParameters.BuildForPbkdf2(salt.AsBuffer(), iterations);

            return CryptographicEngine.DeriveKeyMaterial(cryptoKey, keyParams, cb).ToArray();
        }

        /// <summary>
        /// Converts the given string to Base 64.
        /// </summary>
        /// <param name="source">The string to be converted.</param>
        /// <returns>The base 64 representation of the given string.</returns>
        public static string ToBase64String(this string source)
        {
            return XmppEncoding.Utf8.GetBytes(source).ToBase64String();
        }
    }
}
