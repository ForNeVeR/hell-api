using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Hell.LastCircle.System
{
	[StructLayout(LayoutKind.Sequential)]
	public class MMInterface
	{
		/// <summary>
		/// Method for obtaining all these function pointers.
		/// </summary>
		/// <returns>
		/// Valid managed MMInterface object.
		/// </returns>
		public static MMInterface GetMMI()
		{
			var mmi = new MMInterface();

			// Put structure to unmanaged memory:
			IntPtr pMMInterface =
				Marshal.AllocHGlobal(Marshal.SizeOf(typeof (MMInterface)));
			Marshal.StructureToPtr(mmi, pMMInterface, false);

			Plugin.m_CallService(
				"Miranda/System/GetMMI",
				IntPtr.Zero,
				pMMInterface);

			// Get structure back from unmanaged memory:
			mmi = (MMInterface)Marshal.PtrToStructure(pMMInterface,
				typeof (MMInterface));

			// Free allocated unmanaged memory:
			Marshal.FreeHGlobal(pMMInterface);

			return mmi;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr MallocDelegate(UIntPtr size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr ReallocDelegate(IntPtr ptr, UIntPtr size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void FreeDelegate(IntPtr ptr);

		private UIntPtr cbSize =
			new UIntPtr((uint)Marshal.SizeOf(typeof (MMInterface)));

		[MarshalAs(UnmanagedType.FunctionPtr)] public MallocDelegate mmi_malloc;

		[MarshalAs(UnmanagedType.FunctionPtr)] public ReallocDelegate mmi_realloc;

		[MarshalAs(UnmanagedType.FunctionPtr)] public FreeDelegate mmi_free;

/* TODO: Finish definition:
struct MM_INTERFACE
{
	size_t cbSize;
	void* (*mmi_malloc) (size_t);
	void* (*mmi_realloc) (void*, size_t);
	void  (*mmi_free) (void*);

	#if MIRANDA_VER >= 0x0600
		void*    (*mmi_calloc) (size_t);
		char*    (*mmi_strdup) (const char *src);
		wchar_t* (*mmi_wstrdup) (const wchar_t *src);
	#endif
	#if MIRANDA_VER >= 0x0700
      int      (*mir_snprintf) (char *buffer, size_t count, const char* fmt, ...);
		int      (*mir_sntprintf) (TCHAR *buffer, size_t count, const TCHAR* fmt, ...);
		int      (*mir_vsnprintf) (char *buffer, size_t count, const char* fmt, va_list va);
		int      (*mir_vsntprintf) (TCHAR *buffer, size_t count, const TCHAR* fmt, va_list va);

		wchar_t* (*mir_a2u_cp) (const char* src, int codepage);
		wchar_t* (*mir_a2u)(const char* src);
		char*    (*mir_u2a_cp)(const wchar_t* src, int codepage);
		char*    (*mir_u2a)( const wchar_t* src);
	#endif
};
*/
	}
}