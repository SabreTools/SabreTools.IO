using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Interfaces;

namespace SabreTools.IO.Matching
{
    /// <summary>
    /// Path matching criteria
    /// </summary>
    public class PathMatch : IMatch<string>
    {
        /// <summary>
        /// String to match
        /// </summary>
        public string Needle { get; }

        /// <summary>
        /// Match casing instead of invariant
        /// </summary>
        private readonly bool _matchCase;

        /// <summary>
        /// Match that values end with the needle and not just contains
        /// </summary>
        private readonly bool _useEndsWith;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">String representing the search</param>
        /// <param name="matchCase">True to match exact casing, false otherwise</param>
        /// <param name="useEndsWith">True to match the end only, false for contains</param>
        public PathMatch(string needle, bool matchCase = false, bool useEndsWith = false)
        {
            // Validate the inputs
            if (needle.Length == 0)
                throw new InvalidDataException(nameof(needle));

            Needle = needle;
            _matchCase = matchCase;
            _useEndsWith = useEndsWith;
        }

        #region Conversion

        /// <summary>
        /// Allow conversion from string to PathMatch
        /// </summary>
        public static implicit operator PathMatch(string needle) => new(needle);

        #endregion

        #region Matching

        /// <summary>
        /// Get if this match can be found in a stack
        /// </summary>
        /// <param name="stack">Array of strings to search for the given content</param>
        /// <returns>Matched item on success, null on error</returns>
        public string? Match(string[]? stack)
            => Match(stack == null ? null : new List<string>(stack));

        /// <summary>
        /// Get if this match can be found in a stack
        /// </summary>
        /// <param name="stack">List of strings to search for the given content</param>
        /// <returns>Matched item on success, null on error</returns>
        public string? Match(List<string>? stack)
        {
            // If either set is null or empty
            if (stack == null || stack.Count == 0 || Needle.Length == 0)
                return null;

            // Preprocess the needle, if necessary
            string procNeedle = _matchCase ? Needle : Needle.ToLowerInvariant();

            foreach (string stackItem in stack)
            {
                // Preprocess the stack item, if necessary
                string procStackItem = _matchCase ? stackItem : stackItem.ToLowerInvariant();

                if (_useEndsWith && procStackItem.EndsWith(procNeedle))
                    return stackItem;
                else if (!_useEndsWith && procStackItem.Contains(procNeedle))
                    return stackItem;
            }

            return null;
        }

        #endregion
    }
}
