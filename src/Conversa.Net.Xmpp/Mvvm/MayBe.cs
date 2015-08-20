// Copyright (c) 2000-2014 Developer Express Inc.
// Licensed under the The MIT License (MIT).
// --------------------------------------------------
// https://github.com/DevExpress/DevExpress.Mvvm.Free
// --------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DevExpress.Mvvm.Native
{
    [DebuggerStepThrough]
    public static class MayBe
    {
        public static TR With<TI, TR>(this TI input, Func<TI, TR> evaluator)
            where TI : class
            where TR : class
        {
            if (input == null)
                return null;
            return evaluator(input);
        }
        public static TR WithString<TR>(this string input, Func<string, TR> evaluator)
            where TR : class
        {
            if (string.IsNullOrEmpty(input))
                return null;
            return evaluator(input);
        }
        public static TR Return<TI, TR>(this TI? input, Func<TI?, TR> evaluator, Func<TR> fallback) where TI : struct
        {
            if (!input.HasValue)
                return fallback != null ? fallback() : default(TR);
            return evaluator(input.Value);
        }
        public static TR Return<TI, TR>(this TI input, Func<TI, TR> evaluator, Func<TR> fallback) where TI : class
        {
            if (input == null)
                return fallback != null ? fallback() : default(TR);
            return evaluator(input);
        }
        public static bool ReturnSuccess<TI>(this TI input) where TI : class
        {
            return input != null;
        }
        public static TI If<TI>(this TI input, Func<TI, bool> evaluator) where TI : class
        {
            if (input == null)
                return null;
            return evaluator(input) ? input : null;
        }
        public static TI IfNot<TI>(this TI input, Func<TI, bool> evaluator) where TI : class
        {
            if (input == null)
                return null;
            return evaluator(input) ? null : input;
        }
        public static TI Do<TI>(this TI input, Action<TI> action) where TI : class
        {
            if (input == null)
                return null;
            action(input);
            return input;
        }
    }
    public static class DictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValueDelegate)
        {
            TValue result;
            if (!dictionary.TryGetValue(key, out result))
            {
                dictionary[key] = (result = createValueDelegate());
            }
            return result;
        }
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue result;
            dictionary.TryGetValue(key, out result);
            return result;
        }
    }
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T t in source)
                action(t);
        }
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (T t in source)
                action(t, index++);
        }
        public static void ForEach<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Action<TFirst, TSecond> action)
        {
            var en1 = first.GetEnumerator();
            var en2 = second.GetEnumerator();
            while (en1.MoveNext() && en2.MoveNext())
            {
                action(en1.Current, en2.Current);
            }
        }
        public static IEnumerable<T> Unfold<T>(T seed, Func<T, T> next, Func<T, bool> stop)
        {
            for (var current = seed; !stop(current); current = next(current))
            {
                yield return current;
            }
        }
        public static IEnumerable<T> Yield<T>(this T singleElement)
        {
            yield return singleElement;
        }
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getItems)
        {
            return source.SelectMany(item => item.Yield().Concat(getItems(item).Flatten(getItems)));
        }
        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;
            return source.Aggregate((x, y) => comparer.Compare(keySelector(x), keySelector(y)) < 0 ? x : y);
        }
        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;
            return source.Aggregate((x, y) => comparer.Compare(keySelector(x), keySelector(y)) > 0 ? x : y);
        }
    }
}