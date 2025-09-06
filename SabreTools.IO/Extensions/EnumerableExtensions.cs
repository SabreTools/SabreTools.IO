using System;
using System.Collections.Generic;

namespace SabreTools.IO.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Wrap iterating through an enumerable with an action
        /// </summary>
        /// <remarks>
        /// .NET Frameworks 2.0 and 3.5 process in series.
        /// .NET Frameworks 4.0 onward process in parallel.
        /// </remarks>
        public static void IterateWithAction<T>(this IEnumerable<T> source, Action<T> action)
        {
#if NET20 || NET35
            foreach (var item in source)
            {
                action(item);
            }
#else
            System.Threading.Tasks.Parallel.ForEach(source, action);
#endif
        }

        /// <summary>
        /// Safely iterate through an enumerable, skipping any errors
        /// </summary>
        public static IEnumerable<T> SafeEnumerate<T>(this IEnumerable<T> enumerable)
        {
            // Get the enumerator for the enumerable
            IEnumerator<T> enumerator;
            try
            {
                enumerator = enumerable.GetEnumerator();
            }
            catch
            {
                yield break;
            }

            // Iterate through and absorb any errors
            while (true)
            {
                // Attempt to move to the next item
                bool moved;
                try
                {
                    moved = enumerator.MoveNext();
                }
                catch (InvalidOperationException)
                {
                    // Specific case for collections that were modified
                    yield break;
                }
                catch (System.IO.IOException ex) when (ex.Message.Contains("The file or directory is corrupted and unreadable."))
                {
                    // Specific case we can't circumvent
                    yield break;
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
