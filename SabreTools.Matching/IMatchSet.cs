using System.Collections.Generic;

namespace SabreTools.Matching
{
    /// <summary>
    /// Wrapper for a single set of matching criteria
    /// </summary>
    public interface IMatchSet<TMatcher, TNeedle> where TMatcher : IMatch<TNeedle>
    {
        /// <summary>
        /// Set of all matchers
        /// </summary>
        public List<TMatcher> Matchers { get; }

        /// <summary>
        /// Unique name for the match set
        /// </summary>
        public string SetName { get; }
    }
}
