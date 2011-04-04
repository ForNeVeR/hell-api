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

using System.Runtime.InteropServices;

namespace Hell.LastCircle.Database
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    class DBContactWriteSetting
    {
        /// <summary>
        /// name of the module that wrote the setting to set
        /// </summary>
        public string szModule;
        
        /// <summary>
        /// name of the setting to set
        /// </summary>
        public string szSetting;

        /// <summary>
        /// variant containing the value to set
        /// </summary>
        public DBVariant value;
    }
}
