namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 64-bit unsigned value
    /// </summary>
    public sealed class BothUInt64(ulong le, ulong be) : BothEndian<ulong>(le, be)
    {
        public static implicit operator BothUInt64(ulong val)
            => new(val, val);

        #region Arithmetic Unary Operators

        public static BothUInt64 operator ++(BothUInt64 a)
        {
            ulong le = (ulong)(a.LittleEndian + 1);
            ulong be = (ulong)(a.BigEndian + 1);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator --(BothUInt64 a)
        {
            ulong le = (ulong)(a.LittleEndian - 1);
            ulong be = (ulong)(a.BigEndian - 1);
            return new BothUInt64(le, be);
        }

        #endregion

        #region Arithmetic Binary Operators

        public static BothUInt64 operator *(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian * b.LittleEndian);
            ulong be = (ulong)(a.BigEndian * b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator /(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian / b.LittleEndian);
            ulong be = (ulong)(a.BigEndian / b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator %(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian % b.LittleEndian);
            ulong be = (ulong)(a.BigEndian % b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator +(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian + b.LittleEndian);
            ulong be = (ulong)(a.BigEndian + b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator -(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian - b.LittleEndian);
            ulong be = (ulong)(a.BigEndian - b.BigEndian);
            return new BothUInt64(le, be);
        }

        #endregion

        #region Bitwise Unary Operators

        public static BothUInt64 operator ~(BothUInt64 a)
        {
            ulong le = (ulong)(~a.LittleEndian);
            ulong be = (ulong)(~a.BigEndian);
            return new BothUInt64(le, be);
        }

        #endregion

        #region Shift Binary Operators

        public static BothUInt64 operator <<(BothUInt64 a, BothInt32 b)
        {
            ulong le = (ulong)(a.LittleEndian << b.LittleEndian);
            ulong be = (ulong)(a.BigEndian << b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator >>(BothUInt64 a, BothInt32 b)
        {
            ulong le = (ulong)(a.LittleEndian >> b.LittleEndian);
            ulong be = (ulong)(a.BigEndian >> b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator >>>(BothUInt64 a, BothInt32 b)
        {
            ulong le = (ulong)(a.LittleEndian >>> b.LittleEndian);
            ulong be = (ulong)(a.BigEndian >>> b.BigEndian);
            return new BothUInt64(le, be);
        }

        #endregion

        #region Bitwise Binary Operators

        public static BothUInt64 operator &(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian & b.LittleEndian);
            ulong be = (ulong)(a.BigEndian & b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator |(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian | b.LittleEndian);
            ulong be = (ulong)(a.BigEndian | b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static BothUInt64 operator ^(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian ^ b.LittleEndian);
            ulong be = (ulong)(a.BigEndian ^ b.BigEndian);
            return new BothUInt64(le, be);
        }

        #endregion
    }
}
