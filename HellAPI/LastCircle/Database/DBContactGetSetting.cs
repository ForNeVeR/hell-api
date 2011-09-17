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
    /// <summary>
    /// DBCONTACTGETSETTING structure. Used for retrieving DB settings.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public class DBContactGetSetting
    {
        /// <summary>
        /// name of the module that wrote the setting to get
        /// </summary>
        public string szModule;
        
        /// <summary>
        /// name of the setting to get
        /// </summary>
        public string szSetting;

        /// <summary>
        /// Pointer to unmanaged DBVariant structure that will receive the
        /// value.
        /// </summary>
        public IntPtr pValue;
    }
}
