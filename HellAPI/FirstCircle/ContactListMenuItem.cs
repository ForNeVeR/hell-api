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

using System;
using System.Runtime.InteropServices;
using Hell.LastCircle.CList;

namespace Hell.FirstCircle
{
    /// <summary>
    /// Class for creating item in Miranda IM contact list menu.
    /// </summary>
    public class ContactListMenuItem : IDisposable
    {
        /// <summary>
        /// Delegate for miranda service.
        /// </summary>
        private readonly MirandaService _service;

        /// <summary>
        /// Action to be called on menu item click.
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// Creates a service named <paramref name="serviceName"/> and creates an item in Miranda IM contact list.
        /// </summary>
        /// <param name="pluginLink">An object containing links to Miranda service functions.</param>
        /// <param name="serviceName">Unique name of service to be created.</param>
        /// <param name="menuItemText">Text of menu item.</param>
        /// <param name="action">Method to be called when user selects menu item.</param>
        public ContactListMenuItem(PluginLink pluginLink, string serviceName, string menuItemText, Action action)
        {
            _action = action;
            _service = Service;
            pluginLink.CreateServiceFunction(serviceName, _service);

            var cListMenuItem = new CListMenuItem
                {
                    position = -0x7FFFFFFF,
                    flags = 0,
                    name = menuItemText,
                    service = serviceName
                };

            using (var pCListMenuItem = new AutoPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CListMenuItem)))))
            {
                Marshal.StructureToPtr(cListMenuItem, pCListMenuItem, false);
                pluginLink.CallService("CList/AddMainMenuItem", IntPtr.Zero, pCListMenuItem);
            }
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            // TODO: Delete item from main menu.
        }

        /// <summary>
        /// Miranda service method.
        /// </summary>
        /// <returns>Always returns a null pointer.</returns>
        private IntPtr Service(IntPtr wParam, IntPtr lParam)
        {
            _action();
            return IntPtr.Zero;
        }
    }
}
