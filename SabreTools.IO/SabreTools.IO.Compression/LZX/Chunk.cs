namespace SabreTools.IO.Compression.LZX
{
    /// <summary>
    /// The LZXD compressor emits chunks of compressed data. A chunk represents exactly 32 KB of
    /// uncompressed data until the last chunk in the stream, which can represent less than 32 KB. To
    /// ensure that an exact number of input bytes represent an exact number of output bytes for each
    /// chunk, after each 32 KB of uncompressed data is represented in the output compressed bitstream, the
    /// output bitstream is padded with up to 15 bits of zeros to realign the bitstream on a 16-bit boundary
    /// (even byte boundary) for the next 32 KB of data. This results in a compressed chunk of a byte-aligned
    /// size. The compressed chunk could be smaller than 32 KB or larger than 32 KB if the data is
    /// incompressible when the chunk is not the last one.
    /// </summary>
    internal class Chunk
    {
        /// <summary>
        /// Chunk header
        /// </summary>
        public ChunkHeader? Header { get; set; }

        /// <summary>
        /// Block headers and data
        /// </summary>
        public Block[]? Blocks { get; set; }
    }
}
