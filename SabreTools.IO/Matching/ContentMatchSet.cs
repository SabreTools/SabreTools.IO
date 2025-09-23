using System.Collections.Generic;
using System.IO;
using SabreTools.IO.Interfaces;

namespace SabreTools.IO.Matching
{
    /// <summary>
    /// A set of content matches that work together
    /// </summary>
    public class ContentMatchSet : IMatchSet<ContentMatch, byte?[]>
    {
        /// <inheritdoc/>
        public List<ContentMatch> Matchers { get; }

        /// <inheritdoc/>
        public string SetName { get; }

        /// <summary>
        /// Function to get a content version
        /// </summary>
        /// <remarks>
        /// A content version method takes the file path, the file contents,
        /// and a list of found positions and returns a single string. That
        /// string is either a version string, in which case it will be appended
        /// to the match name, or `null`, in which case it will cause
        /// the match name to be omitted.
        /// </remarks>
        public GetArrayVersion? GetArrayVersion { get; }

        /// <summary>
        /// Function to get a content version
        /// </summary>
        /// <remarks>
        /// A content version method takes the file path, the file contents,
        /// and a list of found positions and returns a single string. That
        /// string is either a version string, in which case it will be appended
        /// to the match name, or `null`, in which case it will cause
        /// the match name to be omitted.
        /// </remarks>
        public GetStreamVersion? GetStreamVersion { get; }

        #region Generic Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">ContentMatch representing the comparisons</param>
        /// <param name="setName">Unique name for the set</param>
        public ContentMatchSet(ContentMatch needle, string setName)
            : this([needle], setName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needles">List of ContentMatch objects representing the comparisons</param>
        /// <param name="setName">Unique name for the set</param>
        public ContentMatchSet(List<ContentMatch> needles, string setName)
        {
            // Validate the inputs
            if (needles.Count == 0)
                throw new InvalidDataException(nameof(needles));

            Matchers = needles;
            SetName = setName;
            GetArrayVersion = null;
            GetStreamVersion = null;
        }

        #endregion

        #region Array Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">ContentMatch representing the comparisons</param>
        /// <param name="getVersion">Delegate for deriving a version on match of an array</param>
        /// <param name="setName">Unique name for the set</param>
        public ContentMatchSet(ContentMatch needle, GetArrayVersion getVersion, string setName)
            : this([needle], getVersion, setName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needles">List of ContentMatch objects representing the comparisons</param>
        /// <param name="getVersion">Delegate for deriving a version on match of an array</param>
        /// <param name="setName">Unique name for the set</param>
        public ContentMatchSet(List<ContentMatch> needles, GetArrayVersion getVersion, string setName)
        {
            // Validate the inputs
            if (needles.Count == 0)
                throw new InvalidDataException(nameof(needles));

            Matchers = needles;
            SetName = setName;
            GetArrayVersion = getVersion;
            GetStreamVersion = null;
        }

        #endregion

        #region Stream Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">ContentMatch representing the comparisons</param>
        /// <param name="getVersion">Delegate for deriving a version on match of a Stream</param>
        /// <param name="setName">Unique name for the set</param>
        public ContentMatchSet(ContentMatch needle, GetStreamVersion getVersion, string setName)
            : this([needle], getVersion, setName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needles">List of ContentMatch objects representing the comparisons</param>
        /// <param name="getVersion">Delegate for deriving a version on match of a Stream</param>
        /// <param name="setName">Unique name for the set</param>
        public ContentMatchSet(List<ContentMatch> needles, GetStreamVersion getVersion, string setName)
        {
            // Validate the inputs
            if (needles.Count == 0)
                throw new InvalidDataException(nameof(needles));

            Matchers = needles;
            SetName = setName;
            GetArrayVersion = null;
            GetStreamVersion = getVersion;
        }

        #endregion

        #region Array Matching

        /// <summary>
        /// Determine whether all content matches pass
        /// </summary>
        /// <param name="stack">Array to search</param>
        /// <returns>List of matching positions, if any</returns>
        public List<int> MatchesAll(byte[]? stack)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || Matchers.Count == 0)
                return [];

            // Initialize the position list
            var positions = new List<int>();

            // Loop through all content matches and make sure all pass
            foreach (var contentMatch in Matchers)
            {
                int position = contentMatch.Match(stack);
                if (position < 0)
                    return [];

                positions.Add(position);
            }

            return positions;
        }

        /// <summary>
        /// Determine whether any content matches pass
        /// </summary>
        /// <param name="stack">Array to search</param>
        /// <returns>First matching position on success, -1 on error</returns>
        public int MatchesAny(byte[]? stack)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || Matchers.Count == 0)
                return -1;

            // Loop through all content matches and make sure all pass
            foreach (var contentMatch in Matchers)
            {
                int position = contentMatch.Match(stack);
                if (position >= 0)
                    return position;
            }

            return -1;
        }

        #endregion

        #region Stream Matching

        /// <summary>
        /// Determine whether all content matches pass
        /// </summary>
        /// <param name="stack">Stream to search</param>
        /// <returns>List of matching positions, if any</returns>
        public List<int> MatchesAll(Stream? stack)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || Matchers.Count == 0)
                return [];

            // Initialize the position list
            var positions = new List<int>();

            // Loop through all content matches and make sure all pass
            foreach (var contentMatch in Matchers)
            {
                int position = contentMatch.Match(stack);
                if (position < 0)
                    return [];

                positions.Add(position);
            }

            return positions;
        }

        /// <summary>
        /// Determine whether any content matches pass
        /// </summary>
        /// <param name="stack">Stream to search</param>
        /// <returns>First matching position on success, -1 on error</returns>
        public int MatchesAny(Stream? stack)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || Matchers.Count == 0)
                return -1;

            // Loop through all content matches and make sure all pass
            foreach (var contentMatch in Matchers)
            {
                int position = contentMatch.Match(stack);
                if (position >= 0)
                    return position;
            }

            return -1;
        }

        #endregion
    }
}
