namespace SabreTools.IO.Compression.LZX
{
    /// <summary>
    /// The Block Type field, as specified in section 2.3.1.1, indicates which type of block follows,
    /// and the Block Size field, as specified in section 2.3.1.2, indicates the number of
    /// uncompressed bytes represented by the block. Following the generic block
    /// header is a type-specific header that describes the remainder of the block.
    /// </summary>
    /// <see href="https://interoperability.blob.core.windows.net/files/MS-PATCH/%5bMS-PATCH%5d.pdf"/>
    internal class BlockHeader
    {
        /// <remarks>3 bits</remarks>
        public BlockType BlockType { get; set; }

        /// <summary>
        /// Block size is the high 8 bits of 24
        /// </summary>
        /// <remarks>8 bits</remarks>
        public byte BlockSizeMSB { get; set; }

        /// <summary>
        /// Block size is the middle 8 bits of 24
        /// </summary>
        /// <remarks>8 bits</remarks>
        public byte BlockSizeByte2 { get; set; }

        /// <summary>
        /// Block size is the low 8 bits of 24
        /// </summary>
        /// <remarks>8 bits</remarks>
        public byte BlocksizeLSB { get; set; }
    }
}
