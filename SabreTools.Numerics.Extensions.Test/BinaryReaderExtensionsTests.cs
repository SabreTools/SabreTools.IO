using System;
using System.IO;
using System.Linq;
using System.Numerics;
using Xunit;

namespace SabreTools.Numerics.Extensions.Test
{
    public class BinaryReaderExtensionsTests
    {
        /// <summary>
        /// Test pattern from 0x00-0x0F
        /// </summary>
        private static readonly byte[] _bytes =
        [
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        /// <summary>
        /// Represents the decimal value 0.0123456789
        /// </summary>
        private static readonly byte[] _decimalBytes =
        [
            0x15, 0xCD, 0x5B, 0x07, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00,
        ];

        /// <summary>
        /// Test pattern for big-endian GUID created from <see cref="_bytes"/>
        /// </summary>
        private static readonly byte[] _guidBigEndianbytes =
        [
            0x03, 0x02, 0x01, 0x00, 0x05, 0x04, 0x07, 0x06,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];

        #region Exact Read

        [Fact]
        public void ReadByteArrayTest()
        {
            byte[] arr = new byte[4];
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.Read(arr, 0, 4);
            Assert.Equal(4, read);
            Assert.True(arr.SequenceEqual(_bytes.Take(4)));
        }

        [Fact]
        public void ReadCharArrayTest()
        {
            char[] arr = new char[4];
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.Read(arr, 0, 4);
            Assert.Equal(4, read);
            Assert.True(arr.SequenceEqual(_bytes.Take(4).Select(b => (char)b)));
        }

        [Fact]
        public void ReadByteTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            byte read = br.ReadByte();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt8 read = br.ReadByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
        }

