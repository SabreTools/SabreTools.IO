using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.IO.Matching;

namespace SabreTools.IO
{
    /// <summary>
    /// Helper class for matching
    /// </summary>
    public static class MatchUtil
    {
        #region Array Content Matching

        /// <summary>
        /// Get all content matches for a given list of matchers
        /// </summary>
        /// <param name="file">File to check for matches</param>
        /// <param name="stack">Array to search</param>
        /// <param name="matchSets">List of ContentMatchSets to be run on the file</param>
        /// <param name="any">True if any content match is a success, false if all have to match</param>
        /// <param name="includeDebug">True to include positional data, false otherwise</param>
        /// <returns>List of strings representing the matches, null or empty otherwise</returns>
        public static List<string> GetAllMatches(string file,
            byte[]? stack,
            List<ContentMatchSet> matchSets,
            bool any = false,
            bool includeDebug = false)
            => FindAllMatches(file, stack, matchSets, any, includeDebug, false);

        /// <summary>
        /// Get first content match for a given list of matchers
        /// </summary>
        /// <param name="file">File to check for matches</param>
        /// <param name="stack">Array to search</param>
        /// <param name="matchSets">List of ContentMatchSets to be run on the file</param>
        /// <param name="any">True if any content match is a success, false if all have to match</param>
        /// <param name="includeDebug">True to include positional data, false otherwise</param>
        /// <returns>String representing the match, null otherwise</returns>
        public static string? GetFirstMatch(string file,
            byte[]? stack,
            List<ContentMatchSet> matchSets,
            bool any = false,
            bool includeDebug = false)
        {
            var contentMatches = FindAllMatches(file, stack, matchSets, any, includeDebug, true);
            if (contentMatches == null || contentMatches.Count == 0)
                return null;

            return contentMatches[0];
        }

        /// <summary>
        /// Get the required set of content matches on a per Matcher basis
        /// </summary>
        /// <param name="file">File to check for matches</param>
        /// <param name="stack">Array to search</param>
        /// <param name="matchSets">List of ContentMatchSets to be run on the file</param>
        /// <param name="any">True if any content match is a success, false if all have to match</param>
        /// <param name="includeDebug">True to include positional data, false otherwise</param>
        /// <param name="stopAfterFirst">True to stop after the first match, false otherwise</param>
        /// <returns>List of strings representing the matches, empty otherwise</returns>
        private static List<string> FindAllMatches(string file,
            byte[]? stack,
            List<ContentMatchSet> matchSets,
            bool any,
            bool includeDebug,
            bool stopAfterFirst)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || matchSets.Count == 0)
                return [];

            // Initialize the list of matches
            var matchesList = new List<string>();

            // Loop through and try everything otherwise
            foreach (var matcher in matchSets)
            {
                // Determine if the matcher passes
                List<int> positions = any
                    ? [matcher.MatchesAny(stack)]
                    : matcher.MatchesAll(stack);

                // If we don't have a pass, just continue
                if (positions.Count == 0 || positions[0] == -1)
                    continue;

                // Build the output string
                var matchString = new StringBuilder();
                matchString.Append(matcher.SetName);

                // Invoke the version delegate, if it exists
                if (matcher.GetArrayVersion != null)
                {
                    // A null version returned means the check didn't pass at the version step
                    var version = matcher.GetArrayVersion(file, stack, positions);
                    if (version == null)
                        continue;

                    // Trim and add the version
                    version = version.Trim();
                    if (version.Length > 0)
                        matchString.Append($" {version}");
                }

                // Append the positional data if required
                if (includeDebug)
                {
                    string positionsString = string.Join(", ", [.. positions.ConvertAll(p => p.ToString())]);
                    matchString.Append($" (Index {positionsString})");
                }

                // Append the match to the list
                matchesList.Add(matchString.ToString());

                // If we're stopping after the first match, bail out here
                if (stopAfterFirst)
                    return matchesList;
            }

