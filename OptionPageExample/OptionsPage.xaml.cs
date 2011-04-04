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
using System.Windows.Controls;
using Hell.FirstCircle;

namespace Hell
{
    /// <summary>
    /// Interaction logic for OptionsPage.xaml.
    /// </summary>
    public partial class OptionsPage : UserControl, IDisposable
    {
        /// <summary>
        /// Reference to object controlling Miranda options dialog.
        /// </summary>
        private OptionsPageInterface page;

        /// <summary>
        /// Object constructor: creates page, shows it in Miranda options
        /// dialog.
        /// </summary>
        /// <param name="pluginLink">
        /// Reference to object containing various Miranda service functions.
        /// </param>
        /// <param name="hInstance">
        /// Handle of DLL instance.
        /// </param>
        public OptionsPage(PluginLink pluginLink, IntPtr hInstance)
        {
            InitializeComponent();

            page = new OptionsPageInterface(pluginLink, hInstance, "Example",
                "WPF Page", "Hell.OptionPlugin", this);
        }

        /// <summary>
        /// Dispose method. Recommended to always create such methods and call
        /// them on plugin unloading.
        /// </summary>
        public void Dispose()
        {
            page.Dispose();
        }
    }
}
