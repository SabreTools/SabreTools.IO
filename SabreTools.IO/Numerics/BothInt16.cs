namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 16-bit signed value
    /// </summary>
    public sealed class BothInt16(short le, short be) : BothEndian<short>(le, be)
    {
        #region Operators

        public static BothInt16 operator +(BothInt16 a, BothInt16 b)
        {
            short le = (short)(a.LittleEndian + b.LittleEndian);
            short be = (short)(a.BigEndian + b.BigEndian);
            return new BothInt16(le, be);
        }

        public static BothInt16 operator -(BothInt16 a, BothInt16 b)
        {
            short le = (short)(a.LittleEndian - b.LittleEndian);
            short be = (short)(a.BigEndian - b.BigEndian);
            return new BothInt16(le, be);
        }

        public static BothInt16 operator *(BothInt16 a, BothInt16 b)
        {
            short le = (short)(a.LittleEndian * b.LittleEndian);
            short be = (short)(a.BigEndian * b.BigEndian);
            return new BothInt16(le, be);
        }

        public static BothInt16 operator /(BothInt16 a, BothInt16 b)
        {
            short le = (short)(a.LittleEndian / b.LittleEndian);
            short be = (short)(a.BigEndian / b.BigEndian);
            return new BothInt16(le, be);
        }

        public static BothInt16 operator ^(BothInt16 a, BothInt16 b)
        {
            short le = (short)(a.LittleEndian ^ b.LittleEndian);
            short be = (short)(a.BigEndian ^ b.BigEndian);
            return new BothInt16(le, be);
        }

        public static implicit operator BothInt16(short val)
            => new(val, val);

        #endregion
    }
}
