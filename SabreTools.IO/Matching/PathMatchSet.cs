using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Interfaces;

namespace SabreTools.IO.Matching
{
    /// <summary>
    /// A set of path matches that work together
    /// </summary>
    public class PathMatchSet : IMatchSet<PathMatch, string>
    {
        /// <inheritdoc/>
        public List<PathMatch> Matchers { get; }

        /// <inheritdoc/>
        public string SetName { get; }

        /// <summary>
        /// Function to get a path version for this Matcher
        /// </summary>
        /// <remarks>
        /// A path version method takes the matched path and an enumerable of files
        /// and returns a single string. That string is either a version string,
        /// in which case it will be appended to the match name, or `null`,
        /// in which case it will cause the match name to be omitted.
        /// </remarks>
        public GetPathVersion? GetVersion { get; }

        #region Generic Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">PathMatch representing the comparisons</param>
        /// <param name="setName">Unique name for the set</param>
        public PathMatchSet(PathMatch needle, string setName)
            : this([needle], setName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needles">List of PathMatch objects representing the comparisons</param>
        /// <param name="setName">Unique name for the set</param>
        public PathMatchSet(List<PathMatch> needles, string setName)
        {
            // Validate the inputs
            if (needles.Count == 0)
                throw new InvalidDataException(nameof(needles));

            Matchers = needles;
            SetName = setName;
            GetVersion = null;
        }

        #endregion

        #region Version Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">PathMatch representing the comparisons</param>
        /// <param name="getVersion">Delegate for deriving a version on match</param>
        /// <param name="setName">Unique name for the set</param>
        public PathMatchSet(PathMatch needle, GetPathVersion getVersion, string setName)
            : this([needle], getVersion, setName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needles">List of PathMatch objects representing the comparisons</param>
        /// <param name="getVersion">Delegate for deriving a version on match</param>
        /// <param name="setName">Unique name for the set</param>
        public PathMatchSet(List<PathMatch> needles, GetPathVersion getVersion, string setName)
        {
            // Validate the inputs
            if (needles.Count == 0)
                throw new InvalidDataException(nameof(needles));

            Matchers = needles;
            SetName = setName;
            GetVersion = getVersion;
        }

        #endregion

        #region Matching

        /// <summary>
        /// Get if this match can be found in a stack
        /// </summary>
        /// <param name="stack">List of strings to search for the given content</param>
        /// <returns>Matched item on success, null on error</returns>
        public List<string> MatchesAll(string[]? stack)
            => MatchesAll(stack == null ? null : new List<string>(stack));

        /// <summary>
        /// Determine whether all path matches pass
        /// </summary>
        /// <param name="stack">List of strings to try to match</param>
        /// <returns>List of matching values, if any</returns>
        public List<string> MatchesAll(List<string>? stack)
        {
            // If either set is null or empty, we can't do anything
            if (stack == null || stack.Count == 0 || Matchers.Count == 0)
                return [];

            // Initialize the value list
            List<string> values = [];

            // Loop through all path matches and make sure all pass
            foreach (var pathMatch in Matchers)
            {
                string? value = pathMatch.Match(stack);
                if (value == null)
                    return [];
                else
                    values.Add(value);
            }

            return values;
        }

        /// <summary>
        /// Get if this match can be found in a stack
        /// </summary>
        /// <param name="stack">List of strings to search for the given content</param>
        /// <returns>Matched item on success, null on error</returns>
        public string? MatchesAny(string[]? stack)
            => MatchesAny(stack == null ? null : new List<string>(stack));

        /// <summary>
        /// Determine whether any path matches pass
        /// </summary>
        /// <param name="stack">List of strings to try to match</param>
        /// <returns>First matching value on success, null on error</returns>
        public string? MatchesAny(List<string>? stack)
        {
            // If either set is null or empty, we can't do anything
            if (stack == null || stack.Count == 0 || Matchers.Count == 0)
                return null;

            // Loop through all path matches and make sure all pass
            foreach (var pathMatch in Matchers)
            {
                string? value = pathMatch.Match(stack);
                if (value != null)
                    return value;
            }

            return null;
        }

        #endregion
    }
}
