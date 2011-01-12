/*
 * Main HellAPI plugin controller.
 */
#include <Windows.h>

#include <newpluginapi.h>
#include <m_clist.h>

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
    "© 2010 ForNeVeR",
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
        static List<Plugin ^> plugins;
    };
}

#pragma unmanaged

BOOL WINAPI DllMain(HINSTANCE instance, DWORD, LPVOID)
{
    hInstance = instance;
    return TRUE;
}

#pragma managed

/* This function loads all managed plugins into memory. */
void LoadSubplugins()
{
    String ^path =
        Path::GetDirectoryName(Assembly::GetExecutingAssembly()->Location);
    DirectoryInfo ^directory = gcnew DirectoryInfo(path);
    for each(FileInfo ^file in directory->GetFiles())
    {
        try
        {
            Assembly ^assembly = Assembly::LoadFile(file->FullName);
            array<Type ^> ^types = assembly->GetExportedTypes();
            for each(Type ^type in types)
            {
                if(type->GetCustomAttributes(MirandaPluginAttribute::typeid,
                    false)->Length != 0)
                {
                    IntPtr ^instance = gcnew IntPtr(hInstance);
                    array<Object ^> ^args = gcnew array<Object ^>(1);
                    args[0] = instance;

                    PluginCollection::plugins.Add(safe_cast<Plugin ^>(
                        assembly->CreateInstance(type->FullName, false,
                        BindingFlags::Default, nullptr, args, nullptr,
                        nullptr)));
                }
            }
        }
        catch (BadImageFormatException ^)
        {
            // Do nothing. File is not managed plugin.
        }
    }
}

/* A function that returns pointer to PLUGININFOEX structure. */
MIRANDA_EXPORT PLUGININFOEX *MirandaPluginInfoEx(DWORD mirandaVersion)
{
    LoadSubplugins();
    
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
    // Load all plugins:
    for each(Plugin ^plugin in PluginCollection::plugins)
    {
        plugin->Load(IntPtr(pluginLink));
    }

    return 0;
}

/* A function called on plugin unload. */
MIRANDA_EXPORT int Unload()
{
    for each(Plugin ^plugin in PluginCollection::plugins)
    {
        plugin->Unload();
    }
    PluginCollection::plugins.Clear();

    return 0;
}