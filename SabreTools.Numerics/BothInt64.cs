namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 64-bit signed value
    /// </summary>
    public sealed class BothInt64(long le, long be) : BothEndian<long>(le, be)
    {
        public static implicit operator BothInt64(long val)
            => new(val, val);

        #region Arithmetic Unary Operators

        public static BothInt64 operator ++(BothInt64 a)
        {
            long le = (long)(a.LittleEndian + 1);
            long be = (long)(a.BigEndian + 1);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator --(BothInt64 a)
        {
            long le = (long)(a.LittleEndian - 1);
            long be = (long)(a.BigEndian - 1);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator +(BothInt64 a)
        {
            long le = (long)(+a.LittleEndian);
            long be = (long)(+a.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator -(BothInt64 a)
        {
            long le = (long)(-a.LittleEndian);
            long be = (long)(-a.BigEndian);
            return new BothInt64(le, be);
        }

        #endregion

        #region Arithmetic Binary Operators

        public static BothInt64 operator *(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian * b.LittleEndian);
            long be = (long)(a.BigEndian * b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator /(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian / b.LittleEndian);
            long be = (long)(a.BigEndian / b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator %(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian % b.LittleEndian);
            long be = (long)(a.BigEndian % b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator +(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian + b.LittleEndian);
            long be = (long)(a.BigEndian + b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator -(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian - b.LittleEndian);
            long be = (long)(a.BigEndian - b.BigEndian);
            return new BothInt64(le, be);
        }

        #endregion

        #region Bitwise Unary Operators

        public static BothInt64 operator ~(BothInt64 a)
        {
            long le = (long)(~a.LittleEndian);
            long be = (long)(~a.BigEndian);
            return new BothInt64(le, be);
        }

        #endregion

        #region Shift Binary Operators

        public static BothInt64 operator <<(BothInt64 a, BothInt32 b)
        {
            long le = (long)(a.LittleEndian << b.LittleEndian);
            long be = (long)(a.BigEndian << b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator >>(BothInt64 a, BothInt32 b)
        {
            long le = (long)(a.LittleEndian >> b.LittleEndian);
            long be = (long)(a.BigEndian >> b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator >>>(BothInt64 a, BothInt32 b)
        {
            long le = (long)(a.LittleEndian >>> b.LittleEndian);
            long be = (long)(a.BigEndian >>> b.BigEndian);
            return new BothInt64(le, be);
        }

        #endregion

        #region Bitwise Binary Operators

        public static BothInt64 operator &(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian & b.LittleEndian);
            long be = (long)(a.BigEndian & b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator |(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian | b.LittleEndian);
            long be = (long)(a.BigEndian | b.BigEndian);
            return new BothInt64(le, be);
        }

        public static BothInt64 operator ^(BothInt64 a, BothInt64 b)
        {
            long le = (long)(a.LittleEndian ^ b.LittleEndian);
            long be = (long)(a.BigEndian ^ b.BigEndian);
            return new BothInt64(le, be);
        }

        #endregion
    }
}
