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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Hell.FirstCircle;

namespace HellGateMM.GUI
{
    /// <summary>
    /// Main messaging window.
    /// </summary>
    partial class MessagingWindow : Window
    {
        HellGatePlugin plugin;
        
        public MessagingWindow(HellGatePlugin plugin)
        {
            InitializeComponent();

            this.plugin = plugin;

            plugin.ContactDoubleClickedEvent +=
                Plugin_ContactDoubleClickedEvent;
            plugin.MessageSentEvent += Plugin_MessageSentEvent;
            plugin.MessageReceivedEvent += Plugin_MessageReceivedEvent;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            plugin.ContactDoubleClickedEvent -=
                Plugin_ContactDoubleClickedEvent;
            plugin.MessageSentEvent -= Plugin_MessageSentEvent;
            plugin.MessageReceivedEvent -= Plugin_MessageReceivedEvent;

            plugin.MessagingWindow = null;
        }

        private void Plugin_ContactDoubleClickedEvent(Contact contact)
        {
            TabItem tab = GetContactTab(contact);
            if (tab != null)
                MainTabControl.SelectedItem = tab;
            else
            {
                var isChatRoom = 
                    (byte)plugin.Database.GetContactSetting(contact,
                        contact.Protocol, "ChatRoom");

                if (isChatRoom != 0)
                    MainTabControl.Items.Add(new ContactTabItem(this,
                        contact));
                else
                    MainTabControl.Items.Add(new ChatTabItem(this, contact));
            }

        }

        private void Plugin_MessageSentEvent(Contact contact,
            DateTime eventTime, string message)
        {
            ContactTabItem tab = GetContactTab(contact);
            if (tab != null)
            {
                tab.AddMessage(eventTime, message, false);
            }
        }

        private void Plugin_MessageReceivedEvent(Contact contact,
            DateTime eventTime, string message)
        {
            ContactTabItem tab = GetContactTab(contact);
            if (tab != null)
            {
                tab.AddMessage(eventTime, message, true);
            }
        }      
  
        private ContactTabItem GetContactTab(Contact contact)
        {
            foreach (ContactTabItem item in MainTabControl.Items)
            {
                if (item.Contact == contact)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
