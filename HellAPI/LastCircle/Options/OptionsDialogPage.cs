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

namespace Hell.LastCircle.Options
{
    /// <summary>
    /// Mapping of C structure OPTIONSDIALOGPAGE from m_options.h header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public class OptionsDialogPage
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate IntPtr DlgProc(IntPtr hDlg, uint message, 
            IntPtr wParam, IntPtr hParam);
        
        private int cbSize = Marshal.SizeOf(typeof(OptionsDialogPage));
        public int position;
        public string pszTitle;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public DlgProc pfnDlgProc;
        public IntPtr pszTemplate;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public string pszGroup;
        public int groupPosition;
        public IntPtr hGroupIcon;
        public int flags;
        public string tab;
        public IntPtr dwInitParam;
	    public int hLangpack;
    }
}
