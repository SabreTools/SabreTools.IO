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
    // TODO: Add string reading tests
    public class ByteArrayExtensionsReadTests
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
        public void ReadInt24Test()
        {
            int offset = 0;
            int read = _bytes.ReadInt24(ref offset);
            Assert.Equal(0x020100, read);
        }

        [Fact]
        public void ReadInt24BigEndianTest()
        {
            int offset = 0;
            int read = _bytes.ReadInt24BigEndian(ref offset);
            Assert.Equal(0x000102, read);
        }

        [Fact]
        public void ReadUInt24Test()
        {
            int offset = 0;
            uint read = _bytes.ReadUInt24(ref offset);
            Assert.Equal((uint)0x020100, read);
        }

        [Fact]
        public void ReadUInt24BigEndianTest()
        {
            int offset = 0;
            uint read = _bytes.ReadUInt24BigEndian(ref offset);
            Assert.Equal((uint)0x000102, read);
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
        public void ReadSingleTest()
        {
            int offset = 0;
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = _bytes.ReadSingle(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadSingleBigEndianTest()
        {
            int offset = 0;
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = _bytes.ReadSingleBigEndian(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt48Test()
        {
            int offset = 0;
            long read = _bytes.ReadInt48(ref offset);
            Assert.Equal(0x050403020100, read);
        }

        [Fact]
        public void ReadInt48BigEndianTest()
        {
            int offset = 0;
            long read = _bytes.ReadInt48BigEndian(ref offset);
            Assert.Equal(0x000102030405, read);
        }

        [Fact]
        public void ReadUInt48Test()
        {
            int offset = 0;
            ulong read = _bytes.ReadUInt48(ref offset);
            Assert.Equal((ulong)0x050403020100, read);
        }

        [Fact]
        public void ReadUInt48BigEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.ReadUInt48BigEndian(ref offset);
            Assert.Equal((ulong)0x000102030405, read);
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
        public void ReadDoubleTest()
        {
            int offset = 0;
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = _bytes.ReadDouble(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDoubleBigEndianTest()
        {
            int offset = 0;
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = _bytes.ReadDoubleBigEndian(ref offset);
            Assert.Equal(expected, read);
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
        public void ReadGuidBigEndianTest()
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
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = _bytes.ReadInt128(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = (Int128)new BigInteger(reversed);
            Int128 read = _bytes.ReadInt128BigEndian(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128Test()
        {
            int offset = 0;
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = _bytes.ReadUInt128(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            UInt128 read = _bytes.ReadUInt128BigEndian(ref offset);
            Assert.Equal(expected, read);
        }
#endif

        [Fact]
        public void ReadTypeExplicitTest()
        {
            int offset = 0;
            var expected = new TestStructExplicit
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
                ThirdValue = 0x0504,
                FourthValue = 0x0706,
            };
            var read = _bytes.ReadType<TestStructExplicit>(ref offset);
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
        }

        [Fact]
        public void ReadTypeSequentialTest()
        {
            int offset = 0;
            var expected = new TestStructSequential
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
            };
            var read = _bytes.ReadType<TestStructSequential>(ref offset);
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
        }
    }
}