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
            foreach (Type type in UnloadedTypes)
            {
                bool hasOption = settings.Contains(type.FullName);
                if ((hasOption && (byte)Database.GetSetting("HellAdapter",
                    type.FullName) == 1) || !hasOption)
                {
                    var plugin = type.GetConstructor(new Type[0]).Invoke(
                        new object[0]) as Plugin;
                    plugin.Load(hInstance, pluginLink);
                    
                    loadedPlugins.Add(plugin);
                    LoadedTypes.Add(type);
                }
            }

            foreach (Type type in LoadedTypes)
                UnloadedTypes.Remove(type);

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
    }
}
