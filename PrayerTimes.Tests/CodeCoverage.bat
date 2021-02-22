@echo off

REM Clean and build the project
dotnet clean
dotnet build /p:DebugType=Full

REM Instrument assemblies in our test project to detect hits to source files on our main project
minicover instrument --workdir ../ --assemblies PrayerTimes.Tests/**/bin/**/*.dll --sources PrayerTimes.Library/**/*.cs --exclude-sources PrayerTimes.Console/*.cs


REM Reset previous counters
minicover reset --workdir ../

REM Run the tests
dotnet test --no-build

REM Uninstrument assemblies in case we want to deploy
minicover uninstrument --workdir ../

REM Print the console report
minicover report --workdir ../ --threshold 70

REM Generate the HTML report
minicover htmlreport --workdir ../ --threshold 70