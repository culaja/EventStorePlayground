using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static IReadOnlyList<TK> Map<T, TK>(this IEnumerable<T> enumerable, Func<T, TK> transformer) =>
            enumerable.Select(transformer).ToList();
    }
}