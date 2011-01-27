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
            options = new OptionsPage(pluginLink, hInstance);
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
