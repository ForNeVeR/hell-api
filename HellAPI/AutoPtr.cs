/*
 * Copyright (C) 2010-2011 by ForNeVeR
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

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
