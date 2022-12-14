#!/bin/bash


if [ ! -d CommunicationCallingXamarinSampleApp ]; then
  cd .. 
fi


cd MAUIAndroidBindings/acs_ui_library_calling_binding


if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

echo "Downloading CommunicationUILibrary Jars"
curl -0 -L "https://repo1.maven.org/maven2/com/azure/android/azure-communication-ui-calling/1.0.0/azure-communication-ui-calling-1.0.0.aar" --output azure-communication-ui-calling-1.0.0.aar

echo "CommunicationUILibrary Jars Download Complete"
cd ../../

cd azure_log_binding


if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

echo "Downloading log Jars"
curl -0 -L "https://repo1.maven.org/maven2/com/azure/android/azure-core-logging/1.0.0-beta.9/azure-core-logging-1.0.0-beta.9.aar" --output azure-core-logging-1.0.0-beta.9.aar

echo "log Jars Download Complete"
cd ../../

echo "Downloading CommunicationCore Jars"

cd azure_core_binding

if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

curl -0 -L "https://repo1.maven.org/maven2/com/azure/android/azure-core/1.0.0-beta.9/azure-core-1.0.0-beta.9.aar" --output azure-core-1.0.0-beta.9.aar
curl -0 -L "https://repo1.maven.org/maven2/com/fasterxml/jackson/core/jackson-annotations/2.11.2/jackson-annotations-2.11.2.jar" --output jackson-annotations-2.11.2.jar
curl -0 -L "https://repo1.maven.org/maven2/com/fasterxml/jackson/core/jackson-core/2.11.2/jackson-core-2.11.2.jar" --output jackson-core-2.11.2.jar
curl -0 -L "https://repo1.maven.org/maven2/com/fasterxml/jackson/core/jackson-databind/2.11.3/jackson-databind-2.11.3.jar" --output jackson-databind-2.11.3.jar
curl -0 -L "https://repo1.maven.org/maven2/com/fasterxml/jackson/dataformat/jackson-dataformat-xml/2.11.2/jackson-dataformat-xml-2.11.2.jar" --output jackson-dataformat-xml-2.11.2.jar
curl -0 -L "https://repo1.maven.org/maven2/com/fasterxml/jackson/module/jackson-module-jaxb-annotations/2.11.2/jackson-module-jaxb-annotations-2.11.2.jar" --output jackson-module-jaxb-annotations-2.11.2.jar
echo "CommunicationCore Jars Download Complete"

cd ../../
echo "Downloading CommunicationCommon Jars"
cd azure_common_binding

if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

curl -0 -L "https://repo1.maven.org/maven2/net/sourceforge/streamsupport/android-retrofuture/1.7.3/android-retrofuture-1.7.3.jar" --output android-retrofuture-1.7.3.jar
curl -0 -L "https://repo1.maven.org/maven2/net/sourceforge/streamsupport/android-retrostreams/1.7.3/android-retrostreams-1.7.3.jar" --output android-retrostreams-1.7.3.jar
curl -0 -L "https://repo1.maven.org/maven2/com/azure/android/azure-communication-common/1.0.0/azure-communication-common-1.0.0.aar" --output azure-communication-common-1.0.0.aar
curl -0 -L "https://repo1.maven.org/maven2/com/azure/android/azure-core/1.0.0-beta.9/azure-core-1.0.0-beta.9.aar" --output azure-core-1.0.0-beta.9.aar
curl -0 -L "https://repo1.maven.org/maven2/com/azure/android/azure-core-logging/1.0.0-beta.9/azure-core-logging-1.0.0-beta.9.aar" --output azure-core-logging-1.0.0-beta.9.aar
echo "CommunicationCommon Jars Download Complete"
echo "Downloading aars..."

cd ../../../

cd MyMauiApp

if [ ! -d "$aar" ]; then
  mkdir aar
fi

cd aar

curl -0 -L "https://repo1.maven.org/maven2/com/azure/android/azure-communication-calling/2.2.0/azure-communication-calling-2.2.0.aar" --output azure-communication-calling-2.2.0.aar
curl -0 -L "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_core/0.0.20/fluentui_core-0.0.20.aar" --output fluentui_core-0.0.20.aar
curl -0 -L "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_drawer/0.0.20/fluentui_drawer-0.0.20.aar" --output fluentui_drawer-0.0.20.aar
curl -0 -L "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_listitem/0.0.20/fluentui_listitem-0.0.20.aar" --output fluentui_listitem-0.0.20.aar
curl -0 -L "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_others/0.0.20/fluentui_others-0.0.20.aar" --output fluentui_others-0.0.20.aar
curl -0 -L "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_persona/0.0.20/fluentui_persona-0.0.20.aar" --output fluentui_persona-0.0.20.aar
curl -0 -L "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_transients/0.0.20/fluentui_transients-0.0.20.aar" --output fluentui_transients-0.0.20.aar
curl -0 -L "https://pkgs.dev.azure.com/MicrosoftDeviceSDK/DuoSDK-Public/_packaging/Duo-SDK-Feed/maven/v1/com/microsoft/device/dualscreen-layout/dualscreen-layout-1.0.0-alpha01.aar" --output dualscreen-layout-1.0.0-alpha01.aar

echo "All required jars/aars downloaded"