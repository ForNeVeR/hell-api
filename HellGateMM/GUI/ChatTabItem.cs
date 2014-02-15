using System.Windows.Controls;
using Hell.FirstCircle;

namespace HellGateMM.GUI
{
	internal class ChatTabItem : TabItem
	{
		private MessagingWindow window;
		private ChatTabContent tabContent;
		public Contact Contact { get; private set; }

		public ChatTabItem(MessagingWindow parentWindow, Contact contact)
		{
			window = parentWindow;
			Contact = contact;
			Header = Contact.Nickname;
			tabContent = new ChatTabContent();
			Content = tabContent;
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