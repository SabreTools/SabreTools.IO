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
using System.Text.RegularExpressions;

namespace SabreTools.Text.Compare
{
    public class NaturalReversedComparer : Comparer<string>, IDisposable
    {
        private readonly Dictionary<string, string[]> _table;

        public NaturalReversedComparer()
        {
            _table = [];
        }

        public void Dispose()
        {
            _table.Clear();
        }

        public override int Compare(string? x, string? y)
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
                return y.CompareTo(x);

            if (!_table.TryGetValue(x, out string[]? x1))
            {
                //x1 = Regex.Split(x.Replace(" ", string.Empty), "([0-9]+)");
                x1 = Regex.Split(x.ToLowerInvariant(), "([0-9]+)");
                x1 = Array.FindAll(x1, s => !string.IsNullOrEmpty(s));
                _table.Add(x, x1);
            }

            if (!_table.TryGetValue(y, out string[]? y1))
            {
                //y1 = Regex.Split(y.Replace(" ", string.Empty), "([0-9]+)");
                y1 = Regex.Split(y.ToLowerInvariant(), "([0-9]+)");
                y1 = Array.FindAll(y1, s => !string.IsNullOrEmpty(s));
                _table.Add(y, y1);
            }

            for (int i = 0; i < x1.Length && i < y1.Length; i++)
            {
                if (x1[i] != y1[i])
                    return PartCompare(x1[i], y1[i]);
            }

            if (y1.Length > x1.Length)
                return 1;
            else if (x1.Length > y1.Length)
                return -1;
            else
                return y.CompareTo(x);
        }

        private static int PartCompare(string left, string right)
        {
            if (!long.TryParse(left, out long x))
                return NaturalComparerUtil.ComparePaths(right, left);

            if (!long.TryParse(right, out long y))
                return NaturalComparerUtil.ComparePaths(right, left);

            // If we have an equal part, then make sure that "longer" ones are taken into account
            if (y.CompareTo(x) == 0)
                return right.Length - left.Length;

            return y.CompareTo(x);
        }
    }
}
