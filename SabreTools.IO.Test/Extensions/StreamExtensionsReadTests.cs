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
    // TODO: Add string reading tests
    public class StreamExtensionsReadTests
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

#if NET6_0_OR_GREATER
        [Fact]
        public void ReadHalfTest()
        {
            var stream = new MemoryStream(_bytes);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = stream.ReadHalf();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadHalfBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            Half read = stream.ReadHalfBigEndian();
            Assert.Equal(expected, read);
        }
#endif

        [Fact]
        public void ReadInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.ReadInt24();
            Assert.Equal(0x020100, read);
        }

        [Fact]
        public void ReadInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.ReadInt24BigEndian();
            Assert.Equal(0x000102, read);
        }

        [Fact]
        public void ReadUInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadUInt24();
            Assert.Equal((uint)0x020100, read);
        }

        [Fact]
        public void ReadUInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadUInt24BigEndian();
            Assert.Equal((uint)0x000102, read);
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
        public void ReadSingleTest()
        {
            var stream = new MemoryStream(_bytes);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = stream.ReadSingle();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadSingleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = stream.ReadSingleBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.ReadInt48();
            Assert.Equal(0x050403020100, read);
        }

        [Fact]
        public void ReadInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.ReadInt48BigEndian();
            Assert.Equal(0x000102030405, read);
        }

        [Fact]
        public void ReadUInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadUInt48();
            Assert.Equal((ulong)0x050403020100, read);
        }

        [Fact]
        public void ReadUInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadUInt48BigEndian();
            Assert.Equal((ulong)0x000102030405, read);
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
        public void ReadDoubleTest()
        {
            var stream = new MemoryStream(_bytes);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = stream.ReadDouble();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDoubleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = stream.ReadDoubleBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDecimalTest()
        {
            var stream = new MemoryStream(_decimalBytes);
            decimal expected = 0.0123456789M;
            decimal read = stream.ReadDecimal();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDecimalBigEndianTest()
        {
            var stream = new MemoryStream(_decimalBytes.Reverse().ToArray());
            decimal expected = 0.0123456789M;
            decimal read = stream.ReadDecimalBigEndian();
            Assert.Equal(expected, read);
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
        public void ReadGuidBigEndianTest()
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
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = stream.ReadInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (Int128)new BigInteger(reversed);
            Int128 read = stream.ReadInt128BigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = stream.ReadUInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            UInt128 read = stream.ReadUInt128BigEndian();
            Assert.Equal(expected, read);
        }
#endif

        [Fact]
        public void ReadTypeExplicitTest()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new TestStructExplicit
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
                ThirdValue = 0x0504,
                FourthValue = 0x0706,
            };
            var read = stream.ReadType<TestStructExplicit>();
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
        }

        [Fact]
        public void ReadTypeSequentialTest()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new TestStructSequential
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
            };
            var read = stream.ReadType<TestStructSequential>();
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
        }
    }
}