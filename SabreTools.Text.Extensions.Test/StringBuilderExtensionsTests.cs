using System;
using System.Text;
using SabreTools.Numerics;
using Xunit;

namespace SabreTools.Text.Extensions.Test
{
    public class StringBuilderExtensionsTests
    {
        [Theory]
        [InlineData(true, "Prefix: True")]
        [InlineData(false, "Prefix: False")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Boolean_Formatted(bool? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('a', "Prefix: a")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Char_Formatted(char? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((sbyte)0x01, "Prefix: 1 (0x01)")]
        [InlineData((sbyte)-0x01, "Prefix: -1 (0xFF)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_SByte_Formatted(sbyte? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((sbyte)0x01, true, "Prefix: 1 (0x01)")]
        [InlineData((sbyte)0x01, false, "Prefix (Little-Endian): 1 (0x01)\nPrefix (Big-Endian): 127 (0x7F)")]
        [InlineData((sbyte)-0x01, true, "Prefix: -1 (0xFF)")]
        [InlineData((sbyte)-0x01, false, "Prefix (Little-Endian): -1 (0xFF)\nPrefix (Big-Endian): 127 (0x7F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothInt8_Formatted(sbyte? le, bool match, string expected)
        {
            BothInt8? value = le is null
                ? null
                : new BothInt8(le.Value, match ? le.Value : (sbyte)0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((byte)0x01, "Prefix: 1 (0x01)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Byte_Formatted(byte? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((byte)0x01, true, "Prefix: 1 (0x01)")]
        [InlineData((byte)0x01, false, "Prefix (Little-Endian): 1 (0x01)\nPrefix (Big-Endian): 127 (0x7F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothUInt8_Formatted(byte? le, bool match, string expected)
        {
            BothUInt8? value = le is null
                ? null
                : new BothUInt8(le.Value, match ? le.Value : (byte)0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((short)0x0001, "Prefix: 1 (0x0001)")]
        [InlineData((short)-0x0001, "Prefix: -1 (0xFFFF)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int16_Formatted(short? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((short)0x0001, true, "Prefix: 1 (0x0001)")]
        [InlineData((short)0x0001, false, "Prefix (Little-Endian): 1 (0x0001)\nPrefix (Big-Endian): 127 (0x007F)")]
        [InlineData((short)-0x0001, true, "Prefix: -1 (0xFFFF)")]
        [InlineData((short)-0x0001, false, "Prefix (Little-Endian): -1 (0xFFFF)\nPrefix (Big-Endian): 127 (0x007F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothInt16_Formatted(short? le, bool match, string expected)
        {
            BothInt16? value = le is null
                ? null
                : new BothInt16(le.Value, match ? le.Value : (short)0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ushort)0x0001, "Prefix: 1 (0x0001)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt16_Formatted(ushort? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ushort)0x0001, true, "Prefix: 1 (0x0001)")]
        [InlineData((ushort)0x0001, false, "Prefix (Little-Endian): 1 (0x0001)\nPrefix (Big-Endian): 127 (0x007F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothUInt16_Formatted(ushort? le, bool match, string expected)
        {
            BothUInt16? value = le is null
                ? null
                : new BothUInt16(le.Value, match ? le.Value : (ushort)0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((short)0x00000001, "Prefix: 1")]
        [InlineData((short)-0x00000001, "Prefix: -1")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Half_Formatted(short? shortValue, string expected)
        {
            Half? value = shortValue is null
                ? null
                : (Half)shortValue.Value;

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x000001, "Prefix: 1 (0x000001)")]
        [InlineData(-0x000001, "Prefix: -1 (0xFFFFFF)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int24_Formatted(int? intValue, string expected)
        {
            Int24? value = intValue is null
                ? null
                : new Int24(intValue.Value);

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((uint)0x000001, "Prefix: 1 (0x000001)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt24_Formatted(uint? intValue, string expected)
        {
            UInt24? value = intValue is null
                ? null
                : new UInt24(intValue.Value);

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x00000001, "Prefix: 1 (0x00000001)")]
        [InlineData(-0x00000001, "Prefix: -1 (0xFFFFFFFF)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int32_Formatted(int? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x00000001, true, "Prefix: 1 (0x00000001)")]
        [InlineData(0x00000001, false, "Prefix (Little-Endian): 1 (0x00000001)\nPrefix (Big-Endian): 127 (0x0000007F)")]
        [InlineData(-0x00000001, true, "Prefix: -1 (0xFFFFFFFF)")]
        [InlineData(-0x00000001, false, "Prefix (Little-Endian): -1 (0xFFFFFFFF)\nPrefix (Big-Endian): 127 (0x0000007F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothInt32_Formatted(int? le, bool match, string expected)
        {
            BothInt32? value = le is null
                ? null
                : new BothInt32(le.Value, match ? le.Value : 0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((uint)0x00000001, "Prefix: 1 (0x00000001)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt32_Formatted(uint? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((uint)0x00000001, true, "Prefix: 1 (0x00000001)")]
        [InlineData((uint)0x00000001, false, "Prefix (Little-Endian): 1 (0x00000001)\nPrefix (Big-Endian): 127 (0x0000007F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothUInt32_Formatted(uint? le, bool match, string expected)
        {
            BothUInt32? value = le is null
                ? null
                : new BothUInt32(le.Value, match ? le.Value : 0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((float)0x00000001, "Prefix: 1")]
        [InlineData((float)-0x00000001, "Prefix: -1")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Single_Formatted(float? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((long)0x000000000001, "Prefix: 1 (0x000000000001)")]
        [InlineData((long)-0x000000000001, "Prefix: -1 (0xFFFFFFFFFFFF)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int48_Formatted(long? longValue, string expected)
        {
            Int48? value = longValue is null
                ? null
                : new Int48(longValue.Value);

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ulong)0x000000000001, "Prefix: 1 (0x000000000001)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt48_Formatted(ulong? longValue, string expected)
        {
            UInt48? value = longValue is null
                ? null
                : new UInt48(longValue.Value);

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((long)0x0000000000000001, "Prefix: 1 (0x0000000000000001)")]
        [InlineData((long)-0x0000000000000001, "Prefix: -1 (0xFFFFFFFFFFFFFFFF)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int64_Formatted(long? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((long)0x0000000000000001, true, "Prefix: 1 (0x0000000000000001)")]
        [InlineData((long)0x0000000000000001, false, "Prefix (Little-Endian): 1 (0x0000000000000001)\nPrefix (Big-Endian): 127 (0x000000000000007F)")]
        [InlineData((long)-0x0000000000000001, true, "Prefix: -1 (0xFFFFFFFFFFFFFFFF)")]
        [InlineData((long)-0x0000000000000001, false, "Prefix (Little-Endian): -1 (0xFFFFFFFFFFFFFFFF)\nPrefix (Big-Endian): 127 (0x000000000000007F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothInt64_Formatted(long? le, bool match, string expected)
        {
            BothInt64? value = le is null
                ? null
                : new BothInt64(le.Value, match ? le.Value : 0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ulong)0x0000000000000001, "Prefix: 1 (0x0000000000000001)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt64_Formatted(ulong? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ulong)0x0000000000000001, true, "Prefix: 1 (0x0000000000000001)")]
        [InlineData((ulong)0x0000000000000001, false, "Prefix (Little-Endian): 1 (0x0000000000000001)\nPrefix (Big-Endian): 127 (0x000000000000007F)")]
        [InlineData(null, true, "Prefix: [NULL]")]
        [InlineData(null, false, "Prefix: [NULL]")]
        public void AppendLine_BothUInt64_Formatted(ulong? le, bool match, string expected)
        {
            BothUInt64? value = le is null
                ? null
                : new BothUInt64(le.Value, match ? le.Value : 0x7F);

            var sb = new StringBuilder();
            sb.AppendLineBothEndian(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((double)0x0000000000000001, "Prefix: 1")]
        [InlineData((double)-0x0000000000000001, "Prefix: -1")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Double_Formatted(double? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((long)0x0000000000000001, "Prefix: 1 (0x00000000000000000000000000000001)")]
        [InlineData((long)-0x0000000000000001, "Prefix: -1 (0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int128_Formatted(long? longValue, string expected)
        {
            Int128? value = longValue is null
                ? null
                : (Int128)longValue.Value;
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ulong)0x0000000000000001, "Prefix: 1 (0x00000000000000000000000000000001)")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt128_Formatted(ulong? longValue, string expected)
        {
            UInt128? value = longValue is null
                ? null
                : (UInt128)longValue.Value;
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((long)0x0000000000000001, "Prefix: 1")]
        [InlineData((long)-0x0000000000000001, "Prefix: -1")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Decimal_Formatted(long? longValue, string expected)
        {
            decimal? value = longValue;
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "Prefix: [EMPTY]")]
        [InlineData("Value", "Prefix: Value")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_String_Formatted(string? value, string expected)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((byte)0, "Prefix: 00000000-0000-0000-0000-000000000000")]
        [InlineData((byte)1, "Prefix: 00000001-0001-0001-0101-010101010101")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Guid_Formatted(byte? valueByte, string expected)
        {
            Guid? value = valueByte switch
            {
                null => null,
                0 => Guid.Empty,
                1 => new Guid(valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value,
                    valueByte.Value),
                _ => throw new ArgumentOutOfRangeException(nameof(valueByte)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 01 02 03 04")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int8Array_Formatted(int? index, string expected)
        {
            sbyte[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x01, 0x02, 0x03, 0x04],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 01 02 03 04")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt8Array_Formatted(int? index, string expected)
        {
            byte[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x01, 0x02, 0x03, 0x04],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: ABCD")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt8ArrayEncoded_Formatted(int? index, string expected)
        {
            byte[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x41, 0x42, 0x43, 0x44],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix", Encoding.ASCII);
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 01 02 03 04")]
        [InlineData(2, "Prefix: 01 02 03 04, 01 02 03 04")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt8ArrayArray_Formatted(int? index, string expected)
        {
            byte[][]? value = index switch
            {
                null => null,
                0 => [],
                1 => [[0x01, 0x02, 0x03, 0x04]],
                2 => [[0x01, 0x02, 0x03, 0x04], [0x01, 0x02, 0x03, 0x04]],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: A, B, C, D")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_CharArray_Formatted(int? index, string expected)
        {
            char[]? value = index switch
            {
                null => null,
                0 => [],
                1 => ['A', 'B', 'C', 'D'],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int16Array_Formatted(int? index, string expected)
        {
            short[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x0001, 0x0002, 0x0003, 0x0004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt16Array_Formatted(int? index, string expected)
        {
            ushort[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x0001, 0x0002, 0x0003, 0x0004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_HalfArray_Formatted(int? index, string expected)
        {
            Half[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [(Half)0x0001, (Half)0x0002, (Half)0x0003, (Half)0x0004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int24Array_Formatted(int? index, string expected)
        {
            Int24[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [(Int24)0x000001, (Int24)0x000002, (Int24)0x000003, (Int24)0x000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt24Array_Formatted(int? index, string expected)
        {
            UInt24[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [(UInt24)0x000001, (UInt24)0x000002, (UInt24)0x000003, (UInt24)0x000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int32Array_Formatted(int? index, string expected)
        {
            int[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x00000001, 0x00000002, 0x00000003, 0x00000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt32Array_Formatted(int? index, string expected)
        {
            uint[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x00000001, 0x00000002, 0x00000003, 0x00000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_SingleArray_Formatted(int? index, string expected)
        {
            float[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x00000001, 0x00000002, 0x00000003, 0x00000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int48Array_Formatted(int? index, string expected)
        {
            Int48[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [(Int48)0x000000000001, (Int48)0x000000000002, (Int48)0x000000000003, (Int48)0x000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt48Array_Formatted(int? index, string expected)
        {
            UInt48[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [(UInt48)0x000000000001, (UInt48)0x000000000002, (UInt48)0x000000000003, (UInt48)0x000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int64Array_Formatted(int? index, string expected)
        {
            long[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x000000000000001, 0x000000000000002, 0x000000000000003, 0x000000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt64Array_Formatted(int? index, string expected)
        {
            ulong[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x000000000000001, 0x000000000000002, 0x000000000000003, 0x000000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_DoubleArray_Formatted(int? index, string expected)
        {
            double[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x000000000000001, 0x000000000000002, 0x000000000000003, 0x000000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_Int128Array_Formatted(int? index, string expected)
        {
            Int128[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x000000000000001, 0x000000000000002, 0x000000000000003, 0x000000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_UInt128Array_Formatted(int? index, string expected)
        {
            UInt128[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x000000000000001, 0x000000000000002, 0x000000000000003, 0x000000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 1, 2, 3, 4")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_DecimalArray_Formatted(int? index, string expected)
        {
            decimal[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [0x000000000000001, 0x000000000000002, 0x000000000000003, 0x000000000000004],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: A, B, C, D")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_StringArray_Formatted(int? index, string expected)
        {
            string[]? value = index switch
            {
                null => null,
                0 => [],
                1 => ["A", "B", "C", "D"],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "Prefix: [EMPTY]")]
        [InlineData(1, "Prefix: 00000000-0000-0000-0000-000000000000, 00000000-0000-0000-0000-000000000000")]
        [InlineData(null, "Prefix: [NULL]")]
        public void AppendLine_GuidArray_Formatted(int? index, string expected)
        {
            Guid[]? value = index switch
            {
                null => null,
                0 => [],
                1 => [Guid.Empty, Guid.Empty],
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };

            var sb = new StringBuilder();
            sb.AppendLine(value, "Prefix");
            string actual = sb.ToString().Trim();
            Assert.Equal(expected, actual);
        }
    }
}
