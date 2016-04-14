@ECHO OFF 

set COMMITCOMMENT=%1
IF [%COMMITCOMMENT%] EQU [] goto :nocomment
IF [%COMMITCOMMENT%] EQU [""] goto :nocomment

@ECHO ON

xxcopy "ReactiveUI\bin\Release\*\ReactiveUI*.dll" "Nuget\ReactiveUI-Core\lib\" /R /s /y
xxcopy "ReactiveUI.AndroidSupport\bin\Release\*\ReactiveUI.AndroidSupport*.dll" "Nuget\ReactiveUI-AndroidSupport\lib\" /R /s /y
xxcopy "ReactiveUI.Events\bin\Release\*\ReactiveUI.Events*.dll" "Nuget\ReactiveUI-Events\lib\" /R /s /y
xxcopy "ReactiveUI.XamForms\bin\Release\*\ReactiveUI.XamForms*.dll" "Nuget\ReactiveUI-XamForms\lib\" /R /s /y
xxcopy "ReactiveUI.Events\bin\Release\Portable-Net45+WinRT45+WP8+MonoAndroid10+MonoTouch10\ReactiveUI.Events_*.dll" "Nuget\ReactiveUI-XamForms\lib\Portable-Net45+WinRT45+WP8+MonoAndroid10+MonoTouch10\" /R /s /y
xxcopy "ReactiveUI.Events\bin\Release\Xamarin.iOS10\ReactiveUI.Events_*.dll" "Nuget\ReactiveUI-XamForms\lib\Xamarin.iOS10\" /R /s /y



cd "%~dp0Nuget"

nuget pack "ReactiveUI-Core\ReactiveUI-Core.nuspec"
nuget pack "ReactiveUI\ReactiveUI.nuspec"
nuget pack "ReactiveUI-AndroidSupport\ReactiveUI-AndroidSupport.nuspec"
nuget pack "ReactiveUI-Events\ReactiveUI-Events.nuspec"
nuget pack "ReactiveUI-XamForms\ReactiveUI-XamForms.nuspec"


nuget push "*.nupkg" 1660902d-ef34-4ce3-8217-bef69d90ba6b -s https://www.myget.org/F/bluechilli-mobile/api/v2/package
