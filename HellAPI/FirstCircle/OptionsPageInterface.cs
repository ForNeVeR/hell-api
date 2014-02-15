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
		/// Miranda language pack handle.
		/// </summary>
		private readonly int _hLangpack;

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
		private string uniquePageId;

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
		/// <param name="hLangpack">Miranda language pack handle.</param>
		/// <param name="groupName">
		/// Not localized name of group in Miranda options dialog.
		/// </param>
		/// <param name="pageName">
		/// Name of page in Miranda options dialog.
		/// </param>
		/// <param name="uniquePageId">
		/// Unique string ID used for this page. Use of full qualified plugin
		/// class name recommended.
		/// </param>
		/// <param name="content">
		/// Visual object representing the content to be placed into options
		/// page.
		/// </param>
		public OptionsPageInterface(
			IntPtr hInstance,
			int hLangpack,
			string groupName,
			string pageName,
			string uniquePageId,
			Visual content)
		{
			this.hInstance = hInstance;
			_hLangpack = hLangpack;
			this.groupName = groupName;
			this.pageName = pageName;
			this.uniquePageId = uniquePageId;
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
				Marshal.SizeOf(typeof (OptionsDialogPage)))))
			{
				var optionPage = new OptionsDialogPage
				{
					position = -800000000,
					hInstance = hInstance,
					pszTemplate = new IntPtr(Utils.StubDialogID),
					pszGroup = groupName,
					pszTitle = pageName,
					pfnDlgProc = dlgProc,
					hLangpack = _hLangpack
				};

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

				var parameters = new HwndSourceParameters(uniquePageId);
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
				var NMHDR = Marshal.PtrToStructure(lParam, typeof (NMHDR))
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