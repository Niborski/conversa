// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace System.Collections.Concurrent
{
    /// <summary>
    /// Extension methods for ConcurrentBag class
    /// </summary>
    public static class ConcurrentBagExtensions
    {
        public static void Clear<T>(this ConcurrentBag<T> bag)
        {
            if (bag != null && !bag.IsEmpty)
            {
                T item = default(T);

                while (!bag.IsEmpty)
                {
                    bag.TryTake(out item);
                }
            }
        }
    }
}