        [Fact]
        public void ReadBytesTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int length = 4;
            byte[] read = br.ReadBytes(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length)));
        }

        [Fact]
        public void ReadCharsTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int length = 4;
            char[] read = br.ReadChars(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length).Select(b => (char)b)));
        }

        [Fact]
        public void ReadSByteTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            sbyte read = br.ReadSByte();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void ReadSByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt8 read = br.ReadSByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
        }

        [Fact]
        public void ReadCharTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            char read = br.ReadChar();
            Assert.Equal('\0', read);
        }

        [Fact]
        public void ReadInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.ReadInt16();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.ReadInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.ReadInt16LittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt16 read = br.ReadInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
        }

        [Fact]
        public void ReadUInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadUInt16();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadUInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadUInt16BigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadUInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadUInt16LittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadUInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt16 read = br.ReadUInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
        }

        [Fact]
        public void ReadWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadWORD();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadWORDBigEndian();
            Assert.Equal(0x0001, read);
        }

        [Fact]
        public void ReadWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.ReadWORDLittleEndian();
            Assert.Equal(0x0100, read);
        }

        [Fact]
        public void ReadWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt16 read = br.ReadWORDBothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
        }

        [Fact]
        public void ReadHalfTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = br.ReadHalf();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadHalfBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            Half read = br.ReadHalfBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadHalfLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = br.ReadHalfLittleEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int24 read = br.ReadInt24();
            Assert.Equal(0x020100, (int)read);
        }

        [Fact]
        public void ReadInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int24 read = br.ReadInt24BigEndian();
            Assert.Equal(0x000102, (int)read);
        }

        [Fact]
        public void ReadInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int24 read = br.ReadInt24LittleEndian();
            Assert.Equal(0x020100, (int)read);
        }

        [Fact]
        public void ReadUInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt24 read = br.ReadUInt24();
            Assert.Equal((uint)0x020100, (uint)read);
        }

        [Fact]
        public void ReadUInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt24 read = br.ReadUInt24BigEndian();
            Assert.Equal((uint)0x000102, (uint)read);
        }

        [Fact]
        public void ReadUInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt24 read = br.ReadUInt24LittleEndian();
            Assert.Equal((uint)0x020100, (uint)read);
        }

        [Fact]
        public void ReadInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt32();
            Assert.Equal(0x03020100, read);
        }

        [Fact]
        public void ReadInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt32BigEndian();
            Assert.Equal(0x00010203, read);
        }

        [Fact]
        public void ReadInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.ReadInt32LittleEndian();
            Assert.Equal(0x03020100, read);
        }

        [Fact]
        public void ReadInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt32 read = br.ReadInt32BothEndian();
            Assert.Equal(0x03020100, read.LittleEndian);
            Assert.Equal(0x04050607, read.BigEndian);
        }

        [Fact]
        public void ReadUInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt32();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadUInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt32BigEndian();
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadUInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadUInt32LittleEndian();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadUInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt32 read = br.ReadUInt32BothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
        }

        [Fact]
        public void ReadDWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadDWORD();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadDWORDBigEndian();
            Assert.Equal((uint)0x00010203, read);
        }

        [Fact]
        public void ReadDWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.ReadDWORDLittleEndian();
            Assert.Equal((uint)0x03020100, read);
        }

        [Fact]
        public void ReadDWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt32 read = br.ReadDWORDBothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
        }

        [Fact]
        public void ReadSingleTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = br.ReadSingle();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadSingleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = br.ReadSingleBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadSingleLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = br.ReadSingleLittleEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int48 read = br.ReadInt48();
            Assert.Equal(0x050403020100, (long)read);
        }

        [Fact]
        public void ReadInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int48 read = br.ReadInt48BigEndian();
            Assert.Equal(0x000102030405, (long)read);
        }

        [Fact]
        public void ReadInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int48 read = br.ReadInt48LittleEndian();
            Assert.Equal(0x050403020100, (long)read);
        }

        [Fact]
        public void ReadUInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt48 read = br.ReadUInt48();
            Assert.Equal((ulong)0x050403020100, (ulong)read);
        }

        [Fact]
        public void ReadUInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt48 read = br.ReadUInt48BigEndian();
            Assert.Equal((ulong)0x000102030405, (ulong)read);
        }

        [Fact]
        public void ReadUInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt48 read = br.ReadUInt48LittleEndian();
            Assert.Equal((ulong)0x050403020100, (ulong)read);
        }

        [Fact]
        public void ReadInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt64();
            Assert.Equal(0x0706050403020100, read);
        }

        [Fact]
        public void ReadInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt64BigEndian();
            Assert.Equal(0x0001020304050607, read);
        }

        [Fact]
        public void ReadInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.ReadInt64LittleEndian();
            Assert.Equal(0x0706050403020100, read);
        }

        [Fact]
        public void ReadInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt64 read = br.ReadInt64BothEndian();
            Assert.Equal(0x0706050403020100, read.LittleEndian);
            Assert.Equal(0x08090A0B0C0D0E0F, read.BigEndian);
        }

        [Fact]
        public void ReadUInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt64();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadUInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt64BigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadUInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadUInt64LittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadUInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt64 read = br.ReadUInt64BothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
        }

        [Fact]
        public void ReadQWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadQWORD();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadQWORDBigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
        }

        [Fact]
        public void ReadQWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.ReadQWORDLittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
        }

        [Fact]
        public void ReadQWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt64 read = br.ReadQWORDBothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
        }

        [Fact]
        public void ReadDoubleTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = br.ReadDouble();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDoubleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = br.ReadDoubleBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDoubleLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = br.ReadDoubleLittleEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadGuidTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_bytes);
            Guid read = br.ReadGuid();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadGuidBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_guidBigEndianbytes);
            Guid read = br.ReadGuidBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadGuidLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_bytes);
            Guid read = br.ReadGuidLittleEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = br.ReadInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var reversed = Enumerable.Reverse(_bytes).ToArray();
            var expected = (Int128)new BigInteger(reversed);
            Int128 read = br.ReadInt128BigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadInt128LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = br.ReadInt128LittleEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = br.ReadUInt128();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var reversed = Enumerable.Reverse(_bytes).ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            UInt128 read = br.ReadUInt128BigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadUInt128LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = br.ReadUInt128LittleEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDecimalTest()
        {
            var stream = new MemoryStream(_decimalBytes);
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.ReadDecimal();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDecimalBigEndianTest()
        {
            var stream = new MemoryStream([.. Enumerable.Reverse(_decimalBytes)]);
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.ReadDecimalBigEndian();
            Assert.Equal(expected, read);
        }

        [Fact]
        public void ReadDecimalLittleEndianTest()
        {
            var stream = new MemoryStream(_decimalBytes);
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.ReadDecimalLittleEndian();
            Assert.Equal(expected, read);
        }

        #endregion

        #region Peek Read

        [Fact]
        public void PeekByteTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            byte read = br.PeekByte();
            Assert.Equal(0x00, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt8 read = br.PeekByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekBytesTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int length = 4;
            byte[] read = br.PeekBytes(length);
            Assert.Equal(length, read.Length);
            Assert.True(read.SequenceEqual(_bytes.Take(length)));
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekSByteTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            sbyte read = br.PeekSByte();
            Assert.Equal(0x00, read);
        }

        [Fact]
        public void PeekSByteBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt8 read = br.PeekSByteBothEndian();
            Assert.Equal(0x00, read.LittleEndian);
            Assert.Equal(0x01, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.PeekInt16();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.PeekInt16BigEndian();
            Assert.Equal(0x0001, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            short read = br.PeekInt16LittleEndian();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt16 read = br.PeekInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt16Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.PeekUInt16();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt16BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.PeekUInt16BigEndian();
            Assert.Equal(0x0001, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt16LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.PeekUInt16LittleEndian();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt16BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt16 read = br.PeekUInt16BothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.PeekWORD();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.PeekWORDBigEndian();
            Assert.Equal(0x0001, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ushort read = br.PeekWORDLittleEndian();
            Assert.Equal(0x0100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt16 read = br.PeekWORDBothEndian();
            Assert.Equal(0x0100, read.LittleEndian);
            Assert.Equal(0x0203, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekHalfTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = br.PeekHalf();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekHalfBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0001);
            Half read = br.PeekHalfBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekHalfLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Half expected = BitConverter.Int16BitsToHalf(0x0100);
            Half read = br.PeekHalfLittleEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int24 read = br.PeekInt24();
            Assert.Equal(0x020100, (int)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int24 read = br.PeekInt24BigEndian();
            Assert.Equal(0x000102, (int)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int24 read = br.PeekInt24LittleEndian();
            Assert.Equal(0x020100, (int)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt24Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt24 read = br.PeekUInt24();
            Assert.Equal((uint)0x020100, (uint)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt24BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt24 read = br.PeekUInt24BigEndian();
            Assert.Equal((uint)0x000102, (uint)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt24LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt24 read = br.PeekUInt24LittleEndian();
            Assert.Equal((uint)0x020100, (uint)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.PeekInt32();
            Assert.Equal(0x03020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.PeekInt32BigEndian();
            Assert.Equal(0x00010203, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            int read = br.PeekInt32LittleEndian();
            Assert.Equal(0x03020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt32 read = br.PeekInt32BothEndian();
            Assert.Equal(0x03020100, read.LittleEndian);
            Assert.Equal(0x04050607, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt32Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.PeekUInt32();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt32BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.PeekUInt32BigEndian();
            Assert.Equal((uint)0x00010203, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt32LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.PeekUInt32LittleEndian();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt32BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt32 read = br.PeekUInt32BothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.PeekDWORD();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.PeekDWORDBigEndian();
            Assert.Equal((uint)0x00010203, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            uint read = br.PeekDWORDLittleEndian();
            Assert.Equal((uint)0x03020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt32 read = br.PeekDWORDBothEndian();
            Assert.Equal((uint)0x03020100, read.LittleEndian);
            Assert.Equal((uint)0x04050607, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekSingleTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = br.PeekSingle();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekSingleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x00010203);
            float read = br.PeekSingleBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekSingleLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            float expected = BitConverter.Int32BitsToSingle(0x03020100);
            float read = br.PeekSingleLittleEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int48 read = br.PeekInt48();
            Assert.Equal(0x050403020100, (long)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int48 read = br.PeekInt48BigEndian();
            Assert.Equal(0x000102030405, (long)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            Int48 read = br.PeekInt48LittleEndian();
            Assert.Equal(0x050403020100, (long)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt48Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt48 read = br.PeekUInt48();
            Assert.Equal((ulong)0x050403020100, (ulong)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt48BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt48 read = br.PeekUInt48BigEndian();
            Assert.Equal((ulong)0x000102030405, (ulong)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt48LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            UInt48 read = br.PeekUInt48LittleEndian();
            Assert.Equal((ulong)0x050403020100, (ulong)read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.PeekInt64();
            Assert.Equal(0x0706050403020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.PeekInt64BigEndian();
            Assert.Equal(0x0001020304050607, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            long read = br.PeekInt64LittleEndian();
            Assert.Equal(0x0706050403020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothInt64 read = br.PeekInt64BothEndian();
            Assert.Equal(0x0706050403020100, read.LittleEndian);
            Assert.Equal(0x08090A0B0C0D0E0F, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt64Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.PeekUInt64();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt64BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.PeekUInt64BigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt64LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.PeekUInt64LittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt64BothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt64 read = br.PeekUInt64BothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekQWORDTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.PeekQWORD();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekQWORDBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.PeekQWORDBigEndian();
            Assert.Equal((ulong)0x0001020304050607, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekQWORDLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            ulong read = br.PeekQWORDLittleEndian();
            Assert.Equal((ulong)0x0706050403020100, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekQWORDBothEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            BothUInt64 read = br.PeekQWORDBothEndian();
            Assert.Equal((ulong)0x0706050403020100, read.LittleEndian);
            Assert.Equal((ulong)0x08090A0B0C0D0E0F, read.BigEndian);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDoubleTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = br.PeekDouble();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDoubleBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0001020304050607);
            double read = br.PeekDoubleBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDoubleLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            double expected = BitConverter.Int64BitsToDouble(0x0706050403020100);
            double read = br.PeekDoubleLittleEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekGuidTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_bytes);
            Guid read = br.PeekGuid();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekGuidBigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_guidBigEndianbytes);
            Guid read = br.PeekGuidBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekGuidLittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = new Guid(_bytes);
            Guid read = br.PeekGuidLittleEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = br.PeekInt128();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var reversed = Enumerable.Reverse(_bytes).ToArray();
            var expected = (Int128)new BigInteger(reversed);
            Int128 read = br.PeekInt128BigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekInt128LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (Int128)new BigInteger(_bytes);
            Int128 read = br.PeekInt128LittleEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt128Test()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = br.PeekUInt128();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt128BigEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var reversed = Enumerable.Reverse(_bytes).ToArray();
            var expected = (UInt128)new BigInteger(reversed);
            UInt128 read = br.PeekUInt128BigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekUInt128LittleEndianTest()
        {
            var stream = new MemoryStream(_bytes);
            var br = new BinaryReader(stream);
            var expected = (UInt128)new BigInteger(_bytes);
            UInt128 read = br.PeekUInt128LittleEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDecimalTest()
        {
            var stream = new MemoryStream(_decimalBytes);
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.PeekDecimal();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDecimalBigEndianTest()
        {
            var stream = new MemoryStream([.. Enumerable.Reverse(_decimalBytes)]);
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.PeekDecimalBigEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        [Fact]
        public void PeekDecimalLittleEndianTest()
        {
            var stream = new MemoryStream(_decimalBytes);
            var br = new BinaryReader(stream);
            decimal expected = 0.0123456789M;
            decimal read = br.PeekDecimalLittleEndian();
            Assert.Equal(expected, read);
            Assert.Equal(0, br.BaseStream.Position);
        }

        #endregion

        #region Try Read

        [Fact]
        public void TryReadByteArrayTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadBytes(4, out byte[] read);
            Assert.False(actual);
            Assert.Empty(read);
        }

        [Fact]
        public void TryReadByteTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadByteValue(out byte read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadByteBothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadByteBothEndian(out BothUInt8 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadBytesTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            int length = 4;
            bool actual = br.TryReadBytes(length, out byte[] read);
            Assert.False(actual);
            Assert.Empty(read);
        }

        [Fact]
        public void TryReadSByteTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadSByte(out sbyte read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadSByteBothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadSByteBothEndian(out BothInt8 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadCharTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadChar(out char read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt16(out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt16BigEndian(out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt16LittleEndian(out short read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt16BothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt16BothEndian(out BothInt16 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadUInt16Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt16(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt16BigEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt16LittleEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt16BothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt16BothEndian(out BothUInt16 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadWORDTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadWORD(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadWORDBigEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadWORDLittleEndian(out ushort read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadWORDBothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadWORDBothEndian(out BothUInt16 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadHalfTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadHalf(out Half read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadHalfBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadHalfBigEndian(out Half read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadHalfLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadHalfLittleEndian(out Half read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt24Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt24(out Int24 read);
            Assert.False(actual);
            Assert.Equal(default, (int)read);
        }

        [Fact]
        public void TryReadInt24BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt24BigEndian(out Int24 read);
            Assert.False(actual);
            Assert.Equal(default, (int)read);
        }

        [Fact]
        public void TryReadInt24LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt24LittleEndian(out Int24 read);
            Assert.False(actual);
            Assert.Equal(default, (int)read);
        }

        [Fact]
        public void TryReadUInt24Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt24(out UInt24 read);
            Assert.False(actual);
            Assert.Equal(default, (uint)read);
        }

        [Fact]
        public void TryReadUInt24BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt24BigEndian(out UInt24 read);
            Assert.False(actual);
            Assert.Equal(default, (uint)read);
        }

        [Fact]
        public void TryReadUInt24LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt24LittleEndian(out UInt24 read);
            Assert.False(actual);
            Assert.Equal(default, (uint)read);
        }

        [Fact]
        public void TryReadInt32Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt32(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt32BigEndian(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt32LittleEndian(out int read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt32BothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt32BothEndian(out BothInt32 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadUInt32Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt32(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt32BigEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt32LittleEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt32BothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt32BothEndian(out BothUInt32 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadDWORDTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDWORD(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDWORDBigEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDWORDLittleEndian(out uint read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDWORDBothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDWORDBothEndian(out BothUInt32 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadSingleTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadSingle(out float read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadSingleBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadSingleBigEndian(out float read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadSingleLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadSingleLittleEndian(out float read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt48Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt48(out Int48 read);
            Assert.False(actual);
            Assert.Equal(default, (long)read);
        }

        [Fact]
        public void TryReadInt48BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt48BigEndian(out Int48 read);
            Assert.False(actual);
            Assert.Equal(default, (long)read);
        }

        [Fact]
        public void TryReadInt48LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt48LittleEndian(out Int48 read);
            Assert.False(actual);
            Assert.Equal(default, (long)read);
        }

        [Fact]
        public void TryReadUInt48Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt48(out UInt48 read);
            Assert.False(actual);
            Assert.Equal(default, (ulong)read);
        }

        [Fact]
        public void TryReadUInt48BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt48BigEndian(out UInt48 read);
            Assert.False(actual);
            Assert.Equal(default, (ulong)read);
        }

        [Fact]
        public void TryReadUInt48LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt48LittleEndian(out UInt48 read);
            Assert.False(actual);
            Assert.Equal(default, (ulong)read);
        }

        [Fact]
        public void TryReadInt64Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt64(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt64BigEndian(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt64LittleEndian(out long read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt64BothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt64BothEndian(out BothInt64 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadUInt64Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt64(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt64BigEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt64LittleEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt64BothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt64BothEndian(out BothUInt64 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadQWORDTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadQWORD(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadQWORDBigEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadQWORDLittleEndian(out ulong read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadQWORDBothEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadQWORDBothEndian(out BothUInt64 read);
            Assert.False(actual);
            Assert.Equal(default, read.LittleEndian);
            Assert.Equal(default, read.BigEndian);
        }

        [Fact]
        public void TryReadDoubleTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDouble(out double read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDoubleBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDoubleBigEndian(out double read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDoubleLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDoubleLittleEndian(out double read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadGuidTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadGuid(out Guid read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadGuidBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadGuidBigEndian(out Guid read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadGuidLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadGuidLittleEndian(out Guid read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt128Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt128(out Int128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt128BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt128BigEndian(out Int128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadInt128LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadInt128LittleEndian(out Int128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt128Test()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt128(out UInt128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt128BigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt128BigEndian(out UInt128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadUInt128LittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadUInt128LittleEndian(out UInt128 read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDecimalTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDecimal(out decimal read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDecimalBigEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDecimalBigEndian(out decimal read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        [Fact]
        public void TryReadDecimalLittleEndianTest()
        {
            var stream = new MemoryStream([]);
            var br = new BinaryReader(stream);
            bool actual = br.TryReadDecimalLittleEndian(out decimal read);
            Assert.False(actual);
            Assert.Equal(default, read);
        }

        #endregion
    }
}
