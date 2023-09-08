/*
 * 
 * Links for info and original source code:
 * 
 * https://blog.codinghorror.com/sorting-for-humans-natural-sort-order/
 * http://www.codeproject.com/Articles/22517/Natural-Sort-Comparer
 *
 * Exact code implementation used with permission, originally by motoschifo
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// TODO: Make this namespace a separate library
namespace NaturalSort
{
    internal class NaturalReversedComparer : Comparer<string>, IDisposable
    {
        private readonly Dictionary<string, string[]> table;

        public NaturalReversedComparer()
        {
            table = new Dictionary<string, string[]>();
        }

        public void Dispose()
        {
            table.Clear();
        }

#if NET48
        public override int Compare(string x, string y)
#else
        public override int Compare(string? x, string? y)
#endif
        {
            if (x == null || y == null)
            {
                if (x == null && y != null)
                    return -1;
                else if (x != null && y == null)
                    return 1;
                else
                    return 0;
            }
            if (y.ToLowerInvariant() == x.ToLowerInvariant())
            {
                return y.CompareTo(x);
            }
#if NET48
            if (!table.TryGetValue(x, out string[] x1))
#else
            if (!table.TryGetValue(x, out string[]? x1))
#endif
            {
                //x1 = Regex.Split(x.Replace(" ", string.Empty), "([0-9]+)");
                x1 = Regex.Split(x.ToLowerInvariant(), "([0-9]+)").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                table.Add(x, x1);
            }
#if NET48
            if (!table.TryGetValue(y, out string[] y1))
#else
            if (!table.TryGetValue(y, out string[]? y1))
#endif
            {
                //y1 = Regex.Split(y.Replace(" ", string.Empty), "([0-9]+)");
                y1 = Regex.Split(y.ToLowerInvariant(), "([0-9]+)").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                table.Add(y, y1);
            }

            for (int i = 0; i < x1.Length && i < y1.Length; i++)
            {
                if (x1[i] != y1[i])
                {
                    return PartCompare(x1[i], y1[i]);
                }
            }
            if (y1.Length > x1.Length)
            {
                return 1;
            }
            else if (x1.Length > y1.Length)
            {
                return -1;
            }
            else
            {
                return y.CompareTo(x);
            }
        }

        private static int PartCompare(string left, string right)
        {
            if (!long.TryParse(left, out long x))
            {
                return NaturalComparerUtil.CompareNumeric(right, left);
            }

            if (!long.TryParse(right, out long y))
            {
                return NaturalComparerUtil.CompareNumeric(right, left);
            }

            // If we have an equal part, then make sure that "longer" ones are taken into account
            if (y.CompareTo(x) == 0)
            {
                return right.Length - left.Length;
            }

            return y.CompareTo(x);
        }
    }
}
