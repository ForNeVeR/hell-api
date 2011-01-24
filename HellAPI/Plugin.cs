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
