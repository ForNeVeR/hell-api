using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Hell.LastCircle.Options;
using System.Windows.Controls;

namespace Hell
{
    /// <summary>
    /// Example of creating Miranda options page.
    /// </summary>
    [MirandaPlugin]
    public class OptionPlugin : Plugin
    {
        /// <summary>
        /// Hook for creating options page.
        /// </summary>
        private MirandaHook optInit;

        /// <summary>
        /// DlgProc delegate.
        /// </summary>
        private OptionsDialogPage.DlgProc dlgProc;

        /// <summary>
        /// Object for handling WPF object hosting in native environment.
        /// </summary>
        private HwndSource hwndSource;

        /// <summary>
        /// Object constructor.
        /// </summary>
        public OptionPlugin()
        {
            optInit = OptInitialise;
            dlgProc = DlgProc;
        }

        /// <summary>
        /// Load method will be called on plugin load.
        /// </summary>
        /// <param name="pluginLink">
        /// Provided PluginLink object contains pointers to Miranda service
        /// functions.
        /// </param>
        protected override void Load()
        {
            pluginLink.HookEvent("Opt/Initialise", optInit);
        }

        /// <summary>
        /// Unload method will be called on plugin unloading (for example, on
        /// Miranda exit).
        /// </summary>
        public override void Unload()
        {
            
        }

        /// <summary>
        /// Hook for initialising Miranda options dialog.
        /// </summary>
        /// <param name="wParam">
        /// Pointer that should be passed to Opt/AddPage service.
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
                optionPage.pszGroup = "Example";
                optionPage.pszTitle = "Example";
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
                var parameters = new HwndSourceParameters("OptionPageExample");
                parameters.PositionX = 0;
                parameters.PositionY = 0;
                parameters.Width = Utils.MirandaOptionsWidth;
                parameters.Height = Utils.MirandaOptionsHeight;                
                parameters.ParentWindow = hDlg;
                parameters.WindowStyle = Utils.WS_VISIBLE | Utils.WS_CHILD;
                hwndSource = new HwndSource(parameters);

                var page = new OptionsPage();
                hwndSource.RootVisual = page;
            }
            
            return IntPtr.Zero;
        }
    }
}
