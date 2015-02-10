@echo off
IF "%~1"=="" GOTO ERROR

@REM "%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" /p:Configuration=Release /t:Clean /t:Rebuild /m
del *.nupkg
.nuget\NuGet.exe pack AppCreator.nuspec -Version %1
.nuget\NuGet.exe pack AppCreator.UI.nuspec -Version %1

for %%i in (AppCreator*.nupkg) do .nuget\NuGet.exe push %%i %NUGET%

:ERROR
echo Please specify version