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

/*
 * Main HellAPI plugin controller.
 */
#include <Windows.h>

#include <newpluginapi.h>

#include "constants.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::IO;
using namespace System::Reflection;
using namespace Hell;

#define MIRANDA_EXPORT extern "C" __declspec(dllexport)

/* Main plugin info. */
PLUGININFOEX pluginInfo =
{
    sizeof(PLUGININFOEX),
    "Hell plugin controller",
    PLUGIN_MAKE_VERSION(0,0,1,0),
    "Plugin that controls managed .NET plugins.",
    "ForNeVeR",
    "neverthness@gmail.com",
    "Copyright 2010-2011 ForNeVeR",
    "http://fornever.no-ip.org/",
    UNICODE_AWARE,
    0,
    // {3E83A62E-8C17-4810-9872-EB4577802F98}
    { 0x3e83a62e, 0x8c17, 0x4810,
        { 0x98, 0x72, 0xeb, 0x45, 0x77, 0x80, 0x2f, 0x98 } }
};

/* Plugin interfaces list. */
static MUUID interfaces[] = { MIID_TESTPLUGIN, MIID_LAST };

/* Pointer to library instance. */
HINSTANCE hInstance;

namespace Hell
{
    /* Class for storing loaded plugins. */
    ref class PluginCollection
    {
    public:
        static Plugin ^ManagerPlugin;
    };
}

#pragma unmanaged

BOOL WINAPI DllMain(HINSTANCE instance, DWORD, LPVOID)
{
    hInstance = instance;
    return TRUE;
}

#pragma managed

/* This function loads plugin manager assembly and creates manager instance. */
void GetManager()
{
    List<Type ^> ^pluginsForLoading = gcnew List<Type ^>();

    String ^path =
        Path::GetDirectoryName(Assembly::GetExecutingAssembly()->Location);
    DirectoryInfo ^directory = gcnew DirectoryInfo(path);	

    // First, create a list of all managed plugin types.
    for each(FileInfo ^file in directory->GetFiles("*.dll"))
    {
        try
        {
            Assembly ^assembly = Assembly::LoadFile(file->FullName);

            if (file->Name == MANAGER_PLUGIN_NAME ".dll")
            {
                continue;
            }

            array<Type ^> ^types = assembly->GetExportedTypes();
            for each(Type ^type in types)
            {
                if(type->GetCustomAttributes(MirandaPluginAttribute::typeid,
                    false)->Length != 0)
                {
                    pluginsForLoading->Add(type);
                }
            }
        }
        catch(BadImageFormatException ^)
        {
            // Do nothing. File is not managed plugin.
        }
    }

    // Create plugin manager instance.
    Assembly ^managerAssembly = Assembly::LoadFile(path +
        "\\" MANAGER_PLUGIN_NAME ".dll");

    array<Object ^> ^args = gcnew array<Object ^>(1);
    args[0] = pluginsForLoading;

    PluginCollection::ManagerPlugin = safe_cast<Plugin ^>(
        managerAssembly->CreateInstance(MANAGER_TYPE_NAME, false,
        BindingFlags::Default, nullptr, args, nullptr, nullptr));
}

/* A function that returns pointer to PLUGININFOEX structure. */
MIRANDA_EXPORT PLUGININFOEX *MirandaPluginInfoEx(DWORD mirandaVersion)
{
    GetManager();
    
    return &pluginInfo;
}

/* A function that returns interfaces list. */
MIRANDA_EXPORT const MUUID *MirandaPluginInterfaces()
{
    return interfaces;
}

/* A function called on plugin load. */
MIRANDA_EXPORT int Load(PLUGINLINK *pluginLink)
{
    // Load manager plugin:
    array<Object ^> ^args = gcnew array<Object ^>(2);
    args[0] = gcnew IntPtr(hInstance);
    args[1] = gcnew IntPtr(pluginLink);

    PluginCollection::ManagerPlugin->GetType()->InvokeMember("Load",
        BindingFlags::InvokeMethod, nullptr, PluginCollection::ManagerPlugin,
        args);

    return 0;
}

/* A function called on plugin unload. */
MIRANDA_EXPORT int Unload()
{
    // Unload manager plugin:
    PluginCollection::ManagerPlugin->Unload();

    return 0;
}