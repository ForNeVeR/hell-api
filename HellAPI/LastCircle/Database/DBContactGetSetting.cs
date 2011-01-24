using System;
using System.Runtime.InteropServices;

namespace Hell.LastCircle.Database
{
    /// <summary>
    /// DBCONTACTGETSETTING structure. Used for retrieving DB settings.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public class DBContactGetSetting
    {
        /// <summary>
        /// name of the module that wrote the setting to get
        /// </summary>
        public string szModule;
        
        /// <summary>
        /// name of the setting to get
        /// </summary>
        public string szSetting;

        /// <summary>
        /// Pointer to unmanaged DBVariant structure that will receive the
        /// value.
        /// </summary>
        public IntPtr pValue;
    }
}
