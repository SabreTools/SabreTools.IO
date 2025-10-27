namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 8-bit signed value
    /// </summary>
    public sealed class BothInt8(sbyte le, sbyte be) : BothEndian<sbyte>(le, be)
    {
        #region Arithmetic Unary Operators

        public static BothInt8 operator ++(BothInt8 a)
        {
            sbyte le = (sbyte)(a.LittleEndian + 1);
            sbyte be = (sbyte)(a.BigEndian + 1);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator --(BothInt8 a)
        {
            sbyte le = (sbyte)(a.LittleEndian - 1);
            sbyte be = (sbyte)(a.BigEndian - 1);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator +(BothInt8 a)
        {
            sbyte le = (sbyte)(+a.LittleEndian);
            sbyte be = (sbyte)(+a.BigEndian);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator -(BothInt8 a)
        {
            sbyte le = (sbyte)(-a.LittleEndian);
            sbyte be = (sbyte)(-a.BigEndian);
            return new BothInt8(le, be);
        }

        #endregion

        #region Arithmetic Binary Operators

        public static BothInt8 operator *(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian * b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian * b.BigEndian);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator /(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian / b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian / b.BigEndian);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator %(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian % b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian % b.BigEndian);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator +(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian + b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian + b.BigEndian);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator -(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian - b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian - b.BigEndian);
            return new BothInt8(le, be);
        }

        #endregion

        #region Bitwise and Shift Operators

        public static BothInt8 operator ^(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian ^ b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian ^ b.BigEndian);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator |(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian | b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian | b.BigEndian);
            return new BothInt8(le, be);
        }

        public static BothInt8 operator &(BothInt8 a, BothInt8 b)
        {
            sbyte le = (sbyte)(a.LittleEndian & b.LittleEndian);
            sbyte be = (sbyte)(a.BigEndian & b.BigEndian);
            return new BothInt8(le, be);
        }

        public static implicit operator BothInt8(sbyte val)
            => new(val, val);

        #endregion
    }
}
