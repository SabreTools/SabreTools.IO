using System;
using System.Collections.Generic;

#if NET40

namespace SabreTools.IO
{
    internal delegate U LinqOrderByDelegate<T, U>(T str);

    internal static partial class EnumerationExtensions
    {
        public static IEnumerable<T> OrderBy<T, U>(this IEnumerable<T> arr, LinqOrderByDelegate<T, U> func)
        {
            // TODO: Implement ordering
            return arr;
        }
    }
}

#endif