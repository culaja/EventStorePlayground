using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static IReadOnlyList<TK> Map<T, TK>(this IEnumerable<T> enumerable, Func<T, TK> transformer) =>
            enumerable.Select(transformer).ToList();
        
        public static IReadOnlyList<TResult> GroupBy<TSource, TKey, TElement, TResult>(
            this IReadOnlyList<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IReadOnlyList<TElement>, TResult> resultSelector) =>
            (source as IEnumerable<TSource>).GroupBy<TSource, TKey, TElement, TResult>(
                keySelector,
                elementSelector,
                (k ,e) => resultSelector(k, e.ToList()))
            .ToList();

        public static IReadOnlyList<T> Flatten<T>(this IReadOnlyList<IReadOnlyList<T>> source) =>
            source.SelectMany(t => t).ToList();
    }
}