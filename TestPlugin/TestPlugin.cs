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
        protected override void Load()
        {
            this.CreateServiceFunction("TestPlug/MenuCommand",
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
                this.CallService("CList/AddMainMenuItem", IntPtr.Zero,
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
