using System;
using System.IO;
using System.Linq;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test
{
    public class StreamExtensionsTests
    {
        private static readonly byte[] _bytes =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        [Fact]
        public void ReadByteValueTest()
        {
            var stream = new MemoryStream(_bytes);
            byte read = stream.ReadByteValue();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadBytesTest()
        {
            var stream = new MemoryStream(_bytes);
            int length = 4;
            byte[] read = stream.ReadBytes(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length)));
        }

        [Fact]
        public void ReadSByteTest()
        {
            var stream = new MemoryStream(_bytes);
            sbyte read = stream.ReadSByte();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadCharTest()
        {
            var stream = new MemoryStream(_bytes);
            char read = stream.ReadChar();
            Assert.Equal('\0', read);
        }

        [Fact]
        public void ReadInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            short read = stream.ReadInt16();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            short read = stream.ReadInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadUInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.ReadUInt16();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadUInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.ReadUInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.ReadInt32();
            Assert.Equal(0x03020100, read);
        }

        [Fact]
        public void ReadInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.ReadInt32BigEndian();
            Assert.Equal(0x00010203, read);
        }

        [Fact]
        public void ReadUInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadUInt32();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadUInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadUInt32BigEndian();
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.ReadInt64();
            Assert.Equal(0x0706050403020100, read);
        }

        [Fact]
        public void ReadInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.ReadInt64BigEndian();
            Assert.Equal(0x0001020304050607, read);
        }

        [Fact]
        public void ReadUInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadUInt64();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadUInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadUInt64BigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadGuidTest()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new Guid(_bytes);
            Guid read = stream.ReadGuid();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadGuidBigEndian()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new Guid(_bytes.Reverse().ToArray());
            Guid read = stream.ReadGuidBigEndian();
            Assert.Equal(expected, read);
        }

#if NET7_0_OR_GREATER
        [Fact]
        public void ReadInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new Int128(BitConverter.ToUInt64(_bytes, 0), BitConverter.ToUInt64(_bytes, 8));
            Int128 read = stream.ReadInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var reversed = _bytes.Reverse().ToArray();
            var expected = new Int128(BitConverter.ToUInt64(reversed, 0), BitConverter.ToUInt64(reversed, 8));
            Int128 read = stream.ReadInt128BigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new UInt128(BitConverter.ToUInt64(_bytes, 0), BitConverter.ToUInt64(_bytes, 8));
            UInt128 read = stream.ReadUInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var reversed = _bytes.Reverse().ToArray();
            var expected = new UInt128(BitConverter.ToUInt64(reversed, 0), BitConverter.ToUInt64(reversed, 8));
            UInt128 read = stream.ReadUInt128BigEndian();
            Assert.Equal(expected, read);
        }
#endif

        // TODO: Add string reading tests
    }
}