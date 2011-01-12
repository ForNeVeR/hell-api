using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Hell.CList
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
