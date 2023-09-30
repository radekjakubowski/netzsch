$CurrentPath = Get-Location
$WebServerRelativePath = "..\WebServer\"

$WebServerPath = Join-Path -Path $CurrentPath -ChildPath $WebServerRelativePath

Set-Location $WebServerPath
dotnet watch