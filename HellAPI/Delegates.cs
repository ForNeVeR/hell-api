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