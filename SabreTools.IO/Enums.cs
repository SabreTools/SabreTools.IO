namespace SabreTools.IO
{
    /// <summary>
    /// Determines the swapping operation
    /// </summary>
    public enum SwapOperation
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
