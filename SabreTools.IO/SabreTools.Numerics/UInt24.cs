using System;

namespace SabreTools.Numerics
{
    /// <summary>
    /// Represents a 24-bit unsigned integer.
    /// </summary>
    /// <remarks>Range: 0 to 16,777,215</remarks>
    public class UInt24 : IComparable, IConvertible, IEquatable<UInt24>
    {
        #region Properties

        /// <summary>
        /// 32-bit unsigned backing private property
        /// </summary>
        private uint Value
        {
            get => field & 0x00FFFFFF;
            set => field = value & 0x00FFFFFF;
        }

        #endregion

        #region Constructors

        public UInt24()
        {
            Value = 0;
        }

        public UInt24(uint value)
        {
            Value = value;
        }

        #endregion

        #region Arithmetic operators

        public static UInt24 operator ++(UInt24 a)
        {
            uint value = a.Value++;
            return new UInt24(value);
        }

        public static UInt24 operator --(UInt24 a)
        {
            uint value = a.Value--;
            return new UInt24(value);
        }

        public static UInt24 operator +(UInt24 a)
        {
            uint value = a.Value;
            return new UInt24(value);
        }

        public static UInt24 operator *(UInt24 a, UInt24 b)
        {
            uint value = a.Value * b.Value;
            return new UInt24(value);
        }

        public static UInt24 operator /(UInt24 a, UInt24 b)
        {
            uint value = a.Value / b.Value;
            return new UInt24(value);
        }

        public static UInt24 operator %(UInt24 a, UInt24 b)
        {
            uint value = a.Value % b.Value;
            return new UInt24(value);
        }

        public static UInt24 operator +(UInt24 a, UInt24 b)
        {
            uint value = a.Value + b.Value;
            return new UInt24(value);
        }

        public static UInt24 operator -(UInt24 a, UInt24 b)
        {
            uint value = a.Value - b.Value;
            return new UInt24(value);
        }

        #endregion

        #region Bitwise and shift operators

        public static UInt24 operator ~(UInt24 a)
        {
            uint value = ~a.Value;
            return new UInt24(value);
        }

        public static UInt24 operator <<(UInt24 a, int shift)
        {
            uint value = a.Value << shift;
            return new UInt24(value);
        }

        public static UInt24 operator >>(UInt24 a, int shift)
        {
            uint value = a.Value >> shift;
            return new UInt24(value);
        }

        public static UInt24 operator >>>(UInt24 a, int shift)
        {
            uint value = a.Value >>> shift;
            return new UInt24(value);
        }

        public static UInt24 operator &(UInt24 a, UInt24 b)
        {
            uint value = a.Value & b.Value;
            return new UInt24(value);
        }

        public static UInt24 operator |(UInt24 a, UInt24 b)
        {
            uint value = a.Value | b.Value;
            return new UInt24(value);
        }

        public static UInt24 operator ^(UInt24 a, UInt24 b)
        {
            uint value = a.Value ^ b.Value;
            return new UInt24(value);
        }

        public static UInt24 operator &(UInt24 a, byte b)
        {
            uint value = a.Value & b;
            return new UInt24(value);
        }

        public static UInt24 operator |(UInt24 a, byte b)
        {
            uint value = a.Value | b;
            return new UInt24(value);
        }

        public static UInt24 operator ^(UInt24 a, byte b)
        {
            uint value = a.Value ^ b;
            return new UInt24(value);
        }

        public static UInt24 operator &(UInt24 a, sbyte b)
        {
            uint value = a.Value & (uint)b;
            return new UInt24(value);
        }

        public static UInt24 operator |(UInt24 a, sbyte b)
        {
            uint value = a.Value | (byte)b;
            return new UInt24(value);
        }

        public static UInt24 operator ^(UInt24 a, sbyte b)
        {
            uint value = a.Value ^ (uint)b;
            return new UInt24(value);
        }

        public static UInt24 operator &(UInt24 a, short b)
        {
            uint value = a.Value & (uint)b;
            return new UInt24(value);
        }

        public static UInt24 operator |(UInt24 a, short b)
        {
            uint value = a.Value | (ushort)b;
            return new UInt24(value);
        }

        public static UInt24 operator ^(UInt24 a, short b)
        {
            uint value = a.Value ^ (uint)b;
            return new UInt24(value);
        }

        public static UInt24 operator &(UInt24 a, ushort b)
        {
            uint value = a.Value & b;
            return new UInt24(value);
        }

        public static UInt24 operator |(UInt24 a, ushort b)
        {
            uint value = a.Value | b;
            return new UInt24(value);
        }

        public static UInt24 operator ^(UInt24 a, ushort b)
        {
            uint value = a.Value ^ b;
            return new UInt24(value);
        }

        public static UInt24 operator &(UInt24 a, int b)
        {
            uint value = a.Value & (uint)b;
            return new UInt24(value);
        }

        public static UInt24 operator |(UInt24 a, int b)
        {
            uint value = a.Value | (uint)b;
            return new UInt24(value);
        }

        public static UInt24 operator ^(UInt24 a, int b)
        {
            uint value = a.Value ^ (uint)b;
            return new UInt24(value);
        }

        public static UInt24 operator &(UInt24 a, uint b)
        {
            uint value = a.Value & b;
            return new UInt24(value);
        }

        public static UInt24 operator |(UInt24 a, uint b)
        {
            uint value = a.Value | b;
            return new UInt24(value);
        }

        public static UInt24 operator ^(UInt24 a, uint b)
        {
            uint value = a.Value ^ b;
            return new UInt24(value);
        }

