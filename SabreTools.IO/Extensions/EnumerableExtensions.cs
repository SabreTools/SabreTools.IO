using System.Collections.Generic;

namespace SabreTools.IO.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Safely iterate through an enumerable, skipping any errors
        /// </summary>
        public static IEnumerable<T> SafeEnumerate<T>(this IEnumerable<T> enumerable)
        {
            // Get the enumerator for the enumerable
            var enumerator = enumerable.GetEnumerator();

            // Iterate through and absorb any errors
            while (true)
            {
                // Attempt to move to the next item
                bool moved;
                try
                {
                    moved = enumerator.MoveNext();
                }
                catch
                {
                    continue;
                }

                // If the end of the enumeration is reached
                if (!moved)
                    yield break;

                // Return the next value from the enumeration
                yield return enumerator.Current;
            }
        }
    }
}