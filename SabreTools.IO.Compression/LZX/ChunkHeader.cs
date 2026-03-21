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
    internal class ChunkHeader
    {
        /// <summary>
        /// The LZXD engine encodes a compressed, chunk-size prefix field preceding each compressed chunk in
        /// the compressed byte stream. The compressed, chunk-size prefix field is a byte aligned, little-endian,
        /// 16-bit field. The chunk prefix chain could be followed in the compressed stream without
        /// decompressing any data. The next chunk prefix is at a location computed by the absolute byte offset
        /// location of this chunk prefix plus 2 (for the size of the chunk-size prefix field) plus the current chunk
        /// size.
        /// </summary>
        public ushort ChunkSize { get; set; }

        /// <summary>
        /// The first bit in the first chunk in the LZXD bitstream (following the 2-byte, chunk-size prefix described
        /// in section 2.2.1) indicates the presence or absence of two 16-bit fields immediately following the
        /// single bit. If the bit is set, E8 translation is enabled for all the following chunks in the stream using the
        /// 32-bit value derived from the two 16-bit fields as the E8_file_size provided to the compressor when E8
        /// translation was enabled. Note that E8_file_size is completely independent of the length of the
        /// uncompressed data. E8 call translation is disabled after the 32,768th chunk (after 1 gigabyte (GB) of
        /// uncompressed data).
        /// </summary>
        public byte E8Translation { get; set; }

        /// <summary>
        /// E8 translation size, high WORD
        /// </summary>
        public ushort? TranslationSizeHighWord { get; set; }

        /// <summary>
        /// E8 translation size, low WORD
        /// </summary>
        public ushort? TranslationSizeLowWord { get; set; }
    }
}
