namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 8-bit unsigned value
    /// </summary>
    public sealed class BothUInt8(byte le, byte be) : BothEndian<byte>(le, be)
    {
        #region Arithmetic Unary Operators

        public static BothUInt8 operator ++(BothUInt8 a)
        {
            byte le = (byte)(a.LittleEndian + 1);
            byte be = (byte)(a.BigEndian + 1);
            return new BothUInt8(le, be);
        }

        public static BothUInt8 operator --(BothUInt8 a)
        {
            byte le = (byte)(a.LittleEndian - 1);
            byte be = (byte)(a.BigEndian - 1);
            return new BothUInt8(le, be);
        }

        #endregion

        #region Arithmetic Binary Operators

        public static BothUInt8 operator *(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian * b.LittleEndian);
            byte be = (byte)(a.BigEndian * b.BigEndian);
            return new BothUInt8(le, be);
        }

        public static BothUInt8 operator /(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian / b.LittleEndian);
            byte be = (byte)(a.BigEndian / b.BigEndian);
            return new BothUInt8(le, be);
        }

        public static BothUInt8 operator %(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian % b.LittleEndian);
            byte be = (byte)(a.BigEndian % b.BigEndian);
            return new BothUInt8(le, be);
        }

        public static BothUInt8 operator +(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian + b.LittleEndian);
            byte be = (byte)(a.BigEndian + b.BigEndian);
            return new BothUInt8(le, be);
        }

        public static BothUInt8 operator -(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian - b.LittleEndian);
            byte be = (byte)(a.BigEndian - b.BigEndian);
            return new BothUInt8(le, be);
        }

        #endregion

        #region Bitwise and Shift Operators

        public static BothUInt8 operator ^(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian ^ b.LittleEndian);
            byte be = (byte)(a.BigEndian ^ b.BigEndian);
            return new BothUInt8(le, be);
        }

        public static BothUInt8 operator |(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian | b.LittleEndian);
            byte be = (byte)(a.BigEndian | b.BigEndian);
            return new BothUInt8(le, be);
        }

        public static BothUInt8 operator &(BothUInt8 a, BothUInt8 b)
        {
            byte le = (byte)(a.LittleEndian & b.LittleEndian);
            byte be = (byte)(a.BigEndian & b.BigEndian);
            return new BothUInt8(le, be);
        }

        public static implicit operator BothUInt8(byte val)
            => new(val, val);

        #endregion
    }
}
