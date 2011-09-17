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

namespace Hell.LastCircle.Database
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public class DBContactEnumSettings
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DBSettingsEnumProcDelegate(
            [MarshalAs(UnmanagedType.LPStr)] string szSetting, IntPtr lParam);
        
        /// <summary>
        /// Will be called once for every setting.
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public DBSettingsEnumProcDelegate pfnEnumProc;
        
        /// <summary>
        /// passed direct to pfnEnumProc
        /// </summary>
        public IntPtr lParam;

        /// <summary>
        /// name of the module to get settings for
        /// </summary>
        public string szModule;

        /// <summary>
        /// filled by the function to contain the offset from the start of the
        /// database of the requested settings group.
        /// </summary>
        public uint ofsSettings;
    }
}
