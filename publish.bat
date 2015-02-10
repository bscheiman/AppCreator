@echo off
del *.nupkg
.nuget\NuGet.exe pack AppCreator.nuspec
.nuget\NuGet.exe pack AppCreator.UI.nuspec
.nuget\NuGet.exe push AppCreator.%1.nupkg %NUGET%
.nuget\NuGet.exe push AppCreator.UI.%1.nupkg %NUGET%