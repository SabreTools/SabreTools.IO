using System;
using System.IO;
using System.Linq;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    // TODO: Add string writing tests
    public class StreamExtensionsWriteTests
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

        [Fact]
        public void WriteByteValueTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(1).ToArray();
            bool write = stream.Write((byte)0x00);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteBytesTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = StreamWriterExtensions.Write(stream, [0x00, 0x01, 0x02, 0x03]);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSByteTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(1).ToArray();
            bool write = stream.Write((sbyte)0x00);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteCharTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(1).ToArray();
            bool write = stream.Write('\0');
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt16Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = stream.Write((short)0x0100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt16BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = stream.WriteBigEndian((short)0x0001);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt16Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = stream.Write((ushort)0x0100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt16BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = stream.WriteBigEndian((ushort)0x0001);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void WriteHalfTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = stream.Write(BitConverter.Int16BitsToHalf(0x0100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteHalfBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = stream.WriteBigEndian(BitConverter.Int16BitsToHalf(0x0001));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }
#endif

        [Fact]
        public void WriteInt24Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = stream.WriteAsInt24(0x020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt24BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = stream.WriteAsInt24BigEndian(0x000102);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt24Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = stream.WriteAsUInt24(0x020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt24BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = stream.WriteAsUInt24BigEndian(0x000102);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt32Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = stream.Write(0x03020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt32BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = stream.WriteBigEndian(0x00010203);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt32Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = stream.Write((uint)0x03020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt32BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = stream.WriteBigEndian((uint)0x00010203);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSingleTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = stream.Write(BitConverter.Int32BitsToSingle(0x03020100));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteSingleBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = stream.WriteBigEndian(BitConverter.Int32BitsToSingle(0x00010203));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt48Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = stream.WriteAsInt48(0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt48BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = stream.WriteAsInt48BigEndian(0x000102030405);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt48Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = stream.WriteAsUInt48(0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt48BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = stream.WriteAsUInt48BigEndian(0x000102030405);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt64Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = stream.Write(0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt64BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = stream.WriteBigEndian(0x0001020304050607);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt64Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = stream.Write((ulong)0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt64BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = stream.WriteBigEndian((ulong)0x0001020304050607);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDecimalTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _decimalBytes.Take(16).ToArray();
            bool write = stream.Write(0.0123456789M);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteDecimalBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _decimalBytes.Take(16).Reverse().ToArray();
            bool write = stream.WriteBigEndian(0.0123456789M);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteGuidTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = stream.Write(new Guid(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteGuidBigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = stream.WriteBigEndian(new Guid(_bytes.Reverse().ToArray()));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

#if NET7_0_OR_GREATER
        [Fact]
        public void WriteInt128Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = stream.Write((Int128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteInt128BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = stream.WriteBigEndian((Int128)new BigInteger(_bytes.Reverse().ToArray()));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt128Test()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = stream.Write((UInt128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteUInt128BigEndianTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = stream.WriteBigEndian((UInt128)new BigInteger(_bytes.Reverse().ToArray()));
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }
#endif

        [Fact]
        public void WriteTypeExplicitTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            var obj = new TestStructExplicit
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
            };
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = stream.WriteType(obj);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteTypeSequentialTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            var obj = new TestStructSequential
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
            };
            byte[] expected = _bytes.Take(12).ToArray();
            bool write = stream.WriteType(obj);
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