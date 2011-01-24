using System;
using System.ComponentModel;
using System.Windows;
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
            ContactTabItem tab = GetContactTab(contact);
            if (tab != null)
                MainTabControl.SelectedItem = tab;
            else
                MainTabControl.Items.Add(new ContactTabItem(this, contact));
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