        #endregion

        #region Equality operators

        public static bool operator ==(UInt24 a, UInt24 b) => a.Value.Equals(b.Value);

        public static bool operator !=(UInt24 a, UInt24 b) => !a.Value.Equals(b.Value);

        #endregion

        #region Comparison operators

        public static bool operator <(UInt24 a, UInt24 b) => a.Value < b.Value;

        public static bool operator >(UInt24 a, UInt24 b) => a.Value > b.Value;

        public static bool operator <=(UInt24 a, UInt24 b) => a.Value <= b.Value;

        public static bool operator >=(UInt24 a, UInt24 b) => a.Value >= b.Value;

        #endregion

        #region User-defined conversion operators

        public static explicit operator byte(UInt24 a) => (byte)a.Value;

        public static explicit operator UInt24(byte a) => new(a);

        public static explicit operator sbyte(UInt24 a) => (sbyte)a.Value;

        public static explicit operator UInt24(sbyte a) => new((uint)a);

        public static explicit operator short(UInt24 a) => (short)a.Value;

        public static explicit operator UInt24(short a) => new((uint)a);

        public static explicit operator ushort(UInt24 a) => (ushort)a.Value;

        public static explicit operator UInt24(ushort a) => new(a);

        public static explicit operator int(UInt24 a) => (int)a.Value;

        public static explicit operator UInt24(int a) => new((uint)a);

        public static explicit operator uint(UInt24 a) => a.Value;

        public static explicit operator UInt24(uint a) => new(a);

        public static explicit operator long(UInt24 a) => a.Value;

        public static explicit operator UInt24(long a) => new((uint)a);

        public static explicit operator ulong(UInt24 a) => a.Value;

        public static explicit operator UInt24(ulong a) => new((uint)a);

        #endregion

        #region Object

#if NETCOREAPP
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UInt24 t)
                return Equals(t);

            return base.Equals(obj);
        }
#else
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is UInt24 t)
                return Equals(t);

            return base.Equals(obj);
        }
#endif

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

#if NETCOREAPP
        /// <inheritdoc/>
        public override string? ToString() => Value.ToString();
#else
        /// <inheritdoc/>
        public override string ToString() => Value.ToString();
#endif

        #endregion

        #region IComparable

        /// <inheritdoc/>
#if NETCOREAPP
        public int CompareTo(object? obj) => Value.CompareTo(obj);
#else
        public int CompareTo(object obj) => Value.CompareTo(obj);
#endif

        #endregion

        #region IConvertible

        /// <inheritdoc/>
        public TypeCode GetTypeCode() => Value.GetTypeCode();

#if NETCOREAPP
        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider? provider) => ((IConvertible)Value).ToBoolean(provider);

        /// <inheritdoc/>
        public char ToChar(IFormatProvider? provider) => ((IConvertible)Value).ToChar(provider);

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider? provider) => ((IConvertible)Value).ToSByte(provider);

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider? provider) => ((IConvertible)Value).ToByte(provider);

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider? provider) => ((IConvertible)Value).ToInt16(provider);

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider? provider) => ((IConvertible)Value).ToUInt16(provider);

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider? provider) => ((IConvertible)Value).ToInt32(provider);

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider? provider) => ((IConvertible)Value).ToUInt32(provider);

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider? provider) => ((IConvertible)Value).ToInt64(provider);

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider? provider) => ((IConvertible)Value).ToUInt64(provider);

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider? provider) => ((IConvertible)Value).ToSingle(provider);

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider? provider) => ((IConvertible)Value).ToDouble(provider);

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider? provider) => ((IConvertible)Value).ToDecimal(provider);

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider? provider) => ((IConvertible)Value).ToDateTime(provider);

        /// <inheritdoc/>
        public string ToString(IFormatProvider? provider) => Value.ToString(provider);

        /// <inheritdoc/>
        public object ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)Value).ToType(conversionType, provider);
#else
        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider provider) => ((IConvertible)Value).ToBoolean(provider);

        /// <inheritdoc/>
        public char ToChar(IFormatProvider provider) => ((IConvertible)Value).ToChar(provider);

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider provider) => ((IConvertible)Value).ToSByte(provider);

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider provider) => ((IConvertible)Value).ToByte(provider);

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider provider) => ((IConvertible)Value).ToInt16(provider);

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider provider) => ((IConvertible)Value).ToUInt16(provider);

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider provider) => ((IConvertible)Value).ToInt32(provider);

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider provider) => ((IConvertible)Value).ToUInt32(provider);

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider provider) => ((IConvertible)Value).ToInt64(provider);

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider provider) => ((IConvertible)Value).ToUInt64(provider);

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider provider) => ((IConvertible)Value).ToSingle(provider);

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider provider) => ((IConvertible)Value).ToDouble(provider);

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider provider) => ((IConvertible)Value).ToDecimal(provider);

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider provider) => ((IConvertible)Value).ToDateTime(provider);

        /// <inheritdoc/>
        public string ToString(IFormatProvider provider) => Value.ToString(provider);

        /// <inheritdoc/>
        public object ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)Value).ToType(conversionType, provider);
#endif

        #endregion

        #region IEquatable

#if NETCOREAPP
        /// <inheritdoc/>
        public bool Equals(UInt24? other)
        {
            if (other is null)
                return false;

            return Value.Equals(other.Value);
        }
#else
        /// <inheritdoc/>
        public bool Equals(UInt24 other)
            => Value.Equals(other.Value);
#endif

        #endregion
    }
}
