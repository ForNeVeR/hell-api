using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Hell.LastCircle.Database;

namespace Hell.FirstCircle
{
    /// <summary>
    /// Helper class for working with database.
    /// </summary>
    public class DatabaseConnector
    {
        /// <summary>
        /// Reference to object containing various Miranda service functions.
        /// </summary>
        private PluginLink pluginLink;

        /// <summary>
        /// Object constructor.
        /// </summary>
        /// <param name="pluginLink">
        /// Reference to object containing various Miranda service functions.
        /// </param>
        public DatabaseConnector(PluginLink pluginLink)
        {
            this.pluginLink = pluginLink;
        }

        /// <summary>
        /// Returns setting from database. Setting can be: byte, short, int,
        /// string, byte[].
        /// </summary>
        /// <param name="moduleName">
        /// Name of module that wrote the setting to get.
        /// </param>
        /// <param name="settingName">
        /// Name of setting to get.
        /// </param>
        /// <returns>
        /// Object that may be casted to one of variant types.
        /// </returns>
        public object GetSetting(string moduleName, string settingName)
        {
            return GetSetting(IntPtr.Zero, moduleName, settingName);
        }

        /// <summary>
        /// Reads setting from database for specified contact. Setting can be:
        /// byte, short, int, string, byte[].
        /// </summary>
        /// <param name="contact">
        /// Reference to contact whose setting must be gotten.
        /// </param>
        /// <param name="moduleName">
        /// Name of module that wrote the setting to get.
        /// </param>
        /// <param name="settingName">
        /// Name of setting to get.
        /// </param>
        /// <returns>
        /// Object that may be casted to one of variant types.
        /// </returns>
        public object GetContactSetting(Contact contact, string moduleName,
            string settingName)
        {
            return GetSetting(contact.hContact, moduleName, settingName);
        }

        /// <summary>
        /// Writes global setting to database.
        /// </summary>
        /// <param name="moduleName">
        /// Name of module that wrote the setting to get.
        /// </param>
        /// <param name="settingName">
        /// Name of setting to get.
        /// <param name="value">
        /// Value of one of specified types: byte, ushort, short, uint, int,
        /// string, byte[].
        /// </param>
        public void SetSetting(string moduleName, string settingName,
            object value)
        {
            SetSetting(IntPtr.Zero, moduleName, settingName, value);
        }

        /// <summary>
        /// Writes setting to database.
        /// </summary>
        /// <param name="Contact">
        /// Contact whose setting must be gotten.
        /// </param>
        /// <param name="moduleName">
        /// Name of module that wrote the setting to get.
        /// </param>
        /// <param name="settingName">
        /// Name of setting to get.
        /// <param name="value">
        /// Value of one of specified types: byte, ushort, short, uint, int,
        /// string, byte[].
        /// </param>
        public void SetContactSetting(Contact contact, string moduleName,
            string settingName, object value)
        {
            SetSetting(contact.hContact, moduleName, settingName, value);
        }

