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
    /// DBVARIANT structure. May contain value of one of various types.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DBVariant
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ByteArray
        {
			public ushort cpbVal;
			public IntPtr pbVal;
		}
        
        [StructLayout(LayoutKind.Explicit)]
        public struct Variant
	    {
            [FieldOffset(0)]
		    public byte bVal;
            [FieldOffset(0)]
            public byte cVal;
            [FieldOffset(0)]
		    public ushort wVal;
            [FieldOffset(0)]
            public short sVal;
            [FieldOffset(0)]
		    public uint dVal;
            [FieldOffset(0)]
            public int lVal;
		    
            /// <summary>
            /// May be psz, ptsz, pwsz.
            /// </summary>
            [FieldOffset(0)]
            public IntPtr pszVal;
            
            /// <summary>
            /// only used for db/contact/getsettingstatic
            /// </summary>
            [FieldOffset(0)]
			public ushort cchVal;

            [FieldOffset(0)]
            public ByteArray ByteArrayValue;
		}        

        public byte type;
        public Variant Value;   
     
        /// <summary>
        /// this setting just got deleted, no other values are valid
        /// </summary>
        public const byte DBVT_DELETED = 0;

        /// <summary>
        /// bVal and cVal are valid
        /// </summary>
        public const byte DBVT_BYTE = 1;

        /// <summary>
        /// wVal and sVal are valid
        /// </summary>
        public const byte DBVT_WORD = 2;

        /// <summary>
        /// dVal and lVal are valid
        /// </summary>
        public const byte DBVT_DWORD = 4;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const byte DBVT_ASCIIZ = 255;

        /// <summary>
        /// cpbVal and pbVal are valid
        /// </summary>
        public const byte DBVT_BLOB = 254;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const byte DBVT_UTF8 = 253;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const byte DBVT_WCHAR = 252;
	}
}
