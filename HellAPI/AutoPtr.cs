using System;
using System.Runtime.InteropServices;

namespace Hell
{
	/// <summary>
	/// Automatic pointer class. Use it for storing pointer to memory
	/// allocated by Marshal.*HGlobal methods.
	/// </summary>
	public class AutoPtr : IDisposable
	{
		/// <summary>
		/// Associated pointer.
		/// </summary>
		private IntPtr _ptr;

		/// <summary>
		/// Pointer status indicator.
		/// </summary>
		private bool _disposed = false;

		/// <summary>
		/// AutoPtr constructor.
		/// </summary>
		/// <param name="pointer">
		/// Pointer to unmanaged memory that must be freed on AutoPtr
		/// disposion.
		/// </param>
		public AutoPtr(IntPtr pointer)
		{
			_ptr = pointer;
		}

		/// <summary>
		/// Cast operator to IntPtr type.
		/// </summary>
		/// <param name="a">
		/// Object to be casted.
		/// </param>
		/// <returns>
		/// Associated IntPtr pointer.
		/// </returns>
		public static implicit operator IntPtr(AutoPtr a)
		{
			if (a._disposed)
				throw new ObjectDisposedException("a");

			return a._ptr;
		}

		/// <summary>
		/// Recycles object. Frees associated pointer.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Private implementation of IDisposable pattern.
		/// </summary>
		/// <param name="disposing">
		/// Variable indicates source of this method call: was it called by
		/// Dispose() method or by finalizer.
		/// </param>
		private void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (_ptr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(_ptr);
				_ptr = IntPtr.Zero;
			}
			_disposed = true;
		}

		/// <summary>
		/// AutoPtr finalizer.
		/// </summary>
		~AutoPtr()
		{
			Dispose(false);
		}
	}
}