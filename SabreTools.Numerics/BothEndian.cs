using System;

namespace SabreTools.Numerics
{
    /// <summary>
    /// Both-endian numeric value
    /// </summary>
    public abstract class BothEndian<TNumeric>(TNumeric le, TNumeric be) : IComparable, IConvertible, IEquatable<BothEndian<TNumeric>>, IEquatable<TNumeric>
        where TNumeric : notnull, IComparable, IConvertible, IEquatable<TNumeric>
    {
        #region Properties

        /// <summary>
        /// Little-endian representation of the number
        /// </summary>
        /// <remarks>Value should match <see cref="BigEndian"/></remarks>
        public readonly TNumeric LittleEndian = le;

        /// <summary>
        /// Big-endian representation of the number
        /// </summary>
        /// <remarks>Value should match <see cref="LittleEndian"/></remarks>
        public readonly TNumeric BigEndian = be;

        /// <summary>
        /// Indicates if the value is valid
        /// </summary>
        /// <remarks>
        /// Validity of a both-endian value is determined based on if both
        /// endianness values match. These values should always match, based
        /// on all implementations.
        /// </remarks>
        public bool IsValid => LittleEndian.Equals(BigEndian);

        #endregion

        #region Operators

        /// <remarks>
        /// Returns either <see cref="LittleEndian"/> or <see cref="BigEndian"/>
        /// depending on the system endianness.
        /// </remarks>
        public static implicit operator TNumeric(BothEndian<TNumeric> val)
            => BitConverter.IsLittleEndian ? val.LittleEndian : val.BigEndian;

        public static bool operator ==(BothEndian<TNumeric> a, BothEndian<TNumeric> b) => a.Equals(b);

        public static bool operator !=(BothEndian<TNumeric> a, BothEndian<TNumeric> b) => !a.Equals(b);

        #endregion

        #region Object

#if NETCOREAPP
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is BothEndian<TNumeric> be)
                return Equals(be);

            if (obj is TNumeric t)
                return Equals(t);

            return base.Equals(obj);
        }
#else
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is BothEndian<TNumeric> be)
                return Equals(be);

            if (obj is TNumeric t)
                return Equals(t);

            return base.Equals(obj);
        }
#endif

        /// <inheritdoc/>
        public override int GetHashCode() => ((TNumeric)this).GetHashCode();

#if NETCOREAPP
        /// <inheritdoc/>
        public override string? ToString() => ((TNumeric)this).ToString();
#else
        /// <inheritdoc/>
        public override string ToString() => ((TNumeric)this).ToString();
#endif

        #endregion

        #region IComparable

        /// <inheritdoc/>
#if NETCOREAPP
        public int CompareTo(object? obj) => ((TNumeric)this).CompareTo(obj);
#else
        public int CompareTo(object obj) => ((TNumeric)this).CompareTo(obj);
#endif

        #endregion

        #region IConvertible

        /// <inheritdoc/>
        public TypeCode GetTypeCode() => ((TNumeric)this).GetTypeCode();

#if NETCOREAPP
        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider? provider) => ((TNumeric)this).ToBoolean(provider);

        /// <inheritdoc/>
        public char ToChar(IFormatProvider? provider) => ((TNumeric)this).ToChar(provider);

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider? provider) => ((TNumeric)this).ToSByte(provider);

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider? provider) => ((TNumeric)this).ToByte(provider);

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider? provider) => ((TNumeric)this).ToInt16(provider);

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider? provider) => ((TNumeric)this).ToUInt16(provider);

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider? provider) => ((TNumeric)this).ToInt32(provider);

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider? provider) => ((TNumeric)this).ToUInt32(provider);

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider? provider) => ((TNumeric)this).ToInt64(provider);

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider? provider) => ((TNumeric)this).ToUInt64(provider);

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider? provider) => ((TNumeric)this).ToSingle(provider);

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider? provider) => ((TNumeric)this).ToDouble(provider);

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider? provider) => ((TNumeric)this).ToDecimal(provider);

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider? provider) => ((TNumeric)this).ToDateTime(provider);

        /// <inheritdoc/>
        public string ToString(IFormatProvider? provider) => ((TNumeric)this).ToString(provider);

        /// <inheritdoc/>
        public object ToType(Type conversionType, IFormatProvider? provider) => ((TNumeric)this).ToType(conversionType, provider);
#else
        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider provider) => ((TNumeric)this).ToBoolean(provider);

        /// <inheritdoc/>
        public char ToChar(IFormatProvider provider) => ((TNumeric)this).ToChar(provider);

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider provider) => ((TNumeric)this).ToSByte(provider);

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider provider) => ((TNumeric)this).ToByte(provider);

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider provider) => ((TNumeric)this).ToInt16(provider);

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider provider) => ((TNumeric)this).ToUInt16(provider);

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider provider) => ((TNumeric)this).ToInt32(provider);

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider provider) => ((TNumeric)this).ToUInt32(provider);

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider provider) => ((TNumeric)this).ToInt64(provider);

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider provider) => ((TNumeric)this).ToUInt64(provider);

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider provider) => ((TNumeric)this).ToSingle(provider);

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider provider) => ((TNumeric)this).ToDouble(provider);

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider provider) => ((TNumeric)this).ToDecimal(provider);

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider provider) => ((TNumeric)this).ToDateTime(provider);

        /// <inheritdoc/>
        public string ToString(IFormatProvider provider) => ((TNumeric)this).ToString(provider);

        /// <inheritdoc/>
        public object ToType(Type conversionType, IFormatProvider provider) => ((TNumeric)this).ToType(conversionType, provider);
#endif

        #endregion

        #region IEquatable

#if NETCOREAPP
        /// <inheritdoc/>
        public bool Equals(BothEndian<TNumeric>? other)
        {
            if (other is null)
                return false;

            return LittleEndian.Equals(other.LittleEndian) && BigEndian.Equals(other.BigEndian);
        }

        /// <inheritdoc/>
        public bool Equals(TNumeric? other) => ((TNumeric)this).Equals(other);
#else
        /// <inheritdoc/>
        public bool Equals(BothEndian<TNumeric> other)
            => LittleEndian.Equals(other.LittleEndian) && BigEndian.Equals(other.BigEndian);

        /// <inheritdoc/>
        public bool Equals(TNumeric other) => ((TNumeric)this).Equals(other);
#endif

        #endregion
    }
}
