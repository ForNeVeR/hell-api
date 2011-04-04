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
using System.Windows;
using Hell;
using Hell.FirstCircle;
using Hell.LastCircle.Database;
using Hell.LastCircle.System;
using HellGateMM.GUI;

namespace HellGateMM
{
    [MirandaPlugin]
    public class HellGatePlugin : Plugin
    {
        public delegate void MessageSentEventHandler(Contact contact,
            DateTime eventTime, string message);
        public delegate void MessageReceivedEventHandler(Contact contact,
            DateTime eventTime, string message);
        public delegate void ContactDoubleClickedEventHandler(Contact contact);

        public event MessageSentEventHandler MessageSentEvent;
        public event MessageReceivedEventHandler MessageReceivedEvent;
        public event ContactDoubleClickedEventHandler
            ContactDoubleClickedEvent;

        private MMInterface mmi;

        private MirandaHook eventAdded;
        private MirandaHook cListDoubleClicked;

        public MessagingWindow MessagingWindow { get; set; }

        internal DatabaseConnector Database;
        
        public HellGatePlugin()
        {
            eventAdded = EventAdded;
            cListDoubleClicked = CListDoubleClicked;
        }

        protected override void Load()
        {
            mmi = MMInterface.GetMMI(pluginLink);
            Database = new DatabaseConnector(pluginLink);

            pluginLink.HookEvent("DB/Event/Added", eventAdded);
            pluginLink.HookEvent("CList/DoubleClicked", cListDoubleClicked);
        }

        public override void Unload()
        {
            
        }

        private int EventAdded(IntPtr wParam, IntPtr lParam)
        {
            IntPtr hContact = wParam;
            IntPtr hDBEvent = lParam;

            int blobSize = pluginLink.CallService("DB/Event/GetBlobSize",
                hDBEvent, IntPtr.Zero).ToInt32();

            using (var pBlob = new AutoPtr(Marshal.AllocHGlobal(blobSize)))
            using (var pDBEventInfo = new AutoPtr(
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DBEventInfo)))))
            {
                var eventInfo = new DBEventInfo();
                eventInfo.pBlob = pBlob;
                eventInfo.cbBlob = (uint)blobSize;

                Marshal.StructureToPtr(eventInfo, pDBEventInfo, false);
                pluginLink.CallService("DB/Event/Get", hDBEvent, pDBEventInfo);

                eventInfo = (DBEventInfo)Marshal.PtrToStructure(pDBEventInfo,
                    typeof(DBEventInfo));


                if (eventInfo.eventType == DBEventInfo.EVENTTYPE_MESSAGE)
                {
                    using (var pDBEventGetText = new AutoPtr(
                        Marshal.AllocHGlobal(Marshal.SizeOf(
                            typeof(DBEventGetText)))))
                    {
                        var getText = new DBEventGetText();
                        getText.dbei = pDBEventInfo;
                        getText.datatype = Utils.DBVT_ASCIIZ;
                        getText.codepage = 1251;

                        Marshal.StructureToPtr(getText, pDBEventGetText,
                            false);

                        IntPtr pString = pluginLink.CallService(
                            "DB/Event/GetText", IntPtr.Zero, pDBEventGetText);
                        string message = Marshal.PtrToStringAnsi(pString);

                        mmi.mmi_free(pString);

                        var contact = new Contact(hContact, pluginLink);

                        DateTime eventTime = new DateTime(1970, 1, 1).
                            AddSeconds(eventInfo.timestamp).ToUniversalTime();
                        if ((eventInfo.flags & DBEventInfo.DBEF_SENT) != 0)
                        {
                            if (MessageSentEvent != null)
                                MessageSentEvent(contact, eventTime, message);
                        }
                        else
                        {
                            if (MessageReceivedEvent != null)
                                MessageReceivedEvent(contact, eventTime,
                                    message);
                        }
                    }
                }
            }          

            return 0;
        }

        private int CListDoubleClicked(IntPtr wParam, IntPtr lParam)
        {
            IntPtr hContact = wParam;
            var contact = new Contact(hContact, pluginLink);

            if (MessagingWindow == null)
            {
                MessagingWindow = new MessagingWindow(this);
                MessagingWindow.Visibility = Visibility.Visible;
            }

            if (ContactDoubleClickedEvent != null)
                ContactDoubleClickedEvent(contact);

            return 0;
        }
    }
}