            return matchesList;
        }

        #endregion

        #region Stream Content Matching

        /// <summary>
        /// Get all content matches for a given list of matchers
        /// </summary>
        /// <param name="file">File to check for matches</param>
        /// <param name="stack">Stream to search</param>
        /// <param name="matchSets">List of ContentMatchSets to be run on the file</param>
        /// <param name="any">True if any content match is a success, false if all have to match</param>
        /// <param name="includeDebug">True to include positional data, false otherwise</param>
        /// <returns>List of strings representing the matches, null or empty otherwise</returns>
        public static List<string> GetAllMatches(string file,
            Stream? stack,
            List<ContentMatchSet> matchSets,
            bool any = false,
            bool includeDebug = false)
            => FindAllMatches(file, stack, matchSets, any, includeDebug, false);

        /// <summary>
        /// Get first content match for a given list of matchers
        /// </summary>
        /// <param name="file">File to check for matches</param>
        /// <param name="stack">Stream to search</param>
        /// <param name="matchSets">List of ContentMatchSets to be run on the file</param>
        /// <param name="any">True if any content match is a success, false if all have to match</param>
        /// <param name="includeDebug">True to include positional data, false otherwise</param>
        /// <returns>String representing the match, null otherwise</returns>
        public static string? GetFirstMatch(string file,
            Stream? stack,
            List<ContentMatchSet> matchSets,
            bool any = false,
            bool includeDebug = false)
        {
            var contentMatches = FindAllMatches(file, stack, matchSets, any, includeDebug, true);
            if (contentMatches == null || contentMatches.Count == 0)
                return null;

            return contentMatches[0];
        }

        /// <summary>
        /// Get the required set of content matches on a per Matcher basis
        /// </summary>
        /// <param name="file">File to check for matches</param>
        /// <param name="stack">Stream to search</param>
        /// <param name="matchSets">List of ContentMatchSets to be run on the file</param>
        /// <param name="any">True if any content match is a success, false if all have to match</param>
        /// <param name="includeDebug">True to include positional data, false otherwise</param>
        /// <param name="stopAfterFirst">True to stop after the first match, false otherwise</param>
        /// <returns>List of strings representing the matches, empty otherwise</returns>
        private static List<string> FindAllMatches(string file,
            Stream? stack,
            List<ContentMatchSet> matchSets,
            bool any,
            bool includeDebug,
            bool stopAfterFirst)
        {
            // If either set is null or empty
            if (stack == null || stack.Length == 0 || matchSets.Count == 0)
                return [];

            // Initialize the list of matches
            var matchesList = new List<string>();

            // Loop through and try everything otherwise
            foreach (var matcher in matchSets)
            {
                // Determine if the matcher passes
                List<int> positions = any
                    ? [matcher.MatchesAny(stack)]
                    : matcher.MatchesAll(stack);

                // If we don't have a pass, just continue
                if (positions.Count == 0 || positions[0] == -1)
                    continue;

                // Build the output string
                var matchString = new StringBuilder();
                matchString.Append(matcher.SetName);

                // Invoke the version delegate, if it exists
                if (matcher.GetStreamVersion != null)
                {
                    // A null version returned means the check didn't pass at the version step
                    var version = matcher.GetStreamVersion(file, stack, positions);
                    if (version == null)
                        continue;

                    // Trim and add the version
                    version = version.Trim();
                    if (version.Length > 0)
                        matchString.Append($" {version}");
                }

                // Append the positional data if required
                if (includeDebug)
                {
                    string positionsString = string.Join(", ", [.. positions.ConvertAll(p => p.ToString())]);
                    matchString.Append($" (Index {positionsString})");
                }

                // Append the match to the list
                matchesList.Add(matchString.ToString());

                // If we're stopping after the first match, bail out here
                if (stopAfterFirst)
                    return matchesList;
            }

            return matchesList;
        }

        #endregion

        #region Path Matching

        /// <summary>
        /// Get all path matches for a given list of matchers
        /// </summary>
        /// <param name="stack">File path to check for matches</param>
        /// <param name="matchSets">List of PathMatchSets to be run on the file</param>
        /// <param name="any">True if any path match is a success, false if all have to match</param>
        /// <returns>List of strings representing the matches, null or empty otherwise</returns>
        public static List<string> GetAllMatches(string stack, List<PathMatchSet> matchSets, bool any = false)
            => FindAllMatches([stack], matchSets, any, false);

        /// <summary>
        /// Get all path matches for a given list of matchers
        /// </summary>
        /// <param name="files">File paths to check for matches</param>
        /// <param name="matchSets">List of PathMatchSets to be run on the file</param>
        /// <param name="any">True if any path match is a success, false if all have to match</param>
        /// <returns>List of strings representing the matches, null or empty otherwise</returns>
        public static List<string> GetAllMatches(List<string>? stack, List<PathMatchSet> matchSets, bool any = false)
            => FindAllMatches(stack, matchSets, any, false);

        /// <summary>
        /// Get first path match for a given list of matchers
        /// </summary>
        /// <param name="stack">File path to check for matches</param>
        /// <param name="matchSets">List of PathMatchSets to be run on the file</param>
        /// <param name="any">True if any path match is a success, false if all have to match</param>
        /// <returns>String representing the match, null otherwise</returns>
        public static string? GetFirstMatch(string stack, List<PathMatchSet> matchSets, bool any = false)
        {
            var contentMatches = FindAllMatches([stack], matchSets, any, true);
            if (contentMatches == null || contentMatches.Count == 0)
                return null;

            return contentMatches[0];
        }

        /// <summary>
        /// Get first path match for a given list of matchers
        /// </summary>
        /// <param name="stack">File paths to check for matches</param>
        /// <param name="matchSets">List of PathMatchSets to be run on the file</param>
        /// <param name="any">True if any path match is a success, false if all have to match</param>
        /// <returns>String representing the match, null otherwise</returns>
        public static string? GetFirstMatch(List<string> stack, List<PathMatchSet> matchSets, bool any = false)
        {
            var contentMatches = FindAllMatches(stack, matchSets, any, true);
            if (contentMatches == null || contentMatches.Count == 0)
                return null;

            return contentMatches[0];
        }

        /// <summary>
        /// Get the required set of path matches on a per Matcher basis
        /// </summary>
        /// <param name="stack">File paths to check for matches</param>
        /// <param name="matchSets">List of PathMatchSets to be run on the file</param>
        /// <param name="any">True if any path match is a success, false if all have to match</param>
        /// <param name="stopAfterFirst">True to stop after the first match, false otherwise</param>
        /// <returns>List of strings representing the matches, null or empty otherwise</returns>
        private static List<string> FindAllMatches(List<string>? stack, List<PathMatchSet> matchSets, bool any, bool stopAfterFirst)
        {
            // If either set is null or empty
            if (stack == null || stack.Count == 0 || matchSets.Count == 0)
                return [];

            // Initialize the list of matches
            var matchesList = new List<string>();

            // Loop through and try everything otherwise
            foreach (var matcher in matchSets)
            {
                // Determine if the matcher passes
                List<string> matches = [];
                if (any)
                {
                    string? anyMatch = matcher.MatchesAny(stack);
                    if (anyMatch != null)
                        matches = [anyMatch];
                }
                else
                {
                    matches = matcher.MatchesAll(stack);
                }

                // If we don't have a pass, just continue
                if (matches.Count == 0)
                    continue;

                // Build the output string
                var matchString = new StringBuilder();
                matchString.Append(matcher.SetName);

                // Invoke the version delegate, if it exists
                if (matcher.GetVersion != null)
                {
                    // A null version returned means the check didn't pass at the version step
                    var version = matcher.GetVersion(matches[0], stack);
                    if (version == null)
                        continue;

                    // Trim and add the version
                    version = version.Trim();
                    if (version.Length > 0)
                        matchString.Append($" {version}");
                }

                // Append the match to the list
                matchesList.Add(matchString.ToString());

                // If we're stopping after the first match, bail out here
                if (stopAfterFirst)
                    return matchesList;
            }

            return matchesList;
        }

        #endregion
    }
}
