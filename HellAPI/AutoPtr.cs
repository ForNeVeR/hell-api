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
        private IntPtr ptr;

        /// <summary>
        /// Pointer status indicator.
        /// </summary>
        private bool disposed = false;
        
        /// <summary>
        /// AutoPtr constructor.
        /// </summary>
        /// <param name="pointer">
        /// Pointer to unmanaged memory that must be freed on AutoPtr
        /// disposion.
        /// </param>
        public AutoPtr(IntPtr pointer)
        {
            ptr = pointer;
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
            return a.ptr;
        }

        /// <summary>
        /// Frees associated pointer.
        /// </summary>
        public void Dispose()
        {
            disposed = true;
            if (ptr != IntPtr.Zero)
                Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// AutoPtr destructor.
        /// </summary>
        ~AutoPtr()
        {
            if (!disposed)
                Dispose();
        }
    }
}
