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
using System.Windows.Forms;
using Hell.LastCircle.CList;

namespace Hell
{
    /// <summary>
    /// Test managed Miranda plugin.
    /// </summary>
    // Every plugin main class must be marked with MirandaPlugin attribute and
    // be derived from abstract Hell.Plugin class.
    [MirandaPlugin]
    public class TestPlugin : Plugin
    {
        /// <summary>
        /// ALWAYS remember to save delegates to your methods that can be
        /// called from Miranda. If you forget to do that, delegate will be
        /// garbage collected and method call will fail.
        /// </summary>
        private MirandaService menuCommand;

        /// <summary>
        /// Plugin object constructor.
        /// </summary>
        public TestPlugin()
        {
            menuCommand = PluginMenuCommand;
        }

        /// <summary>
        /// Load method will be called on plugin load.
        /// </summary>
        /// <param name="pluginLink">
        /// Provided PluginLink object contains pointers to Miranda service
        /// functions.
        /// </param>
        protected override void Load()
        {
            PluginLink.CreateServiceFunction("TestPlug/MenuCommand",
                menuCommand);

            var mi = new CListMenuItem();
            mi.position = -0x7FFFFFFF;
            mi.flags = 0;
            // TODO: Load icon:
            // mi.hIcon = LoadSkinnedIcon(SKINICON_OTHER_MIRANDA);
            mi.name = "&Test Plugin...";
            mi.service = "TestPlug/MenuCommand";

            // You can use raw IntPtr instead of AutoPtr here, but then do not
            // forget to call Marshal.FreeHGlobal to prevent memory leaks.
            using (var pClistMenuItem = new AutoPtr(
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CListMenuItem)))))
            {
                Marshal.StructureToPtr(mi, pClistMenuItem, false);
                PluginLink.CallService("CList/AddMainMenuItem", IntPtr.Zero,
                    pClistMenuItem);
            }
        }

        /// <summary>
        /// Unload method will be called on plugin unloading (for example, on
        /// Miranda exit).
        /// </summary>
        public override void Unload()
        {
            // TODO: Delete menu item, unhook event.
        }

        /// <summary>
        /// This method woll be called when user selects "Test Plugin..." menu
        /// item.
        /// </summary>
        private IntPtr PluginMenuCommand(IntPtr wParam, IntPtr lParam)
        {
            MessageBox.Show("Hello world from TestPlugin!");
            return IntPtr.Zero;
        }
    }
}
