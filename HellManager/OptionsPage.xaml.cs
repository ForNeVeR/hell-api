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

            pageInterface.PageShowed += (_) =>
                {
                    UpdateTable();
                };
            pageInterface.ResetPageQuery += (_) =>
                {
                    UpdateTable();
                };
            pageInterface.ApplyButtonPressed += (_) =>
                {
                    ApplyChanges();
                };
        }

        /// <summary>
        /// Method for releasing resources grabbed by options page.
        /// </summary>
        public void Dispose()
        {
            pageInterface.Dispose();
        }

        /// <summary>
        /// Loads all plugin data into table.
        /// </summary>
        private void UpdateTable()
        {
            PluginsDataGrid.Items.Clear();
            
            foreach (Type type in manager.LoadedTypes)
                PluginsDataGrid.Items.Add(new PluginDataGridRow(type,
                    PluginDataGridRow.State.Loaded));

            foreach (Type type in manager.UnloadedTypes)
                PluginsDataGrid.Items.Add(new PluginDataGridRow(type,
                    PluginDataGridRow.State.Unloaded));
        }

        /// <summary>
        /// Method for applying all pending changes. Writes information to
        /// database.
        /// </summary>
        private void ApplyChanges()
        {
            foreach (PluginDataGridRow row in PluginsDataGrid.Items)
            {
                switch (row.LoadState)
                {
                    case PluginDataGridRow.State.AboutToLoad:
                        manager.LoadType(row.Type);
                        manager.Database.SetSetting("HellAdapter",
                            row.TypeName, (byte)1);
                        break;
                    case PluginDataGridRow.State.AboutToUnload:
                        manager.UnloadType(row.Type);
                        manager.Database.SetSetting("HellAdapter",
                            row.TypeName, (byte)0);
                        break;
                }
            }

            UpdateTable();
        }

        private void EnableButton_Click(object sender, RoutedEventArgs e)
        {
            if (PluginsDataGrid.SelectedItem != null)
            {
                var row = PluginsDataGrid.SelectedItem as PluginDataGridRow;
                switch (row.LoadState)
                {
                    case PluginDataGridRow.State.AboutToUnload:
                        row.LoadState = PluginDataGridRow.State.Loaded;
                        break;
                    case PluginDataGridRow.State.Unloaded:
                        row.LoadState = PluginDataGridRow.State.AboutToLoad;
                        break;
                }
                PluginsDataGrid.Items.Refresh();
            }
            pageInterface.ActivateApplyButton();
        }

        private void DisableButton_Click(object sender, RoutedEventArgs e)
        {
            if (PluginsDataGrid.SelectedItem != null)
            {
                var row = PluginsDataGrid.SelectedItem as PluginDataGridRow;
                switch (row.LoadState)
                {
                    case PluginDataGridRow.State.AboutToLoad:
                        row.LoadState = PluginDataGridRow.State.Unloaded;
                        break;
                    case PluginDataGridRow.State.Loaded:
                        row.LoadState = PluginDataGridRow.State.AboutToUnload;
                        break;
                }
                PluginsDataGrid.Items.Refresh();
            }
            pageInterface.ActivateApplyButton();
        }
    }
}
