using System;
using System.Windows;
using System.Windows.Controls;
using Hell.FirstCircle;

namespace Hell
{
    /// <summary>
    /// Options page for controlling of managed plugins.
    /// </summary>
    public partial class OptionsPage : UserControl, IDisposable
    {
        /// <summary>
        /// Reference to associated plugin manager.
        /// </summary>
        private HellManager manager;

        /// <summary>
        /// Miranda options interface connector.
        /// </summary>
        private OptionsPageInterface pageInterface;
        
        /// <summary>
        /// Creates page and sets up loading of page into Miranda options
        /// dialog.
        /// </summary>
        /// <param name="hInstance">
        /// Handle of DLL instance.
        /// </param>
        /// <param name="pluginLink">
        /// Reference to object containing various Miranda service functions.
        /// </param>
        public OptionsPage(IntPtr hInstance, PluginLink pluginLink,
            HellManager manager)
        {
            InitializeComponent();

            this.manager = manager;

            pageInterface = new OptionsPageInterface(pluginLink, hInstance,
                "Plugins", "Managed Plugins", "Hell.HellManager", this);

            foreach (Type type in manager.LoadedTypes)
                PluginsDataGrid.Items.Add(new PluginDataGridRow(type,
                    PluginDataGridRow.State.Loaded));

            foreach (Type type in manager.UnloadedTypes)
                PluginsDataGrid.Items.Add(new PluginDataGridRow(type,
                    PluginDataGridRow.State.Unloaded));
        }

        /// <summary>
        /// Method for releasing resources grabbed by options page.
        /// </summary>
        public void Dispose()
        {
            pageInterface.Dispose();
        }

        private void EnableButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Enable selected plugin.
            pageInterface.ActivateApplyButton();
        }
    }
}
