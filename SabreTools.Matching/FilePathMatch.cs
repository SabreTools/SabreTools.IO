using System.IO;

namespace SabreTools.Matching
{
    /// <summary>
    /// File path matching criteria
    /// </summary>
    public class FilePathMatch : PathMatch
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="needle">String representing the search</param>
        /// <param name="matchCase">True to match exact casing, false otherwise</param>
        public FilePathMatch(string needle, bool matchCase = false)
            : base($"{Path.DirectorySeparatorChar}{needle}", matchCase, true) { }
    }
}
