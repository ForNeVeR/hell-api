using System;
using System.Runtime.InteropServices;

namespace Hell
{
    /// <summary>
    /// Interface for Miranda plugin.
    /// </summary>
    public abstract class Plugin
    {
        /// <summary>
        /// A pointer to DLL instance.
        /// </summary>
        protected IntPtr hInstance;
        
        protected Plugin(IntPtr hInstance)
        {
            this.hInstance = hInstance;
        }

        public void Load(IntPtr pluginLink)
        {
            PluginLink managedPLink = Marshal.PtrToStructure(pluginLink,
                typeof(PluginLink)) as PluginLink;
            Load(managedPLink);
        }

        public abstract void Unload();
        protected abstract void Load(PluginLink pluginLink);
    }
}
