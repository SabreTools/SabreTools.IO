namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 32-bit signed value
    /// </summary>
    public sealed class BothInt32(int le, int be) : BothEndian<int>(le, be)
    {
        public static implicit operator BothInt32(int val)
            => new(val, val);

        #region Arithmetic Unary Operators

        public static BothInt32 operator ++(BothInt32 a)
        {
            int le = a.LittleEndian + 1;
            int be = a.BigEndian + 1;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator --(BothInt32 a)
        {
            int le = a.LittleEndian - 1;
            int be = a.BigEndian - 1;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator +(BothInt32 a)
        {
            int le = +a.LittleEndian;
            int be = +a.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator -(BothInt32 a)
        {
            int le = -a.LittleEndian;
            int be = -a.BigEndian;
            return new BothInt32(le, be);
        }

        #endregion

        #region Arithmetic Binary Operators

        public static BothInt32 operator *(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian * b.LittleEndian;
            int be = a.BigEndian * b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator /(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian / b.LittleEndian;
            int be = a.BigEndian / b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator %(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian % b.LittleEndian;
            int be = a.BigEndian % b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator +(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian + b.LittleEndian;
            int be = a.BigEndian + b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator -(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian - b.LittleEndian;
            int be = a.BigEndian - b.BigEndian;
            return new BothInt32(le, be);
        }

        #endregion

        #region Bitwise Unary Operators

        public static BothInt32 operator ~(BothInt32 a)
        {
            int le = ~a.LittleEndian;
            int be = ~a.BigEndian;
            return new BothInt32(le, be);
        }

        #endregion

        #region Shift Binary Operators

        public static BothInt32 operator <<(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian << b.LittleEndian;
            int be = a.BigEndian << b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator >>(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian >> b.LittleEndian;
            int be = a.BigEndian >> b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator >>>(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian >>> b.LittleEndian;
            int be = a.BigEndian >>> b.BigEndian;
            return new BothInt32(le, be);
        }

        #endregion

        #region Bitwise Binary Operators

        public static BothInt32 operator &(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian & b.LittleEndian;
            int be = a.BigEndian & b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator |(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian | b.LittleEndian;
            int be = a.BigEndian | b.BigEndian;
            return new BothInt32(le, be);
        }

        public static BothInt32 operator ^(BothInt32 a, BothInt32 b)
        {
            int le = a.LittleEndian ^ b.LittleEndian;
            int be = a.BigEndian ^ b.BigEndian;
            return new BothInt32(le, be);
        }

        #endregion
    }
}
