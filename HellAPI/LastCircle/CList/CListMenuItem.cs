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
using System.Runtime.InteropServices;

namespace Hell.LastCircle.CList
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public class CListMenuItem
    {
        int size = Marshal.SizeOf(typeof(CListMenuItem));
        public string name;
        public int flags;
        public int position;
        IntPtr icon;
        public string service;
        string popupName;
        int popupPosition;
        int hotKey;
        string contactOwner;
    }
/* TODO: More precisely implement this:
typedef struct {
	int cbSize;	            //size in bytes of this structure
	union {
      char*  pszName;      //[TRANSLATED-BY-CORE] text of the menu item
		TCHAR* ptszName;     //Unicode text of the menu item
	};
	DWORD flags;            //set of CMIF_* flags
	int position;           //approx position on the menu. lower numbers go nearer the top
	union {
		HICON hIcon;         //icon to put by the item. If this was not loaded from
                           //a resource, you can delete it straight after the call
		HANDLE icolibItem;   //set CMIF_ICONFROMICOLIB to pass this value
	};
	char* pszService;       //name of service to call when the item gets selected
	union {
		char* pszPopupName;  //[TRANSLATED-BY-CORE] name of the popup menu that this item is on. If this
		TCHAR* ptszPopupName; //is NULL the item is on the root of the menu
		HGENMENU hParentMenu; // valid if CMIF_ROOTHANDLE is set. NULL or (HGENMENU)-1 means the root menu
	};

	int popupPosition;      //position of the popup menu on the root menu. Ignored
                           //if pszPopupName is NULL or the popup menu already
                           //existed
	DWORD hotKey;           //keyboard accelerator, same as lParam of WM_HOTKEY,0 for none
	char *pszContactOwner;  //contact menus only. The protocol module that owns
                           //the contacts to which this menu item applies. NULL if it
                           //applies to all contacts. If it applies to multiple but not all
                           //protocols, add multiple menu items or use ME_CLIST_PREBUILDCONTACTMENU
} CLISTMENUITEM;
*/
}
