namespace SabreTools.IO.Compression.RVZPack
{
    /// <summary>
    /// Result of packing a single chunk: compressed payload and its logical size.
    /// </summary>
    public struct ChunkResult
    {
        /// <summary>
        /// Packed payload, or null if the chunk contains no junk.
        /// </summary>
        public byte[]? Packed;

        /// <summary>
        /// Number of bytes the decompressor needs to consume from <see cref="Packed"/>.
        /// </summary>
        public uint RvzPackedSize;
    }
}
