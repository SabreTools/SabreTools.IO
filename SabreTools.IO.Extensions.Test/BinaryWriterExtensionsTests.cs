using System;
using System.IO;
using System.Linq;
using System.Numerics;
using Xunit;

namespace SabreTools.IO.Extensions.Test
{
    public class BinaryWriterExtensionsTests
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
            var stream = new MemoryStream(new byte[4], 0, 4, true, true);
            var bw = new BinaryWriter(stream);
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = bw.WriteNullTerminatedAnsiString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteNullTerminatedUTF8StringTest()
        {
            var stream = new MemoryStream(new byte[4], 0, 4, true, true);
            var bw = new BinaryWriter(stream);
            byte[] expected = [0x41, 0x42, 0x43, 0x00];

            bool write = bw.WriteNullTerminatedUTF8String("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteNullTerminatedUnicodeStringTest()
        {
            var stream = new MemoryStream(new byte[8], 0, 8, true, true);
            var bw = new BinaryWriter(stream);
            byte[] expected = [0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x00];

            bool write = bw.WriteNullTerminatedUnicodeString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteNullTerminatedUTF32StringTest()
        {
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            var bw = new BinaryWriter(stream);
            byte[] expected = [0x41, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];

            bool write = bw.WriteNullTerminatedUTF32String("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WritePrefixedAnsiStringTest()
        {
            var stream = new MemoryStream(new byte[4], 0, 4, true, true);
            var bw = new BinaryWriter(stream);
            byte[] expected = [0x03, 0x41, 0x42, 0x43];

            bool write = bw.WritePrefixedAnsiString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WritePrefixedUnicodeStringTest()
        {
            var stream = new MemoryStream(new byte[8], 0, 8, true, true);
            var bw = new BinaryWriter(stream);
            byte[] expected = [0x03, 0x00, 0x41, 0x00, 0x42, 0x00, 0x43, 0x00];

            bool write = bw.WritePrefixedUnicodeString("ABC");
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteTypeTest()
        {
            // Guid
            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            var bw = new BinaryWriter(stream);
            bool actual = bw.WriteType(new Guid(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, stream.GetBuffer());

            // Half
            stream = new MemoryStream(new byte[2], 0, 2, true, true);
            bw = new BinaryWriter(stream);
            actual = bw.WriteType(BitConverter.Int16BitsToHalf(0x0100));
            Assert.True(actual);
            ValidateBytes([.. _bytes.Take(2)], stream.GetBuffer());

            // Int128
            stream = new MemoryStream(new byte[16], 0, 16, true, true);
            bw = new BinaryWriter(stream);
            actual = bw.WriteType((Int128)new BigInteger(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, stream.GetBuffer());

            // UInt128
            stream = new MemoryStream(new byte[16], 0, 16, true, true);
            bw = new BinaryWriter(stream);
            actual = bw.WriteType((UInt128)new BigInteger(_bytes));
            Assert.True(actual);
            ValidateBytes(_bytes, stream.GetBuffer());

            // Enum
            stream = new MemoryStream(new byte[4], 0, 4, true, true);
            bw = new BinaryWriter(stream);
            actual = bw.WriteType((TestEnum)0x03020100);
            Assert.True(actual);
            ValidateBytes([.. _bytes.Take(4)], stream.GetBuffer());
        }

        [Fact]
        public void WriteTypeExplicitTest()
        {
            byte[] bytesWithString =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x41, 0x42, 0x43, 0x00,
            ];

            var stream = new MemoryStream(new byte[16], 0, 16, true, true);
            var bw = new BinaryWriter(stream);
            var obj = new TestStructExplicit
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                FifthValue = "ABC",
            };
            byte[] expected = [.. bytesWithString.Take(12)];
            bool write = bw.WriteType(obj);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
        }

        [Fact]
        public void WriteTypeSequentialTest()
        {
            byte[] bytesWithString =
            [
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x41, 0x42, 0x43, 0x00,
            ];

            var stream = new MemoryStream(new byte[24], 0, count: 24, true, true);
            var bw = new BinaryWriter(stream);
            var obj = new TestStructSequential
            {
                FirstValue = TestEnum.RecognizedTestValue,
                SecondValue = 0x07060504,
                ThirdValue = 0x0908,
                FourthValue = 0x0B0A,
                FifthValue = "ABC",
            };
            byte[] expected = [.. bytesWithString.Take(16)];
            bool write = bw.WriteType(obj);
            Assert.True(write);
            ValidateBytes(expected, stream.GetBuffer());
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
