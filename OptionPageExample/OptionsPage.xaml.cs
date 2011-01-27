using System;
using System.Windows.Controls;
using Hell.FirstCircle;

namespace Hell
{
    /// <summary>
    /// Interaction logic for OptionsPage.xaml.
    /// </summary>
    public partial class OptionsPage : UserControl, IDisposable
    {
        /// <summary>
        /// Reference to object controlling Miranda options dialog.
        /// </summary>
        private OptionsPageInterface page;

        /// <summary>
        /// Object constructor: creates page, shows it in Miranda options
        /// dialog.
        /// </summary>
        /// <param name="pluginLink">
        /// Reference to object containing various Miranda service functions.
        /// </param>
        /// <param name="hInstance">
        /// Handle of DLL instance.
        /// </param>
        public OptionsPage(PluginLink pluginLink, IntPtr hInstance)
        {
            InitializeComponent();

            page = new OptionsPageInterface(pluginLink, hInstance, "Example",
                "WPF Page", "Hell.OptionPlugin", this);
        }

        /// <summary>
        /// Dispose method. Recommended to always create such methods and call
        /// them on plugin unloading.
        /// </summary>
        public void Dispose()
        {
            page.Dispose();
        }
    }
}
