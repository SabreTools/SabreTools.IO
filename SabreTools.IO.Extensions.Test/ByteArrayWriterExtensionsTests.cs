using System;
using System.Linq;
using System.Numerics;
using Xunit;

namespace SabreTools.IO.Extensions.Test
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
            bool actual = buffer.WriteType(ref offset, new Guid(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, buffer);

            // Half
            offset = 0;
            buffer = new byte[2];
            actual = buffer.WriteType(ref offset, BitConverter.Int16BitsToHalf(0x0100));
            Assert.True(actual);
            ValidateBytes([.. _bytes.Take(2)], buffer);

            // Int128
            offset = 0;
            buffer = new byte[16];
            actual = buffer.WriteType(ref offset, (Int128)new BigInteger(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, buffer);

            // UInt128
            offset = 0;
            buffer = new byte[16];
            actual = buffer.WriteType(ref offset, (UInt128)new BigInteger(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, buffer);

            // Enum
            offset = 0;
            buffer = new byte[4];
            actual = buffer.WriteType(ref offset, (TestEnum)0x03020100);
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
            byte[] expected = [.. bytesWithString.Take(12)];
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
            byte[] expected = [.. bytesWithString.Take(16)];
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
