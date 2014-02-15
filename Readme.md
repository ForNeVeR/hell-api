HellAPI
=======

HellAPI is a CLI (Mono and .NET supported) library set for writing managed .NET plugins for the Miranda NG client.

Building
--------

### Cloning the repository
Don't forget to clone git submodule when cloning this repository. It can be done by adding the `--recursive` option
when cloning or by issuing the `git submodule init` command *after* the initial cloning.

### Compilation
Compilation process is straightforward: load a solution file (`Hell.sln`) in your favorite IDE (for example, Visual
Studio 2010) and invoke the Build command.

### Deployment
There is a `Deploy.ps1` script for cleaning a `Deploy` and preparing the file layout into it. Use `-X64` switch if you
need to deploy x64 versions of the plugins.

Plugin layout
-------------

For proper functioning HellAPI requires you to put following files to following places (relative to the Miranda NG root
directory):

    ./HellAPI.dll
    ./Plugins/HellAdapter.dll
    ./Plugins/Hell/HellManager.dll

Any Hell-based plugins must be also placed in the `./Plugins/Hell/` directory.

Writing plugins
---------------

### Sample plugins
See project TestPlugin as a simple plugin example (this is also ported version of original Miranda TestPlugin from
the official SDK). There are also bunch of other samples inside the repository.

### Library levels
In HellAPI, there are two namespaces: `Hell.FirstCircle` and `Hell.LastCircle`. Ideally, all things your plugin uses
should be from `FirstCircle`; use `LastCircle` or direct Miranda API / Interop calls only as a last resort.

Contact authors
---------------

Full Miranda SDK is very big and somewhat uneasy thing to port. So HellAPI is mostly developed on demand. If you need
any feature you can make a feature request (or make the feature yourself).

Feel free to contact author:

    ForNeVeR
    jabber: fornever@codingteam.org.ru
    e-mail: neverthness@gmail.com
