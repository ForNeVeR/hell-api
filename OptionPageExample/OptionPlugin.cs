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
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Hell.LastCircle.Options;
using Hell.LastCircle.WinAPI;

namespace Hell
{
    /// <summary>
    /// Example of creating Miranda options page.
    /// </summary>
    [MirandaPlugin]
    public class OptionPlugin : Plugin
    {
        /// <summary>
        /// Options page object.
        /// </summary>
        private OptionsPage options;
        
        /// <summary>
        /// Load method will be called on plugin load.
        /// </summary>
        protected override void Load()
        {
            options = new OptionsPage(pluginLink, hInstance);
        }

        /// <summary>
        /// Unload method will be called on plugin unloading (for example, on
        /// Miranda exit).
        /// Always recommended to dispose all used resources and Miranda hooks
        /// on unloading, because your plugin may be unloaded without Miranda
        /// termination (by plugin manager).
        /// </summary>
        public override void Unload()
        {
            options.Dispose();
        }
    }
}
