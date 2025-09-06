using System;

namespace SabreTools.IO.Extensions
{
    public static class StringExtensions
    {
        /// <inheritdoc cref="string.Contains(string)"/>
        public static bool OptionalContains(this string? self, string value)
            => OptionalContains(self, value, StringComparison.Ordinal);

        /// <inheritdoc cref="string.Contains(string, StringComparison)"/>
        public static bool OptionalContains(this string? self, string value, StringComparison comparisonType)
        {
            if (self == null)
                return false;

#if NETFRAMEWORK || NETSTANDARD2_0
            return self.Contains(value);
#else
            return self.Contains(value, comparisonType);
#endif
        }

        /// <inheritdoc cref="string.Equals(string)"/>
        public static bool OptionalEquals(this string? self, string value)
            => OptionalEquals(self, value, StringComparison.Ordinal);

        /// <inheritdoc cref="string.Equals(string, StringComparison)"/>
        public static bool OptionalEquals(this string? self, string value, StringComparison comparisonType)
        {
            if (self == null)
                return false;

            return self.Equals(value, comparisonType);
        }

        /// <inheritdoc cref="string.StartsWith(string)"/>
        public static bool OptionalStartsWith(this string? self, string value)
            => OptionalStartsWith(self, value, StringComparison.Ordinal);

        /// <inheritdoc cref="string.StartsWith(string, StringComparison)"/>
        public static bool OptionalStartsWith(this string? self, string value, StringComparison comparisonType)
        {
            if (self == null)
                return false;

            return self.StartsWith(value, comparisonType);
        }
    }
}