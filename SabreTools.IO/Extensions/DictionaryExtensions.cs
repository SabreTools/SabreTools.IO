using System.Collections.Generic;

namespace SabreTools.IO.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Merge a dictionary into an existing one, if possible
        /// </summary>
        /// <param name="dict">Source dictionary to add to</param>
        /// <param name="other">Second dictionary to add from</param>
        /// <remarks>This only performs a shallow copy</remarks>
        public static void MergeWith(this Dictionary<string, List<string>> dict, Dictionary<string, List<string>> other)
        {
            // Ignore if there are no values to append
            if (other.Count == 0)
                return;

            // Loop through and add from the new dictionary
            foreach (var kvp in other)
            {
                // Ignore empty values
                if (kvp.Value.Count == 0)
                    continue;

                if (!dict.ContainsKey(kvp.Key))
                    dict[kvp.Key] = [];

                dict[kvp.Key].AddRange(kvp.Value);
            }
        }
    }
}
