using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Xunit;

namespace SabreTools.Numerics.Extensions.Test
{
    public class StreamWriterExtensionsTests
    {
        /// <summary>
        /// Test pattern from 0x00-0x0F
        /// </summary>
        private static readonly byte[] _bytes =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        /// <summary>
        /// Represents the decimal value 0.0123456789
        /// </summary>
        private static readonly byte[] _decimalBytes =
        [
            0x15, 0xCD, 0x5B, 0x07, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00,
        ];

        /// <summary>
        /// Test pattern for big-endian GUID created from <see cref="_bytes"/>
        /// </summary>
        private static readonly byte[] _guidBigEndianbytes =
        [
            0x03, 0x02, 0x01, 0x00, 0x05, 0x04, 0x07, 0x06,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        [Fact]
        public void WriteByteValueTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(1)];
            bool write = stream.Write((byte)0x00);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteByteBothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadByteBothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteBytesTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = StreamWriterExtensions.Write(stream, [0x00, 0x01, 0x02, 0x03]);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteBytesBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            stream.WriteBigEndian([0x03, 0x02, 0x01, 0x00]);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSByteTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(1)];
            bool write = stream.Write((sbyte)0x00);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSByteBothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadSByteBothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteCharTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(1)];
            bool write = stream.Write('\0');
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteCharEncodingTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [0x00, 0x00];
            stream.Write('\0', Encoding.Unicode);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt16Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.Write((short)0x0100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt16BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.WriteBigEndian((short)0x0001);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt16LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.WriteLittleEndian((short)0x0100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt16BothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadInt16BothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt16Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.Write((ushort)0x0100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt16BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.WriteBigEndian((ushort)0x0001);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt16LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.WriteLittleEndian((ushort)0x0100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt16BothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadUInt16BothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteHalfTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.Write(BitConverter.Int16BitsToHalf(0x0100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteHalfBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.WriteBigEndian(BitConverter.Int16BitsToHalf(0x0001));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteHalfLittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(2)];
            bool write = stream.WriteLittleEndian(BitConverter.Int16BitsToHalf(0x0100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt24Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(3)];
            bool write = stream.Write((Int24)0x020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt24BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(3)];
            bool write = stream.WriteBigEndian((Int24)0x000102);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt24LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(3)];
            bool write = stream.WriteLittleEndian((Int24)0x020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt24Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(3)];
            bool write = stream.Write((UInt24)0x020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt24BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(3)];
            bool write = stream.WriteBigEndian((UInt24)0x000102);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt24LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(3)];
            bool write = stream.WriteLittleEndian((UInt24)0x020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt32Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.Write(0x03020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt32BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.WriteBigEndian(0x00010203);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt32LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.WriteLittleEndian(0x03020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt32BothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadInt32BothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt32Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.Write((uint)0x03020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt32BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.WriteBigEndian((uint)0x00010203);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt32LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.WriteLittleEndian((uint)0x03020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt32BothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadUInt32BothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSingleTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.Write(BitConverter.Int32BitsToSingle(0x03020100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSingleBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.WriteBigEndian(BitConverter.Int32BitsToSingle(0x00010203));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSingleLittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(4)];
            bool write = stream.WriteLittleEndian(BitConverter.Int32BitsToSingle(0x03020100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt48Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(6)];
            bool write = stream.Write((Int48)0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt48BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(6)];
            bool write = stream.WriteBigEndian((Int48)0x000102030405);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt48LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(6)];
            bool write = stream.WriteLittleEndian((Int48)0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt48Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(6)];
            bool write = stream.Write((UInt48)0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt48BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(6)];
            bool write = stream.WriteBigEndian((UInt48)0x000102030405);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt48LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(6)];
            bool write = stream.WriteLittleEndian((UInt48)0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt64Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.Write(0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt64BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.WriteBigEndian(0x0001020304050607);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt64LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.WriteLittleEndian(0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt64BothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadInt64BothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt64Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.Write((ulong)0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt64BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.WriteBigEndian((ulong)0x0001020304050607);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt64LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.WriteLittleEndian((ulong)0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt64BothEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];

            int offset = 0;
            stream.WriteBothEndian(_bytes.ReadUInt64BothEndian(ref offset));
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDoubleTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.Write(BitConverter.Int64BitsToDouble(0x0706050403020100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDoubleBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.WriteBigEndian(BitConverter.Int64BitsToDouble(0x0001020304050607));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDoubleLittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(8)];
            bool write = stream.WriteLittleEndian(BitConverter.Int64BitsToDouble(0x0706050403020100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteGuidTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.Write(new Guid(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteGuidBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _guidBigEndianbytes.Take(16)];
            bool write = stream.WriteBigEndian(new Guid(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteGuidLittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.WriteLittleEndian(new Guid(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt128Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.Write((Int128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt128BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.WriteBigEndian((Int128)new BigInteger(Enumerable.Reverse(_bytes).ToArray()));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt128LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.WriteLittleEndian((Int128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt128Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.Write((UInt128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt128BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.WriteBigEndian((UInt128)new BigInteger(Enumerable.Reverse(_bytes).ToArray()));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt128LittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _bytes.Take(16)];
            bool write = stream.WriteLittleEndian((UInt128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDecimalTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _decimalBytes.Take(16)];
            bool write = stream.Write(0.0123456789M);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDecimalBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _decimalBytes.Take(16).Reverse()];
            bool write = stream.WriteBigEndian(0.0123456789M);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDecimalLittleEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = [.. _decimalBytes.Take(16)];
            bool write = stream.WriteLittleEndian(0.0123456789M);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        /// <summary>
        /// Validate that a set of actual bytes matches the expected bytes
        /// </summary>
        private static void ValidateBytes(byte[] expected, byte[] actual)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
