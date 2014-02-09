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
            mmi = MMInterface.GetMMI();
            Database = new DatabaseConnector();

            this.HookEvent("DB/Event/Added", eventAdded);
            this.HookEvent("CList/DoubleClicked", cListDoubleClicked);
        }

        public override void Unload()
        {
            
        }

        private int EventAdded(IntPtr wParam, IntPtr lParam)
        {
            IntPtr hContact = wParam;
            IntPtr hDBEvent = lParam;

            int blobSize = this.CallService("DB/Event/GetBlobSize",
                hDBEvent, IntPtr.Zero).ToInt32();

            using (var pBlob = new AutoPtr(Marshal.AllocHGlobal(blobSize)))
            using (var pDBEventInfo = new AutoPtr(
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DBEventInfo)))))
            {
                var eventInfo = new DBEventInfo();
                eventInfo.pBlob = pBlob;
                eventInfo.cbBlob = (uint)blobSize;

                Marshal.StructureToPtr(eventInfo, pDBEventInfo, false);
                this.CallService("DB/Event/Get", hDBEvent, pDBEventInfo);

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

                        IntPtr pString = this.CallService(
                            "DB/Event/GetText", IntPtr.Zero, pDBEventGetText);
                        string message = Marshal.PtrToStringAnsi(pString);

                        mmi.mmi_free(pString);

                        var contact = new Contact(hContact);

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
            var contact = new Contact(hContact);

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
