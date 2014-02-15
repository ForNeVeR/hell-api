using System;
using System.Windows.Controls;
using Hell.FirstCircle;

namespace HellGateMM.GUI
{
	internal class ContactTabItem : TabItem
	{
		private MessagingWindow window;
		private ContactTabContent tabContent;
		public Contact Contact { get; private set; }

		public ContactTabItem(MessagingWindow parentWindow, Contact contact)
		{
			window = parentWindow;
			Contact = contact;
			Header = Contact.Nickname;
			tabContent = new ContactTabContent(this, contact);
			Content = tabContent;
		}

		public void AddMessage(DateTime time, string message, bool incoming)
		{
			tabContent.Chat.Text += String.Format("\n{0} <{1}> {2}",
				time, incoming ? Contact.Nickname : "Me", message);
		}

		public void Delete()
		{
			window.MainTabControl.Items.Remove(this);
			// Close window on last tab closing:
			if (window.MainTabControl.Items.IsEmpty)
				window.Close();
		}
	}
}