        /// <summary>
        /// Reads setting from database for specified contact. Setting can be:
        /// byte, short, int, string, byte[].
        /// </summary>
        /// <param name="hContact">
        /// Handle of contact whose setting must be gotten.
        /// </param>
        /// <param name="moduleName">
        /// Name of module that wrote the setting to get.
        /// </param>
        /// <param name="settingName">
        /// Name of setting to get.
        /// </param>
        /// <returns>
        /// Object that may be casted to one of variant types.
        /// </returns>
        private object GetSetting(IntPtr hContact, string moduleName,
            string settingName)
        {
            using (var pDBContactGetSetting = new AutoPtr(Marshal.AllocHGlobal(
                Marshal.SizeOf(typeof(DBContactGetSetting)))))
            using (var pVariant = new AutoPtr(
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DBVariant)))))
            { 
                var getSetting = new DBContactGetSetting();
                getSetting.szModule = moduleName;
                getSetting.szSetting = settingName;
                getSetting.pValue = pVariant;

                Marshal.StructureToPtr(getSetting, pDBContactGetSetting,
                    false);

                IntPtr result = pluginLink.CallService("DB/Contact/GetSetting",
                    hContact, pDBContactGetSetting);

                if (result == new IntPtr(2))
                    throw new SettingDeletedException();
                if (result != IntPtr.Zero)
                    throw new DatabaseException();

                var variant = Marshal.PtrToStructure(pVariant,
                    typeof(DBVariant)) as DBVariant;
                switch (variant.type)
                {
                    case DBVariant.DBVT_DELETED:
                        throw new SettingDeletedException();
                    case DBVariant.DBVT_BYTE:
                        return variant.Value.bVal;
                    case DBVariant.DBVT_WORD:
                        return variant.Value.sVal;
                    case DBVariant.DBVT_DWORD:
                        return variant.Value.lVal;
                    case DBVariant.DBVT_ASCIIZ:
                        return Marshal.PtrToStringAnsi(variant.Value.pszVal);
                    case DBVariant.DBVT_BLOB:
                        int size = variant.Value.ByteArrayValue.cpbVal;
                        IntPtr pBlob = variant.Value.ByteArrayValue.pbVal;
                        var blob = new byte[size];
                        for (int i = 0; i < size; ++i)
                            blob[i] = Marshal.ReadByte(pBlob, i);
                        return blob;
                    case DBVariant.DBVT_UTF8:
                        throw new NotImplementedException("UTF-8 decoding " +
                            "still not implemented.");
                    case DBVariant.DBVT_WCHAR:
                        return Marshal.PtrToStringUni(variant.Value.pszVal);
                    default:
                        throw new DatabaseException();
                }
            }
        }

        /// <summary>
        /// Writes setting to database.
        /// </summary>
        /// <param name="hContact">
        /// Handle of contact whose setting must be gotten.
        /// </param>
        /// <param name="moduleName">
        /// Name of module that wrote the setting to get.
        /// </param>
        /// <param name="settingName">
        /// Name of setting to get.
        /// <param name="value">
        /// Value of one of specified types: byte, ushort, short, uint, int,
        /// string, byte[].
        /// </param>
        private void SetSetting(IntPtr hContact, string moduleName,
            string settingName, object value)
        {
            using(var pDBContactWriteSetting =
                new AutoPtr(Marshal.AllocHGlobal(
                    Marshal.SizeOf(typeof(DBContactWriteSetting)))))
            {
                var writeSetting = new DBContactWriteSetting();
                writeSetting.szModule = moduleName;
                writeSetting.szSetting = settingName;

                var variant = new DBVariant();

                // This pointer will be set if we allocate unmanaged memory to
                // store the value:
                AutoPtr valuePtr = null;
                if (value is byte)
                {
                    variant.type = DBVariant.DBVT_BYTE;
                    variant.Value.bVal = (byte)value;
                }
                else if (value is ushort)
                {
                    variant.type = DBVariant.DBVT_WORD;
                    variant.Value.wVal = (ushort)value;
                }
                else if (value is short)
                {
                    variant.type = DBVariant.DBVT_WORD;
                    variant.Value.sVal = (short)value;
                }
                else if (value is uint)
                {
                    variant.type = DBVariant.DBVT_DWORD;
                    variant.Value.dVal = (uint)value;
                }
                else if (value is int)
                {
                    variant.type = DBVariant.DBVT_DWORD;
                    variant.Value.lVal = (int)value;
                }
                else if (value is string)
                {
                    variant.type = DBVariant.DBVT_WCHAR;
                    valuePtr = new AutoPtr(
                        Marshal.StringToHGlobalUni(value as string));
                    variant.Value.pszVal = valuePtr;
                }
                else if (value is byte[])
                {
                    var blob = value as byte[];
                    valuePtr = new AutoPtr(Marshal.AllocHGlobal(blob.Length));
                    for (int i = 0; i < blob.Length; ++i)
                        Marshal.WriteByte(valuePtr, i, blob[i]);

                    variant.Value.ByteArrayValue.cpbVal = (ushort)blob.Length;
                    variant.Value.ByteArrayValue.pbVal = valuePtr;
                }
                else
                    throw new ArgumentException("Type of argument not " +
                        "supported.", "value");

                writeSetting.value = variant;

                Marshal.StructureToPtr(writeSetting, pDBContactWriteSetting,
                    false);

                IntPtr result =
                    pluginLink.CallService("DB/Contact/WriteSetting", hContact,
                    pDBContactWriteSetting);

                // Free allocated memory:
                if (valuePtr != null)
                    valuePtr.Dispose();
                
                if (result != IntPtr.Zero)
                    throw new DatabaseException();
            }
        }

        /// <summary>
        /// Reads all setting names from database ofspecified module.
        /// </summary>
        /// <param name="moduleName">
        /// Name of module that wrote the settings to get.
        /// </param>
        /// <returns>
        /// Array with setting names.
        /// </returns>
        public string[] EnumSettings(string moduleName)
        {
            return EnumContactSettings(IntPtr.Zero, moduleName);
        }

        /// <summary>
        /// Reads all setting names from database for spedified contact of
        /// specified module.
        /// </summary>
        /// <param name="contact">
        /// Contact whose settings must be gotten.
        /// </param>
        /// <param name="moduleName">
        /// Name of module that wrote the settings to get.
        /// </param>
        /// <returns>
        /// Array with setting names.
        /// </returns>
        public string[] EnumContactSettings(Contact contact, string moduleName)
        {
            return EnumContactSettings(contact.hContact, moduleName);
        }

        /// <summary>
        /// Reads all setting names from database for spedified contact of
        /// specified module.
        /// </summary>
        /// <param name="hContact">
        /// Handle of contact whose settings must be gotten.
        /// </param>
        /// <param name="moduleName">
        /// Name of module that wrote the settings to get.
        /// </param>
        /// <returns>
        /// Array with setting names.
        /// </returns>
        private string[] EnumContactSettings(IntPtr hContact,
            string moduleName)
        {
            var result = new List<string>();

            using (var pDBContactEnumSettings =
                new AutoPtr(Marshal.AllocHGlobal(
                    Marshal.SizeOf(typeof(DBContactEnumSettings)))))
            {
                var enumSettings = new DBContactEnumSettings();
                enumSettings.pfnEnumProc = (settingName, _) =>
                    {
                        result.Add(settingName);
                        return 0;
                    };
                enumSettings.szModule = moduleName;

                Marshal.StructureToPtr(enumSettings, pDBContactEnumSettings,
                    false);
                pluginLink.CallService("DB/Contact/EnumSettings", hContact,
                    pDBContactEnumSettings);
            }

            return result.ToArray();
        }
    }

    public class DatabaseException : Exception { }
    public class SettingDeletedException : DatabaseException { }
}
