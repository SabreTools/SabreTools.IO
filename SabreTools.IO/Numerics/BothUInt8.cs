namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 8-bit unsigned value
    /// </summary>
    public sealed class BothUInt8(byte le, byte be) : BothEndian<byte>(le, be)
    {
        #region Operators

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
