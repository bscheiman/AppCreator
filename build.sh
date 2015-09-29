#!/bin/bash

if [ -z "$1" ]
  then
    echo "Version argument required"
    exit
fi

rm *.nupkg 2>/dev/null
rm /Users/bscheiman/NuGet/AppCreator*.nupkg 2>/dev/null

xbuild /t:Clean
xbuild /t:Rebuild /p:Configuration=Release
nuget pack AppCreator.nuspec -Version $1 -Verbosity detailed
nuget pack AppCreator.UI.nuspec -Version $1 -Verbosity detailed

cp -f *.nupkg /Users/bscheiman/NuGet/

if [ "$2" = "push" ]; then
  nuget push AppCreator.$1.nupkg
  nuget push AppCreator.UI.$1.nupkg
fi
