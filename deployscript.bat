@ECHO OFF 

set COMMITCOMMENT=%1
IF [%COMMITCOMMENT%] EQU [] goto :nocomment
IF [%COMMITCOMMENT%] EQU [""] goto :nocomment

@ECHO ON

cd "%~dp0NuGetPackages"

BlueChilli.Nuget.exe deploy "%~dp0BCKit.nuspec" "BCKit.Droid"
BlueChilli.Nuget.exe deploy "%~dp0BCKit.nuspec" "BCKit.iOS"