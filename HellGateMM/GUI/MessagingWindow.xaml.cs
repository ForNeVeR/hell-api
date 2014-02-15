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
		private HellGatePlugin plugin;

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