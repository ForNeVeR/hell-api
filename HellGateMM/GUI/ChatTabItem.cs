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

using System.Windows.Controls;
using Hell.FirstCircle;

namespace HellGateMM.GUI
{
    class ChatTabItem : TabItem
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
