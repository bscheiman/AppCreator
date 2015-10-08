#!/bin/bash

mcs -r:System.Xml.Linq.dll SyncNuGet.cs && mono SyncNuGet.exe AppCreator/packages.config AppCreator.nuspec && rm SyncNuGet.exe
./build.sh 2.1.`date +%s` $1
