// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace System.Collections.Generic
{
    public static class ListOfTExtensions
    {
        public static bool IsEmpty<T>(this List<T> list)
        {
            return (list == null || list.Count == 0);
        }

        public static bool IsEmpty<T>(this IList<T> list)
        {
            return (list == null || list.Count == 0);
        }
    }
}
