@echo off

echo Patching version numbers...

rem Get current assembly version in environment variable
for /f %%i in ('PowerShell -File Scripts\get-version.ps1') do set CUSTOM_MAJOR_VERSION=%%i

set CUSTOM_VERSION=%CUSTOM_MAJOR_VERSION%.%APPVEYOR_BUILD_NUMBER%
set CUSTOM_INFORMATIONAL_VERSION=%CUSTOM_VERSION%

if %APPVEYOR_REPO_BRANCH%==master (
  if not [%APPVEYOR_PULL_REQUEST_NUMBER%]==[] (
    set CUSTOM_INFORMATIONAL_VERSION=%CUSTOM_INFORMATIONAL_VERSION%-pullrequest%APPVEYOR_PULL_REQUEST_NUMBER%
  )
) else (
  set CUSTOM_INFORMATIONAL_VERSION=%CUSTOM_INFORMATIONAL_VERSION%-prerelease
)

set APPVEYOR_BUILD_VERSION=%APPVEYOR_REPO_BRANCH%-%CUSTOM_INFORMATIONAL_VERSION%
appveyor UpdateBuild -Version "%APPVEYOR_BUILD_VERSION%"

echo Patched version: %CUSTOM_VERSION%
echo Patched informational version: %CUSTOM_INFORMATIONAL_VERSION%
echo AppVeyor build version: %APPVEYOR_BUILD_VERSION%