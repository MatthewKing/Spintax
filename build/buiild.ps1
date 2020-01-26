$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$project =  "$root/src/Spintax"
$output = "$root/artifacts"
dotnet pack $project --configuration Release --output $output
