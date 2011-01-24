using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hell.LastCircle.Database
{
    /// <summary>
    /// Various constants for database layer. Mainly for services
    /// db/contact/getsetting and db/contact/writesetting.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// this setting just got deleted, no other values are valid
        /// </summary>
        public const int DBVT_DELETED = 0;

        /// <summary>
        /// bVal and cVal are valid
        /// </summary>
        public const int DBVT_BYTE = 1;

        /// <summary>
        /// wVal and sVal are valid
        /// </summary>
        public const int DBVT_WORD = 2;

        /// <summary>
        /// dVal and lVal are valid
        /// </summary>
        public const int DBVT_DWORD = 4;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const int DBVT_ASCIIZ = 255;

        /// <summary>
        /// cpbVal and pbVal are valid
        /// </summary>
        public const int DBVT_BLOB = 254;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const int DBVT_UTF8 = 253;

        /// <summary>
        /// pszVal is valid
        /// </summary>
        public const int DBVT_WCHAR = 252;
    }
}
