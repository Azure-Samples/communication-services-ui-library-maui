# Azure Communication UI Android Library .Net MAUI Support

This project demonstrates the integration of Communication UI library into .Net MAUI. 

### .Net MAUI Android Sample Application

The sample application has following dependencies.

* Bindings for `azure-communication-common`
* Bindings for `azure-core`
* Bindings for `azure-core-logging`
* Bindings for `azure-communication-ui-calling`
* .aar - All these aar's are included in sample application `aar` folder. 
  * azure-communication-calling
  * azure-core-logging
  * dualscreen-layout
  * fluentui_core
  * fluentui_drawer
  * fluentui_listitem
  * fluentui_others
  * fluentui_persona
  * fluentui_transients

## Download JAR's/AAR's

Run `downloadJarScript.sh` and all the required jar/aar's will be downloaded in their specific folders.

After cloning repo, open terminal and navigate to the `AndroidMauiBindings` folder and run the following command:

### Windows

Windows users can use powershell and run the script as an administator.

```cs
./downloadJarScript.sh
```

### Mac
```cs
chmod +x downloadJarScript.sh && ./downloadJarScript.sh
```


### .Net MAUI Android Bindings

To support .Net MAUI for Calling UI library, the following bindings are required. Bindings are required for `.aar` when `c#` code requires to access object.

`CommunicationUICallingLibrary` - Bindings for Azure Communication UI Calling library. The `C#` code requires to access `CallComposite` class from `azure-communication-ui-calling-*.aar`. This binding has following dependencies.

```xml
  <ItemGroup>
    <ProjectReference Include="..\azure_common_binding\azure_common_binding.csproj" />
    <ProjectReference Include="..\azure_core_binding\azure_core_binding.csproj" />
    <ProjectReference Include="..\azure_log_binding\azure_log_binding.csproj" />
  </ItemGroup>
```

`CommunicationCommon` - Bindings for `azure-communication-common-*.aar` are required for accessing class  `CommunicationTokenCredentials` in `C#`. This binding has following dependencies.

``` xml
  <ItemGroup>
    <AndroidLibrary Include="Jars\azure-communication-common-1.0.0.aar" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\azure_core_binding\azure_core_binding.csproj" />
  </ItemGroup>
```


`CommunicationCore` - Bindings for `azure-core-*.aar` are required to access `ExpandableStringEnum` in `C#`. This binding has following dependencies.

```xml
  <ItemGroup>
       <AndroidLibrary Include="Jars\jackson-annotations-2.11.2.jar" Bind="false" />
        <AndroidLibrary Include="Jars\jackson-core-2.11.2.jar" Bind="false" />
        <AndroidLibrary Include="Jars\jackson-databind-2.11.3.jar" Bind="false" />
        <AndroidLibrary Include="Jars\jackson-dataformat-xml-2.11.2.jar" Bind="false" />
        <AndroidLibrary Include="Jars\jackson-module-jaxb-annotations-2.11.2.jar" Bind="false" />
  </ItemGroup>
  <ItemGroup>
    <AndroidLibrary Include="Jars\azure-core-1.0.0-beta.9.aar" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\azure_log_binding\azure_log_binding.csproj" />
  </ItemGroup>
```

`AzureCoreLogging` - Bindings for `azure-core-logging-*.aa`. This binding has following dependencies.

```xml
  <ItemGroup>
     <PackageReference Include="Slf4jApi" Version="1.7.30">
    </PackageReference>
  </ItemGroup>
     
  <ItemGroup>
    <AndroidLibrary Include="Jars\azure-core-logging-1.0.0-beta.9.aar" />
  </ItemGroup>
```


## Launching Composite
The .NET MAUI library supports all the same features as the native UI [composite](https://github.com/Azure/communication-ui-library-android). 
Developers can launch the composite for a group call using a uuid. 

### Create Call Composite and Setup Authentication

```cs
CallComposite callComposite = new CallCompositeBuilder();

CommunicationTokenCredential credentials = new CommunicationTokenCredential("");
```

### Group Call

```cs
CallCompositeGroupCallLocator locator = new CallCompositeGroupCallLocator(UUID.FromString(callID));

CallCompositeRemoteOptions remoteOptions = new CallCompositeRemoteOptions(locator, credentials, name);

callComposite.Launch(MainActivity.Instance, remoteOptions);

```

### Teams interop

```cs
CallCompositeTeamsMeetingLinkLocator locator = new CallCompositeTeamsMeetingLinkLocator(callID);

CallCompositeRemoteOptions remoteOptions = new CallCompositeRemoteOptions(locator, credentials, name);

callComposite.Launch(MainActivity.Instance, remoteOptions);

```      

More information available in [QuickStart](https://docs.microsoft.com/en-us/azure/communication-services/quickstarts/ui-library/get-started-composites?tabs=kotlin&pivots=platform-android).
