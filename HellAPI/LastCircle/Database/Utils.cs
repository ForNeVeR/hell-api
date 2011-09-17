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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hell.LastCircle.Database
{
    /// <summary>
    /// Various constants for database layer. Mainly for services
    /// db/contact/getsetting and db/contact/writesetting.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// this setting just got deleted, no other values are valid
        /// </summary>
        public const int DBVT_DELETED = 0;

        /// <summary>
        /// bVal and cVal are valid
        /// </summary>
        public const int DBVT_BYTE = 1;

        /// <summary>
        /// wVal and sVal are valid
        /// </summary>
        public const int DBVT_WORD = 2;

        /// <summary>
        /// dVal and lVal are valid
        /// </summary>
        public const int DBVT_DWORD = 4;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const int DBVT_ASCIIZ = 255;

        /// <summary>
        /// cpbVal and pbVal are valid
        /// </summary>
        public const int DBVT_BLOB = 254;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const int DBVT_UTF8 = 253;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const int DBVT_WCHAR = 252;
    }
}
