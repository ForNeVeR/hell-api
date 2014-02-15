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
    /// <summary>
    /// Interface for Miranda plugin. Note that you must NOT try to use
    /// HInstance or PluginLink fields from constructor. They become available
    /// only after adapter calls your Load() method.
    /// </summary>
    public abstract class Plugin
    {
        /// <summary>
        /// Handle of DLL instance.
        /// </summary>
        protected IntPtr HInstance { get; private set; }

		/// <summary>
		/// Miranda language pack handle.
		/// </summary>
		protected int HLangpack { get; private set; }

        /// <summary>
        /// This method will be called first on plugin creation.
        /// </summary>
        /// <param name="hInstance">
        /// Handle of DLL instance.
        /// </param>
		/// <param name="hLangpack">Miranda language pack handle.</param>
        public void Load(IntPtr hInstance, int hLangpack)
        {
            HInstance = hInstance;
	        HLangpack = hLangpack;
            Load();
        }

        /// <summary>
        /// This method will be called by adapter system when all preparations
        /// for plugin loading done (i.e. calling private Load method).
        /// </summary>
        protected abstract void Load();

        /// <summary>
        /// This method called on plugin unloading (for example, on Miranda
        /// exit).
        /// </summary>
        public abstract void Unload();

        #region Miranda core api

        [DllImport("mir_core.dll", EntryPoint="CreateHookableEvent")]
        internal static extern IntPtr m_CreateHookableEvent(string name);
        protected IntPtr CreateHookableEvent(string name)
        {
            return Plugin.m_CreateHookableEvent(name);
        }

        [DllImport("mir_core.dll", EntryPoint="DestroyHookableEvent")]
        internal static extern int m_DestroyHookableEvent(IntPtr hEvent);
        protected int DestroyHookableEvent(IntPtr hEvent)
        {
            return Plugin.m_DestroyHookableEvent(hEvent);
        }

        [DllImport("mir_core.dll", EntryPoint="SetHookDefaultForHookableEvent")]
        internal static extern int m_SetHookDefaultForHookableEvent(IntPtr hEvent, MirandaHook hookProc);
        protected int SetHookDefaultForHookableEvent(IntPtr hEvent, MirandaHook hookProc)
        {
            return Plugin.m_SetHookDefaultForHookableEvent(hEvent, hookProc);
        }

        [DllImport("mir_core.dll", EntryPoint="CallPluginEventHook")]
        internal static extern int m_CallPluginEventHook(IntPtr hInstance, IntPtr hEvent, IntPtr wParam, IntPtr lParam);
        protected int CallPluginEventHook(IntPtr hInstance, IntPtr hEvent, IntPtr wParam, IntPtr lParam)
        {
            return Plugin.m_CallPluginEventHook(hInstance, hEvent, wParam, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="NotifyEventHooks")]
        internal static extern int m_NotifyEventHooks(IntPtr hEvent, IntPtr wParam, IntPtr lParam);
        protected int NotifyEventHooks(IntPtr hEvent, IntPtr wParam, IntPtr lParam)
        {
            return Plugin.m_NotifyEventHooks(hEvent, wParam, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="NotifyFastHook")]
        internal static extern int m_NotifyFastHook(IntPtr hEvent, IntPtr wParam, IntPtr lParam);
        protected int NotifyFastHook(IntPtr hEvent, IntPtr wParam, IntPtr lParam)
        {
            return Plugin.m_NotifyFastHook(hEvent, wParam, lParam);
        }

        //[DllImport("mir_core.dll", EntryPoint="GetSubscribersCount")]
        //internal static extern int GetSubscribersCount(THook* pHook);

        [DllImport("mir_core.dll", EntryPoint="HookEvent")]
        internal static extern IntPtr m_HookEvent(string name, MirandaHook hookProc);
        protected IntPtr HookEvent(string name, MirandaHook hookProc)
        {
            return Plugin.m_HookEvent(name, hookProc);
        }

        [DllImport("mir_core.dll", EntryPoint="HookEventParam")]
        internal static extern IntPtr m_HookEventParam(string name, MirandaHookParam hookProc, IntPtr lParam);
        protected IntPtr HookEventParam(string name, MirandaHookParam hookProc, IntPtr lParam)
        {
            return Plugin.m_HookEventParam(name, hookProc, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="HookEventObj")]
        internal static extern IntPtr m_HookEventObj(string name, MirandaHookObj hookProc, IntPtr pObject);
        protected IntPtr HookEventObj(string name, MirandaHookObj hookProc, IntPtr pObject)
        {
            return Plugin.m_HookEventObj(name, hookProc, pObject);
        }

        [DllImport("mir_core.dll", EntryPoint="HookEventObjParam")]
        internal static extern IntPtr m_HookEventObjParam(string name, MirandaHookObjParam hookProc, IntPtr pObject, IntPtr lParam);
        protected IntPtr HookEventObjParam(string name, MirandaHookObjParam hookProc, IntPtr pObject, IntPtr lParam)
        {
            return Plugin.m_HookEventObjParam(name, hookProc, pObject, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="HookEventMessage")]
        internal static extern IntPtr m_HookEventMessage(string name, IntPtr hwnd, uint message);
        protected IntPtr HookEventMessage(string name, IntPtr hwnd, uint message)
        {
            return Plugin.m_HookEventMessage(name, hwnd, message);
        }

        [DllImport("mir_core.dll", EntryPoint="UnhookEvent")]
        internal static extern int m_UnhookEvent(IntPtr hHook);
        protected int UnhookEvent(IntPtr hHook)
        {
            return Plugin.m_UnhookEvent(hHook);
        }

        [DllImport("mir_core.dll", EntryPoint="KillModuleEventHooks")]
        internal static extern void m_KillModuleEventHooks(IntPtr hInstance);
        protected void KillModuleEventHooks(IntPtr hInstance)
        {
            Plugin.m_KillModuleEventHooks(hInstance);
        }

        [DllImport("mir_core.dll", EntryPoint="KillObjectEventHooks")]
        internal static extern void m_KillObjectEventHooks(IntPtr pObject);
        protected void KillObjectEventHooks(IntPtr pObject)
        {
            Plugin.m_KillObjectEventHooks(pObject);
        }

        [DllImport("mir_core.dll", EntryPoint="CreateServiceFunction")]
        internal static extern IntPtr m_CreateServiceFunction(string name, MirandaService serviceProc);
        protected IntPtr CreateServiceFunction(string name, MirandaService serviceProc)
        {
            return Plugin.m_CreateServiceFunction(name, serviceProc);
        }

        [DllImport("mir_core.dll", EntryPoint="CreateServiceFunctionParam")]
        internal static extern IntPtr m_CreateServiceFunctionParam(string name, MirandaServiceParam serviceProc, IntPtr lParam);
        protected IntPtr CreateServiceFunctionParam(string name, MirandaServiceParam serviceProc, IntPtr lParam)
        {
            return Plugin.m_CreateServiceFunctionParam(name, serviceProc, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="CreateServiceFunctionObj")]
        internal static extern IntPtr m_CreateServiceFunctionObj(string name, MirandaServiceObj serviceProc, IntPtr pObject);
        protected IntPtr CreateServiceFunctionObj(string name, MirandaServiceObj serviceProc, IntPtr pObject)
        {
            return Plugin.m_CreateServiceFunctionObj(name, serviceProc, pObject);
        }

        [DllImport("mir_core.dll", EntryPoint="CreateServiceFunctionObjParam")]
        internal static extern IntPtr m_CreateServiceFunctionObjParam(string name, MirandaServiceObjParam serviceProc, IntPtr pObject, IntPtr lParam);
        protected IntPtr CreateServiceFunctionObjParam(string name, MirandaServiceObjParam serviceProc, IntPtr pObject, IntPtr lParam)
        {
            return Plugin.m_CreateServiceFunctionObjParam(name, serviceProc, pObject, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="DestroyServiceFunction")]
        internal static extern int m_DestroyServiceFunction(IntPtr hService);
        protected int DestroyServiceFunction(IntPtr hService)
        {
            return Plugin.m_DestroyServiceFunction(hService);
        }

        [DllImport("mir_core.dll", EntryPoint="ServiceExists")]
        internal static extern bool m_ServiceExists(string name);
        protected bool ServiceExists(string name)
        {
             return Plugin.m_ServiceExists(name);
        }

        [DllImport("mir_core.dll", EntryPoint="CallService")]
        internal static extern IntPtr m_CallService(string name, IntPtr wParam, IntPtr lParam);
        protected IntPtr CallService(string name, IntPtr wParam, IntPtr lParam)
        {
            return Plugin.m_CallService(name, wParam, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="CallServiceSync")]
        internal static extern IntPtr m_CallServiceSync(string name, IntPtr wParam, IntPtr lParam);
        protected IntPtr CallServiceSync(string name, IntPtr wParam, IntPtr lParam)
        {
            return Plugin.m_CallServiceSync(name, wParam, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint="CallFunctionAsync")]
        internal static extern int m_CallFunctionAsync(AsyncFunc callback, IntPtr arg);
        protected int CallFunctionAsync(AsyncFunc callback, IntPtr arg)
        {
            return Plugin.m_CallFunctionAsync(callback, arg);
        }

        [DllImport("mir_core.dll", EntryPoint="KillModuleServices")]
        internal static extern void m_KillModuleServices(IntPtr hInstance);
        protected void KillModuleServices(IntPtr hInstance)
        {
            Plugin.m_KillModuleServices(hInstance);
        }

        [DllImport("mir_core.dll", EntryPoint="KillObjectServices")]
        internal static extern void m_KillObjectServices(IntPtr pObject);
        protected void KillObjectServices(IntPtr pObject)
        {
            Plugin.m_KillObjectServices(pObject);
        }

        [DllImport("mir_core.dll", EntryPoint="RegisterModule")]
        internal static extern void m_RegisterModule(IntPtr hInstance);
        protected void RegisterModule(IntPtr hInstance)
        {
            Plugin.m_RegisterModule(hInstance);
        }

        [DllImport("mir_core.dll", EntryPoint="UnregisterModule")]
        internal static extern void m_UnregisterModule(IntPtr hInstance);
        protected void UnregisterModule(IntPtr hInstance)
        {
            Plugin.m_UnregisterModule(hInstance);
        }

        [DllImport("mir_core.dll", EntryPoint="GetInstByAddress")]
        internal static extern IntPtr m_GetInstByAddress(IntPtr codePtr);
        protected IntPtr GetInstByAddress(IntPtr codePtr)
        {
            return Plugin.m_GetInstByAddress(codePtr);
        }

        [DllImport("mir_core.dll", EntryPoint = "CallContactService")]
        internal static extern IntPtr m_CallContactService(IntPtr hContact, string name, IntPtr wParam, IntPtr lParam);
        protected IntPtr CallContactService(IntPtr hContact, string name, IntPtr wParam, IntPtr lParam)
        {
            return Plugin.m_CallContactService(hContact, name, wParam, lParam);
        }

        [DllImport("mir_core.dll", EntryPoint = "CallProtoService")]
        internal static extern IntPtr m_CallProtoService(string module, string name, IntPtr wParam, IntPtr lParam);
        protected IntPtr CallProtoService(string module, string name, IntPtr wParam, IntPtr lParam)
        {
            return Plugin.m_CallProtoService(module, name, wParam, lParam);
        }

        #endregion
    }
}
