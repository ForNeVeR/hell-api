using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hell.FirstCircle;

namespace HellGateMM.GUI
{
	/// <summary>
	/// Interaction logic for ContactTabContent.xaml
	/// </summary>
	partial class ContactTabContent : UserControl
	{
		private ContactTabItem tabItem;
		private Contact contact;

		internal ContactTabContent(ContactTabItem container, Contact contact)
		{
			InitializeComponent();

			tabItem = container;
			this.contact = contact;
		}

		private void MessageBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SendMessage();
			}
		}

		private void SendMessage()
		{
			string message = MessageBox.Text;
			MessageBox.Text = "";
			contact.SendMessage(message);
		}

		private void SendButton_Click(object sender, RoutedEventArgs e)
		{
			SendMessage();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			tabItem.Delete();
		}
	}
}