@echo off

echo Running webserver...
start powershell.exe -ExecutionPolicy Bypass -File ".\1RunServer.ps1"
echo Running local clients...
powershell.exe -ExecutionPolicy Bypass -File ".\2RunLocalClients.ps1" -NoNewWindow

echo Startup completed.