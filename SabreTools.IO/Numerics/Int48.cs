using System;

namespace SabreTools.Numerics
{
    /// <summary>
    /// Represents a 48-bit signed integer.
    /// </summary>
    /// <remarks>Range: -140,737,488,355,328 to 140,737,488,355,327</remarks>
    /// TODO: Do bit shifts account for the sign bit?
    public class Int48 : IComparable, IConvertible, IEquatable<Int48>
    {
        #region Properties

        /// <summary>
        /// 64-bit signed backing private property
        /// </summary>
        private long Value
        {
            get
            {
                if (field < 0)
                    return -(field & 0x0000FFFFFFFFFFFF);
                else
                    return field & 0x0000FFFFFFFFFFFF;
            }
            set
            {
                if (value < 0)
                    field = -(value & 0x0000FFFFFFFFFFFF);
                else
                    field = value & 0x0000FFFFFFFFFFFF;
            }
        }

        #endregion

        #region Constructors

        public Int48()
        {
            Value = 0;
        }

        public Int48(long value)
        {
            Value = value;
        }

        #endregion

        #region Arithmetic operators

        public static Int48 operator ++(Int48 a)
        {
            long value = a.Value++;
            return new Int48(value);
        }

        public static Int48 operator --(Int48 a)
        {
            long value = a.Value--;
            return new Int48(value);
        }

        public static Int48 operator +(Int48 a)
        {
            long value = a.Value;
            return new Int48(value);
        }

        public static Int48 operator -(Int48 a)
        {
            long value = -a.Value;
            return new Int48(value);
        }

        public static Int48 operator *(Int48 a, Int48 b)
        {
            long value = a.Value * b.Value;
            return new Int48(value);
        }

        public static Int48 operator /(Int48 a, Int48 b)
        {
            long value = a.Value / b.Value;
            return new Int48(value);
        }

        public static Int48 operator %(Int48 a, Int48 b)
        {
            long value = a.Value % b.Value;
            return new Int48(value);
        }

        public static Int48 operator +(Int48 a, Int48 b)
        {
            long value = a.Value + b.Value;
            return new Int48(value);
        }

        public static Int48 operator -(Int48 a, Int48 b)
        {
            long value = a.Value - b.Value;
            return new Int48(value);
        }

        #endregion

        #region Bitwise and shift operators

        public static Int48 operator ~(Int48 a)
        {
            long value = ~a.Value;
            return new Int48(value);
        }

        public static Int48 operator <<(Int48 a, int shift)
        {
            long value = a.Value << shift;
            return new Int48(value);
        }

        public static Int48 operator >>(Int48 a, int shift)
        {
            long value = a.Value >> shift;
            return new Int48(value);
        }

        public static Int48 operator >>>(Int48 a, int shift)
        {
            long value = a.Value >>> shift;
            return new Int48(value);
        }

        public static Int48 operator &(Int48 a, Int48 b)
        {
            long value = a.Value & b.Value;
            return new Int48(value);
        }

        public static Int48 operator |(Int48 a, Int48 b)
        {
            long value = a.Value | b.Value;
            return new Int48(value);
        }

        public static Int48 operator ^(Int48 a, Int48 b)
        {
            long value = a.Value ^ b.Value;
            return new Int48(value);
        }

        #endregion

        #region Equality operators

        public static bool operator ==(Int48 a, Int48 b) => a.Value.Equals(b.Value);

        public static bool operator !=(Int48 a, Int48 b) => !a.Value.Equals(b.Value);

        #endregion

        #region Comparison operators

        public static bool operator <(Int48 a, Int48 b) => a.Value < b.Value;

        public static bool operator >(Int48 a, Int48 b) => a.Value > b.Value;

        public static bool operator <=(Int48 a, Int48 b) => a.Value <= b.Value;

        public static bool operator >=(Int48 a, Int48 b) => a.Value >= b.Value;

        #endregion

        #region User-defined conversion operators

        public static explicit operator long(Int48 a) => a.Value;

        public static explicit operator Int48(long a) => new(a);

        #endregion

        #region Object

#if NETCOREAPP
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Int48 t)
                return Equals(t);

            return base.Equals(obj);
        }
#else
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Int48 t)
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
        public bool Equals(Int48? other)
        {
            if (other is null)
                return false;

            return Value.Equals(other.Value);
        }
#else
        /// <inheritdoc/>
        public bool Equals(Int48 other)
            => Value.Equals(other.Value);
#endif

        #endregion
    }
}
