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
    // TODO: Add byte[], char[] tests
    // TODO: Add decimal tests
    // TODO: Add string reading tests
    public class BinaryReaderExtensionsTests
    {
        private static readonly byte[] _bytes =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        [Fact]
        public void ReadInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.ReadInt16();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.ReadInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadUInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadUInt16();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadUInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadUInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt32();
            Assert.Equal(0x03020100, read);
        }

        [Fact]
        public void ReadInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt32BigEndian();
            Assert.Equal(0x00010203, read);
        }

        [Fact]
        public void ReadUInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt32();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadUInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt32BigEndian();
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadSingleTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = br.ReadSingle();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadSingleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = br.ReadSingleBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt64();
            Assert.Equal(0x0706050403020100, read);
        }

        [Fact]
        public void ReadInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt64BigEndian();
            Assert.Equal(0x0001020304050607, read);
        }

        [Fact]
        public void ReadUInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt64();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadUInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt64BigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadDoubleTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = br.ReadDouble();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDoubleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = br.ReadDoubleBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadGuidTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_bytes);
            Guid read = br.ReadGuid();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadGuidBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_bytes.Reverse().ToArray());
            Guid read = br.ReadGuidBigEndian();
            Assert.Equal(expected, read);
        }

#if NET7_0_OR_GREATER
        [Fact]
        public void ReadInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = br.ReadInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (Int128)new BigInteger(reversed);
            Int128 read = br.ReadInt128BigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = br.ReadUInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            UInt128 read = br.ReadUInt128BigEndian();
            Assert.Equal(expected, read);
        }
#endif

        [Fact]
        public void ReadTypeExplicitTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new TestStructExplicit
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
                ThirdValue = 0x0504,
                FourthValue = 0x0706,
            };
            var read = br.ReadType<TestStructExplicit>();
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
        }

        [Fact]
        public void ReadTypeSequentialTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new TestStructSequential
            {
                FirstValue = 0x03020100,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
            };
            var read = br.ReadType<TestStructSequential>();
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
        }
    }
}