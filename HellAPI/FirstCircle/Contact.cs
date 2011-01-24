using System;
using System.Runtime.InteropServices;
using Hell.LastCircle.Contacts;
using Hell.LastCircle.Database;

namespace Hell.FirstCircle
{
    /// <summary>
    /// Class representing Miranda contact.
    /// </summary>
    public class Contact
    {
        internal IntPtr hContact;
        private PluginLink pluginLink;

        /// <summary>
        /// Object constructor.
        /// </summary>
        /// <param name="hContact">
        /// Handle used for various Miranda contact manipulations.
        /// </param>
        public Contact(IntPtr hContact, PluginLink pluginLink)
        {
            this.hContact = hContact;
            this.pluginLink = pluginLink;
        }

        /// <summary>
        /// Nickname of contact from database.
        /// </summary>
        public string Nickname
        {
            get
            {
                // Create ContactInfo object:
                var info = new ContactInfo();
                info.hContact = hContact;
                info.dwFlag = ContactInfo.CNF_NICK;

                // Copy ContactInfo to unmanaged memory:
                IntPtr pContactInfo =
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ContactInfo)));
                Marshal.StructureToPtr(info, pContactInfo, false);

                IntPtr result =
                    pluginLink.CallService("Miranda/Contact/GetContactInfo",
                        IntPtr.Zero, pContactInfo);

                string nick = null;
                if (result == IntPtr.Zero)
                {
                    info = (ContactInfo)Marshal.PtrToStructure(pContactInfo,
                        typeof(ContactInfo));
                    nick = Marshal.PtrToStringAnsi(info.value.pszVal);
                }

                Marshal.FreeHGlobal(pContactInfo);

                return nick;
            }
        }

        /// <summary>
        /// Internal protocol name of contact from database.
        /// </summary>
        public string Protocol
        {
            get
            {
                // Create ContactInfo object:
                var info = new ContactInfo();
                info.hContact = hContact;

                // Copy ContactInfo to unmanaged memory:
                IntPtr pContactInfo =
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ContactInfo)));
                Marshal.StructureToPtr(info, pContactInfo, false);

                IntPtr result =
                    pluginLink.CallService("Miranda/Contact/GetContactInfo",
                        IntPtr.Zero, pContactInfo);

                info = (ContactInfo)Marshal.PtrToStructure(pContactInfo,
                    typeof(ContactInfo));
                
                Marshal.FreeHGlobal(pContactInfo);

                return info.szProto;
            }
        }

        /// <summary>
        /// Sends simple message to contact.
        /// </summary>
        /// <param name="message">
        /// Message string to send.
        /// </param>
        public void SendMessage(string message)
        {
            var eventInfo = new DBEventInfo();
            eventInfo.eventType = DBEventInfo.EVENTTYPE_MESSAGE;
            eventInfo.flags = DBEventInfo.DBEF_SENT;
            eventInfo.szModule = Protocol;
            eventInfo.timestamp = (int)(DateTime.Now.ToUniversalTime() -
                new DateTime(1970, 1, 1)).TotalSeconds;
            
            IntPtr pString = Marshal.StringToHGlobalAnsi(message);
            eventInfo.cbBlob = (uint)message.Length + 1;
            eventInfo.pBlob = pString;

            IntPtr pDBEventInfo =
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DBEventInfo)));
            Marshal.StructureToPtr(eventInfo, pDBEventInfo, false);

            pluginLink.CallService("DB/Event/Add", hContact, pDBEventInfo);
            pluginLink.CallContactService(hContact, "/SendMsg", IntPtr.Zero,
                pString);
            
            Marshal.FreeHGlobal(pString);
            Marshal.FreeHGlobal(pDBEventInfo);
        }

        public static bool operator ==(Contact c1, Contact c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Contact c1, Contact c2)
        {
            return !c1.Equals(c2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Contact)
                return hContact == (obj as Contact).hContact;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return hContact.GetHashCode();
        }
    }
}
