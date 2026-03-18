namespace SabreTools.IO.Transform
{
    /// <summary>
    /// Determines the block size of an operation
    /// </summary>
    public enum BlockSize
    {
        /// <summary>
        /// 1 byte blocks
        /// </summary>
        Byte = 1,

        /// <summary>
        /// 2 byte blocks
        /// </summary>
        Word = 2,

        /// <summary>
        /// 4 byte blocks
        /// </summary>
        Dword = 4,

        /// <summary>
        /// 8 byte blocks
        /// </summary>
        Qword = 8,
    }

    /// <summary>
    /// Determines the swapping operation
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// Reverse endianness of each byte
        /// </summary>
        Bitswap,

        /// <summary>
        /// Swap every 1 byte
        /// </summary>
        Byteswap,

        /// <summary>
        /// Swap every 2 bytes
        /// </summary>
        Wordswap,

        /// <summary>
        /// Swap every 2 bytes and bytes within the 2 bytes
        /// </summary>
        WordByteswap,
    }
}
