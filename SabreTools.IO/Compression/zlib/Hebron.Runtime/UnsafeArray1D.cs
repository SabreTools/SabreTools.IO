using System;
using System.Runtime.InteropServices;

namespace SabreTools.IO.Compression.zlib
{
	public unsafe class UnsafeArray1D<T> where T : struct
	{
		private readonly T[] _data;
		private readonly GCHandle _pinHandle;
		public bool IsFreed { get; private set; }

		internal GCHandle PinHandle => _pinHandle;

		public T this[int index]
		{
			get => _data[index];
			set
			{
				_data[index] = value;
			}
		}

		public T this[uint index]
		{
			get => _data[index];
			set
			{
				_data[index] = value;
			}
		}

		public T[] Data => _data;

		public UnsafeArray1D(int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(size));
			}

			_data = new T[size];
			_pinHandle = GCHandle.Alloc(_data, GCHandleType.Pinned);
			IsFreed = false;
		}

		public UnsafeArray1D(T[] data, int sizeOf)
		{
			if (sizeOf <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(sizeOf));
			}

			_data = data ?? throw new ArgumentNullException(nameof(data));
			_pinHandle = GCHandle.Alloc(_data, GCHandleType.Pinned);
			IsFreed = false;
		}

		public void Free()
		{
			if (!IsFreed)
			{
				_pinHandle.Free();
				IsFreed = true;
			}
		}

		~UnsafeArray1D()
		{
			if (!IsFreed)
				_pinHandle.Free();
		}

		public void* ToPointer()
		{
			return _pinHandle.AddrOfPinnedObject().ToPointer();
		}

		public static implicit operator void*(UnsafeArray1D<T> array)
		{
			return array.ToPointer();
		}

		public static void* operator +(UnsafeArray1D<T> array, int delta)
		{
			return array.ToPointer();
		}
	}
}