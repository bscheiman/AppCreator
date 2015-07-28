@echo off
IF "%~1"=="" GOTO ERROR
cls

"%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" /p:Configuration=Release /t:Clean /t:Rebuild
del *.nupkg
.nuget\NuGet.exe pack AppCreator.nuspec -Version %1
.nuget\NuGet.exe pack AppCreator.UI.nuspec -Version %1

for %%i in (AppCreator*.nupkg) do .nuget\NuGet.exe push %%i %NUGET%
goto DONE

:ERROR
echo Please specify version

:DONE