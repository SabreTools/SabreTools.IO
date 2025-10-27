using System;
using System.Linq;
using System.Numerics;
using System.Text;
using SabreTools.IO.Extensions;
using SabreTools.IO.Numerics;
using Xunit;

namespace SabreTools.IO.Test.Extensions
{
    public class ByteArrayReaderExtensionsTests
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
        public void ReadByteBothEndianTest()
        {
            int offset = 0;
            BothUInt8 read = _bytes.ReadByteBothEndian(ref offset);
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
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
        public void ReadSByteBothEndianTest()
        {
            int offset = 0;
            BothInt8 read = _bytes.ReadSByteBothEndian(ref offset);
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
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
        public void ReadInt16LittleEndianTest()
        {
            int offset = 0;
            short read = _bytes.ReadInt16LittleEndian(ref offset);
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadInt16BothEndianTest()
        {
            int offset = 0;
            BothInt16 read = _bytes.ReadInt16BothEndian(ref offset);
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
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
        public void ReadUInt16LittleEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.ReadUInt16LittleEndian(ref offset);
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadUInt16BothEndianTest()
        {
            int offset = 0;
            BothUInt16 read = _bytes.ReadUInt16BothEndian(ref offset);
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
        }

        [Fact]
        public void ReadWORDTest()
        {
            int offset = 0;
            ushort read = _bytes.ReadWORD(ref offset);
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDBigEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.ReadWORDBigEndian(ref offset);
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadWORDLittleEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.ReadWORDLittleEndian(ref offset);
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDBothEndianTest()
        {
            int offset = 0;
            BothUInt16 read = _bytes.ReadWORDBothEndian(ref offset);
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
        }

        [Fact]
        public void ReadHalfTest()
        {
            int offset = 0;
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = _bytes.ReadHalf(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadHalfBigEndianTest()
        {
            int offset = 0;
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            Half read = _bytes.ReadHalfBigEndian(ref offset);
            Assert.Equal(expected, read);
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
        public void ReadInt24LittleEndianTest()
        {
            int offset = 0;
            int read = _bytes.ReadInt24LittleEndian(ref offset);
            Assert.Equal(0x020100, read);
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
        public void ReadUInt24LittleEndianTest()
        {
            int offset = 0;
            uint read = _bytes.ReadUInt24LittleEndian(ref offset);
            Assert.Equal((uint)0x020100, read);
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
        public void ReadInt32LittleEndianTest()
        {
            int offset = 0;
            int read = _bytes.ReadInt32LittleEndian(ref offset);
            Assert.Equal(0x03020100, read);
        }

        [Fact]
        public void ReadInt32BothEndianTest()
        {
            int offset = 0;
            BothInt32 read = _bytes.ReadInt32BothEndian(ref offset);
            Assert.Equal(0x03020100, read.LittleEndian);
            Assert.Equal(0x04050607, read.BigEndian);
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
        public void ReadUInt32LittleEndianTest()
        {
            int offset = 0;
            uint read = _bytes.ReadUInt32LittleEndian(ref offset);
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadUInt32BothEndianTest()
        {
            int offset = 0;
            BothUInt32 read = _bytes.ReadUInt32BothEndian(ref offset);
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
        }

        [Fact]
        public void ReadDWORDTest()
        {
            int offset = 0;
            uint read = _bytes.ReadDWORD(ref offset);
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDBigEndianTest()
        {
            int offset = 0;
            uint read = _bytes.ReadDWORDBigEndian(ref offset);
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadDWORDLittleEndianTest()
        {
            int offset = 0;
            uint read = _bytes.ReadDWORDLittleEndian(ref offset);
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDBothEndianTest()
        {
            int offset = 0;
            BothUInt32 read = _bytes.ReadDWORDBothEndian(ref offset);
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
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
        public void ReadInt48LittleEndianTest()
        {
            int offset = 0;
            long read = _bytes.ReadInt48LittleEndian(ref offset);
            Assert.Equal(0x050403020100, read);
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
        public void ReadUInt48LittleEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.ReadUInt48LittleEndian(ref offset);
            Assert.Equal((ulong)0x050403020100, read);
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
        public void ReadInt64LittleEndianTest()
        {
            int offset = 0;
            long read = _bytes.ReadInt64LittleEndian(ref offset);
            Assert.Equal(0x0706050403020100, read);
        }

        [Fact]
        public void ReadInt64BothEndianTest()
        {
            int offset = 0;
            BothInt64 read = _bytes.ReadInt64BothEndian(ref offset);
            Assert.Equal(0x0706050403020100, read.LittleEndian);
            Assert.Equal(0x08090A0B0C0D0E0F, read.BigEndian);
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
        public void ReadUInt64LittleEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.ReadUInt64LittleEndian(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadUInt64BothEndianTest()
        {
            int offset = 0;
            BothUInt64 read = _bytes.ReadUInt64BothEndian(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
        }

        [Fact]
        public void ReadQWORDTest()
        {
            int offset = 0;
            ulong read = _bytes.ReadQWORD(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDBigEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.ReadQWORDBigEndian(ref offset);
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadQWORDLittleEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.ReadQWORDLittleEndian(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDBothEndianTest()
        {
            int offset = 0;
            BothUInt64 read = _bytes.ReadQWORDBothEndian(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
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
        public void ReadDecimalTest()
        {
            int offset = 0;
            decimal expected = 0.0123456789M;
            decimal read = _decimalBytes.ReadDecimal(ref offset);
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDecimalBigEndianTest()
        {
            int offset = 0;
            decimal expected = 0.0123456789M;
            decimal read = _decimalBytes.Reverse().ToArray().ReadDecimalBigEndian(ref offset);
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

        [Fact]
        public void ReadNullTerminatedStringTest()
        {
            // Encoding.ASCII
            int offset = 0;
            byte[] bytes = [0x41, 0x42, 0x43, 0x00];
            string? actual = bytes.ReadNullTerminatedString(ref offset, Encoding.ASCII);
            Assert.Equal("ABC", actual);

            // Encoding.UTF8
            offset = 0;
            bytes = [0x41, 0x42, 0x43, 0x00];
            actual = bytes.ReadNullTerminatedString(ref offset, Encoding.UTF8);
            Assert.Equal("ABC", actual);

            // Encoding.Unicode
            offset = 0;
            bytes = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00, 0x00];
            actual = bytes.ReadNullTerminatedString(ref offset, Encoding.Unicode);
            Assert.Equal("ABC", actual);

            // Encoding.UTF32
            offset = 0;
            bytes = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
            actual = bytes.ReadNullTerminatedString(ref offset, Encoding.UTF32);
            Assert.Equal("ABC", actual);

            // Encoding.Latin1
            offset = 0;
            bytes = [0x41, 0x42, 0x43, 0x00];
            actual = bytes.ReadNullTerminatedString(ref offset, Encoding.Latin1);
            Assert.Equal("ABC", actual);
        }

        [Fact]
        public void ReadTypeTest()
        {
            // Guid
            int offset = 0;
            var expectedGuid = new Guid(_bytes);
            Guid actualGuid = _bytes.ReadType<Guid>(ref offset);
            Assert.Equal(expectedGuid, actualGuid);

            // Half
            offset = 0;
            Half expectedHalf = BitConverter.Int16BitsToHalf(0x0100);
            Half actualHalf = _bytes.ReadType<Half>(ref offset);
            Assert.Equal(expectedHalf, actualHalf);

            // Int128
            offset = 0;
            Int128 expectedInt128 = (Int128)new BigInteger(_bytes);
            Int128 actualInt128 = _bytes.ReadType<Int128>(ref offset);
            Assert.Equal(expectedHalf, actualHalf);

            // UInt128
            offset = 0;
            UInt128 expectedUInt128 = (UInt128)new BigInteger(_bytes);
            UInt128 actualUInt128 = _bytes.ReadType<UInt128>(ref offset);
            Assert.Equal(expectedHalf, actualHalf);

            // Enum
            offset = 0;
            TestEnum expectedTestEnum = (TestEnum)0x03020100;
            TestEnum actualTestEnum = _bytes.ReadType<TestEnum>(ref offset);
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

            int offset = 0;
            var expected = new TestStructExplicit
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0504,
                FourthValue = 0x0706,
                FifthValue = "ABC",
            };
            var read = bytesWithString.ReadType<TestStructExplicit>(ref offset);
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

            int offset = 0;
            var expected = new TestStructSequential
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
                FifthValue = "ABC",
            };
            var read = bytesWithString.ReadType<TestStructSequential>(ref offset);
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

            int offset = 0;
            var expected = new TestStructStrings
            {
                AnsiBStr = "ABC",
                BStr = "ABC",
                ByValTStr = "ABC",
                LPStr = "ABC",
                LPWStr = "ABC",
            };
            var read = structBytes.ReadType<TestStructStrings>(ref offset);
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

            int offset = 0;
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
            var read = structBytes.ReadType<TestStructArrays>(ref offset);
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

            int offset1 = 0;
            var expected1 = new TestStructInheritanceChild1
            {
                Signature = [0x41, 0x42, 0x43, 0x44],
                IdentifierType = 0xFF00FF00,
                FieldA = 0x55AA55AA,
                FieldB = 0xAA55AA55,
            };
            var read1 = structBytes1.ReadType<TestStructInheritanceChild1>(ref offset1);
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

            int offset2 = 0;
            var expected2 = new TestStructInheritanceChild2
            {
                Signature = [0x41, 0x42, 0x43, 0x44],
                IdentifierType = 0xFF00FF00,
                FieldA = 0x55AA,
                FieldB = 0xAA55,
            };
            var read2 = structBytes2.ReadType<TestStructInheritanceChild2>(ref offset2);
            Assert.NotNull(read2?.Signature);
            Assert.Equal(expected2.Signature, read2.Signature);
            Assert.Equal(expected2.IdentifierType, read2.IdentifierType);
            Assert.Equal(expected2.FieldA, read2.FieldA);
            Assert.Equal(expected2.FieldB, read2.FieldB);
        }

        #endregion

        #region Peek Read

        [Fact]
        public void PeekByteTest()
        {
            int offset = 0;
            byte read = _bytes.PeekByte(ref offset);
            Assert.Equal(0x00, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekByteValueTest()
        {
            int offset = 0;
            byte read = _bytes.PeekByteValue(ref offset);
            Assert.Equal(0x00, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekBytesTest()
        {
            int offset = 0, length = 4;
            byte[] read = _bytes.PeekBytes(ref offset, length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length)));
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekSByteTest()
        {
            int offset = 0;
            sbyte read = _bytes.PeekSByte(ref offset);
            Assert.Equal(0x00, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekCharTest()
        {
            int offset = 0;
            char read = _bytes.PeekChar(ref offset);
            Assert.Equal('\0', read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt16Test()
        {
            int offset = 0;
            short read = _bytes.PeekInt16(ref offset);
            Assert.Equal(0x0100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt16BigEndianTest()
        {
            int offset = 0;
            short read = _bytes.PeekInt16BigEndian(ref offset);
            Assert.Equal(0x0001, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt16LittleEndianTest()
        {
            int offset = 0;
            short read = _bytes.PeekInt16LittleEndian(ref offset);
            Assert.Equal(0x0100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt16Test()
        {
            int offset = 0;
            ushort read = _bytes.PeekUInt16(ref offset);
            Assert.Equal(0x0100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt16BigEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.PeekUInt16BigEndian(ref offset);
            Assert.Equal(0x0001, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt16LittleEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.PeekUInt16LittleEndian(ref offset);
            Assert.Equal(0x0100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekWORDTest()
        {
            int offset = 0;
            ushort read = _bytes.PeekWORD(ref offset);
            Assert.Equal(0x0100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekWORDBigEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.PeekWORDBigEndian(ref offset);
            Assert.Equal(0x0001, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekWORDLittleEndianTest()
        {
            int offset = 0;
            ushort read = _bytes.PeekWORDLittleEndian(ref offset);
            Assert.Equal(0x0100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekHalfTest()
        {
            int offset = 0;
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = _bytes.PeekHalf(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekHalfBigEndianTest()
        {
            int offset = 0;
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            Half read = _bytes.PeekHalfBigEndian(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt24Test()
        {
            int offset = 0;
            int read = _bytes.PeekInt24(ref offset);
            Assert.Equal(0x020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt24BigEndianTest()
        {
            int offset = 0;
            int read = _bytes.PeekInt24BigEndian(ref offset);
            Assert.Equal(0x000102, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt24LittleEndianTest()
        {
            int offset = 0;
            int read = _bytes.PeekInt24LittleEndian(ref offset);
            Assert.Equal(0x020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt24Test()
        {
            int offset = 0;
            uint read = _bytes.PeekUInt24(ref offset);
            Assert.Equal((uint)0x020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt24BigEndianTest()
        {
            int offset = 0;
            uint read = _bytes.PeekUInt24BigEndian(ref offset);
            Assert.Equal((uint)0x000102, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt24LittleEndianTest()
        {
            int offset = 0;
            uint read = _bytes.PeekUInt24LittleEndian(ref offset);
            Assert.Equal((uint)0x020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt32Test()
        {
            int offset = 0;
            int read = _bytes.PeekInt32(ref offset);
            Assert.Equal(0x03020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt32BigEndianTest()
        {
            int offset = 0;
            int read = _bytes.PeekInt32BigEndian(ref offset);
            Assert.Equal(0x00010203, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt32LittleEndianTest()
        {
            int offset = 0;
            int read = _bytes.PeekInt32LittleEndian(ref offset);
            Assert.Equal(0x03020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt32Test()
        {
            int offset = 0;
            uint read = _bytes.PeekUInt32(ref offset);
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt32BigEndianTest()
        {
            int offset = 0;
            uint read = _bytes.PeekUInt32BigEndian(ref offset);
            Assert.Equal((uint)0x00010203, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt32LittleEndianTest()
        {
            int offset = 0;
            uint read = _bytes.PeekUInt32LittleEndian(ref offset);
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekDWORDTest()
        {
            int offset = 0;
            uint read = _bytes.PeekDWORD(ref offset);
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekDWORDBigEndianTest()
        {
            int offset = 0;
            uint read = _bytes.PeekDWORDBigEndian(ref offset);
            Assert.Equal((uint)0x00010203, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekDWORDLittleEndianTest()
        {
            int offset = 0;
            uint read = _bytes.PeekDWORDLittleEndian(ref offset);
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekSingleTest()
        {
            int offset = 0;
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = _bytes.PeekSingle(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekSingleBigEndianTest()
        {
            int offset = 0;
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = _bytes.PeekSingleBigEndian(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt48Test()
        {
            int offset = 0;
            long read = _bytes.PeekInt48(ref offset);
            Assert.Equal(0x050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt48BigEndianTest()
        {
            int offset = 0;
            long read = _bytes.PeekInt48BigEndian(ref offset);
            Assert.Equal(0x000102030405, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt48LittleEndianTest()
        {
            int offset = 0;
            long read = _bytes.PeekInt48LittleEndian(ref offset);
            Assert.Equal(0x050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt48Test()
        {
            int offset = 0;
            ulong read = _bytes.PeekUInt48(ref offset);
            Assert.Equal((ulong)0x050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt48BigEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.PeekUInt48BigEndian(ref offset);
            Assert.Equal((ulong)0x000102030405, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt48LittleEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.PeekUInt48LittleEndian(ref offset);
            Assert.Equal((ulong)0x050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt64Test()
        {
            int offset = 0;
            long read = _bytes.PeekInt64(ref offset);
            Assert.Equal(0x0706050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt64BigEndianTest()
        {
            int offset = 0;
            long read = _bytes.PeekInt64BigEndian(ref offset);
            Assert.Equal(0x0001020304050607, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt64LittleEndianTest()
        {
            int offset = 0;
            long read = _bytes.PeekInt64LittleEndian(ref offset);
            Assert.Equal(0x0706050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt64Test()
        {
            int offset = 0;
            ulong read = _bytes.PeekUInt64(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt64BigEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.PeekUInt64BigEndian(ref offset);
            Assert.Equal((ulong)0x0001020304050607, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt64LittleEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.PeekUInt64LittleEndian(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekQWORDTest()
        {
            int offset = 0;
            ulong read = _bytes.PeekQWORD(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekQWORDBigEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.PeekQWORDBigEndian(ref offset);
            Assert.Equal((ulong)0x0001020304050607, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekQWORDLittleEndianTest()
        {
            int offset = 0;
            ulong read = _bytes.PeekQWORDLittleEndian(ref offset);
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekDoubleTest()
        {
            int offset = 0;
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = _bytes.PeekDouble(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekDoubleBigEndianTest()
        {
            int offset = 0;
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = _bytes.PeekDoubleBigEndian(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekDecimalTest()
        {
            int offset = 0;
            decimal expected = 0.0123456789M;
            decimal read = _decimalBytes.PeekDecimal(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekDecimalBigEndianTest()
        {
            int offset = 0;
            decimal expected = 0.0123456789M;
            decimal read = _decimalBytes.Reverse().ToArray().PeekDecimalBigEndian(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekGuidTest()
        {
            int offset = 0;
            var expected = new Guid(_bytes);
            Guid read = _bytes.PeekGuid(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekGuidBigEndianTest()
        {
            int offset = 0;
            var expected = new Guid(_bytes.Reverse().ToArray());
            Guid read = _bytes.PeekGuidBigEndian(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt128Test()
        {
            int offset = 0;
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = _bytes.PeekInt128(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = (Int128)new BigInteger(reversed);
            Int128 read = _bytes.PeekInt128BigEndian(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt128Test()
        {
            int offset = 0;
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = _bytes.PeekUInt128(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        [Fact]
        public void PeekUInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            UInt128 read = _bytes.PeekUInt128BigEndian(ref offset);
            Assert.Equal(expected, read);
            Assert.Equal(0, offset);
        }

        #endregion

        #region Try Read

        [Fact]
        public void TryReadByteTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadByte(ref offset, out byte read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadByteValueTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadByteValue(ref offset, out byte read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadBytesTest()
        {
            int offset = 0, length = 4;
            bool actual = Array.Empty<byte>().TryReadBytes(ref offset, length, out byte[] read);
            Assert.False(actual);
            Assert.Empty(read);
        }

        [Fact]
        public void TryReadSByteTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadSByte(ref offset, out sbyte read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadCharTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadChar(ref offset, out char read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt16(ref offset, out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt16BigEndian(ref offset, out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt16LittleEndian(ref offset, out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt16(ref offset, out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt16BigEndian(ref offset, out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt16LittleEndian(ref offset, out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadWORD(ref offset, out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDBigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadWORDBigEndian(ref offset, out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDLittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadWORDLittleEndian(ref offset, out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadHalfTest()
        {
            int offset = 0;
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            bool actual = Array.Empty<byte>().TryReadHalf(ref offset, out Half read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadHalfBigEndianTest()
        {
            int offset = 0;
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            bool actual = Array.Empty<byte>().TryReadHalfBigEndian(ref offset, out Half read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt24Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt24(ref offset, out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt24BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt24BigEndian(ref offset, out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt24LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt24LittleEndian(ref offset, out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt24Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt24(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt24BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt24BigEndian(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt24LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt24LittleEndian(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt32(ref offset, out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt32BigEndian(ref offset, out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt32LittleEndian(ref offset, out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt32(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt32BigEndian(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt32LittleEndian(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadDWORD(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDBigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadDWORDBigEndian(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDLittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadDWORDLittleEndian(ref offset, out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadSingleTest()
        {
            int offset = 0;
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            bool actual = Array.Empty<byte>().TryReadSingle(ref offset, out float read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadSingleBigEndianTest()
        {
            int offset = 0;
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            bool actual = Array.Empty<byte>().TryReadSingleBigEndian(ref offset, out float read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt48Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt48(ref offset, out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt48BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt48BigEndian(ref offset, out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt48LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt48LittleEndian(ref offset, out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt48Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt48(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt48BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt48BigEndian(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt48LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt48LittleEndian(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt64(ref offset, out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt64BigEndian(ref offset, out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadInt64LittleEndian(ref offset, out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64Test()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt64(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64BigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt64BigEndian(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64LittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadUInt64LittleEndian(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadQWORD(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDBigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadQWORDBigEndian(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDLittleEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadQWORDLittleEndian(ref offset, out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDoubleTest()
        {
            int offset = 0;
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            bool actual = Array.Empty<byte>().TryReadDouble(ref offset, out double read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDoubleBigEndianTest()
        {
            int offset = 0;
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            bool actual = Array.Empty<byte>().TryReadDoubleBigEndian(ref offset, out double read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDecimalTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadDecimal(ref offset, out decimal read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDecimalBigEndianTest()
        {
            int offset = 0;
            bool actual = Array.Empty<byte>().TryReadDecimalBigEndian(ref offset, out decimal read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadGuidTest()
        {
            int offset = 0;
            var expected = new Guid(_bytes);
            bool actual = Array.Empty<byte>().TryReadGuid(ref offset, out Guid read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadGuidBigEndianTest()
        {
            int offset = 0;
            var expected = new Guid(_bytes.Reverse().ToArray());
            bool actual = Array.Empty<byte>().TryReadGuidBigEndian(ref offset, out Guid read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt128Test()
        {
            int offset = 0;
            var expected = (Int128)new BigInteger(_bytes);
            bool actual = Array.Empty<byte>().TryReadInt128(ref offset, out Int128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = (Int128)new BigInteger(reversed);
            bool actual = Array.Empty<byte>().TryReadInt128BigEndian(ref offset, out Int128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt128Test()
        {
            int offset = 0;
            var expected = (UInt128)new BigInteger(_bytes);
            bool actual = Array.Empty<byte>().TryReadUInt128(ref offset, out UInt128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt128BigEndianTest()
        {
            int offset = 0;
            var reversed = _bytes.Reverse().ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            bool actual = Array.Empty<byte>().TryReadUInt128BigEndian(ref offset, out UInt128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        #endregion
    }
}
