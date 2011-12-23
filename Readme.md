HellAPI
=======

License
-------

Copyright (C) 2010-2011 by ForNeVeR

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

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
suggestions, feel free to contact author:

    ForNeVeR
    jabber: revenrof@jabber.ru
	e-mail: neverthness@gmail.com
