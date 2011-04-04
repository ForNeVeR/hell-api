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
