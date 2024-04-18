using System;
using System.Linq;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test
{
    public class ByteArrayExtensionsTests
    {
        private static readonly byte[] _bytes =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        [Fact]
        public void ReadByteTest()
        {
            int offset = 0;
            byte read = _bytes.ReadByte(ref offset);
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadByteValueTest()
        {
            int offset = 0;
            byte read = _bytes.ReadByteValue(ref offset);
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadBytesTest()
        {
            int offset = 0, length = 4;
            byte[] read = _bytes.ReadBytes(ref offset, length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length)));
        }

        [Fact]
        public void ReadSByteTest()
        {
            int offset = 0;
            sbyte read = _bytes.ReadSByte(ref offset);
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadCharTest()
        {
            int offset = 0;
            char read = _bytes.ReadChar(ref offset);
            Assert.Equal('\0', read);
        }

        [Fact]
        public void ReadInt16Test()
        {
            int offset = 0;
            short read = _bytes.ReadInt16(ref offset);
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadInt16BigEndianTest()
        {
            int offset = 0;
            short read = _bytes.ReadInt16BigEndian(ref offset);
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadUInt16Test()
        {
            int offset = 0;
            ushort read = _bytes.ReadUInt16(ref offset);
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadUInt16BigEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.ReadUInt16BigEndian(ref offset);
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadInt32Test()
        {
            int offset = 0;
            int read = _bytes.ReadInt32(ref offset);
            Assert.Equal(0x03020100, read);
        }

        [Fact]
        public void ReadInt32BigEndianTest()
        {
            int offset = 0;
            int read = _bytes.ReadInt32BigEndian(ref offset);
            Assert.Equal(0x00010203, read);
        }

        [Fact]
        public void ReadUInt32Test()
        {
            int offset = 0;
            uint read = _bytes.ReadUInt32(ref offset);
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadUInt32BigEndianTest()
        {
            int offset = 0;
            uint read = _bytes.ReadUInt32BigEndian(ref offset);
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadInt64Test()
        {
            int offset = 0;
            long read = _bytes.ReadInt64(ref offset);
            Assert.Equal(0x0706050403020100, read);
        }

        [Fact]
        public void ReadInt64BigEndianTest()
        {
            int offset = 0;
            long read = _bytes.ReadInt64BigEndian(ref offset);
            Assert.Equal(0x0001020304050607, read);
        }

        [Fact]
        public void ReadUInt64Test()
        {
            int offset = 0;
            ulong read = _bytes.ReadUInt64(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadUInt64BigEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.ReadUInt64BigEndian(ref offset);
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadGuidTest()
        {
            int offset = 0;
            var expected = new Guid(_bytes);
            Guid read = _bytes.ReadGuid(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadGuidBigEndian()
        {
            int offset = 0;
            var expected = new Guid(_bytes.Reverse().ToArray());
            Guid read = _bytes.ReadGuidBigEndian(ref offset);
            Assert.Equal(expected, read);
        }

#if NET7_0_OR_GREATER
        [Fact]
        public void ReadInt128Test()
        {
            int offset = 0;
            var expected = new Int128(BitConverter.ToUInt64(_bytes, 0), BitConverter.ToUInt64(_bytes, 8));
            Int128 read = _bytes.ReadInt128(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = new Int128(BitConverter.ToUInt64(reversed, 0), BitConverter.ToUInt64(reversed, 8));
            Int128 read = _bytes.ReadInt128BigEndian(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128Test()
        {
            int offset = 0;
            var expected = new UInt128(BitConverter.ToUInt64(_bytes, 0), BitConverter.ToUInt64(_bytes, 8));
            UInt128 read = _bytes.ReadUInt128(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = new UInt128(BitConverter.ToUInt64(reversed, 0), BitConverter.ToUInt64(reversed, 8));
            UInt128 read = _bytes.ReadUInt128BigEndian(ref offset);
            Assert.Equal(expected, read);
        }
#endif

        // TODO: Add string reading tests
    }
}