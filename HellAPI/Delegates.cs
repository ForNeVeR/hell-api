using System;
using System.Runtime.InteropServices;

namespace Hell
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr MirandaService(IntPtr wParam, IntPtr lParam);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MirandaHook(IntPtr wParam, IntPtr lParam);
}