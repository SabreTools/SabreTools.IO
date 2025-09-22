namespace SabreTools.IO.Compression.LZX
{
    /// <summary>
    /// An LZXD block represents a sequence of compressed data that is encoded with the same set of
    /// Huffman trees, or a sequence of uncompressed data. There can be one or more LZXD blocks in a
    /// compressed stream, each with its own set of Huffman trees. Blocks do not have to start or end on a
    /// chunk boundary; blocks can span multiple chunks, or a single chunk can contain multiple blocks. The
    /// number of chunks is related to the size of the data being compressed, while the number of blocks is
    /// related to how well the data is compressed.
    /// </summary>
    /// <see href="https://interoperability.blob.core.windows.net/files/MS-PATCH/%5bMS-PATCH%5d.pdf"/>
    internal class Block
    {
        /// <summary>
        /// Block header
        /// </summary>
        public BlockHeader? Header { get; set; }

        /// <summary>
        /// Block data
        /// </summary>
        public BlockData? BlockData { get; set; }
    }
}