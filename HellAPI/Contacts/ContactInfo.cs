using System;
using System.Runtime.InteropServices;

namespace Hell.Contacts
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public class ContactInfo
    {
        private int cbSize = Marshal.SizeOf(typeof(ContactInfo));
	    public byte dwFlag;
	    public IntPtr hContact;
	    public string szProto;
	    public byte type;
        
        [StructLayout(LayoutKind.Explicit)]
        public struct Value
        {
            [FieldOffset(0)]
	        public byte bVal;
            [FieldOffset(0)]
	        public short wVal;
            [FieldOffset(0)]
	        public int dVal;
            // Pointer to string.
            [FieldOffset(0)]
	        public IntPtr pszVal;
            [FieldOffset(0)]
	        public int cchVal;
        }

        public Value value;

        public string GetValueAsString()
        {
            return Marshal.PtrToStringAnsi(value.pszVal);
        }
    }
}
