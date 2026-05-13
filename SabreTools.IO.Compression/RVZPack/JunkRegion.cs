namespace SabreTools.IO.Compression.RVZPack
{
    /// <summary>
    /// Junk region information for recreation
    /// </summary>
    internal struct JunkRegion
    {
        /// <summary>
        /// Starting offset of the junk region
        /// </summary>
        public long StartOffset;

        /// <summary>
        /// Seed used to recreate the junk
        /// </summary>
        public uint[]? Seed;
    }
}
