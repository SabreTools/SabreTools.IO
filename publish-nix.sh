#! /bin/bash

# This batch file assumes the following:
# - .NET 10.0 (or newer) SDK is installed and in PATH
#
# If any of these are not satisfied, the operation may fail
# in an unpredictable way and result in an incomplete output.

# Optional parameters
NO_BUILD=false
while getopts "b" OPTION
do
    case $OPTION in
    b)
        NO_BUILD=true
        ;;
    *)
        echo "Invalid option provided"
        exit 1
        ;;
    esac
done

# Set the current directory as a variable
BUILD_FOLDER=$PWD

# Only build if requested
if [ $NO_BUILD = false ]
then
    # Restore Nuget packages for all builds
    echo "Restoring Nuget packages"
    dotnet restore

    # Create published Nuget Package
    dotnet pack SabreTools.IO.Meta/SabreTools.IO.Meta.csproj --output $BUILD_FOLDER

    # Create unpublished Nuget Packages
    dotnet pack SabreTools.Collections.Extensions/SabreTools.Collections.Extensions.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.IO/SabreTools.IO.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.IO.Compression/SabreTools.IO.Compression.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.IO.Extensions/SabreTools.IO.Extensions.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Logging/SabreTools.Logging.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Matching/SabreTools.Matching.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Numerics/SabreTools.Numerics.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Numerics.Extensions/SabreTools.Numerics.Extensions.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Security.Cryptography/SabreTools.Security.Cryptography.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Text.ClrMamePro/SabreTools.Text.ClrMamePro.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Text.Compare/SabreTools.Text.Compare.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Text.Extensions/SabreTools.Text.Extensions.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Text.INI/SabreTools.Text.INI.csproj --output $BUILD_FOLDER
    dotnet pack SabreTools.Text.SeparatedValue/SabreTools.Text.SeparatedValue.csproj --output $BUILD_FOLDER
fi