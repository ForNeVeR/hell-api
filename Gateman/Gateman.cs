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
using System.Collections.Generic;
using System.Windows.Forms;
using Hell;
using Hell.FirstCircle;

namespace Gateman
{
    [MirandaPluginAttribute]
    public class Gateman : Plugin
    {
        IEnumerable<Contact> contacts;

        protected override void Load()
        {
            contacts = Contact.Enumerate(pluginLink);
            foreach (Contact contact in contacts)
            {

            }
        }

        public void StatusChanged(Contact contact, Contact.Status status)
        {
            MessageBox.Show(
                String.Format("Contact {0}'s status has changed to {1}.",
                contact.Nickname, status.ToString()));
        }

        public override void Unload()
        {
            foreach (Contact contact in contacts)
            {
                contact.Dispose();
            }
        }
    }
}
