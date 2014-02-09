HellAPI
=======

What the Hell is HellAPI?
-------------------------

HellAPI is a CLR (Mono and .NET support) library set for writing
managed .NET plugins for Miranda IM client.

Compilation
-----------

Compilation process is straightforward: load a solution file
(`Hell.sln`) in your favorite IDE (for example, Visual Studio) and
invoke Build command.

Plugin layout
-------------

For proper functioning HellAPI requires you to put following files to
following places (relative to Miranda root directory):

    ./HellAPI.dll
    ./Plugins/HellAdapter.dll
    ./Plugins/Hell/HellManager.dll

Any Hell-based plugins must be also placed in the `./Plugins/Hell/`
directory.

Writing plugins
---------------

See project TestPlugin as a simple plugin example (this is also ported
version of original Miranda TestPlugin from the official SDK).

Library levels
--------------

In HellAPI, there are two namespaces: `Hell.FirstCircle` and
`Hell.LastCircle`. Ideally, all things you use must be from
FirstCircle; use LastCircle or direct Miranda API / Interop calls only
as last resort.

Contact authors
---------------

Full Miranda SDK is very big and somewhat uneasy thing to port. So
HellAPI is nostly developed on demand. You need feature - you make
feature (and make it available on github ;) ).

So if you need HellAPI to be extended and / or have any patches or
suggestions, feel free to contact authors:

    ForNeVeR
    jabber: fornever@codingteam.org.ru
	e-mail: neverthness@gmail.com
