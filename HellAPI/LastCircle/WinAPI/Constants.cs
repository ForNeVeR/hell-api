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

namespace Hell.LastCircle.WinAPI
{
    /// <summary>
    /// Class containing various WinAPI constants.
    /// </summary>
    public static class Constants
    {
        public const int WM_INITDIALOG = 0x0110;
        public const int WM_NOTIFY = 0x004E;

        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000; 

        private static readonly uint PSN_FIRST;
        public static readonly uint PSN_APPLY;
        public static readonly uint PSN_RESET;

        static Constants()
        {
            unchecked
            {
                PSN_FIRST = (uint)-200;
                PSN_APPLY = PSN_FIRST - 2;
                PSN_RESET = PSN_FIRST - 3;
            }
        }

        private static uint WM_USER = 0x400;
        public static uint PSM_CHANGED = WM_USER + 104;
    }
}
