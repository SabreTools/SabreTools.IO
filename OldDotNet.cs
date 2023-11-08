using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#if NET20

namespace System.Linq
{
    public delegate U LinqSelectDelegate<T, U>(T str);

    public delegate bool LinqWhereDelegate<T>(T str);
    
    public static partial class EnumerationExtensions
    {
        public static IEnumerable<T> Cast<T>(this MatchCollection arr)
        {
            var output = new List<T>();
            foreach (var val in arr)
            {
                output.Add((T)val);
            }
            return output;
        }

        public static IEnumerable<U> Select<T, U>(this T[] arr, LinqSelectDelegate<T, U> func)
        {
            var output = new List<U>();
            for (int i = 0; i < arr.Length; i++)
            {
                output.Add(func(arr[i]));
            }
            return output;
        }

        public static IEnumerable<U> Select<T, U>(this IEnumerable<T> arr, LinqSelectDelegate<T, U> func)
        {
            var output = new List<U>();
            foreach (var s in arr)
            {
                output.Add(func(s));
            }
            return output;
        }

        public static IEnumerable<T> Skip<T>(this T[] arr, int skip)
        {
            var output = new List<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (i < skip)
                    continue;
                output.Add(arr[i]);
            }
            return output;
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> arr, int skip)
        {
            var output = new List<T>();
            int index = 0;
            foreach (var s in arr)
            {
                if (index++ < skip)
                    continue;
                output.Add(s);
            }
            return output;
        }

        public static T[] ToArray<T>(this IEnumerable<T> arr)
        {
            var list = arr.ToList();
            var output = new T[list.Count];
            int index = 0;
            foreach (var s in list)
            {
                output[index++] = s;
            }
            return output;
        }

        public static U[] ToArray<T, U>(this Dictionary<T, U>.ValueCollection arr)
        {
            var list = arr.ToList();
            var output = new U[list.Count];
            int index = 0;
            foreach (var s in list)
            {
                output[index++] = s;
            }
            return output;
        }

        public static List<T> ToList<T>(this IEnumerable<T> arr)
        {
            var output = new List<T>();
            foreach (var s in arr)
            {
                output.Add(s);
            }
            return output;
        }

        public static List<U> ToList<T, U>(this Dictionary<T, U>.ValueCollection arr)
        {
            var output = new List<U>();
            foreach (var s in arr)
            {
                output.Add(s);
            }
            return output;
        }

        public static IEnumerable<T> Where<T>(this T[] arr, LinqWhereDelegate<T> func)
        {
            var output = new List<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (func(arr[i]))
                    output.Add(arr[i]);
            }
            return output;
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> arr, LinqWhereDelegate<T> func)
        {
            var output = new List<T>();
            foreach (var s in arr)
            {
                if (func(s))
                    output.Add(s);
            }
            return output;
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    internal sealed class ExtensionAttribute : Attribute {}
}

#endif

#if NET20 || NET35

namespace SabreTools.IO
{
    internal static partial class EnumerationExtensions
    {
        public static bool SequenceEqual<T>(this T[] arr1, T[] arr2)
        {
            if (arr1 == null || arr2 == null)
                return false;

            if (arr1.Length != arr2.Length)
                return false;

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == null || !arr1![i]!.Equals(arr2[i]))
                    return false;
            }

            return true;
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

    internal static partial class StringExtensions
    {
        
    }
}

#endif