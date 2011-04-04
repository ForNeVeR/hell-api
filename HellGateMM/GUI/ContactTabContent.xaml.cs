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
