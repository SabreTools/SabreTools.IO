namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Determines the swapping operation
    /// </summary>
    public enum SwapOperation
    {
        /// <summary>
        /// Reverse endianness of each byte
        /// </summary>
        Bitswap = 0,

        /// <summary>
        /// Swap every 1 byte
        /// </summary>
        Byteswap = 1,

        /// <summary>
        /// Swap every 2 bytes
        /// </summary>
        Wordswap = 2,

        /// <summary>
        /// Swap every 2 bytes and bytes within the 2 bytes
        /// </summary>
        WordByteswap = 3,
    }
}
