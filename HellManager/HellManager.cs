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
        /// Load method will be called after finishing all preparations for
        /// loading.
        /// </summary>
        protected override void Load()
        {
            LoadedTypes = new List<Type>();

            Database = new DatabaseConnector();

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

            options = new OptionsPage(HInstance, HLangpack, this);
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
            plugin.Load(HInstance, HLangpack);

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
