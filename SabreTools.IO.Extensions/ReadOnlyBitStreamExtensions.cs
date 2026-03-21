namespace SabreTools.IO.Extensions
{
    /// <summary>
    /// Extensions for ReadOnlyBitStream
    /// </summary>
    public static class ReadOnlyBitStreamExtensions
    {
        /// <summary>
        /// Read a byte, if possible
        /// </summary>
        /// <returns>The next byte, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public static byte? ReadByte(this ReadOnlyBitStream stream)
        {
            try
            {
                stream.Discard();
                return stream._source.ReadByteValue();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a UInt16, if possible
        /// </summary>
        /// <returns>The next UInt16, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public static ushort? ReadUInt16(this ReadOnlyBitStream stream)
        {
            try
            {
                stream.Discard();
                return stream._source.ReadUInt16();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a UInt32, if possible
        /// </summary>
        /// <returns>The next UInt32, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public static uint? ReadUInt32(this ReadOnlyBitStream stream)
        {
            try
            {
                stream.Discard();
                return stream._source.ReadUInt32();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a UInt64, if possible
        /// </summary>
        /// <returns>The next UInt64, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public static ulong? ReadUInt64(this ReadOnlyBitStream stream)
        {
            try
            {
                stream.Discard();
                return stream._source.ReadUInt64();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read <paramref name="bytes"/> bytes, if possible
        /// </summary>
        /// <param name="bytes">Number of bytes to read</param>
        /// <returns>The next <paramref name="bytes"/> bytes, null on error or end of stream</returns>
        /// <remarks>Assumes the stream is byte-aligned</remarks>
        public static byte[]? ReadBytes(this ReadOnlyBitStream stream, int bytes)
        {
            try
            {
                stream.Discard();
                return stream._source.ReadBytes(bytes);
            }
            catch
            {
                return null;
            }
        }
    }
}
