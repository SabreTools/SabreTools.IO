namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 32-bit unsigned value
    /// </summary>
    public sealed class BothUInt32(uint le, uint be) : BothEndian<uint>(le, be)
    {
        #region Operators

        public static BothUInt32 operator +(BothUInt32 a, BothUInt32 b)
        {
            uint le = (uint)(a.LittleEndian + b.LittleEndian);
            uint be = (uint)(a.BigEndian + b.BigEndian);
            return new BothUInt32(le, be);
        }

        public static BothUInt32 operator -(BothUInt32 a, BothUInt32 b)
        {
            uint le = (uint)(a.LittleEndian - b.LittleEndian);
            uint be = (uint)(a.BigEndian - b.BigEndian);
            return new BothUInt32(le, be);
        }

        public static BothUInt32 operator *(BothUInt32 a, BothUInt32 b)
        {
            uint le = (uint)(a.LittleEndian * b.LittleEndian);
            uint be = (uint)(a.BigEndian * b.BigEndian);
            return new BothUInt32(le, be);
        }

        public static BothUInt32 operator /(BothUInt32 a, BothUInt32 b)
        {
            uint le = (uint)(a.LittleEndian / b.LittleEndian);
            uint be = (uint)(a.BigEndian / b.BigEndian);
            return new BothUInt32(le, be);
        }

        public static BothUInt32 operator ^(BothUInt32 a, BothUInt32 b)
        {
            uint le = (uint)(a.LittleEndian ^ b.LittleEndian);
            uint be = (uint)(a.BigEndian ^ b.BigEndian);
            return new BothUInt32(le, be);
        }

        public static implicit operator BothUInt32(uint val)
            => new(val, val);

        #endregion
    }
}
