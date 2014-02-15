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
            options = new OptionsPage(HInstance, HLangpack);
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
