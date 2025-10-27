namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian 64-bit unsigned value
    /// </summary>
    public sealed class BothUInt64(ulong le, ulong be) : BothEndian<ulong>(le, be)
    {
        #region Operators

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

        public static BothUInt64 operator ^(BothUInt64 a, BothUInt64 b)
        {
            ulong le = (ulong)(a.LittleEndian ^ b.LittleEndian);
            ulong be = (ulong)(a.BigEndian ^ b.BigEndian);
            return new BothUInt64(le, be);
        }

        public static implicit operator BothUInt64(ulong val)
            => new(val, val);

        #endregion
    }
}
