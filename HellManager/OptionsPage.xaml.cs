using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Interop;
using Hell.LastCircle.Options;

namespace Hell
{
    /// <summary>
    /// Options page for controlling of managed plugins.
    /// </summary>
    public partial class OptionsPage : UserControl
    {
        /// <summary>
        /// Handle of DLL instance.
        /// </summary>
        private IntPtr hInstance;

        /// <summary>
        /// Reference to object containing various Miranda service functions.
        /// </summary>
        private PluginLink pluginLink;

        /// <summary>
        /// Reference to associated plugin manager.
        /// </summary>
        private HellManager manager;
        
        /// <summary>
        /// Hook for creating options page.
        /// </summary>
        private MirandaHook optInitialise;

        /// <summary>
        /// DlgProc delegate.
        /// </summary>
        private OptionsDialogPage.DlgProc dlgProc;

        /// <summary>
        /// Object for handling WPF object hosting in native environment.
        /// </summary>
        private HwndSource hwndSource;
        
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

            this.hInstance = hInstance;
            this.pluginLink = pluginLink;
            this.manager = manager;

            optInitialise = OptInitialise;
            dlgProc = DlgProc;

            pluginLink.HookEvent("Opt/Initialise", optInitialise);

            foreach (Type type in manager.LoadedTypes)
                PluginsDataGrid.Items.Add(new PluginDataGridRow(type,
                    PluginDataGridRow.State.Loaded));

            foreach (Type type in manager.UnloadedTypes)
                PluginsDataGrid.Items.Add(new PluginDataGridRow(type,
                    PluginDataGridRow.State.Unloaded));

        }

        /// <summary>
        /// Method for initialising Miranda options dialog.
        /// </summary>
        /// <param name="wParam">
        /// Pointer that should be passed to Opt/AddPage service.
        /// </param>
        /// <param name="lParam">
        /// Unused.
        /// </param>
        private int OptInitialise(IntPtr wParam, IntPtr lParam)
        {
            IntPtr addInfo = wParam;

            using (var pOptionPage = new AutoPtr(Marshal.AllocHGlobal(
                Marshal.SizeOf(typeof(OptionsDialogPage)))))
            {
                var optionPage = new OptionsDialogPage();
                optionPage.position = -800000000;
                optionPage.hInstance = hInstance;
                optionPage.pszTemplate = new IntPtr(Utils.StubDialogID);
                optionPage.pszGroup = "Plugins";
                optionPage.pszTitle = "Managed Plugins";
                optionPage.pfnDlgProc = dlgProc;

                Marshal.StructureToPtr(optionPage, pOptionPage, false);
                pluginLink.CallService("Opt/AddPage", addInfo, pOptionPage);
            }

            return 0;
        }

        /// <summary>
        /// Method processing options page events.
        /// </summary>
        IntPtr DlgProc(IntPtr hDlg, uint message, IntPtr wParam, IntPtr hParam)
        {
            if (message == Utils.WM_INITDIALOG)
            {
                var parameters =
                    new HwndSourceParameters("ManagedPluginsOptionsPage");
                parameters.PositionX = 0;
                parameters.PositionY = 0;
                parameters.Width = Utils.MirandaOptionsWidth;
                parameters.Height = Utils.MirandaOptionsHeight;
                parameters.ParentWindow = hDlg;
                parameters.WindowStyle = Utils.WS_VISIBLE | Utils.WS_CHILD;
                
                hwndSource = new HwndSource(parameters);
                hwndSource.RootVisual = this;
            }

            return IntPtr.Zero;
        }
    }
}
