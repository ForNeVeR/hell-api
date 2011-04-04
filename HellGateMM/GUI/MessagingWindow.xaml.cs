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
