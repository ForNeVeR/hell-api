using System;
using System.Runtime.InteropServices;

namespace Hell.LastCircle.Database
{
	/// <summary>
	/// DBEVENTGETTEXT structure.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public class DBEventGetText
	{
		/// <summary>
		/// egt->dbei should be the valid database event read via
		/// MS_DB_EVENT_GET
		/// </summary>
		public IntPtr dbei;

		/// <summary>
		/// egt->datatype = DBVT_WCHAR or DBVT_ASCIIZ or DBVT_TCHAR. If a
		/// caller wants to suppress Unicode part of event in answer, add
		/// DBVTF_DENYUNICODE to this field.
		/// </summary>
		public int datatype;

		/// <summary>
		/// egt->codepage is any valid codepage, CP_ACP by default.
		/// </summary>
		public int codepage;
	}
}