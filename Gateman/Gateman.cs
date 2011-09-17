/*
 * Copyright (C) 2011 by ForNeVeR
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

using System.Collections.Generic;
using System.Linq;
using Hell.FirstCircle;

namespace Hell.Gateman
{
    /// <summary>
    /// Plugin for logging users online time.
    /// </summary>
    [MirandaPluginAttribute]
    public class Gateman : Plugin
    {
        private IList<Contact> _contacts;

        protected override void Load()
        {
            // TODO: Handle contact creation and deletion.
            _contacts = Contact.Enumerate(PluginLink).ToList();
            foreach (Contact contact in _contacts)
            {
                contact.StatusChanged += contact_StatusChangedEvent;
            }
        }

        public override void Unload()
        {
            foreach (Contact contact in _contacts)
            {
                contact.Dispose();
            }
        }

        private void contact_StatusChangedEvent(Contact sender, Contact.Status newStatus)
        {
            // TODO: Log status change.
        }
    }
}
