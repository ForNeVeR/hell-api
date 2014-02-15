using System;
using System.Runtime.InteropServices;

namespace Hell
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int MirandaHook(IntPtr wParam, IntPtr lParam);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int MirandaHookObj(IntPtr pObject, IntPtr wParam, IntPtr lParam);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int MirandaHookParam(IntPtr wParam, IntPtr lParam, IntPtr lParam2);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int MirandaHookObjParam(IntPtr pObject, IntPtr wParam, IntPtr lParam, IntPtr param);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr MirandaService(IntPtr wParam, IntPtr lParam);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr MirandaServiceObj(IntPtr pObject, IntPtr wParam, IntPtr lParam);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr MirandaServiceParam(IntPtr wParam, IntPtr lParam, IntPtr param);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr MirandaServiceObjParam(IntPtr pObject, IntPtr wParam, IntPtr lParam, IntPtr param);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void AsyncFunc(IntPtr pObject);
}