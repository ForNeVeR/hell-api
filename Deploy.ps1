$deployPath = 'Deploy'

if (Test-Path $deployPath) {
    Remove-Item -Recurse $deployPath
}

mkdir $deployPath
mkdir $deployPath\Plugins
mkdir $deployPath\Plugins\Hell

# Hell Adapter
Copy-Item Debug\HellAdapter.dll $deployPath\Plugins
Copy-Item Debug\HellAdapter.pdb $deployPath\Plugins

# Hell API
Copy-Item HellAPI\bin\x86\Debug\* $deployPath

# Hell Manager
Copy-Item HellManager\bin\x86\Debug\* $deployPath\Plugins\Hell