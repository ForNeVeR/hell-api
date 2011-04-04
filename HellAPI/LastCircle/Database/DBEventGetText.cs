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

namespace Hell.LastCircle.Database
{
    /// <summary>
    /// DBEVENTGETTEXT structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DBEventGetText
    {
        /// <summary>
        /// egt->dbei should be the valid database event read via
        /// MS_DB_EVENT_GET
        /// </summary>
	    public IntPtr dbei;

        /// <summary>
        /// egt->datatype = DBVT_WCHAR or DBVT_ASCIIZ or DBVT_TCHAR. If a
        /// caller wants to suppress Unicode part of event in answer, add
        /// DBVTF_DENYUNICODE to this field.
        /// </summary>
	    public int datatype;

        /// <summary>
        /// egt->codepage is any valid codepage, CP_ACP by default.
        /// </summary>
	    public int codepage;
    }
}
