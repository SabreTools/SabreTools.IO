using System;

namespace SabreTools.IO.Numerics
{
    /// <summary>
    /// Both-endian numeric value
    /// </summary>
    public abstract class BothEndian<T>(T le, T be) : IComparable, IConvertible, IEquatable<BothEndian<T>>, IEquatable<T>
        where T : notnull, IComparable, IConvertible, IEquatable<T>
    {
        #region Properties

        /// <summary>
        /// Little-endian representation of the number
        /// </summary>
        /// <remarks>Value should match <see cref="BigEndian"/></remarks>
        public readonly T LittleEndian = le;

        /// <summary>
        /// Big-endian representation of the number
        /// </summary>
        /// <remarks>Value should match <see cref="LittleEndian"/></remarks>
        public readonly T BigEndian = be;

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
        public static implicit operator T(BothEndian<T> val)
            => BitConverter.IsLittleEndian ? val.LittleEndian : val.BigEndian;

        public static bool operator ==(BothEndian<T> a, BothEndian<T> b) => a.Equals(b);

        public static bool operator !=(BothEndian<T> a, BothEndian<T> b) => !a.Equals(b);

        #endregion

        #region Object

#if NETCOREAPP
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is BothEndian<T> be)
                return Equals(be);

            if (obj is T t)
                return Equals(t);

            return base.Equals(obj);
        }
#else
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is BothEndian<T> be)
                return Equals(be);

            if (obj is T t)
                return Equals(t);

            return base.Equals(obj);
        }
#endif

        /// <inheritdoc/>
        public override int GetHashCode() => ((T)this).GetHashCode();

#if NETCOREAPP
        /// <inheritdoc/>
        public override string? ToString() => ((T)this).ToString();
#else
        /// <inheritdoc/>
        public override string ToString() => ((T)this).ToString();
#endif

#endregion

        #region IComparable

        /// <inheritdoc/>
#if NETCOREAPP
        public int CompareTo(object? obj) => ((T)this).CompareTo(obj);
#else
        public int CompareTo(object obj) => ((T)this).CompareTo(obj);
#endif

        #endregion

        #region IConvertible

        /// <inheritdoc/>
        public TypeCode GetTypeCode() => ((T)this).GetTypeCode();

#if NETCOREAPP
        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider? provider) => ((T)this).ToBoolean(provider);

        /// <inheritdoc/>
        public char ToChar(IFormatProvider? provider) => ((T)this).ToChar(provider);

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider? provider) => ((T)this).ToSByte(provider);

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider? provider) => ((T)this).ToByte(provider);

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider? provider) => ((T)this).ToInt16(provider);

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider? provider) => ((T)this).ToUInt16(provider);

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider? provider) => ((T)this).ToInt32(provider);

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider? provider) => ((T)this).ToUInt32(provider);

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider? provider) => ((T)this).ToInt64(provider);

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider? provider) => ((T)this).ToUInt64(provider);

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider? provider) => ((T)this).ToSingle(provider);

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider? provider) => ((T)this).ToDouble(provider);

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider? provider) => ((T)this).ToDecimal(provider);

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider? provider) => ((T)this).ToDateTime(provider);

        /// <inheritdoc/>
        public string ToString(IFormatProvider? provider) => ((T)this).ToString(provider);

        /// <inheritdoc/>
        public object ToType(Type conversionType, IFormatProvider? provider) => ((T)this).ToType(conversionType, provider);
#else
        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider provider) => ((T)this).ToBoolean(provider);

        /// <inheritdoc/>
        public char ToChar(IFormatProvider provider) => ((T)this).ToChar(provider);

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider provider) => ((T)this).ToSByte(provider);

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider provider) => ((T)this).ToByte(provider);

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider provider) => ((T)this).ToInt16(provider);

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider provider) => ((T)this).ToUInt16(provider);

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider provider) => ((T)this).ToInt32(provider);

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider provider) => ((T)this).ToUInt32(provider);

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider provider) => ((T)this).ToInt64(provider);

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider provider) => ((T)this).ToUInt64(provider);

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider provider) => ((T)this).ToSingle(provider);

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider provider) => ((T)this).ToDouble(provider);

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider provider) => ((T)this).ToDecimal(provider);

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider provider) => ((T)this).ToDateTime(provider);

        /// <inheritdoc/>
        public string ToString(IFormatProvider provider) => ((T)this).ToString(provider);

        /// <inheritdoc/>
        public object ToType(Type conversionType, IFormatProvider provider) => ((T)this).ToType(conversionType, provider);
#endif

        #endregion

        #region IEquatable

#if NETCOREAPP
        /// <inheritdoc/>
        public bool Equals(BothEndian<T>? other)
        {
            if (other is null)
                return false;

            return LittleEndian.Equals(other.LittleEndian) && BigEndian.Equals(other.BigEndian);
        }

        /// <inheritdoc/>
        public bool Equals(T? other) => ((T)this).Equals(other);
#else
        /// <inheritdoc/>
        public bool Equals(BothEndian<T> other)
            => LittleEndian.Equals(other.LittleEndian) && BigEndian.Equals(other.BigEndian);

        /// <inheritdoc/>
        public bool Equals(T other) => ((T)this).Equals(other);
#endif

        #endregion
    }
}
