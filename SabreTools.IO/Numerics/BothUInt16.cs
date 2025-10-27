namespace SabreTools.IO.Numerics
{
    /// <summary>
    /// Both-endian 16-bit unsigned value
    /// </summary>
    public sealed class BothUInt16(ushort le, ushort be) : BothEndian<ushort>(le, be)
    {
        #region Operators

        public static BothUInt16 operator +(BothUInt16 a, BothUInt16 b)
        {
            ushort le = (ushort)(a.LittleEndian + b.LittleEndian);
            ushort be = (ushort)(a.BigEndian + b.BigEndian);
            return new BothUInt16(le, be);
        }

        public static BothUInt16 operator -(BothUInt16 a, BothUInt16 b)
        {
            ushort le = (ushort)(a.LittleEndian - b.LittleEndian);
            ushort be = (ushort)(a.BigEndian - b.BigEndian);
            return new BothUInt16(le, be);
        }

        public static BothUInt16 operator *(BothUInt16 a, BothUInt16 b)
        {
            ushort le = (ushort)(a.LittleEndian * b.LittleEndian);
            ushort be = (ushort)(a.BigEndian * b.BigEndian);
            return new BothUInt16(le, be);
        }

        public static BothUInt16 operator /(BothUInt16 a, BothUInt16 b)
        {
            ushort le = (ushort)(a.LittleEndian / b.LittleEndian);
            ushort be = (ushort)(a.BigEndian / b.BigEndian);
            return new BothUInt16(le, be);
        }

        public static BothUInt16 operator ^(BothUInt16 a, BothUInt16 b)
        {
            ushort le = (ushort)(a.LittleEndian ^ b.LittleEndian);
            ushort be = (ushort)(a.BigEndian ^ b.BigEndian);
            return new BothUInt16(le, be);
        }

        public static implicit operator BothUInt16(ushort val)
            => new(val, val);

        #endregion
    }
}
