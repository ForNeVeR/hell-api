/*
 * Copyright 2010-2011 ForNeVeR.
 *
 * This file is part of Hell API.
 *
 * Hell API is free software: you can redistribute it and/or modify it under
 * the terms of the GNU Lesser General Public License as published by the Free
 * Software Foundation, either version 3 of the License, or (at your option)
 * any later version.
 *
 * Hell API is distributed in the hope that it will be useful, but WITHOUT ANY
 * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
 * details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Hell API. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Runtime.InteropServices;

namespace Hell
{
    /// <summary>
    /// PluginLink is object containing pointers to important Miranda
    /// functions.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PluginLink
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl,
            CharSet=CharSet.Ansi)]
        public delegate IntPtr CreateHookableEventDelegate(string charPtr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DestroyHookableEventDelegate(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int NotifyEventHooksDelegate(IntPtr handle,
            IntPtr hParam, IntPtr wParam);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl,
            CharSet = CharSet.Ansi)]
        public delegate IntPtr HookEventDelegate(string charPtr,
            [MarshalAs(UnmanagedType.FunctionPtr)] MirandaHook hook);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int UnhookEventDelegate(IntPtr hHook);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl,
            CharSet=CharSet.Ansi)]
        public delegate IntPtr CreateServiceFunctionDelegate(string charPtr,
            [MarshalAs(UnmanagedType.FunctionPtr)] MirandaService fPtr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl,
            CharSet = CharSet.Ansi)]
        public delegate IntPtr CallServiceDelegate(string service,
            IntPtr wParam, IntPtr lParam);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl,
            CharSet = CharSet.Ansi)]
        public delegate IntPtr CallContactServiceDelegate(IntPtr hContact, 
            string charPtr, IntPtr wParam, IntPtr lParam);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void UnspecifiedDelegate();
        // TODO: Remove this from final version.

        [MarshalAs(UnmanagedType.FunctionPtr)]
        CreateHookableEventDelegate CreateHookableEvent;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        DestroyHookableEventDelegate DestroyHookableEvent;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        NotifyEventHooksDelegate NotifyEventHooks;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public HookEventDelegate HookEvent;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate HookEventMessage;        

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public UnhookEventDelegate UnhookEvent;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public CreateServiceFunctionDelegate CreateServiceFunction;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate CreateTransientServiceFunction;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate DestroyServiceFunction;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public CallServiceDelegate CallService;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate ServiceExists;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate CallServiceSync;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate CallFunctionAsync;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate SetHookDefaultForHookableEvent;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate CreateServiceFunctionParam;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate NotifyEventHooksDirect;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate CallProtoService;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public CallContactServiceDelegate CallContactService;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate HookEventParam;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate HookEventObj;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate HookEventObjParam;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate CreateServiceFunctionObj;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate CreateServiceFunctionObjParam;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate KillObjectServices;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        UnspecifiedDelegate KillObjectEventHooks;
    }
}
