/*
 * Copyright 2010-2011 ForNeVeR.
 *
 * This file is part of Hell API.
 *
 * Hell API is free software: you can redistribute it and/or modify it under
 * the terms of the GNU Lesser General Public License as published by the Free
 * Software Foundation, either version 3 of the License, or (at your option)
 * any later version.
 *
 * Hell API is distributed in the hope that it will be useful, but WITHOUT ANY
 * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
 * details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Hell API. If not, see <http://www.gnu.org/licenses/>.
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
