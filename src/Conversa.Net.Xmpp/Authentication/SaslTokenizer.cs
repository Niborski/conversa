// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Conversa.Net.Xmpp.Authentication
{
    internal static class SaslTokenizer
    {
        private static readonly Regex SaslRegex = new Regex
        (
            @"(?<key>[\w\s\d^=])=(?<value>[^,]*)", RegexOptions.Singleline | RegexOptions.ExplicitCapture
        );

        internal static Dictionary<string, string> ToDictionary(string value)
        {
            var kvp     = new Dictionary<string, string>();
            var buffer  = Convert.FromBase64String(value);
            var matches = SaslRegex.Matches(Encoding.UTF8.GetString(buffer, 0, buffer.Length));

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    kvp.Add(match.Groups["key"].Value, match.Groups["value"].Value);
                }
            }

            return kvp;
        }
    }
}
