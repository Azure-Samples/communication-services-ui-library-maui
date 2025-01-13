#!/bin/bash


if [ ! -d CommunicationCallingXamarinSampleApp ]; then
  cd .. 
fi


cd AndroidMauiBindings/Android.CallingUI.Bindings


if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

echo "Downloading CommunicationUILibrary Jars"
curl "https://repo1.maven.org/maven2/com/azure/android/azure-communication-ui-calling/1.13.0/azure-communication-ui-calling-1.13.0.aar" --output azure-communication-ui-calling-1.13.0.aar

echo "CommunicationUILibrary Jars Download Complete"
cd ../../

cd Android.Azure.Log.Bindings


if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

echo "Downloading log Jars"
curl  "https://repo1.maven.org/maven2/com/azure/android/azure-core-logging/1.0.0-beta.12/azure-core-logging-1.0.0-beta.12.aar" --output azure-core-logging-1.0.0-beta.12.aar

echo "log Jars Download Complete"
cd ../../

echo "Downloading CommunicationCore Jars"

cd Android.Azure.Core.Bindings

if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

curl  "https://repo1.maven.org/maven2/com/azure/android/azure-core/1.0.0-beta.12/azure-core-1.0.0-beta.12.aar" --output azure-core-1.0.0-beta.12.aar
curl  "https://repo1.maven.org/maven2/com/fasterxml/jackson/core/jackson-annotations/2.11.2/jackson-annotations-2.11.2.jar" --output jackson-annotations-2.11.2.jar
curl  "https://repo1.maven.org/maven2/com/fasterxml/jackson/core/jackson-core/2.11.2/jackson-core-2.11.2.jar" --output jackson-core-2.11.2.jar
curl  "https://repo1.maven.org/maven2/com/fasterxml/jackson/core/jackson-databind/2.11.3/jackson-databind-2.11.3.jar" --output jackson-databind-2.11.3.jar
curl  "https://repo1.maven.org/maven2/com/fasterxml/jackson/dataformat/jackson-dataformat-xml/2.11.2/jackson-dataformat-xml-2.11.2.jar" --output jackson-dataformat-xml-2.11.2.jar
curl  "https://repo1.maven.org/maven2/com/fasterxml/jackson/module/jackson-module-jaxb-annotations/2.11.2/jackson-module-jaxb-annotations-2.11.2.jar" --output jackson-module-jaxb-annotations-2.11.2.jar
echo "CommunicationCore Jars Download Complete"

cd ../../
echo "Downloading CommunicationCommon Jars"
cd Android.Azure.Common.Bindings

if [ ! -d "$Jars" ]; then
  mkdir Jars
fi

cd Jars

curl  "https://repo1.maven.org/maven2/net/sourceforge/streamsupport/android-retrofuture/1.7.3/android-retrofuture-1.7.3.jar" --output android-retrofuture-1.7.3.jar
curl  "https://repo1.maven.org/maven2/net/sourceforge/streamsupport/android-retrostreams/1.7.3/android-retrostreams-1.7.3.jar" --output android-retrostreams-1.7.3.jar
curl  "https://repo1.maven.org/maven2/com/azure/android/azure-communication-common/1.1.0/azure-communication-common-1.1.0.aar" --output azure-communication-common-1.1.0.aar
curl  "https://repo1.maven.org/maven2/com/azure/android/azure-core/1.0.0-beta.12/azure-core-1.0.0-beta.12.aar" --output azure-core-1.0.0-beta.12.aar
curl  "https://repo1.maven.org/maven2/com/azure/android/azure-core-logging/1.0.0-beta.12/azure-core-logging-1.0.0-beta.12.aar" --output azure-core-logging-1.0.0-beta.12.aar
echo "CommunicationCommon Jars Download Complete"
echo "Downloading aars..."

cd ../../../

cd CommunicationCallingSampleMauiApp

if [ ! -d "$aar" ]; then
  mkdir aar
fi

cd aar

curl  "https://repo1.maven.org/maven2/com/azure/android/azure-communication-calling/2.12.0/azure-communication-calling-2.12.0.aar" --output azure-communication-calling-2.12.0.aar
curl  "https://repo1.maven.org/maven2/com/microsoft/trouter-client-android/0.1.7/trouter-client-android-0.1.7.aar" --output trouter-client-android-0.1.7.aar
curl  "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_core/0.0.21/fluentui_core-0.0.21.aar" --output fluentui_core-0.0.21.aar
curl  "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_drawer/0.0.21/fluentui_drawer-0.0.21.aar" --output fluentui_drawer-0.0.21.aar
curl  "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_listitem/0.0.21/fluentui_listitem-0.0.21.aar" --output fluentui_listitem-0.0.21.aar
curl  "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_others/0.0.21/fluentui_others-0.0.21.aar" --output fluentui_others-0.0.21.aar
curl  "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_persona/0.0.21/fluentui_persona-0.0.21.aar" --output fluentui_persona-0.0.21.aar
curl  "https://repo1.maven.org/maven2/com/microsoft/fluentui/fluentui_transients/0.0.21/fluentui_transients-0.0.21.aar" --output fluentui_transients-0.0.21.aar
curl  "https://pkgs.dev.azure.com/MicrosoftDeviceSDK/DuoSDK-Public/_packaging/Duo-SDK-Feed/maven/v1/com/microsoft/device/dualscreen-layout/1.0.0-alpha01/dualscreen-layout-1.0.0-alpha01.aar" --output dualscreen-layout-1.0.0-alpha01.aar

echo "All required jars/aars downloaded"
