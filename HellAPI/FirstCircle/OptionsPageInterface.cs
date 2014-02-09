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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using Hell.LastCircle.Options;
using Hell.LastCircle.WinAPI;

namespace Hell.FirstCircle
{
    /// <summary>
    /// Interface for working with Miranda options page. Implements methods and
    /// provides events for operating with Miranda options dialog.
    /// 
    /// Note: this object will not be disposed by garbage collector, because
    /// this leads to Miranda crashing if garbage collector runs after Miranda
    /// hook engine stopped.
    /// </summary>
    public class OptionsPageInterface : IDisposable
    {
        /// <summary>
        /// Event generated on press of Miranda "Apply" options dialog button.
        /// </summary>
        public event Action<OptionsPageInterface> ApplyButtonPressed;

        /// <summary>
        /// Event generated when Miranda queries reset page (when Cancel button
        /// pressed or when Ok button pressed with no changes).
        /// </summary>
        // TODO: Check this comment.
        public event Action<OptionsPageInterface> ResetPageQuery;

        /// <summary>
        /// Event generated on showing of current page in Miranda options
        /// dialog.
        /// </summary>
        public event Action<OptionsPageInterface> PageShowed;
        
        /// <summary>
        /// Hook for creating options page.
        /// </summary>
        private MirandaHook optInitialise;

        /// <summary>
        /// Handle to Miranda hook.
        /// </summary>
        private IntPtr hOptInitialise;

        /// <summary>
        /// DlgProc delegate.
        /// </summary>
        private OptionsDialogPage.DlgProc dlgProc;

        /// <summary>
        /// Object for handling WPF object hosting in native environment.
        /// </summary>
        private HwndSource hwndSource;

        /// <summary>
        /// Handle of DLL instance. Used to gather resources from it.
        /// </summary>
        private IntPtr hInstance;

        /// <summary>
        /// Name of group in Miranda options dialog.
        /// </summary>
        private string groupName;

        /// <summary>
        /// Name of page in Miranda options dialog.
        /// </summary>
        private string pageName;

        /// <summary>
        /// Unique string ID for HwndSource.
        /// </summary>
        private string uniquePageID;

        /// <summary>
        /// Visual object representing the content to be placed into options
        /// page.
        /// </summary>
        private Visual content;

        /// <summary>
        /// Handle of parent dialog window.
        /// </summary>
        private IntPtr? hDlg;

        /// <summary>
        /// Creates object, hooks all needed Miranda events.
        /// </summary>
        /// <param name="hInstance">
        /// Handle of DLL instance. Used to gather resources from it.
        /// </param>
        /// <param name="groupName">
        /// Not localized name of group in Miranda options dialog.
        /// </param>
        /// <param name="pageName">
        /// Name of page in Miranda options dialog.
        /// </param>
        /// <param name="uniquePageID">
        /// Unique string ID used for this page. Use of full qualified plugin
        /// class name recommended.
        /// </param>
        /// <param name="content">
        /// Visual object representing the content to be placed into options
        /// page.
        /// </param>
        public OptionsPageInterface(IntPtr hInstance, string groupName,
            string pageName, string uniquePageID, Visual content)
        {
            this.hInstance = hInstance;
            this.groupName = groupName;
            this.pageName = pageName;
            this.uniquePageID = uniquePageID;
            this.content = content;
            
            // Prepare delegates:
            optInitialise = Initialise;
            dlgProc = DlgProc;
            
            // Hook options dialog initialise event:
            hOptInitialise = 
                Plugin.m_HookEvent("Opt/Initialise", optInitialise);
        }

        /// <summary>
        /// Called once for options dialog init.
        /// </summary>
        /// <param name="wParam">
        /// addInfo pointer, must be used in calls to Opt/AddPage Miranda
        /// service.
        /// </param>
        /// <param name="lParam">
        /// Not used.
        /// </param>
        /// <returns>
        /// Returns zero on success.
        /// </returns>
        private int Initialise(IntPtr wParam, IntPtr lParam)
        {
            IntPtr addInfo = wParam;

            using (var pOptionPage = new AutoPtr(Marshal.AllocHGlobal(
                Marshal.SizeOf(typeof(OptionsDialogPage)))))
            {
                var optionPage = new OptionsDialogPage();
                optionPage.position = -800000000;
                optionPage.hInstance = hInstance;
                optionPage.pszTemplate = new IntPtr(Utils.StubDialogID);
                optionPage.pszGroup = groupName;
                optionPage.pszTitle = pageName;
                optionPage.pfnDlgProc = dlgProc;

                Marshal.StructureToPtr(optionPage, pOptionPage, false);
                Plugin.m_CallService("Opt/AddPage", addInfo, pOptionPage);
            }

            return 0;
        }

        /// <summary>
        /// Method processing options page events.
        /// </summary>
        /// <param name="hDlg">
        /// Miranda options dialog handle.
        /// </param>
        /// <param name="message">
        /// Message code.
        /// </param>
        /// <param name="wParam">
        /// Parameter.
        /// </param>
        /// <param name="lParam">
        /// Parameter.
        /// </param>
        /// <returns>
        /// Returns zero on success.
        /// </returns>
        private IntPtr DlgProc(IntPtr hDlg, uint message, IntPtr wParam,
            IntPtr lParam)
        {
            if (message == Constants.WM_INITDIALOG)
            {
                this.hDlg = hDlg;
                
                var parameters = new HwndSourceParameters(uniquePageID);
                parameters.PositionX = 0;
                parameters.PositionY = 0;
                parameters.Width = Utils.MirandaOptionsWidth;
                parameters.Height = Utils.MirandaOptionsHeight;
                parameters.ParentWindow = hDlg;
                parameters.WindowStyle = Constants.WS_VISIBLE |
                    Constants.WS_CHILD;
                hwndSource = new HwndSource(parameters);

                hwndSource.RootVisual = content;

                if (PageShowed != null)
                    PageShowed(this);
            }
            else if (message == Constants.WM_NOTIFY)
            {
                var NMHDR = Marshal.PtrToStructure(lParam, typeof(NMHDR))
                    as NMHDR;
                if (NMHDR.idFrom == UIntPtr.Zero)
                {
                    if (NMHDR.code == Constants.PSN_APPLY)
                    {
                        if (ApplyButtonPressed != null)
                            ApplyButtonPressed(this);
                    }
                    else if (NMHDR.code == Constants.PSN_RESET)
                    {
                        if (ResetPageQuery != null)
                            ResetPageQuery(this);
                    }
                }
            }
            // TODO: Check for other cases.

            return IntPtr.Zero;
        }

        /// <summary>
        /// Activates the "Apply" button.
        /// </summary>
        public void ActivateApplyButton()
        {
            if (hDlg != null)
                Functions.SendMessage(Functions.GetParent(hDlg.Value),
                    Constants.PSM_CHANGED, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Unhooks all events to prevent external calls to unallocated
        /// delegates.
        /// </summary>
        public void Dispose()
        {
            Plugin.m_UnhookEvent(hOptInitialise);
            
            // TODO: Remember hwndSource in static collection and remove it
            // only when options dialog is closed.
        }
    }
}
