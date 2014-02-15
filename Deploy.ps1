param(
	[switch] $X64
)

$deployPath = 'Deploy'

if (Test-Path $deployPath) {
    Remove-Item -Recurse $deployPath
}

mkdir $deployPath
mkdir $deployPath\Plugins
mkdir $deployPath\Plugins\Hell

# Hell Adapter
$adapterPath = if ($X64) { 'Debug' } else { 'x64\Debug' }
Copy-Item $adapterPath\HellAdapter.dll $deployPath\Plugins
Copy-Item $adapterPath\HellAdapter.pdb $deployPath\Plugins

# Hell API
$bin = if ($X64) { 'x86' } else { 'x64' }
Copy-Item HellAPI\bin\$bin\Debug\* $deployPath

# Hell Manager
Copy-Item HellManager\bin\$bin\Debug\* $deployPath\Plugins\Hell