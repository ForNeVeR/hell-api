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

namespace Hell.LastCircle.Contacts
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
            
            /// <summary>
            /// May be char * or wchar_t *.
            /// </summary>
            [FieldOffset(0)]
	        public IntPtr pszVal;

            [FieldOffset(0)]
	        public int cchVal;
        }

        public Value value;

        /// <summary>
        /// returns first name (string)
        /// </summary>
        public const byte CNF_FIRSTNAME = 1;

        /// <summary>
        /// returns last name (string)
        /// </summary>
        public const byte CNF_LASTNAME = 2;

        /// <summary>
        /// returns nick name (string)
        /// </summary>
        public const byte CNF_NICK = 3;

        /// <summary>
        /// returns custom nick name, clist name (string)
        /// </summary>
        public const byte CNF_CUSTOMNICK = 4;

        /// <summary>
        /// returns email (string)
        /// </summary>
        public const byte CNF_EMAIL = 5;

        /// <summary>
        /// returns city (string)
        /// </summary>
        public const byte CNF_CITY = 6;

        /// <summary>
        /// returns state (string)
        /// </summary>
        public const byte CNF_STATE = 7;

        /// <summary>
        /// returns country (string)
        /// </summary>
        public const byte CNF_COUNTRY = 8;

        /// <summary>
        /// returns phone (string)
        /// </summary>
        public const byte CNF_PHONE = 9;

        /// <summary>
        /// returns homepage (string)
        /// </summary>
        public const byte CNF_HOMEPAGE = 10;

        /// <summary>
        /// returns about info (string)
        /// </summary>
        public const byte CNF_ABOUT = 11;

        /// <summary>
        /// returns gender (byte,'M','F' character)
        /// </summary>
        public const byte CNF_GENDER = 12;

        /// <summary>
        /// returns age (byte, 0==unspecified)
        /// </summary>
        public const byte CNF_AGE = 13;

        /// <summary>
        /// returns first name + last name (string)
        /// </summary>
        public const byte CNF_FIRSTLAST = 14;

        /// <summary>
        /// returns uniqueid, protocol username (must check type for type of
        /// return)
        /// </summary>
        public const byte CNF_UNIQUEID = 15;

        /// <summary>
        /// returns fax (string)
        /// </summary>
        public const byte CNF_FAX = 18;

        /// <summary>
        /// returns cellular (string)
        /// </summary>
        public const byte CNF_CELLULAR = 19;

        /// <summary>
        /// returns timezone (string)
        /// </summary>
        public const byte CNF_TIMEZONE = 20;

        /// <summary>
        /// returns user specified notes (string)
        /// </summary>
        public const byte CNF_MYNOTES = 21;

        /// <summary>
        /// returns birthday day of month (byte)
        /// </summary>
        public const byte CNF_BIRTHDAY = 22;

        /// <summary>
        /// returns birthday month (byte)
        /// </summary>
        public const byte CNF_BIRTHMONTH = 23;

        /// <summary>
        /// returns birthday year (word)
        /// </summary>
        public const byte CNF_BIRTHYEAR = 24;

        /// <summary>
        /// returns street (string)
        /// </summary>
        public const byte CNF_STREET = 25;

        /// <summary>
        /// returns zip code (string)
        /// </summary>
        public const byte CNF_ZIP = 26;

        /// <summary>
        /// returns language1 (string)
        /// </summary>
        public const byte CNF_LANGUAGE1 = 27;

        /// <summary>
        /// returns language2 (string)
        /// </summary>
        public const byte CNF_LANGUAGE2 = 28;

        /// <summary>
        /// returns language3 (string)
        /// </summary>
        public const byte CNF_LANGUAGE3 = 29;

        /// <summary>
        /// returns company name (string)
        /// </summary>
        public const byte CNF_CONAME = 30;

        /// <summary>
        /// returns company department (string)
        /// </summary>
        public const byte CNF_CODEPT = 31;

        /// <summary>
        /// returns company position (string)
        /// </summary>
        public const byte CNF_COPOSITION = 32;

        /// <summary>
        /// returns company street (string)
        /// </summary>
        public const byte CNF_COSTREET = 33;

        /// <summary>
        /// returns company city (string)
        /// </summary>
        public const byte CNF_COCITY = 34;

        /// <summary>
        /// returns company state (string)
        /// </summary>
        public const byte CNF_COSTATE = 35;

        /// <summary>
        /// returns company zip code (string)
        /// </summary>
        public const byte CNF_COZIP = 36;

        /// <summary>
        /// returns company country (string)
        /// </summary>
        public const byte CNF_COCOUNTRY = 37;

        /// <summary>
        /// returns company homepage (string)
        /// </summary>
        public const byte CNF_COHOMEPAGE = 38;

        /// <summary>
        /// returns uniqueid to display in interface (must check type for type
        /// of return)
        /// </summary>
        public const byte CNF_DISPLAYUID = 39;
    }
}
