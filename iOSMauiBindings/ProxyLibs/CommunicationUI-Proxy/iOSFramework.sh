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

/usr/bin/xcodebuild -sdk iphoneos -configuration Release -workspace CommunicationUI-Proxy.xcworkspace -scheme CommunicationUI-Proxy -verbose CODE_SIGNING_ALLOWED=NO SYMROOT=$(PWD)/'DeviceFramework'   build
/usr/bin/xcodebuild -sdk iphonesimulator -configuration Release -workspace CommunicationUI-Proxy.xcworkspace -scheme CommunicationUI-Proxy -verbose CODE_SIGNING_ALLOWED=NO SYMROOT=$(PWD)/'SimulatorFramework'  build


UNIVERSAL_OUTPUTFOLDER=$(PWD)/'Framework'/${CONFIGURATION}'-universal'

# make sure the output directory exists
mkdir -p "${UNIVERSAL_OUTPUTFOLDER}"

/usr/bin/xcodebuild -create-xcframework -framework $(PWD)/'DeviceFramework'/'Release-iphoneos'/'CommunicationUI_Proxy.framework' -framework $(PWD)/'SimulatorFramework'/'Release-iphonesimulator'/'CommunicationUI_Proxy.framework' -output "${UNIVERSAL_OUTPUTFOLDER}"/'CommunicationUI_Proxy.xcframework'
/usr/bin/xcodebuild -create-xcframework -framework $(PWD)/'DeviceFramework'/'Release-iphoneos'/'AzureCommunicationCommon'/'AzureCommunicationCommon.framework' -framework $(PWD)/'SimulatorFramework'/'Release-iphonesimulator'/'AzureCommunicationCommon'/'AzureCommunicationCommon.framework' -output "${UNIVERSAL_OUTPUTFOLDER}"/'AzureCommunicationCommon.xcframework'
/usr/bin/xcodebuild -create-xcframework -framework $(PWD)/'DeviceFramework'/'Release-iphoneos'/'AzureCommunicationUICalling'/'AzureCommunicationUICalling.framework' -framework $(PWD)/'SimulatorFramework'/'Release-iphonesimulator'/'AzureCommunicationUICalling'/'AzureCommunicationUICalling.framework' -output "${UNIVERSAL_OUTPUTFOLDER}"/'AzureCommunicationUICalling.xcframework'
/usr/bin/xcodebuild -create-xcframework -framework $(PWD)/'DeviceFramework'/'Release-iphoneos'/'AzureCore'/'AzureCore.framework' -framework $(PWD)/'SimulatorFramework'/'Release-iphonesimulator'/'AzureCore'/'AzureCore.framework' -output "${UNIVERSAL_OUTPUTFOLDER}"/'AzureCore.xcframework'
/usr/bin/xcodebuild -create-xcframework -framework $(PWD)/'DeviceFramework'/'Release-iphoneos'/'MicrosoftFluentUI'/'FluentUI.framework' -framework $(PWD)/'SimulatorFramework'/'Release-iphonesimulator'/'MicrosoftFluentUI'/'FluentUI.framework' -output "${UNIVERSAL_OUTPUTFOLDER}"/'FluentUI.xcframework'
/usr/bin/xcodebuild -create-xcframework -framework $(PWD)/'DeviceFramework'/'Release-iphoneos'/'XCFrameworkIntermediates'/'AzureCommunicationCalling'/'AzureCommunicationCalling.framework' -framework $(PWD)/'SimulatorFramework'/'Release-iphonesimulator'/'XCFrameworkIntermediates'/'AzureCommunicationCalling'/'AzureCommunicationCalling.framework' -output "${UNIVERSAL_OUTPUTFOLDER}"/'AzureCommunicationCalling.xcframework'


