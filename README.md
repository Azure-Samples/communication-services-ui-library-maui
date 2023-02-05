![Hero Image](/mobile-ui-library-hero-image.png)

# Azure Communication UI Mobile Library for MAUI

This project demonstrates the integration of Azure Communication UI library into MAUI applications that utilizes the native Azure Communication UI library and Azure Communication Services to build a calling experience that features both voice and video calling.

## Features

Please refer to our native [UI Library overview](https://docs.microsoft.com/en-us/azure/communication-services/concepts/ui-library/ui-library-overview?pivots=platform-mobile)

## Prerequisites

- iOS [Requirements](https://github.com/Azure/communication-ui-library-ios#requirements)
- Android [Requirements](https://github.com/Azure/communication-ui-library-android#prerequisites)
- Visual Studio [Setup Instructions](https://docs.microsoft.com/en-us/xamarin/get-started/installation/?pivots=macos)
- Azure Communication Services Token [See example](https://docs.microsoft.com/azure/communication-services/tutorials/trusted-service-tutorial)

## Run Sample

Clone repo and open `CommunicationCallingSampleMauiApp.sln` in Visual Studio

#### For Android

1. Navigate to `/AndroidMauiBindings` and follow `README.md` to download dependencies for Android (section: `Download JAR's/AAR's`).

#### For iOS

1. Navigate to `/MAUIiOSBinding/ProxyLibs/CommunicationUI-Proxy` and in this directory in terminal run `sh iOSFramework -d`
2. Next navigate to `/MAUIiOSBinding/iOS.CallingUI.Binding` and build the `iOS.CallingUI.Binding.sln`. This will generate `iOS.CallingUI.Binding\bin` folder where it will have `iOS.CallingUI.Binding.dll` for you to use.

## Key Sample Highlights

### Folder Structure

```
| CommunicationCallingSampleMauiApp
    | Platforms/(Android | iOS)/Composite.cs -> Class to communicate with native binding libraries
    | MyMauiApp.sln -> MAUI application
| MAUIiOSBindings
    | iOS.CallingUI.Binding -> Bindings for Azure Communication UI library
    | ProxyLibs -> CommunicationUI proxy library to bridge swift methods to objective-c and generate frameworks
```

### Android and iOS Common code

The common code for Android and iOS is all under the `MyMauiApp.sln`
Depending on the platform we are running on we use the appropriate library.

```cs
#if ANDROID
using MyMauiApp.Platforms.Android;
#elif IOS
using MyMauiApp.Platforms.iOS;
#endif
```

`MainPage.xaml.cs` has common UI code for Android and iOS. On Button click, Android and iOS app is triggered to start a call.

```cs
Composite composite = new Composite();
string name = <DISPLAY_NAME>;
string acsToken = <TOKEN>;
string callId = <GROUP_CALL_ID>;
bool isTeamsCall = false;
composite.JoinCall(name, acsToken, callId, isTeamsCall);
```

### Bridging Guide

To learn more about how this sample was created and communicates with the native ACS Mobile UI Library, please refer to our briding guides:

[Android Bridging Guide](XamarinAndroidBindings/README.md)

[iOS Bridging Guide](MAUIiOSBindings/README.md)
