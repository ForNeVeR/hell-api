/*
 * Copyright 2010-2011 ForNeVeR.
 *
 * This file is part of Hell API.
 *
 * Hell API is free software: you can redistribute it and/or modify it under
 * the terms of the GNU Lesser General Public License as published by the Free
 * Software Foundation, either version 3 of the License, or (at your option)
 * any later version.
 *
 * Hell API is distributed in the hope that it will be useful, but WITHOUT ANY
 * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
 * details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Hell API. If not, see <http://www.gnu.org/licenses/>.
 */

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

                string nick = null;
                
                using (var pContactInfo = new AutoPtr(
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ContactInfo)))))
                {
                    // Copy ContactInfo to unmanaged memory:
                    Marshal.StructureToPtr(info, pContactInfo, false);

                    IntPtr result =
                        pluginLink.CallService("Miranda/Contact/GetContactInfo",
                            IntPtr.Zero, pContactInfo);

                    if (result == IntPtr.Zero)
                    {
                        info = (ContactInfo)Marshal.PtrToStructure(pContactInfo,
                            typeof(ContactInfo));
                        nick = Marshal.PtrToStringAnsi(info.value.pszVal);
                    }
                }

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
                using (var pContactInfo = new AutoPtr(
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ContactInfo)))))
                {
                    Marshal.StructureToPtr(info, pContactInfo, false);

                    IntPtr result = pluginLink.CallService(
                        "Miranda/Contact/GetContactInfo", IntPtr.Zero,
                        pContactInfo);

                    info = (ContactInfo)Marshal.PtrToStructure(pContactInfo,
                        typeof(ContactInfo));
                }

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
            
            using (var pString =
                new AutoPtr(Marshal.StringToHGlobalAnsi(message)))
            using (var pDBEventInfo = new AutoPtr(
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DBEventInfo)))))
            {
                eventInfo.cbBlob = (uint)message.Length + 1;
                eventInfo.pBlob = pString;

                Marshal.StructureToPtr(eventInfo, pDBEventInfo, false);

                pluginLink.CallService("DB/Event/Add", hContact, pDBEventInfo);
                pluginLink.CallContactService(hContact, "/SendMsg", IntPtr.Zero,
                    pString);
            }
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
