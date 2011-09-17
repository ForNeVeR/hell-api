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

namespace Hell
{
    /// <summary>
    /// Interface for Miranda plugin. Note that you must NOT try to use
    /// HInstance or PluginLink fields from constructor. They become available
    /// only after adapter calls your Load() method.
    /// </summary>
    public abstract class Plugin
    {
        /// <summary>
        /// Handle of DLL instance.
        /// </summary>
        protected IntPtr HInstance { get; private set; }

        /// <summary>
        /// Reference to object containing various Miranda service functions.
        /// </summary>
        protected PluginLink PluginLink { get; private set; }

        /// <summary>
        /// This method will be called first on plugin creation.
        /// </summary>
        /// <param name="hInstance">
        /// Handle of DLL instance.
        /// </param>
        /// <param name="pluginLink">
        /// Reference to object containing various Miranda service functions.
        /// </param>
        public void Load(IntPtr hInstance, PluginLink pluginLink)
        {
            HInstance = hInstance;
            PluginLink = pluginLink;
            Load();
        }

        /// <summary>
        /// This method will be called by adapter system when all preparations
        /// for plugin loading done (i.e. calling private Load method).
        /// </summary>
        protected abstract void Load();

        /// <summary>
        /// This method called on plugin unloading (for example, on Miranda
        /// exit).
        /// </summary>
        public abstract void Unload();        
    }
}
