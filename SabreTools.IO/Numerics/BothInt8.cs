namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 8-bit signed value
    /// </summary>
    public sealed class BothInt8(sbyte le, sbyte be) : BothEndian<sbyte>(le, be)
    {
        #region Operators

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
