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
