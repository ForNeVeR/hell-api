using System;
using System.Runtime.InteropServices;

namespace Hell.LastCircle.Database
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public class DBContactEnumSettings
    {
        public delegate int DBSettingsEnumProcDelegate(
            [MarshalAs(UnmanagedType.LPStr)] string szSetting, IntPtr lParam);
        
        /// <summary>
        /// Will be called once for every setting.
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public DBSettingsEnumProcDelegate pfnEnumProc;
        
        /// <summary>
        /// passed direct to pfnEnumProc
        /// </summary>
        public IntPtr lParam;

        /// <summary>
        /// name of the module to get settings for
        /// </summary>
        public string szModule;

        /// <summary>
        /// filled by the function to contain the offset from the start of the
        /// database of the requested settings group.
        /// </summary>
        public uint ofsSettings;
    }
}
