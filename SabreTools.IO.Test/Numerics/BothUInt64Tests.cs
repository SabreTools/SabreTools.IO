using System;
using SabreTools.Numerics;
using Xunit;

namespace SabreTools.IO.Test.Numerics
{
    public class BothUInt64Tests
    {
        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, false)]
        public void IsValidTest(ulong le, ulong be, bool expected)
        {
            var val = new BothUInt64(le, be);

            Assert.Equal(le, val.LittleEndian);
            Assert.Equal(be, val.BigEndian);
            Assert.Equal(expected, val.IsValid);
        }

        [Fact]
        public void ImplicitConversionTest()
        {
            ulong expected = 1;
            var val = new BothUInt64(expected, expected);

            ulong to = (ulong)val;
            Assert.Equal(expected, to);

            BothUInt64 back = (BothUInt64)to;
            Assert.Equal(expected, back.LittleEndian);
            Assert.Equal(expected, back.BigEndian);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        public void CompareToTest(ulong le, int expected)
        {
            ulong compare = 1;
            var val = new BothUInt64(le, le);

            int actual = val.CompareTo(compare);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTypeCodeTest()
        {
            TypeCode expected = ((ulong)1).GetTypeCode();

            var val = new BothUInt64(1, 1);
            Assert.Equal(expected, val.GetTypeCode());
        }

        [Fact]
        public void ToTypesTest()
        {
            var val = new BothUInt64(1, 1);

            bool expectedBool = Convert.ToBoolean((ulong)1);
            Assert.Equal(expectedBool, val.ToBoolean(null));

            char expectedChar = Convert.ToChar((ulong)1);
            Assert.Equal(expectedChar, val.ToChar(null));

            sbyte expectedSByte = Convert.ToSByte((ulong)1);
            Assert.Equal(expectedSByte, val.ToSByte(null));

            byte expectedByte = Convert.ToByte((ulong)1);
            Assert.Equal(expectedByte, val.ToByte(null));

            short expectedInt16 = Convert.ToInt16((ulong)1);
            Assert.Equal(expectedInt16, val.ToInt16(null));

            ushort expectedUInt16 = Convert.ToUInt16((ulong)1);
            Assert.Equal(expectedUInt16, val.ToUInt16(null));

            int expectedInt32 = Convert.ToInt32((ulong)1);
            Assert.Equal(expectedInt32, val.ToInt32(null));

            uint expectedUInt32 = Convert.ToUInt32((ulong)1);
            Assert.Equal(expectedUInt32, val.ToUInt32(null));

            long expectedInt64 = Convert.ToInt64((ulong)1);
            Assert.Equal(expectedInt64, val.ToInt64(null));

            ulong expectedUInt64 = Convert.ToUInt64((ulong)1);
            Assert.Equal(expectedUInt64, val.ToUInt64(null));

            float expectedSingle = Convert.ToSingle((ulong)1);
            Assert.Equal(expectedSingle, val.ToSingle(null));

            double expectedDouble = Convert.ToDouble((ulong)1);
            Assert.Equal(expectedDouble, val.ToDouble(null));

            decimal expectedDecimal = Convert.ToDecimal((ulong)1);
            Assert.Equal(expectedDecimal, val.ToDecimal(null));

            Assert.Throws<InvalidCastException>(() => val.ToDateTime(null));

            string expectedString = Convert.ToString((ulong)1);
            Assert.Equal(expectedString, val.ToString(null));

            ulong expectedObject = Convert.ToUInt64((ulong)1);
            Assert.Equal(expectedObject, val.ToType(typeof(ulong), null));
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(1, 0, false)]
        [InlineData(1, 1, true)]
        public void Equals_BothEndian(ulong le, ulong be, bool expected)
        {
            var val = new BothUInt64(le, be);
            var equalTo = new BothUInt64(1, 1);

            bool actual = val.Equals(equalTo);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(1, 1, true)]
        public void Equals_BaseType(ulong le, ulong be, bool expected)
        {
            var val = new BothUInt64(le, be);
            ulong equalTo = 1;

            bool actual = val.Equals(equalTo);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArithmeticUnaryOperatorsTest()
        {
            var valA = new BothUInt64(2, 2);

            ulong expected = 3;
            BothUInt64 actual = valA++;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 1;
            actual = valA--;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);
        }

        [Fact]
        public void ArithmeticBinaryOperatorsTest()
        {
            var valA = new BothUInt64(3, 3);
            var valB = new BothUInt64(2, 2);

            ulong expected = 6;
            BothUInt64 actual = valA * valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 1;
            actual = valA / valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 1;
            actual = valA % valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 5;
            actual = valA + valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 1;
            actual = valA - valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);
        }

        [Fact]
        public void OperatorsTest()
        {
            var valA = new BothUInt64(2, 2);
            var valB = new BothUInt64(1, 1);

            ulong expected = (ulong)2 + (ulong)1;
            BothUInt64 actual = valA + valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = (ulong)2 - (ulong)1;
            actual = valA - valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = (ulong)2 * (ulong)1;
            actual = valA * valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = (ulong)2 / (ulong)1;
            actual = valA / valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = (ulong)2 ^ (ulong)1;
            actual = valA ^ valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = (ulong)2 & (ulong)1;
            actual = valA & valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = (ulong)2 | (ulong)1;
            actual = valA | valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);
        }
    }
}
