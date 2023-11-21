using System.Collections.Generic;
using System.Text.RegularExpressions;

#if NET20

namespace SabreTools.IO
{
    internal static partial class EnumerationExtensions
    {
        internal delegate U LinqSelectDelegate<T, U>(T str);

        internal delegate bool LinqWhereDelegate<T>(T str);

        public static bool Any<T>(this IEnumerable<T> arr)
        {
            return arr.Count() != 0;
        }

        public static IEnumerable<T> Cast<T>(this MatchCollection arr)
        {
            foreach (var val in arr)
            {
                if (val is not T castVal)
                    throw new System.ArrayTypeMismatchException();

                yield return castVal;
            }
        }

        public static int Count<T>(this IEnumerable<T> arr)
        {
            int counter = 0;
            foreach (var val in arr)
            {
                counter++;
            }

            return counter;
        }
        public static IEnumerable<U> Select<T, U>(this IEnumerable<T> arr, LinqSelectDelegate<T, U> func)
        {
            foreach (var val in arr)
            {
                yield return func(val);
            }
        }

        public static bool SequenceEqual<T>(this IEnumerable<T> arr1, IEnumerable<T> arr2)
        {
            if (arr1 == null || arr2 == null)
                return false;

            if (arr1.Count() != arr2.Count())
                return false;

            var arr1Array = arr1.ToArray();
            var arr2Array = arr2.ToArray();

            for (int i = 0; i < arr1Array.Length; i++)
            {
                if (!arr1Array[i]?.Equals(arr2Array[i]) != true)
                    return false;
            }

            return true;
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> arr, int skip)
        {
            int counter = 0;
            foreach (var val in arr)
            {
                if (counter++ < skip)
                    continue;

                yield return val;
            }
        }

        public static T[] ToArray<T>(this IEnumerable<T> arr)
        {
            return [.. arr];
        }

        public static List<T> ToList<T>(this IEnumerable<T> arr)
        {
            return [.. arr];
        }
        public static IEnumerable<T> Where<T>(this T[] arr, LinqWhereDelegate<T> func)
        {
            foreach (var val in arr)
            {
                if (func(val))
                    yield return val;
            }
        }
        public static IEnumerable<T> Where<T>(this IEnumerable<T> arr, LinqWhereDelegate<T> func)
        {
            foreach (var val in arr)
            {
                if (func(val))
                    yield return val;
            }
        }
    }
}

#endif

#if NET20 || NET35 || NET40

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