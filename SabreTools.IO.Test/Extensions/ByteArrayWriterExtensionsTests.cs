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
    public class ByteArrayWriterExtensionsTests
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
        public void WriteBytesBigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(4).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, [0x03, 0x02, 0x01, 0x00]);
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
        public void WriteCharEncodingTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = [0x00, 0x00];
            bool write = buffer.Write(ref offset, '\0', Encoding.Unicode);
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

#if NET6_0_OR_GREATER
        [Fact]
        public void WriteHalfTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = buffer.Write(ref offset, BitConverter.Int16BitsToHalf(0x0100));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteHalfBigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(2).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, BitConverter.Int16BitsToHalf(0x0001));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }
#endif

        [Fact]
        public void WriteInt24Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = buffer.WriteAsInt24(ref offset, 0x020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt24BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = buffer.WriteAsInt24BigEndian(ref offset, 0x000102);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt24Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = buffer.WriteAsUInt24(ref offset, 0x020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt24BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(3).ToArray();
            bool write = buffer.WriteAsUInt24BigEndian(ref offset, 0x000102);
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
        public void WriteInt48Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = buffer.WriteAsInt48(ref offset, 0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteInt48BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = buffer.WriteAsInt48BigEndian(ref offset, 0x000102030405);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt48Test()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = buffer.WriteAsUInt48(ref offset, 0x050403020100);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteUInt48BigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(6).ToArray();
            bool write = buffer.WriteAsUInt48BigEndian(ref offset, 0x000102030405);
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
        public void WriteDoubleTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = buffer.Write(ref offset, BitConverter.Int64BitsToDouble(0x0706050403020100));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteDoubleBigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _bytes.Take(8).ToArray();
            bool write = buffer.WriteBigEndian(ref offset, BitConverter.Int64BitsToDouble(0x0001020304050607));
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteDecimalTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _decimalBytes.Take(16).ToArray();
            bool write = buffer.Write(ref offset, 0.0123456789M);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteDecimalBigEndianTest()
        {
            byte[] buffer = new byte[16];
            int offset = 0;
            byte[] expected = _decimalBytes.Take(16).Reverse().ToArray();
            bool write = buffer.WriteBigEndian(ref offset, 0.0123456789M);
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

        [Fact]
        public void WriteNullTerminatedAnsiStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[4];
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = buffer.WriteNullTerminatedAnsiString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteNullTerminatedUTF8StringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[4];
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = buffer.WriteNullTerminatedUTF8String(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteNullTerminatedUnicodeStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[8];
            byte[] expected = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00];

            bool write = buffer.WriteNullTerminatedUnicodeString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteNullTerminatedUTF32StringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[16];
            byte[] expected = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];

            bool write = buffer.WriteNullTerminatedUTF32String(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WritePrefixedAnsiStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[4];
            byte[] expected = [0x03, 0x41, 0x42, 0x43];

            bool write = buffer.WritePrefixedAnsiString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WritePrefixedUnicodeStringTest()
        {
            int offset = 0;
            byte[] buffer = new byte[8];
            byte[] expected = [0x03, 0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00];

            bool write = buffer.WritePrefixedUnicodeString(ref offset, "ABC");
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteTypeTest()
        {
            // Guid
            int offset = 0;
            byte[] buffer = new byte[16];
            bool actual = buffer.WriteType<Guid>(ref offset, new Guid(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, buffer);

#if NET6_0_OR_GREATER
            // Half
            offset = 0;
            buffer = new byte[2];
            actual = buffer.WriteType<Half>(ref offset, BitConverter.Int16BitsToHalf(0x0100));
            Assert.True(actual);
            ValidateBytes([.. _bytes.Take(2)], buffer);
#endif

#if NET7_0_OR_GREATER
            // Int128
            offset = 0;
            buffer = new byte[16];
            actual = buffer.WriteType<Int128>(ref offset, (Int128)new BigInteger(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, buffer);

            // UInt128
            offset = 0;
            buffer = new byte[16];
            actual = buffer.WriteType<UInt128>(ref offset, (UInt128)new BigInteger(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, buffer);
#endif

            // Enum
            offset = 0;
            buffer = new byte[4];
            actual = buffer.WriteType<TestEnum>(ref offset, (TestEnum)0x03020100);
            Assert.True(actual);
            ValidateBytes([.. _bytes.Take(4)], buffer);
        }

        [Fact]
        public void WriteTypeExplicitTest()
        {
            byte[] bytesWithString =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x41, 0x42, 0x43, 0x00,
            ];

            byte[] buffer = new byte[16];
            int offset = 0;
            var obj = new TestStructExplicit
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                FifthValue = "ABC",
            };
            byte[] expected = bytesWithString.Take(12).ToArray();
            bool write = buffer.WriteType(ref offset, obj);
            Assert.True(write);
            ValidateBytes(expected, buffer);
        }

        [Fact]
        public void WriteTypeSequentialTest()
        {
            byte[] bytesWithString =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x41, 0x42, 0x43, 0x00,
            ];

            byte[] buffer = new byte[24];
            int offset = 0;
            var obj = new TestStructSequential
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
                FifthValue = "ABC",
            };
            byte[] expected = bytesWithString.Take(16).ToArray();
            bool write = buffer.WriteType(ref offset, obj);
            Assert.True(write);
            ValidateBytes(expected, buffer);
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