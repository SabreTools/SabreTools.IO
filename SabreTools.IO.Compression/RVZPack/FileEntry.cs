namespace SabreTools.IO.Compression.RVZPack
{
    /// <summary>
    /// File entry with start and end byte offsets on disc
    /// </summary>
    public struct FileEntry
    {
        /// <summary>
        /// Indicates if the entry respresents a directory
        /// </summary>
        public bool IsDirectory;

        /// <summary>
        /// Starting data offset for the entry
        /// </summary>
        public long FileStart;

        /// <summary>
        /// Ending data offset for the entry
        /// </summary>
        public long FileEnd;
    }
}
