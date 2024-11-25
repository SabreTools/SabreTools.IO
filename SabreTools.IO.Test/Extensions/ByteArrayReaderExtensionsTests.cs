using System;
using System.Linq;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Text;
using SabreTools.IO.Extensions;
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

#if NET6_0_OR_GREATER
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
#endif

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

#if NET6_0_OR_GREATER
            // Half
            offset = 0;
            Half expectedHalf = BitConverter.Int16BitsToHalf(0x0100);
            Half actualHalf = _bytes.ReadType<Half>(ref offset);
            Assert.Equal(expectedHalf, actualHalf);
#endif

#if NET7_0_OR_GREATER
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
#endif

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
    }
}