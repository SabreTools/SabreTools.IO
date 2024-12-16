using System;
using System.IO;
using System.Linq;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Text;
using SabreTools.IO.Extensions;
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
        public void ReadInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            short read = stream.ReadInt16LittleEndian();
            Assert.Equal(0x0100, read);
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

#if NET6_0_OR_GREATER
            // Half
            stream = new MemoryStream(_bytes);
            Half expectedHalf = BitConverter.Int16BitsToHalf(0x0100);
            Half actualHalf = stream.ReadType<Half>();
            Assert.Equal(expectedHalf, actualHalf);
#endif

#if NET7_0_OR_GREATER
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
#endif

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
    }
}