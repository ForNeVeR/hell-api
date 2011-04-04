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

namespace Hell
{
    /// <summary>
    /// Interface for Miranda plugin. Note that you must NOT try to use
    /// hInstance or pluginLink fields from constructor. They become available
    /// only after adapter calls your Load() method.
    /// </summary>
    public abstract class Plugin
    {
        /// <summary>
        /// Handle of DLL instance.
        /// </summary>
        protected IntPtr hInstance;

        /// <summary>
        /// Reference to object containing various Miranda service functions.
        /// </summary>
        protected PluginLink pluginLink;

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
            this.hInstance = hInstance;
            this.pluginLink = pluginLink;
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
