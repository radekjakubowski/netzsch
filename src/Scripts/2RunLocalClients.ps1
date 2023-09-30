$CurrentPath = Get-Location
$LocalApplicationRelativePath = "..\LocalApplication\"

$LocalAppPath = Join-Path -Path $CurrentPath -ChildPath $LocalApplicationRelativePath

Set-Location $LocalAppPath
dotnet build

Set-Location ".\bin\Debug\net7.0-windows"
$numberOfInstances = 5

for ($i = 1; $i -le $numberOfInstances; $i++) {
    $clientId = "Client$i"
    Start-Process -FilePath "dotnet" -ArgumentList "LocalApplication.dll", "ClientId=$clientId" -NoNewWindow
    Start-Sleep -Seconds 1
}