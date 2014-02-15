using System;
using System.Runtime.InteropServices;

namespace Hell.LastCircle.Options
{
	/// <summary>
	/// Mapping of C structure OPTIONSDIALOGPAGE from m_options.h header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class OptionsDialogPage
	{
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate IntPtr DlgProc(IntPtr hDlg, uint message,
			IntPtr wParam, IntPtr hParam);

		private int cbSize = Marshal.SizeOf(typeof (OptionsDialogPage));
		public int position;
		public string pszTitle;
		[MarshalAs(UnmanagedType.FunctionPtr)] public DlgProc pfnDlgProc;
		public IntPtr pszTemplate;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public string pszGroup;
		public int groupPosition;
		public IntPtr hGroupIcon;
		public int flags;
		public string tab;
		public IntPtr dwInitParam;
		public int hLangpack;
	}
}