namespace SabreTools.IO.Compression.SZDD
{
    /// <summary>
    /// Represents the SZDD format being decompressed
    /// </summary>
    internal enum Format
    {
        /// <summary>
        /// Standard SZDD implementation
        /// </summary>
        SZDD,

        /// <summary>
        /// QBasic 4.5 installer variant
        /// </summary>
        QBasic,

        /// <summary>
        /// KWAJ variant, no compression
        /// </summary>
        KWAJNoCompression,

        /// <summary>
        /// KWAJ variant, XORed with 0xFF
        /// </summary>
        KWAJXor,

        /// <summary>
        /// KWAJ variant, QBasic variant compression
        /// </summary>
        KWAJQBasic,

        /// <summary>
        /// KWAJ variant, LZ + Huffman compression
        /// </summary>
        KWAJLZH,

        /// <summary>
        /// KWAJ variant, MS-ZIP compression
        /// </summary>
        KWAJMSZIP,
    }
}
