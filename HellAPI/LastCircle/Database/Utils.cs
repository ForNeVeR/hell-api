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
