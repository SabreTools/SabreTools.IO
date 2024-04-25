using System;
using System.Linq;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    // TODO: Add decimal tests
    public class ByteArrayExtensionsWriteTests
    {
        private static readonly byte[] _bytes =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        [Fact]
        public void WriteByteTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(1).ToArray();
            bool write = buffer.Write(ref offset, (byte)0x00);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteBytesTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.Write(ref offset, [0x00, 0x01, 0x02, 0x03]);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteSByteTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(1).ToArray();
            bool write = buffer.Write(ref offset, (sbyte)0x00);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteCharTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(1).ToArray();
            bool write = buffer.Write(ref offset, '\0');
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt16Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = buffer.Write(ref offset, (short)0x0100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt16BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, (short)0x0001);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt16Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = buffer.Write(ref offset, (ushort)0x0100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt16BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, (ushort)0x0001);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt32Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.Write(ref offset, 0x03020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt32BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, 0x00010203);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt32Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.Write(ref offset, (uint)0x03020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt32BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, (uint)0x00010203);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteSingleTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.Write(ref offset, BitConverter.Int32BitsToSingle(0x03020100));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteSingleBigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, BitConverter.Int32BitsToSingle(0x00010203));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt64Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = buffer.Write(ref offset, 0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt64BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, 0x0001020304050607);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt64Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = buffer.Write(ref offset, (ulong)0x0706050403020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt64BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, (ulong)0x0001020304050607);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteGuidTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = buffer.Write(ref offset, new Guid(_bytes));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteGuidBigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, new Guid(_bytes.Reverse().ToArray()));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

#if NET7_0_OR_GREATER
        [Fact]
        public void WriteInt128Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = buffer.Write(ref offset, (Int128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt128BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, (Int128)new BigInteger(_bytes.Reverse().ToArray()));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt128Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = buffer.Write(ref offset, (UInt128)new BigInteger(_bytes));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt128BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(16).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, (UInt128)new BigInteger(_bytes.Reverse().ToArray()));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }
#endif

        // [Fact]
        // public void WriteTypeExplicitTest()
        // {
        //     byte[] buffer = new byte[16];
        //     int offset = 0;
        //     var expected = new TestStructExplicit
        //     {
        //         FirstValue = 0x03020100,
        //         SecondValue = 0x07060504,
        //         ThirdValue = 0x0504,
        //         FourthValue = 0x0706,
        //     };
        //     bool write = buffer.WriteType<TestStructExplicit>(ref offset);
        //     Assert.True(write);
        //     ValidateBytes(expected, buffer);
        // }

        // [Fact]
        // public void WriteTypeSequentialTest()
        // {
        //     byte[] buffer = new byte[16];
        //     int offset = 0;
        //     var expected = new TestStructSequential
        //     {
        //         FirstValue = 0x03020100,
        //         SecondValue = 0x07060504,
        //         ThirdValue = 0x0908,
        //         FourthValue = 0x0B0A,
        //     };
        //     bool write = buffer.WriteType<TestStructSequential>(ref offset);
        //     Assert.True(write);
        //     ValidateBytes(expected, buffer);
        // }

        /// <summary>
        /// Validate that a set of actual bytes matches the expected bytes
        /// </summary>
        private void ValidateBytes(byte[] expected, byte[] actual)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}