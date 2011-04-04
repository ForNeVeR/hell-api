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
using System.Linq;
using System.Runtime.InteropServices;
using Hell.FirstCircle;

namespace Hell
{
    /// <summary>
    /// HellOptions is plugin implementing options page for HellPlugin adapter.
    /// </summary>
    public class HellManager : Plugin
    {
        /// <summary>
        /// List of all currently loaded plugin types.
        /// </summary>
        public List<Type> LoadedTypes;

        /// <summary>
        /// List of all currently unloaded plugin types.
        /// </summary>
        public List<Type> UnloadedTypes;

        /// <summary>
        /// Reference to options page.
        /// </summary>
        private OptionsPage options;

        /// <summary>
        /// Miranda database.
        /// </summary>
        internal DatabaseConnector Database { get; private set; }

        /// <summary>
        /// List of loaded plugin instances.
        /// </summary>
        private List<Plugin> loadedPlugins = new List<Plugin>();

        /// <summary>
        /// Manager constructor.
        /// </summary>
        /// <param name="detectedPlugins">
        /// List of all detected loadable plugin types.
        /// </param>
        public HellManager(List<Type> detectedPlugins)
        {
            UnloadedTypes = new List<Type>(detectedPlugins);
        }

        /// <summary>
        /// This method will be called by adapter.
        /// </summary>
        /// <param name="hInstance">
        /// Handle of DLL instance.
        /// </param>
        /// <param name="pPluginLink">
        /// Pointer to structure containing various Miranda service functions.
        /// </param>
        public void Load(IntPtr hInstance, IntPtr pPluginLink)
        {
            var pluginLink = Marshal.PtrToStructure(pPluginLink,
                typeof(PluginLink)) as PluginLink;
            Load(hInstance, pluginLink);            
        }

        /// <summary>
        /// Load method will be called after finishing all preparations for
        /// loading.
        /// </summary>
        protected override void Load()
        {
            LoadedTypes = new List<Type>();

            Database = new DatabaseConnector(pluginLink);

            string[] settings = Database.EnumSettings("HellAdapter");
            // Call new List<Type> here for making copy of list, because list
            // itself can be changed during plugin loading.
            foreach (Type type in new List<Type>(UnloadedTypes))
            {
                bool hasOption = settings.Contains(type.FullName);
                if ((hasOption && (byte)Database.GetSetting("HellAdapter",
                    type.FullName) == 1) || !hasOption)
                {
                    LoadType(type);
                }
            }

            options = new OptionsPage(hInstance, pluginLink, this);
        }

        /// <summary>
        /// Unload method will be called on plugin unloading (for example, on
        /// Miranda exit).
        /// </summary>
        public override void Unload()
        {
            options.Dispose();
            
            foreach (Plugin plugin in loadedPlugins)
            {
                plugin.Unload();
            }

            loadedPlugins.Clear();
        }

        /// <summary>
        /// Loads specified plugin type instance. Moves it from list
        /// UnloadedTypes into LoadedTypes.
        /// </summary>
        /// <param name="pluginType">
        /// Plugin type; must be inherited from Plugin.
        /// </param>
        internal void LoadType(Type pluginType)
        {
            var plugin = pluginType.GetConstructor(new Type[0]).Invoke(
                new object[0]) as Plugin;
            plugin.Load(hInstance, pluginLink);

            loadedPlugins.Add(plugin);
            if (UnloadedTypes.Contains(pluginType))
                UnloadedTypes.Remove(pluginType);
            LoadedTypes.Add(pluginType);
        }

        /// <summary>
        /// Unloads all instances of specified plugins. Moves it from list
        /// LoadedTypes into UnloadedTypes.
        /// </summary>
        /// <param name="pluginType">
        /// Plugin type; must be inherited from Plugin.
        /// </param>
        internal void UnloadType(Type pluginType)
        {
            foreach (Plugin plugin in loadedPlugins)
            {
                if (plugin.GetType() == pluginType)
                {
                    plugin.Unload();
                }
            }

            loadedPlugins.RemoveAll(
                (plugin) => plugin.GetType() == pluginType);

            if (LoadedTypes.Contains(pluginType))
                LoadedTypes.Remove(pluginType);
            UnloadedTypes.Add(pluginType);
        }
    }
}
