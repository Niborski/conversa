// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System.Globalization;

namespace System
{
    public static class StringExtensions
    {
        public static bool CultureAwareCompare(this string strA, string strB)
        {
            var options  = CompareOptions.IgnoreSymbols;
            var comparer = CultureInfo.CurrentCulture.CompareInfo;

            return (comparer.Compare(strA, strB, options) == 0 ? true : false);
        }
    }
}
