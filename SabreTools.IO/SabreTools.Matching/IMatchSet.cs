using System.Collections.Generic;

namespace SabreTools.Matching
{
    /// <summary>
    /// Wrapper for a single set of matching criteria
    /// </summary>
    public interface IMatchSet<T, U> where T : IMatch<U>
    {
        /// <summary>
        /// Set of all matchers
        /// </summary>
        public List<T> Matchers { get; }

        /// <summary>
        /// Unique name for the match set
        /// </summary>
        public string SetName { get; }
    }
}
