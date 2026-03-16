using System;

namespace SabreTools.Numerics
{
    /// <summary>
    /// Represents a 24-bit signed integer.
    /// </summary>
    /// <remarks>Range: -8,388,608 to 8,388,607</remarks>
    /// TODO: Do bit shifts account for the sign bit?
    public class Int24 : IComparable, IConvertible, IEquatable<Int24>
    {
        #region Properties

        /// <summary>
        /// 32-bit signed backing private property
        /// </summary>
        private int Value
        {
            get
            {
                if (field < 0)
                    return -(field & 0x00FFFFFF);
                else
                    return field & 0x00FFFFFF;
            }
            set
            {
                if (value < 0)
                    field = -(value & 0x00FFFFFF);
                else
                    field = value & 0x00FFFFFF;
            }
        }

        #endregion

        #region Constructors

        public Int24()
        {
            Value = 0;
        }

        public Int24(int value)
        {
            Value = value;
        }

        #endregion

        #region Arithmetic operators

        public static Int24 operator ++(Int24 a)
        {
            int value = a.Value++;
            return new Int24(value);
        }

        public static Int24 operator --(Int24 a)
        {
            int value = a.Value--;
            return new Int24(value);
        }

        public static Int24 operator +(Int24 a)
        {
            int value = a.Value;
            return new Int24(value);
        }

        public static Int24 operator -(Int24 a)
        {
            int value = -a.Value;
            return new Int24(value);
        }

        public static Int24 operator *(Int24 a, Int24 b)
        {
            int value = a.Value * b.Value;
            return new Int24(value);
        }

        public static Int24 operator /(Int24 a, Int24 b)
        {
            int value = a.Value / b.Value;
            return new Int24(value);
        }

        public static Int24 operator %(Int24 a, Int24 b)
        {
            int value = a.Value % b.Value;
            return new Int24(value);
        }

        public static Int24 operator +(Int24 a, Int24 b)
        {
            int value = a.Value + b.Value;
            return new Int24(value);
        }

        public static Int24 operator -(Int24 a, Int24 b)
        {
            int value = a.Value - b.Value;
            return new Int24(value);
        }

        #endregion

        #region Bitwise and shift operators

        public static Int24 operator ~(Int24 a)
        {
            int value = ~a.Value;
            return new Int24(value);
        }

        public static Int24 operator <<(Int24 a, int shift)
        {
            int value = a.Value << shift;
            return new Int24(value);
        }

        public static Int24 operator >>(Int24 a, int shift)
        {
            int value = a.Value >> shift;
            return new Int24(value);
        }

        public static Int24 operator >>>(Int24 a, int shift)
        {
            int value = a.Value >>> shift;
            return new Int24(value);
        }

        public static Int24 operator &(Int24 a, Int24 b)
        {
            int value = a.Value & b.Value;
            return new Int24(value);
        }

        public static Int24 operator |(Int24 a, Int24 b)
        {
            int value = a.Value | b.Value;
            return new Int24(value);
        }

        public static Int24 operator ^(Int24 a, Int24 b)
        {
            int value = a.Value ^ b.Value;
            return new Int24(value);
        }

        #endregion

        #region Equality operators

        public static bool operator ==(Int24 a, Int24 b) => a.Value.Equals(b.Value);

        public static bool operator !=(Int24 a, Int24 b) => !a.Value.Equals(b.Value);

        #endregion

        #region Comparison operators

        public static bool operator <(Int24 a, Int24 b) => a.Value < b.Value;

        public static bool operator >(Int24 a, Int24 b) => a.Value > b.Value;

        public static bool operator <=(Int24 a, Int24 b) => a.Value <= b.Value;

        public static bool operator >=(Int24 a, Int24 b) => a.Value >= b.Value;

        #endregion

        #region User-defined conversion operators

        public static explicit operator int(Int24 a) => a.Value;

        public static explicit operator Int24(int a) => new(a);

        #endregion

        #region Object

#if NETCOREAPP
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Int24 t)
                return Equals(t);

            return base.Equals(obj);
        }
#else
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Int24 t)
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
        public bool Equals(Int24? other)
        {
            if (other is null)
                return false;

            return Value.Equals(other.Value);
        }
#else
        /// <inheritdoc/>
        public bool Equals(Int24 other)
            => Value.Equals(other.Value);
#endif

        #endregion
    }
}
