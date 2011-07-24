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
using System.Collections.Generic;

namespace Hell.FirstCircle
{
    /// <summary>
    /// Class representing Miranda contact.
    /// </summary>
    public class Contact : IDisposable
    {
        public enum Status
        {
            Offline = 40071,
            Online = 40072,
            Away = 40073,
            DoNotDisturb = 40074,
            NA = 40075,
            Occupied = 40076,
            FreeChat = 40077,
            Invisible = 40078,
            OnThePhone = 40079,
            OutToLunch = 40080
        }

        public delegate void StatusChangedEventHandler(Contact sender, 
            Status newStatus);
        public event StatusChangedEventHandler StatusChangedEvent;
        
        #region Static methods

        /// <summary>
        /// Returns full list of contacts stored in the database.
        /// </summary>
        public static IEnumerable<Contact> Enumerate(PluginLink pluginLink)
        {
            var result = new List<Contact>();
            IntPtr hContact = pluginLink.CallService("DB/Contact/FindFirst",
                IntPtr.Zero, IntPtr.Zero);
            while (hContact != IntPtr.Zero)
            {
                result.Add(new Contact(hContact, pluginLink));
                hContact = pluginLink.CallService("DB/Contact/FindNext",
                    hContact, IntPtr.Zero);
            }

            return result;
        }

        #endregion

        #region Data fields and properties

        internal IntPtr hContact;
        private PluginLink pluginLink;
        private MirandaHook contactSettingChangedHook;
        private IntPtr hContactSettingChangedHook;

        private bool disposed;

        /// <summary>
        /// Nickname of contact from database.
        /// </summary>
        public string Nickname
        {
            get
            {
                
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
        /// Unique user Id.
        /// </summary>
        public string Uid
        {
            get
            {
                // TODO: Extract this code (and code of Nickname property) to
                // some private method.

                // Create ContactInfo object:
                var info = new ContactInfo();
                info.hContact = hContact;
                info.dwFlag = ContactInfo.CNF_UNIQUEID;

                string uid = null;

                using (var pContactInfo = new AutoPtr(
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof (ContactInfo)))))
                {
                    // Copy ContactInfo to unmanaged memory:
                    Marshal.StructureToPtr(info, pContactInfo, false);

                    IntPtr result =
                        pluginLink.CallService("Miranda/Contact/GetContactInfo",
                                               IntPtr.Zero, pContactInfo);

                    if (result == IntPtr.Zero)
                    {
                        info =
                            (ContactInfo) Marshal.PtrToStructure(pContactInfo,
                                                                 typeof (
                                                                     ContactInfo
                                                                     ));
                        uid = Marshal.PtrToStringAnsi(info.value.pszVal);
                    }
                }

                return uid;
            }
        }

        #endregion

        #region Equality stuff

        public override bool Equals(object obj)
        {
            if (obj is Contact)
                return hContact == (obj as Contact).hContact;
            else
                return false;
        }

        public bool Equals(Contact other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.hContact.Equals(hContact) &&
                   Equals(other.pluginLink, pluginLink);
        }

        /// <summary>
        /// Hash code is calculated uisng protocol name and user id because it
        /// may be shared with another Miranda instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Protocol.GetHashCode() * 397) ^ Uid.GetHashCode();
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

        #endregion

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

            contactSettingChangedHook = ContactSettingChanged;
            hContactSettingChangedHook =
                pluginLink.HookEvent("DB/Contact/SettingChanged",
                contactSettingChangedHook);
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

        private int ContactSettingChanged(IntPtr wParam, IntPtr lParam)
        {
            IntPtr changedHContact = wParam;
            if (changedHContact == hContact)
            {
                IntPtr pDBContactWriteSetting = lParam;
                var setting = new DBContactWriteSetting();;
                Marshal.PtrToStructure(pDBContactWriteSetting, setting);

                // TODO: Check this setting name
                if (setting.szSetting == "Status" &&
                    setting.szModule == Protocol)
                {
                    Status newStatus = (Status) setting.value.Value.dVal;
                    if (StatusChangedEvent != null)
                        StatusChangedEvent(this, newStatus);
                }
            }
            return 0;
        }

        public void Dispose()
        {
            disposed = true;
            pluginLink.UnhookEvent(hContactSettingChangedHook);
        }

        ~Contact()
        {
            if (!disposed)
                Dispose();
        }

        /// <summary>
        /// Gets contact info string from database.
        /// </summary>
        /// <param name="fieldFlag">
        /// Flag from ContactInfo.CNF_* list.
        /// </param>
        /// <returns>
        /// String from database interpreted as ANSI.
        /// </returns>
        private string GetContactField(byte fieldFlag)
        {
            // Create ContactInfo object:
            var info = new ContactInfo();
            info.hContact = hContact;
            info.dwFlag = fieldFlag;

            string result = null;

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
                    result = Marshal.PtrToStringAnsi(info.value.pszVal);
                }
            }

            return result;
        }
    }
}
