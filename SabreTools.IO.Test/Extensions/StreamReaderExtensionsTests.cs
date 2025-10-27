using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using SabreTools.IO.Extensions;
using SabreTools.IO.Numerics;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class StreamReaderExtensionsTests
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

        #region Exact Read

        [Fact]
        public void ReadByteArrayTest()
        {
            byte[] arr = new byte[4];
            var stream = new MemoryStream(_bytes);
            int read = stream.Read(arr, 0, 4);
            Assert.Equal(4, read);
            Assert.True(arr.SequenceEqual(_bytes.Take(4)));
        }

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
        public void ReadByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt8 read = stream.ReadByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
        }

        [Fact]
        public void ReadSByteTest()
        {
            var stream = new MemoryStream(_bytes);
            sbyte read = stream.ReadSByte();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadSByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt8 read = stream.ReadSByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
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
        public void ReadInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            short read = stream.ReadInt16LittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt16 read = stream.ReadInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
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
        public void ReadUInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.ReadUInt16LittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadUInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt16 read = stream.ReadUInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
        }

        [Fact]
        public void ReadWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.ReadWORD();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.ReadWORDBigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.ReadWORDLittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt16 read = stream.ReadWORDBothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
        }

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
        public void ReadInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.ReadInt24LittleEndian();
            Assert.Equal(0x020100, read);
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
        public void ReadUInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadUInt24LittleEndian();
            Assert.Equal((uint)0x020100, read);
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
        public void ReadInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.ReadInt32LittleEndian();
            Assert.Equal(0x03020100, read);
        }

        [Fact]
        public void ReadInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt32 read = stream.ReadInt32BothEndian();
            Assert.Equal(0x03020100, read.LittleEndian);
            Assert.Equal(0x04050607, read.BigEndian);
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
        public void ReadUInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadUInt32LittleEndian();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadUInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt32 read = stream.ReadUInt32BothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
        }

        [Fact]
        public void ReadDWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadDWORD();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadDWORDBigEndian();
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadDWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.ReadDWORDLittleEndian();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt32 read = stream.ReadDWORDBothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
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
        public void ReadInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.ReadInt48LittleEndian();
            Assert.Equal(0x050403020100, read);
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
        public void ReadUInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadUInt48LittleEndian();
            Assert.Equal((ulong)0x050403020100, read);
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
        public void ReadInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.ReadInt64LittleEndian();
            Assert.Equal(0x0706050403020100, read);
        }

        [Fact]
        public void ReadInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt64 read = stream.ReadInt64BothEndian();
            Assert.Equal(0x0706050403020100, read.LittleEndian);
            Assert.Equal(0x08090A0B0C0D0E0F, read.BigEndian);
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
        public void ReadUInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadUInt64LittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadUInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt64 read = stream.ReadUInt64BothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
        }

        [Fact]
        public void ReadQWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadQWORD();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadQWORDBigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadQWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.ReadQWORDLittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt64 read = stream.ReadQWORDBothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
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

        [Fact]
        public void ReadNullTerminatedStringTest()
        {
            // Encoding.ASCII
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            var stream = new MemoryStream(bytes);
            string? actual = stream.ReadNullTerminatedString(Encoding.ASCII);
            Assert.Equal("ABC", actual);

            // Encoding.UTF8
            bytes = [0x41, 0x42, 0x43, 0x00];
            stream = new MemoryStream(bytes);
            actual = stream.ReadNullTerminatedString(Encoding.UTF8);
            Assert.Equal("ABC", actual);

            // Encoding.Unicode
            bytes = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00, 0x00];
            stream = new MemoryStream(bytes);
            actual = stream.ReadNullTerminatedString(Encoding.Unicode);
            Assert.Equal("ABC", actual);

            // Encoding.UTF32
            bytes = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
            stream = new MemoryStream(bytes);
            actual = stream.ReadNullTerminatedString(Encoding.UTF32);
            Assert.Equal("ABC", actual);

            // Encoding.Latin1
            bytes = [0x41, 0x42, 0x43, 0x00];
            stream = new MemoryStream(bytes);
            actual = stream.ReadNullTerminatedString(Encoding.Latin1);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadTypeTest()
        {
            // Guid
            var stream = new MemoryStream(_bytes);
            var expectedGuid = new Guid(_bytes);
            Guid actualGuid = stream.ReadType<Guid>();
            Assert.Equal(expectedGuid, actualGuid);

            // Half
            stream = new MemoryStream(_bytes);
            Half expectedHalf = BitConverter.Int16BitsToHalf(0x0100);
            Half actualHalf = stream.ReadType<Half>();
            Assert.Equal(expectedHalf, actualHalf);

            // Int128
            stream = new MemoryStream(_bytes);
            Int128 expectedInt128 = (Int128)new BigInteger(_bytes);
            Int128 actualInt128 = stream.ReadType<Int128>();
            Assert.Equal(expectedHalf, actualHalf);

            // UInt128
            stream = new MemoryStream(_bytes);
            UInt128 expectedUInt128 = (UInt128)new BigInteger(_bytes);
            UInt128 actualUInt128 = stream.ReadType<UInt128>();
            Assert.Equal(expectedHalf, actualHalf);

            // Enum
            stream = new MemoryStream(_bytes);
            TestEnum expectedTestEnum = (TestEnum)0x03020100;
            TestEnum actualTestEnum = stream.ReadType<TestEnum>();
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
            var expected = new TestStructExplicit
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0504,
                FourthValue = 0x0706,
                FifthValue = "ABC",
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
            byte[] bytesWithString =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x41, 0x42, 0x43, 0x00,
            ];

            var stream = new MemoryStream(bytesWithString);
            var expected = new TestStructSequential
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
                FifthValue = "ABC",
            };
            var read = stream.ReadType<TestStructSequential>();
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
            var expected = new TestStructStrings
            {
                AnsiBStr = "ABC",
                BStr = "ABC",
                ByValTStr = "ABC",
                LPStr = "ABC",
                LPWStr = "ABC",
            };
            var read = stream.ReadType<TestStructStrings>();
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
            var read = stream.ReadType<TestStructArrays>();
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
            var expected1 = new TestStructInheritanceChild1
            {
                Signature = [0x41, 0x42, 0x43, 0x44],
                IdentifierType = 0xFF00FF00,
                FieldA = 0x55AA55AA,
                FieldB = 0xAA55AA55,
            };
            var read1 = stream1.ReadType<TestStructInheritanceChild1>();
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
            var expected2 = new TestStructInheritanceChild2
            {
                Signature = [0x41, 0x42, 0x43, 0x44],
                IdentifierType = 0xFF00FF00,
                FieldA = 0x55AA,
                FieldB = 0xAA55,
            };
            var read2 = stream2.ReadType<TestStructInheritanceChild2>();
            Assert.NotNull(read2?.Signature);
            Assert.Equal(expected2.Signature, read2.Signature);
            Assert.Equal(expected2.IdentifierType, read2.IdentifierType);
            Assert.Equal(expected2.FieldA, read2.FieldA);
            Assert.Equal(expected2.FieldB, read2.FieldB);
        }

        #endregion

        #region Peek Read

        [Fact]
        public void PeekByteValueTest()
        {
            var stream = new MemoryStream(_bytes);
            byte read = stream.PeekByteValue();
            Assert.Equal(0x00, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt8 read = stream.PeekByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekBytesTest()
        {
            var stream = new MemoryStream(_bytes);
            int length = 4;
            byte[] read = stream.PeekBytes(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length)));
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekSByteTest()
        {
            var stream = new MemoryStream(_bytes);
            sbyte read = stream.PeekSByte();
            Assert.Equal(0x00, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekSByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt8 read = stream.PeekSByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekCharTest()
        {
            var stream = new MemoryStream(_bytes);
            char read = stream.PeekChar();
            Assert.Equal('\0', read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            short read = stream.PeekInt16();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            short read = stream.PeekInt16BigEndian();
            Assert.Equal(0x0001, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            short read = stream.PeekInt16LittleEndian();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt16 read = stream.PeekInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.PeekUInt16();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.PeekUInt16BigEndian();
            Assert.Equal(0x0001, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.PeekUInt16LittleEndian();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt16 read = stream.PeekUInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.PeekWORD();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.PeekWORDBigEndian();
            Assert.Equal(0x0001, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ushort read = stream.PeekWORDLittleEndian();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt16 read = stream.PeekWORDBothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekHalfTest()
        {
            var stream = new MemoryStream(_bytes);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = stream.PeekHalf();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekHalfBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            Half read = stream.PeekHalfBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.PeekInt24();
            Assert.Equal(0x020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.PeekInt24BigEndian();
            Assert.Equal(0x000102, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.PeekInt24LittleEndian();
            Assert.Equal(0x020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekUInt24();
            Assert.Equal((uint)0x020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekUInt24BigEndian();
            Assert.Equal((uint)0x000102, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekUInt24LittleEndian();
            Assert.Equal((uint)0x020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.PeekInt32();
            Assert.Equal(0x03020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.PeekInt32BigEndian();
            Assert.Equal(0x00010203, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            int read = stream.PeekInt32LittleEndian();
            Assert.Equal(0x03020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt32 read = stream.PeekInt32BothEndian();
            Assert.Equal(0x03020100, read.LittleEndian);
            Assert.Equal(0x04050607, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekUInt32();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekUInt32BigEndian();
            Assert.Equal((uint)0x00010203, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekUInt32LittleEndian();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt32 read = stream.PeekUInt32BothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekDWORD();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekDWORDBigEndian();
            Assert.Equal((uint)0x00010203, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            uint read = stream.PeekDWORDLittleEndian();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt32 read = stream.PeekDWORDBothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekSingleTest()
        {
            var stream = new MemoryStream(_bytes);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = stream.PeekSingle();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekSingleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = stream.PeekSingleBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.PeekInt48();
            Assert.Equal(0x050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.PeekInt48BigEndian();
            Assert.Equal(0x000102030405, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.PeekInt48LittleEndian();
            Assert.Equal(0x050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekUInt48();
            Assert.Equal((ulong)0x050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekUInt48BigEndian();
            Assert.Equal((ulong)0x000102030405, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekUInt48LittleEndian();
            Assert.Equal((ulong)0x050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.PeekInt64();
            Assert.Equal(0x0706050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.PeekInt64BigEndian();
            Assert.Equal(0x0001020304050607, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            long read = stream.PeekInt64LittleEndian();
            Assert.Equal(0x0706050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothInt64 read = stream.PeekInt64BothEndian();
            Assert.Equal(0x0706050403020100, read.LittleEndian);
            Assert.Equal(0x08090A0B0C0D0E0F, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekUInt64();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekUInt64BigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekUInt64LittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt64 read = stream.PeekUInt64BothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekQWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekQWORD();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekQWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekQWORDBigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekQWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            ulong read = stream.PeekQWORDLittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekQWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            BothUInt64 read = stream.PeekQWORDBothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDoubleTest()
        {
            var stream = new MemoryStream(_bytes);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = stream.PeekDouble();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDoubleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = stream.PeekDoubleBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDecimalTest()
        {
            var stream = new MemoryStream(_decimalBytes);
            decimal expected = 0.0123456789M;
            decimal read = stream.PeekDecimal();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekDecimalBigEndianTest()
        {
            var stream = new MemoryStream(_decimalBytes.Reverse().ToArray());
            decimal expected = 0.0123456789M;
            decimal read = stream.PeekDecimalBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekGuidTest()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new Guid(_bytes);
            Guid read = stream.PeekGuid();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekGuidBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var expected = new Guid(_bytes.Reverse().ToArray());
            Guid read = stream.PeekGuidBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = stream.PeekInt128();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (Int128)new BigInteger(reversed);
            Int128 read = stream.PeekInt128BigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = stream.PeekUInt128();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        [Fact]
        public void PeekUInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            UInt128 read = stream.PeekUInt128BigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, stream.Position);
        }

        #endregion

        #region Try Read

        [Fact]
        public void TryReadByteValueTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadByteValue(out byte read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadBytesTest()
        {
            var stream = new MemoryStream([]);
            int length = 4;
            bool actual = stream.TryReadBytes(length, out byte[] read);
            Assert.False(actual);
            Assert.Empty(read);
        }

        [Fact]
        public void TryReadByteBothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadByteBothEndian(out BothUInt8 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadSByteTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadSByte(out sbyte read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadSByteBothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadSByteBothEndian(out BothInt8 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadCharTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadChar(out char read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt16(out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt16BigEndian(out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt16LittleEndian(out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16BothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt16BothEndian(out BothInt16 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadUInt16Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt16(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt16BigEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt16LittleEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16BothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt16BothEndian(out BothUInt16 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadWORDTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadWORD(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDBigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadWORDBigEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadWORDLittleEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDBothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadWORDBothEndian(out BothUInt16 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadHalfTest()
        {
            var stream = new MemoryStream([]);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            bool actual = stream.TryReadHalf(out Half read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadHalfBigEndianTest()
        {
            var stream = new MemoryStream([]);
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            bool actual = stream.TryReadHalfBigEndian(out Half read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt24Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt24(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt24BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt24BigEndian(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt24LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt24LittleEndian(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt24Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt24(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt24BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt24BigEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt24LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt24LittleEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt32(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt32BigEndian(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt32LittleEndian(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32BothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt32BothEndian(out BothInt32 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadUInt32Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt32(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt32BigEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt32LittleEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32BothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt32BothEndian(out BothUInt32 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadDWORDTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadDWORD(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDBigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadDWORDBigEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadDWORDLittleEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDBothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadDWORDBothEndian(out BothUInt32 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadSingleTest()
        {
            var stream = new MemoryStream([]);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            bool actual = stream.TryReadSingle(out float read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadSingleBigEndianTest()
        {
            var stream = new MemoryStream([]);
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            bool actual = stream.TryReadSingleBigEndian(out float read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt48Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt48(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt48BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt48BigEndian(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt48LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt48LittleEndian(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt48Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt48(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt48BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt48BigEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt48LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt48LittleEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt64(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt64BigEndian(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt64LittleEndian(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64BothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadInt64BothEndian(out BothInt64 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadUInt64Test()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt64(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64BigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt64BigEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt64LittleEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64BothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadUInt64BothEndian(out BothUInt64 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadQWORDTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadQWORD(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDBigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadQWORDBigEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadQWORDLittleEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDBothEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadQWORDBothEndian(out BothUInt64 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadDoubleTest()
        {
            var stream = new MemoryStream([]);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            bool actual = stream.TryReadDouble(out double read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDoubleBigEndianTest()
        {
            var stream = new MemoryStream([]);
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            bool actual = stream.TryReadDoubleBigEndian(out double read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDecimalTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadDecimal(out decimal read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDecimalBigEndianTest()
        {
            var stream = new MemoryStream([]);
            bool actual = stream.TryReadDecimalBigEndian(out decimal read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadGuidTest()
        {
            var stream = new MemoryStream([]);
            var expected = new Guid(_bytes);
            bool actual = stream.TryReadGuid(out Guid read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadGuidBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var expected = new Guid(_bytes.Reverse().ToArray());
            bool actual = stream.TryReadGuidBigEndian(out Guid read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt128Test()
        {
            var stream = new MemoryStream([]);
            var expected = (Int128)new BigInteger(_bytes);
            bool actual = stream.TryReadInt128(out Int128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt128BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (Int128)new BigInteger(reversed);
            bool actual = stream.TryReadInt128BigEndian(out Int128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt128Test()
        {
            var stream = new MemoryStream([]);
            var expected = (UInt128)new BigInteger(_bytes);
            bool actual = stream.TryReadUInt128(out UInt128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt128BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var reversed = _bytes.Reverse().ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            bool actual = stream.TryReadUInt128BigEndian(out UInt128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        #endregion
    }
}
