#!/bin/bash

if [ -z "$1" ] 
then
	echo "First argument must be -d: For Debug or -r: For Release"
	exit  	
fi
ARG=$1

if [ ${ARG} == "-d" ] 
then
	CONFIGURATION="Debug"
elif [ ${ARG} == "-r" ]
then
	CONFIGURATION="Release"
else
	echo "Unknown Argument: Configuration must be -d Debug or -r Release"
	exit
fi
echo "Configuration set is ${CONFIGURATION}"

/usr/bin/xcodebuild clean
pod install
mkdir -p "$(PWD)/SimulatorFramework"
mkdir -p "$(PWD)/DeviceFramework"

/usr/bin/xcodebuild -sdk iphoneos -configuration ${CONFIGURATION} -workspace CommunicationUI-Proxy.xcworkspace -scheme CommunicationUI-Proxy -verbose CODE_SIGNING_ALLOWED=NO SYMROOT=$(PWD)/'DeviceFramework'  build
/usr/bin/xcodebuild -sdk iphonesimulator -configuration ${CONFIGURATION} -workspace CommunicationUI-Proxy.xcworkspace -scheme CommunicationUI-Proxy -verbose CODE_SIGNING_ALLOWED=NO ARCHS="x86_64" ONLY_ACTIVE_ARCH=NO SYMROOT=$(PWD)/'SimulatorFramework' build

UNIVERSAL_OUTPUTFOLDER=$(PWD)/'Framework'/${CONFIGURATION}'-universal'

# make sure the output directory exists
mkdir -p "${UNIVERSAL_OUTPUTFOLDER}"

# Copy Device(arm64) Framework at fresh universal folder location 
cp -af $(PWD)/'SimulatorFramework'/${CONFIGURATION}'-iphonesimulator'/'CommunicationUI_Proxy.framework' "${UNIVERSAL_OUTPUTFOLDER}/"
cp -af $(PWD)/'SimulatorFramework'/${CONFIGURATION}'-iphonesimulator'/'AzureCommunicationCommon'/'AzureCommunicationCommon.framework' "${UNIVERSAL_OUTPUTFOLDER}/"
cp -af $(PWD)/'SimulatorFramework'/${CONFIGURATION}'-iphonesimulator'/'AzureCommunicationUICalling'/'AzureCommunicationUICalling.framework' "${UNIVERSAL_OUTPUTFOLDER}/"
cp -af $(PWD)/'SimulatorFramework'/${CONFIGURATION}'-iphonesimulator'/'MicrosoftFluentUI'/'FluentUI.framework' "${UNIVERSAL_OUTPUTFOLDER}/"
cp -af $(PWD)/'SimulatorFramework'/${CONFIGURATION}'-iphonesimulator'/'XCFrameworkIntermediates'/'AzureCommunicationCalling'/'AzureCommunicationCalling.framework' "${UNIVERSAL_OUTPUTFOLDER}/"

# Create universal binary file using lipo and place the combined executable in the copied framework directory
lipo -create "$(PWD)/SimulatorFramework/${CONFIGURATION}-iphonesimulator/CommunicationUI_Proxy.framework/CommunicationUI_Proxy" "$(PWD)/DeviceFramework/${CONFIGURATION}-iphoneos/CommunicationUI_Proxy.framework/CommunicationUI_Proxy" -output "${UNIVERSAL_OUTPUTFOLDER}/CommunicationUI_Proxy.framework/CommunicationUI_Proxy"

lipo -create "$(PWD)/SimulatorFramework/${CONFIGURATION}-iphonesimulator/AzureCommunicationCommon/AzureCommunicationCommon.framework/AzureCommunicationCommon" "$(PWD)/DeviceFramework/${CONFIGURATION}-iphoneos/AzureCommunicationCommon/AzureCommunicationCommon.framework/AzureCommunicationCommon" -output "${UNIVERSAL_OUTPUTFOLDER}/AzureCommunicationCommon.framework/AzureCommunicationCommon"

lipo -create "$(PWD)/SimulatorFramework/${CONFIGURATION}-iphonesimulator/AzureCommunicationUICalling/AzureCommunicationUICalling.framework/AzureCommunicationUICalling" "$(PWD)/DeviceFramework/${CONFIGURATION}-iphoneos/AzureCommunicationUICalling/AzureCommunicationUICalling.framework/AzureCommunicationUICalling" -output "${UNIVERSAL_OUTPUTFOLDER}/AzureCommunicationUICalling.framework/AzureCommunicationUICalling"

lipo -create "$(PWD)/SimulatorFramework/${CONFIGURATION}-iphonesimulator/MicrosoftFluentUI/FluentUI.framework/FluentUI" "$(PWD)/DeviceFramework/${CONFIGURATION}-iphoneos/MicrosoftFluentUI/FluentUI.framework/FluentUI" -output "${UNIVERSAL_OUTPUTFOLDER}/FluentUI.framework/FluentUI"

lipo -create "$(PWD)/SimulatorFramework/${CONFIGURATION}-iphonesimulator/XCFrameworkIntermediates/AzureCommunicationCalling/AzureCommunicationCalling.framework/AzureCommunicationCalling" "$(PWD)/DeviceFramework/${CONFIGURATION}-iphoneos/XCFrameworkIntermediates/AzureCommunicationCalling/AzureCommunicationCalling.framework/AzureCommunicationCalling" -output "${UNIVERSAL_OUTPUTFOLDER}/AzureCommunicationCalling.framework/AzureCommunicationCalling"
