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
