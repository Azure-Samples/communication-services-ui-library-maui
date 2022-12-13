![Hero Image](/mobile-ui-library-hero-image.png)

# Azure Communication UI Mobile Library for Xamarin

This project demonstrates the integration of Azure Communication UI library into Xamarin Forms that utilizes the native Azure Communication UI library and Azure Communication Services to build a calling experience that features both voice and video calling. 


## Features

Please refer to our native [UI Library overview](https://docs.microsoft.com/en-us/azure/communication-services/concepts/ui-library/ui-library-overview?pivots=platform-mobile)

## Prerequisites

- iOS [Requirements](https://github.com/Azure/communication-ui-library-ios#requirements)
- Android [Requirements](https://github.com/Azure/communication-ui-library-android#prerequisites)
- Visual Studio [Setup Instructions](https://docs.microsoft.com/en-us/xamarin/get-started/installation/?pivots=macos)
- Azure Communication Services Token [See example](https://docs.microsoft.com/azure/communication-services/tutorials/trusted-service-tutorial)


## Run Sample

Clone repo and open `CommunicationCallingXamarinSampleApp.sln` in Visual Studio

#### For Android
1. Navigate to `/XamarinAndroidBindings/` and in this directory in terminal run `sh downloadJarScript` [Learn More](XamarinAndroidBindings/README.md#download-jarsaars)

Set `CommunicationCallingXamarinSampleApp.Android` as start up project, build solution and select a device or emulator to run application.

#### For iOS
1. Navigate to `/XamariniOSBindings/ProxyLibs/CommunicationUI-Proxy` and in this directory in terminal run `sh iOSFramework -d` [Learn More](https://github.com/Azure-Samples/communication-services-ui-library-xamarin/tree/refactor/cleanup/XamariniOSBindings#create-frameworks)
2. Next navigate to `/XamariniOSBindings/CommunicationUIProxy.Binding` and build the `CommunicationUIProxy.Binding.sln`.  This will generate `CommunicationUIProxy.Binding\bin` folder where it will have `NativeLibrary.dll` for you to use. 

Set `CommunicationCallingXamarinSampleApp.iOS` as start up project, build, and select a device or emulator to run application.

## Key Sample Highlights

### Folder Structure

```
| CommunicationCallingXamarinSampleApp
    | CommunicationCallingXamarinSampleApp.Android -> Android Xamarin sample application.
    | CommunicationCallingXamarinSampleApp.Android -> iOS Xamarin sample application.
    | CommunicationCallingXamarinSampleApp -> Common code for Android and iOS (UI).
| XamarinAndroidBindings
    | CommunicationUILibrary -> Bindings for Azure Communication UI library 
    | CommunicationCommon -> Bindings for communication common (required for object `CommunicationTokenCredentials`)
    | CommunicationCore -> Bindings for communication core (required for object `ExpandableStringEnum`)
| XamariniOSBindings
    | CommunicationUIProxy.Binding -> Bindings for Azure Communication UI library 
    | ProxyLibs -> CommunicationUI proxy library to bridge swift methods to objective-c
```

### Android and iOS Common code 

The common code for Android and iOS include `UI` and `interface` to trigger a call in Android and iOS specific projects.

`CommunicationCallingXamarinSampleApp/JoinCallPage.xaml.cs` has common UI code for Android and iOS. On Button click, Android and iOS app is triggered to start a call.

```cs
void OnButtonClicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tokenEntry.Text) && !String.IsNullOrEmpty(meetingEntry.Text))
            {
                callComposite.joinCall(name.Text, tokenEntry.Text, meetingEntry.Text, isTeamsCall, _localization, _dataModelInjection);
            }
        }
```


`CommunicationCallingXamarinSampleApp/IComposite.cs` has common interface used by Android and iOS sample application to start a call.

```cs
namespace CommunicationCallingXamarinSampleApp
{
    public interface IComposite
    {
        void joinCall(string name, string acsToken, string callID, bool isTeamsCall, LocalizationProps? localization, DataModelInjectionProps? dataModelInjection);
        List<string> languages();
    }
}

```

### Bridging Guide

To learn more about how this sample was created and communicates with the native ACS Mobile UI Library, please refer to our briding guides:

[Android Bridging Guide](XamarinAndroidBindings/README.md)

[iOS Bridging Guide](XamariniOSBindings/README.md)




