using System;
using Xunit;

namespace SabreTools.Numerics.Test
{
    public class BothInt32Tests
    {
        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, false)]
        public void IsValidTest(int le, int be, bool expected)
        {
            var val = new BothInt32(le, be);

            Assert.Equal(le, val.LittleEndian);
            Assert.Equal(be, val.BigEndian);
            Assert.Equal(expected, val.IsValid);
        }

        [Fact]
        public void ImplicitConversionTest()
        {
            int expected = 1;
            var val = new BothInt32(expected, expected);

            int to = (int)val;
            Assert.Equal(expected, to);

            BothInt32 back = (BothInt32)to;
            Assert.Equal(expected, back.LittleEndian);
            Assert.Equal(expected, back.BigEndian);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        public void CompareToTest(int le, int expected)
        {
            int compare = 1;
            var val = new BothInt32(le, le);

            int actual = val.CompareTo(compare);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTypeCodeTest()
        {
            TypeCode expected = ((int)1).GetTypeCode();

            var val = new BothInt32(1, 1);
            Assert.Equal(expected, val.GetTypeCode());
        }

        [Fact]
        public void ToTypesTest()
        {
            var val = new BothInt32(1, 1);

            bool expectedBool = Convert.ToBoolean((int)1);
            Assert.Equal(expectedBool, val.ToBoolean(null));

            char expectedChar = Convert.ToChar((int)1);
            Assert.Equal(expectedChar, val.ToChar(null));

            sbyte expectedSByte = Convert.ToSByte((int)1);
            Assert.Equal(expectedSByte, val.ToSByte(null));

            byte expectedByte = Convert.ToByte((int)1);
            Assert.Equal(expectedByte, val.ToByte(null));

            short expectedInt16 = Convert.ToInt16((int)1);
            Assert.Equal(expectedInt16, val.ToInt16(null));

            ushort expectedUInt16 = Convert.ToUInt16((int)1);
            Assert.Equal(expectedUInt16, val.ToUInt16(null));

            int expectedInt32 = Convert.ToInt32((int)1);
            Assert.Equal(expectedInt32, val.ToInt32(null));

            uint expectedUInt32 = Convert.ToUInt32((int)1);
            Assert.Equal(expectedUInt32, val.ToUInt32(null));

            long expectedInt64 = Convert.ToInt64((int)1);
            Assert.Equal(expectedInt64, val.ToInt64(null));

            ulong expectedUInt64 = Convert.ToUInt64((int)1);
            Assert.Equal(expectedUInt64, val.ToUInt64(null));

            float expectedSingle = Convert.ToSingle((int)1);
            Assert.Equal(expectedSingle, val.ToSingle(null));

            double expectedDouble = Convert.ToDouble((int)1);
            Assert.Equal(expectedDouble, val.ToDouble(null));

            decimal expectedDecimal = Convert.ToDecimal((int)1);
            Assert.Equal(expectedDecimal, val.ToDecimal(null));

            Assert.Throws<InvalidCastException>(() => val.ToDateTime(null));

            string expectedString = Convert.ToString((int)1);
            Assert.Equal(expectedString, val.ToString(null));

            ulong expectedObject = Convert.ToUInt64((int)1);
            Assert.Equal(expectedObject, val.ToType(typeof(ulong), null));
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(1, 0, false)]
        [InlineData(1, 1, true)]
        public void Equals_BothEndian(int le, int be, bool expected)
        {
            var val = new BothInt32(le, be);
            var equalTo = new BothInt32(1, 1);

            bool actual = val.Equals(equalTo);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(1, 1, true)]
        public void Equals_BaseType(int le, int be, bool expected)
        {
            var val = new BothInt32(le, be);
            int equalTo = 1;

            bool actual = val.Equals(equalTo);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArithmeticUnaryOperatorsTest()
        {
            var valA = new BothInt32(2, 2);
            int expected = 3;
            valA++;
            Assert.Equal(expected, valA.LittleEndian);
            Assert.Equal(expected, valA.BigEndian);

            valA = new BothInt32(2, 2);
            expected = 1;
            valA--;
            Assert.Equal(expected, valA.LittleEndian);
            Assert.Equal(expected, valA.BigEndian);

            valA = new BothInt32(2, 2);
            expected = 2;
            BothInt32 actual = +valA;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = -2;
            actual = -valA;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);
        }

        [Fact]
        public void ArithmeticBinaryOperatorsTest()
        {
            var valA = new BothInt32(3, 3);
            var valB = new BothInt32(2, 2);

            int expected = 6;
            BothInt32 actual = valA * valB;
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
        public void BitwiseUnaryOperatorsTest()
        {
            var valA = new BothInt32(2, 2);
            int expected = ~2;
            BothInt32 actual = ~valA;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);
        }

        [Fact]
        public void ShiftBinaryOperatorsTest()
        {
            var valA = new BothInt32(2, 2);
            var valB = new BothInt32(1, 1);

            int expected = 2 << 1;
            BothInt32 actual = valA << valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 2 >> 1;
            actual = valA >> valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 2 >>> 1;
            actual = valA >>> valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);
        }

        [Fact]
        public void BitwiseBinaryOperatorsTest()
        {
            var valA = new BothInt32(3, 3);
            var valB = new BothInt32(2, 2);

            int expected = 3 & 2;
            BothInt32 actual = valA & valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 3 | 2;
            actual = valA | valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);

            expected = 3 ^ 2;
            actual = valA ^ valB;
            Assert.Equal(expected, actual.LittleEndian);
            Assert.Equal(expected, actual.BigEndian);
        }
    }
}
