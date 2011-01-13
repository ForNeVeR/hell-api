using System;
using Hell;
using Hell.Contacts;
using System.Runtime.InteropServices;

namespace Historian
{
    /// <summary>
    /// A plugin for history statistics viewing.
    /// </summary>
    [MirandaPlugin]
    public class Historian : Plugin
    {
        private PluginLink pluginLink;

        /// <summary>
        /// Object constructor.
        /// </summary>
        /// <param name="hInstance">
        /// Pointer to main Hell library instance.
        /// </param>
        public Historian(IntPtr hInstance)
            : base(hInstance)
        {

        }

        /// <summary>
        /// Method called on plugin loading.
        /// </summary>
        /// <param name="pluginLink">
        /// Object contains various delegates for API methods.
        /// </param>
        protected override void Load(PluginLink pluginLink)
        {
            this.pluginLink = pluginLink;

            EnumerateContacts();
        }

        /// <summary>
        /// Method called on plugin unloading.
        /// </summary>
        public override void Unload()
        {
            
        }

        private unsafe void EnumerateContacts()
        {
            IntPtr hContact = pluginLink.CallService("DB/Contact/FindFirst",
                IntPtr.Zero, IntPtr.Zero);
            while (hContact != IntPtr.Zero)
            {
                // Create ContactInfo object:
                var info = new ContactInfo();
                info.hContact = hContact;
                info.szProto = "J1";
                info.dwFlag = 3;
                
                // Copy ContactInfo to unmanaged memory:
                IntPtr pContactInfo = 
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ContactInfo)));
                Marshal.StructureToPtr(info, pContactInfo, false);

                IntPtr result =
                    pluginLink.CallService("Miranda/Contact/GetContactInfo",
                        IntPtr.Zero, pContactInfo);

                if (result == IntPtr.Zero)
                {
                    info = (ContactInfo)Marshal.PtrToStructure(pContactInfo,
                        typeof(ContactInfo));
                    string nick = info.GetValueAsString();
                }

                Marshal.DestroyStructure(pContactInfo, typeof(ContactInfo));

                hContact = pluginLink.CallService("DB/Contact/FindNext",
                    hContact, IntPtr.Zero);
            }
        }
    }
}
