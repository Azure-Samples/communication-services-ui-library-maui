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

Clone repo and open `CommunicationCallingSampleMauiApp/CommunicationCallingSampleMauiApp.sln` in Visual Studio

#### For Android

##### Visual Studio Mac/Windows 2022

1. Open `CommunicationCallingSampleMauiApp/CommunicationCallingSampleMauiApp.csproj` and set `<TargetFrameworks>net7.0-android</TargetFrameworks>`.
2. Navigate to `/AndroidMauiBindings` and in this directory in terminal run `./downloadJarScript.sh`. `GitBash` or `Windows Subsystem for Linux (WSL)` should be enabled to run `.sh` on Windows.
3. Select android device/emulator in visual studio and run `CommunicationCallingSampleMauiApp` app.

#### For iOS

##### Visual Studio Mac 2022

1. Open `CommunicationCallingSampleMauiApp/CommunicationCallingSampleMauiApp.csproj` and set `<TargetFrameworks>net7.0-ios</TargetFrameworks>`.
2. Navigate to `communication-services-ui-library-maui/iOSMauiBindings/ProxyLibs/CommunicationUI-Proxy` and in this directory in terminal run `./iOSFramework -d`.
3. Select iOS device/simulator in visual studio and run `CommunicationCallingSampleMauiApp` app.

### Folder Structure

```
| CommunicationCallingSampleMauiApp
    | Platforms/(Android | iOS)/Composite.cs -> Class to communicate with native binding libraries
    | CommunicationCallingSampleMauiApp.sln -> MAUI application
| MAUIiOSBindings
    | iOS.CallingUI.Binding -> Bindings for Azure Communication UI library
| AndroidMauiBindings
    | Android.CallingUI.Bindings -> Bindings for Azure Communication UI library
```

### Android and iOS Common code

The common code for Android and iOS is all under the `CommunicationCallingSampleMauiApp.sln`
Depending on the platform we are running on we use the appropriate library.

```cs
#if ANDROID
using CommunicationCallingSampleMauiApp.Platforms.Android;
#elif IOS
using CommunicationCallingSampleMauiApp.Platforms.iOS;
#endif
```

`JoinCallPage.xaml.cs` has common UI code for Android and iOS. On Button click, Android and iOS app is triggered to start a call.

```cs
    void OnButtonClicked(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(tokenEntry.Text) && !String.IsNullOrEmpty(meetingEntry.Text))
        {
            callComposite.joinCall(name.Text, tokenEntry.Text, meetingEntry.Text, isTeamsCall, _localization, _dataModelInjection);
        }
    }
```

### Bridging Guide

To learn more about how this sample was created and communicates with the native ACS Mobile UI Library, please refer to our bridging guides:

[Android Bridging Guide](AndroidMauiBindings/README.md)

[iOS Bridging Guide](iOSMauiBindings/README.md)

### Known Issues

[Known Issues](https://github.com/Azure-Samples/communication-services-ui-library-maui/wiki/Known-Issues)
