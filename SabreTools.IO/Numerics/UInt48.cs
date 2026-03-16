using System;

namespace SabreTools.Numerics
{
    /// <summary>
    /// Represents a 48-bit unsigned integer.
    /// </summary>
    /// <remarks>Range: 0 to 281,474,976,710,655</remarks>
    public class UInt48 : IComparable, IConvertible, IEquatable<UInt48>
    {
        #region Properties

        /// <summary>
        /// 64-bit unsigned backing private property
        /// </summary>
        private ulong Value
        {
            get => field & 0x0000FFFFFFFFFFFF;
            set => field = value & 0x0000FFFFFFFFFFFF;
        }

        #endregion

        #region Constructors

        public UInt48()
        {
            Value = 0;
        }

        public UInt48(ulong value)
        {
            Value = value;
        }

        #endregion

        #region Arithmetic operators

        public static UInt48 operator ++(UInt48 a)
        {
            ulong value = a.Value++;
            return new UInt48(value);
        }

        public static UInt48 operator --(UInt48 a)
        {
            ulong value = a.Value--;
            return new UInt48(value);
        }

        public static UInt48 operator +(UInt48 a)
        {
            ulong value = a.Value;
            return new UInt48(value);
        }

        public static UInt48 operator *(UInt48 a, UInt48 b)
        {
            ulong value = a.Value * b.Value;
            return new UInt48(value);
        }

        public static UInt48 operator /(UInt48 a, UInt48 b)
        {
            ulong value = a.Value / b.Value;
            return new UInt48(value);
        }

        public static UInt48 operator %(UInt48 a, UInt48 b)
        {
            ulong value = a.Value % b.Value;
            return new UInt48(value);
        }

        public static UInt48 operator +(UInt48 a, UInt48 b)
        {
            ulong value = a.Value + b.Value;
            return new UInt48(value);
        }

        public static UInt48 operator -(UInt48 a, UInt48 b)
        {
            ulong value = a.Value - b.Value;
            return new UInt48(value);
        }

        #endregion

        #region Bitwise and shift operators

        public static UInt48 operator ~(UInt48 a)
        {
            ulong value = ~a.Value;
            return new UInt48(value);
        }

        public static UInt48 operator <<(UInt48 a, int shift)
        {
            ulong value = a.Value << shift;
            return new UInt48(value);
        }

        public static UInt48 operator >>(UInt48 a, int shift)
        {
            ulong value = a.Value >> shift;
            return new UInt48(value);
        }

        public static UInt48 operator >>>(UInt48 a, int shift)
        {
            ulong value = a.Value >>> shift;
            return new UInt48(value);
        }

        public static UInt48 operator &(UInt48 a, UInt48 b)
        {
            ulong value = a.Value & b.Value;
            return new UInt48(value);
        }

        public static UInt48 operator |(UInt48 a, UInt48 b)
        {
            ulong value = a.Value | b.Value;
            return new UInt48(value);
        }

        public static UInt48 operator ^(UInt48 a, UInt48 b)
        {
            ulong value = a.Value ^ b.Value;
            return new UInt48(value);
        }

        #endregion

        #region Equality operators

        public static bool operator ==(UInt48 a, UInt48 b) => a.Value.Equals(b.Value);

        public static bool operator !=(UInt48 a, UInt48 b) => !a.Value.Equals(b.Value);

        #endregion

        #region Comparison operators

        public static bool operator <(UInt48 a, UInt48 b) => a.Value < b.Value;

        public static bool operator >(UInt48 a, UInt48 b) => a.Value > b.Value;

        public static bool operator <=(UInt48 a, UInt48 b) => a.Value <= b.Value;

        public static bool operator >=(UInt48 a, UInt48 b) => a.Value >= b.Value;

        #endregion

        #region User-defined conversion operators

        public static explicit operator ulong(UInt48 a) => a.Value;

        public static explicit operator UInt48(ulong a) => new(a);

        #endregion

        #region Object

#if NETCOREAPP
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is UInt48 t)
                return Equals(t);

            return base.Equals(obj);
        }
#else
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is UInt48 t)
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
        public bool Equals(UInt48? other)
        {
            if (other is null)
                return false;

            return Value.Equals(other.Value);
        }
#else
        /// <inheritdoc/>
        public bool Equals(UInt48 other)
            => Value.Equals(other.Value);
#endif

        #endregion
    }
}
