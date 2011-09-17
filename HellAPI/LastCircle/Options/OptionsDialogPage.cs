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
    /// Mapping of C structore OPTIONSDIALOGPAGE from m_options.h header.
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
        public int nIDBottomSimpleControl;
        public int nIDRightSimpleControl;
        public IntPtr expertOnlyControls;
        public int nExpertOnlyControls;
        public string tab;
        public IntPtr dwInitParam;
/* TODO: Implement this:
typedef struct {
	int cbSize;
	int position;        //a position number, lower numbers are topmost
	union {
		char* pszTitle; // [TRANSLATED-BY-CORE]
		TCHAR* ptszTitle;
	};
	DLGPROC pfnDlgProc;
	char *pszTemplate;
	HINSTANCE hInstance;
	HICON hIcon;		 //v0.1.0.1+
	union {
		char* pszGroup;		 //v0.1.0.1+ [TRANSLATED-BY-CORE]
		TCHAR* ptszGroup;		 //v0.1.0.1+
	};
	int groupPosition;	 //v0.1.0.1+
	HICON hGroupIcon;	 //v0.1.0.1+
	DWORD flags;         //v0.1.2.1+
	int nIDBottomSimpleControl;  //v0.1.2.1+  if in simple mode the dlg will be cut off after this control, 0 to disable
	int nIDRightSimpleControl;  //v0.1.2.1+  if in simple mode the dlg will be cut off after this control, 0 to disable
	UINT *expertOnlyControls;
	int nExpertOnlyControls;    //v0.1.2.1+  these controls will be hidden in simple mode. Array must remain valid for duration of dlg.

	#if MIRANDA_VER >= 0x0600
	union {
			char* pszTab;		 //v0.6.0.0+ [TRANSLATED-BY-CORE]
			TCHAR* ptszTab;		 //v0.6.0.0+
		};
	#endif

	#if MIRANDA_VER >= 0x0800
		LPARAM dwInitParam;	 //v0.8.0.0+  a value to pass to lParam of WM_INITDIALOG message
	#endif
}
	OPTIONSDIALOGPAGE;
*/
    }
}
