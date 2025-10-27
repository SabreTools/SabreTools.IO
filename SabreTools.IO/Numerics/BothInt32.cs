namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 32-bit signed value
    /// </summary>
    public sealed class BothInt32(int le, int be) : BothEndian<int>(le, be)
    {
        #region Operators

        public static BothInt32 operator +(BothInt32 a, BothInt32 b)
        {
            int le = (int)(a.LittleEndian + b.LittleEndian);
            int be = (int)(a.BigEndian + b.BigEndian);
            return new BothInt32(le, be);
        }

        public static BothInt32 operator -(BothInt32 a, BothInt32 b)
        {
            int le = (int)(a.LittleEndian - b.LittleEndian);
            int be = (int)(a.BigEndian - b.BigEndian);
            return new BothInt32(le, be);
        }

        public static BothInt32 operator *(BothInt32 a, BothInt32 b)
        {
            int le = (int)(a.LittleEndian * b.LittleEndian);
            int be = (int)(a.BigEndian * b.BigEndian);
            return new BothInt32(le, be);
        }

        public static BothInt32 operator /(BothInt32 a, BothInt32 b)
        {
            int le = (int)(a.LittleEndian / b.LittleEndian);
            int be = (int)(a.BigEndian / b.BigEndian);
            return new BothInt32(le, be);
        }

        public static BothInt32 operator ^(BothInt32 a, BothInt32 b)
        {
            int le = (int)(a.LittleEndian ^ b.LittleEndian);
            int be = (int)(a.BigEndian ^ b.BigEndian);
            return new BothInt32(le, be);
        }

        public static BothInt32 operator |(BothInt32 a, BothInt32 b)
        {
            int le = (int)(a.LittleEndian | b.LittleEndian);
            int be = (int)(a.BigEndian | b.BigEndian);
            return new BothInt32(le, be);
        }

        public static BothInt32 operator &(BothInt32 a, BothInt32 b)
        {
            int le = (int)(a.LittleEndian & b.LittleEndian);
            int be = (int)(a.BigEndian & b.BigEndian);
            return new BothInt32(le, be);
        }

        public static implicit operator BothInt32(int val)
            => new(val, val);

        #endregion
    }
}
