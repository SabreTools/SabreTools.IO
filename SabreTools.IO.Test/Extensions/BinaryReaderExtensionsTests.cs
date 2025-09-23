using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using SabreTools.IO.Extensions;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class BinaryReaderExtensionsTests
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
        public void ReadByteArrayTest()
        {
            byte[] arr = new byte[4];
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.Read(arr, 0, 4);
            Assert.Equal(4, read);
            Assert.True(arr.SequenceEqual(_bytes.Take(4)));
        }

        [Fact]
        public void ReadByteArrayBigEndianTest()
        {
            byte[] arr = new byte[4];
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadBigEndian(arr, 0, 4);
            Assert.Equal(4, read);
            Assert.True(arr.SequenceEqual(_bytes.Take(4).Reverse()));
        }

        [Fact]
        public void ReadCharArrayTest()
        {
            char[] arr = new char[4];
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.Read(arr, 0, 4);
            Assert.Equal(4, read);
            Assert.True(arr.SequenceEqual(_bytes.Take(4).Select(b => (char)b)));
        }

        [Fact]
        public void ReadCharArrayBigEndianTest()
        {
            char[] arr = new char[4];
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadBigEndian(arr, 0, 4);
            Assert.Equal(4, read);
            Assert.True(arr.SequenceEqual(_bytes.Take(4).Select(b => (char)b).Reverse()));
        }

        [Fact]
        public void ReadByteTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            byte read = br.ReadByte();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadBytesTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int length = 4;
            byte[] read = br.ReadBytes(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length)));
        }

        [Fact]
        public void ReadBytesBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int length = 4;
            byte[] read = br.ReadBytesBigEndian(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length).Reverse()));
        }

        [Fact]
        public void ReadCharsTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int length = 4;
            char[] read = br.ReadChars(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length).Select(b => (char)b)));
        }

        [Fact]
        public void ReadCharsBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int length = 4;
            char[] read = br.ReadCharsBigEndian(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length).Select(b => (char)b).Reverse()));
        }

        [Fact]
        public void ReadSByteTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            sbyte read = br.ReadSByte();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadCharTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            char read = br.ReadChar();
            Assert.Equal('\0', read);
        }

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
        public void ReadInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.ReadInt16LittleEndian();
            Assert.Equal(0x0100, read);
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
        public void ReadUInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadUInt16LittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadWORD();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadWORDBigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadWORDLittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadHalfTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = br.ReadHalf();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadHalfBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            Half read = br.ReadHalfBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt24();
            Assert.Equal(0x020100, read);
        }

        [Fact]
        public void ReadInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt24BigEndian();
            Assert.Equal(0x000102, read);
        }

        [Fact]
        public void ReadInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt24LittleEndian();
            Assert.Equal(0x020100, read);
        }

        [Fact]
        public void ReadUInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt24();
            Assert.Equal((uint)0x020100, read);
        }

        [Fact]
        public void ReadUInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt24BigEndian();
            Assert.Equal((uint)0x000102, read);
        }

        [Fact]
        public void ReadUInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt24LittleEndian();
            Assert.Equal((uint)0x020100, read);
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
        public void ReadInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt32LittleEndian();
            Assert.Equal(0x03020100, read);
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
        public void ReadUInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt32LittleEndian();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadDWORD();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadDWORDBigEndian();
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadDWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadDWORDLittleEndian();
            Assert.Equal((uint)0x03020100, read);
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
        public void ReadInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt48();
            Assert.Equal(0x050403020100, read);
        }

        [Fact]
        public void ReadInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt48BigEndian();
            Assert.Equal(0x000102030405, read);
        }

        [Fact]
        public void ReadInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt48LittleEndian();
            Assert.Equal(0x050403020100, read);
        }

        [Fact]
        public void ReadUInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt48();
            Assert.Equal((ulong)0x050403020100, read);
        }

        [Fact]
        public void ReadUInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt48BigEndian();
            Assert.Equal((ulong)0x000102030405, read);
        }

        [Fact]
        public void ReadUInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt48LittleEndian();
            Assert.Equal((ulong)0x050403020100, read);
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
        public void ReadInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt64LittleEndian();
            Assert.Equal(0x0706050403020100, read);
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
        public void ReadUInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt64LittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadQWORD();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadQWORDBigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadQWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadQWORDLittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
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
        public void ReadDecimalTest()
        {
            var stream = new MemoryStream(_decimalBytes);
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.ReadDecimal();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDecimalBigEndianTest()
        {
            var stream = new MemoryStream(_decimalBytes.Reverse().ToArray());
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.ReadDecimalBigEndian();
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

        [Fact]
        public void ReadNullTerminatedStringTest()
        {
            // Encoding.ASCII
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            var br = new BinaryReader(stream);
            string? actual = br.ReadNullTerminatedString(Encoding.ASCII);
            Assert.Equal("ABC", actual);

            // Encoding.UTF8
            bytes = [0x41, 0x42, 0x43, 0x00];
            stream = new MemoryStream(bytes);
            br = new BinaryReader(stream);
            actual = br.ReadNullTerminatedString(Encoding.UTF8);
            Assert.Equal("ABC", actual);

            // Encoding.Unicode
            bytes = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00, 0x00];
            stream = new MemoryStream(bytes);
            br = new BinaryReader(stream);
            actual = br.ReadNullTerminatedString(Encoding.Unicode);
            Assert.Equal("ABC", actual);

            // Encoding.UTF32
            bytes = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
            stream = new MemoryStream(bytes);
            br = new BinaryReader(stream);
            actual = br.ReadNullTerminatedString(Encoding.UTF32);
            Assert.Equal("ABC", actual);

            // Encoding.Latin1
            bytes = [0x41, 0x42, 0x43, 0x00];
            stream = new MemoryStream(bytes);
            br = new BinaryReader(stream);
            actual = br.ReadNullTerminatedString(Encoding.Latin1);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadTypeTest()
        {
            // Guid
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expectedGuid = new Guid(_bytes);
            Guid actualGuid = br.ReadType<Guid>();
            Assert.Equal(expectedGuid, actualGuid);

            // Half
            stream = new MemoryStream(_bytes);
            br = new BinaryReader(stream);
            Half expectedHalf = BitConverter.Int16BitsToHalf(0x0100);
            Half actualHalf = br.ReadType<Half>();
            Assert.Equal(expectedHalf, actualHalf);

            // Int128
            stream = new MemoryStream(_bytes);
            br = new BinaryReader(stream);
            Int128 expectedInt128 = (Int128)new BigInteger(_bytes);
            Int128 actualInt128 = br.ReadType<Int128>();
            Assert.Equal(expectedHalf, actualHalf);

            // UInt128
            stream = new MemoryStream(_bytes);
            br = new BinaryReader(stream);
            UInt128 expectedUInt128 = (UInt128)new BigInteger(_bytes);
            UInt128 actualUInt128 = br.ReadType<UInt128>();
            Assert.Equal(expectedHalf, actualHalf);

            // Enum
            stream = new MemoryStream(_bytes);
            br = new BinaryReader(stream);
            TestEnum expectedTestEnum = (TestEnum)0x03020100;
            TestEnum actualTestEnum = br.ReadType<TestEnum>();
            Assert.Equal(expectedTestEnum, actualTestEnum);
        }

        [Fact]
        public void ReadTypeExplicitTest()
        {
            byte[] bytesWithString =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x41, 0x42, 0x43, 0x00,
            ];

            var stream = new MemoryStream(bytesWithString);
            var br = new BinaryReader(stream);
            var expected = new TestStructExplicit
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0504,
                FourthValue = 0x0706,
                FifthValue = "ABC",
            };
            var read = br.ReadType<TestStructExplicit>();
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
            Assert.Equal(expected.FifthValue, read.FifthValue);
        }

        [Fact]
        public void ReadTypeSequentialTest()
        {
            byte[] bytesWithString =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x41, 0x42, 0x43, 0x00,
            ];

            var stream = new MemoryStream(bytesWithString);
            var br = new BinaryReader(stream);
            var expected = new TestStructSequential
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
                FifthValue = "ABC",
            };
            var read = br.ReadType<TestStructSequential>();
            Assert.Equal(expected.FirstValue, read.FirstValue);
            Assert.Equal(expected.SecondValue, read.SecondValue);
            Assert.Equal(expected.ThirdValue, read.ThirdValue);
            Assert.Equal(expected.FourthValue, read.FourthValue);
            Assert.Equal(expected.FifthValue, read.FifthValue);
        }

        [Fact]
        public void ReadTypeStringsTest()
        {
            byte[] structBytes =
            [
                0x03, 0x41, 0x42, 0x43, // AnsiBStr
                0x03, 0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00, // BStr
                0x41, 0x42, 0x43, // ByValTStr
                0x41, 0x42, 0x43, 0x00, // LPStr
                0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00, 0x00, // LPWStr
            ];

            var stream = new MemoryStream(structBytes);
            var br = new BinaryReader(stream);
            var expected = new TestStructStrings
            {
                AnsiBStr = "ABC",
                BStr = "ABC",
                ByValTStr = "ABC",
                LPStr = "ABC",
                LPWStr = "ABC",
            };
            var read = br.ReadType<TestStructStrings>();
            Assert.Equal(expected.AnsiBStr, read.AnsiBStr);
            Assert.Equal(expected.BStr, read.BStr);
            Assert.Equal(expected.ByValTStr, read.ByValTStr);
            Assert.Equal(expected.LPStr, read.LPStr);
            Assert.Equal(expected.LPWStr, read.LPWStr);
        }

        [Fact]
        public void ReadTypeArraysTest()
        {
            byte[] structBytes =
            [
                // Byte Array
                0x00, 0x01, 0x02, 0x03,

                // Int Array
                0x03, 0x02, 0x01, 0x00,
                0x04, 0x03, 0x02, 0x01,
                0x05, 0x04, 0x03, 0x02,
                0x06, 0x05, 0x04, 0x03,

                // Enum Array
                0x03, 0x02, 0x01, 0x00,
                0x04, 0x03, 0x02, 0x01,
                0x05, 0x04, 0x03, 0x02,
                0x06, 0x05, 0x04, 0x03,

                // Struct Array (X, Y)
                0xFF, 0x00, 0x00, 0xFF,
                0x00, 0xFF, 0xFF, 0x00,
                0xAA, 0x55, 0x55, 0xAA,
                0x55, 0xAA, 0xAA, 0x55,

                // LPArray
                0x04, 0x00,
                0x00, 0x01, 0x02, 0x03,
            ];

            var stream = new MemoryStream(structBytes);
            var br = new BinaryReader(stream);
            var expected = new TestStructArrays
            {
                ByteArray = [0x00, 0x01, 0x02, 0x03],
                IntArray = [0x00010203, 0x01020304, 0x02030405, 0x03040506],
                EnumArray =
                [
                    (TestEnum)0x00010203,
                    (TestEnum)0x01020304,
                    (TestEnum)0x02030405,
                    (TestEnum)0x03040506,
                ],
                StructArray =
                [
                    new TestStructPoint { X = 0x00FF, Y = 0xFF00 },
                    new TestStructPoint { X = 0xFF00, Y = 0x00FF },
                    new TestStructPoint { X = 0x55AA, Y = 0xAA55 },
                    new TestStructPoint { X = 0xAA55, Y = 0x55AA },
                ],
                LPByteArrayLength = 0x0004,
                LPByteArray = [0x00, 0x01, 0x02, 0x03],
            };
            var read = br.ReadType<TestStructArrays>();
            Assert.NotNull(read.ByteArray);
            Assert.True(expected.ByteArray.SequenceEqual(read.ByteArray));
            Assert.NotNull(read.IntArray);
            Assert.True(expected.IntArray.SequenceEqual(read.IntArray));
            Assert.NotNull(read.EnumArray);
            Assert.True(expected.EnumArray.SequenceEqual(read.EnumArray));
            Assert.NotNull(read.StructArray);
            Assert.True(expected.StructArray.SequenceEqual(read.StructArray));
            Assert.Equal(expected.LPByteArrayLength, read.LPByteArrayLength);
            Assert.NotNull(read.LPByteArray);
            Assert.True(expected.LPByteArray.SequenceEqual(read.LPByteArray));
        }

        [Fact]
        public void ReadTypeInheritanceTest()
        {
            byte[] structBytes1 =
            [
                0x41, 0x42, 0x43, 0x44, // Signature
                0x00, 0xFF, 0x00, 0xFF, // IdentifierType
                0xAA, 0x55, 0xAA, 0x55, // FieldA
                0x55, 0xAA, 0x55, 0xAA, // FieldB
            ];

            var stream1 = new MemoryStream(structBytes1);
            var br1 = new BinaryReader(stream1);
            var expected1 = new TestStructInheritanceChild1
            {
                Signature = [0x41, 0x42, 0x43, 0x44],
                IdentifierType = 0xFF00FF00,
                FieldA = 0x55AA55AA,
                FieldB = 0xAA55AA55,
            };
            var read1 = br1.ReadType<TestStructInheritanceChild1>();
            Assert.NotNull(read1?.Signature);
            Assert.Equal(expected1.Signature, read1.Signature);
            Assert.Equal(expected1.IdentifierType, read1.IdentifierType);
            Assert.Equal(expected1.FieldA, read1.FieldA);
            Assert.Equal(expected1.FieldB, read1.FieldB);

            byte[] structBytes2 =
            [
                0x41, 0x42, 0x43, 0x44, // Signature
                0x00, 0xFF, 0x00, 0xFF, // IdentifierType
                0xAA, 0x55, // FieldA
                0x55, 0xAA, // FieldB
            ];

            var stream2 = new MemoryStream(structBytes2);
            var br2 = new BinaryReader(stream2);
            var expected2 = new TestStructInheritanceChild2
            {
                Signature = [0x41, 0x42, 0x43, 0x44],
                IdentifierType = 0xFF00FF00,
                FieldA = 0x55AA,
                FieldB = 0xAA55,
            };
            var read2 = br2.ReadType<TestStructInheritanceChild2>();
            Assert.NotNull(read2?.Signature);
            Assert.Equal(expected2.Signature, read2.Signature);
            Assert.Equal(expected2.IdentifierType, read2.IdentifierType);
            Assert.Equal(expected2.FieldA, read2.FieldA);
            Assert.Equal(expected2.FieldB, read2.FieldB);
        }
    }
}
