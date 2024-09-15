namespace CommunicationCallingSampleMauiApp;

public partial class SettingsPage : ContentPage
{
    public delegate void ProcessSettingsCallback(LocalizationProps localization, DataModelInjectionProps dataModelInjection, OrientationProps orientationProps, CallControlProps callControlProps);
    public event ProcessSettingsCallback Callback;

    String localAvatarName = "";
    String remoteAvatarName = "";
    LocalizationProps _localizationProps;
    DataModelInjectionProps _dataModelInjectionProps;
    OrientationProps _orientationProps;

    public SettingsPage(IComposite callComposite, LocalizationProps localizationProps, DataModelInjectionProps dataModelInjectionProps, OrientationProps orientationProps)
    {
        InitializeComponent();

        _localizationProps = localizationProps;
        _dataModelInjectionProps = dataModelInjectionProps;
        _orientationProps = orientationProps;
        languagePicker.ItemsSource = callComposite.languages();
        languagePicker.SelectedItem = _localizationProps.locale;
        callScreenOrientationPicker.ItemsSource = callComposite.orientations();
        callScreenOrientationPicker.SelectedItem = _orientationProps.callScreenOrientation;
        setupScreenOrientationPicker.ItemsSource = callComposite.orientations();
        setupScreenOrientationPicker.SelectedItem = _orientationProps.setupScreenOrientation;
        SetLocalAvatarSelection(_dataModelInjectionProps.localAvatar);
        SetRemoteAvatarSelection(_dataModelInjectionProps.remoteAvatar);
    }

    void OnLeftToRightToggled(object sender, ToggledEventArgs e)
    {
        leftToRightToggle.IsToggled = e.Value;
    }

    void OnSkipSetupToggled(object sender, ToggledEventArgs e)
    {
        skipSetupScreenToggle.IsToggled = e.Value;
    }

    void OnMicOnToggled(object sender, ToggledEventArgs e)
    {
        onMicrophoneOnToggle.IsToggled = e.Value;
    }

    void OnCameraOnToggled(object sender, ToggledEventArgs e)
    {
        onCameraOnToggle.IsToggled = e.Value;
    }

    void onDisableLeaveCallConfirmationToggled(object sender, ToggledEventArgs e)
    {
        onDisableLeaveCallConfirmation.IsToggled = e.Value;
    }

    void onUpdateTitleWithParticipantCountToggled(object sender, ToggledEventArgs e)
    {
        updateTitleWithParticipantCount.IsToggled = e.Value;
    }

    async void OnDismissButtonClicked(object sender, EventArgs args)
    {
        if (Callback != null)
        {
            LocalizationProps localization = new LocalizationProps();
            localization.locale = languagePicker.SelectedItem.ToString();
            localization.isLeftToRight = leftToRightToggle.IsToggled;

            DataModelInjectionProps dataModelInjection = new DataModelInjectionProps();
            dataModelInjection.localAvatar = localAvatarName;
            dataModelInjection.remoteAvatar = remoteAvatarName;

            OrientationProps orientationProps = new OrientationProps();
            orientationProps.setupScreenOrientation = setupScreenOrientationPicker.SelectedItem.ToString();
            orientationProps.callScreenOrientation = callScreenOrientationPicker.SelectedItem.ToString();

            CallControlProps callControlProps = new CallControlProps();
            callControlProps.isSkipSetupON = skipSetupScreenToggle.IsToggled;
            callControlProps.isMicrophoneON = onMicrophoneOnToggle.IsToggled;
            callControlProps.isCameraON = onCameraOnToggle.IsToggled;
            callControlProps.isDisableLeaveCallConfirmation = onDisableLeaveCallConfirmation.IsToggled;
            callControlProps.title = callScreenTitle.Text;
            callControlProps.subtitle = callScreenSubtitle.Text;
            callControlProps.updateSubtitleOnParticipantCountChange = updateTitleWithParticipantCount.IsToggled;
            Callback(localization, dataModelInjection, orientationProps, callControlProps);
        }

        await Navigation.PopModalAsync(true);
    }

    void OnLocalCatAvatarClicked(object sender, EventArgs e)
    {
        localAvatarName = (localAvatarName != "cat.png") ? "cat.png" : "";
        SetLocalAvatarSelection(localAvatarName);
        ReloadControl(sender);
    }

    void OnLocalFoxAvatarClicked(object sender, EventArgs e)
    {
        localAvatarName = (localAvatarName != "fox.png") ? "fox.png" : "";
        SetLocalAvatarSelection(localAvatarName);
        ReloadControl(sender);
    }

    void OnLocalKoalaAvatarClicked(object sender, EventArgs e)
    {
        localAvatarName = (localAvatarName != "koala.png") ? "koala.png" : "";
        SetLocalAvatarSelection(localAvatarName);
        ReloadControl(sender);
    }

    void OnLocalMonkeyAvatarClicked(object sender, EventArgs e)
    {
        localAvatarName = (localAvatarName != "monkey.png") ? "monkey.png" : "";
        SetLocalAvatarSelection(localAvatarName);
        ReloadControl(sender);
    }

    void OnLocalMouseAvatarClicked(object sender, EventArgs e)
    {
        localAvatarName = (localAvatarName != "mouse.png") ? "mouse.png" : "";
        SetLocalAvatarSelection(localAvatarName);
        ReloadControl(sender);
    }

    void OnLocalOctopusAvatarClicked(object sender, EventArgs e)
    {
        localAvatarName = (localAvatarName != "octopus.png") ? "octopus.png" : "";
        SetLocalAvatarSelection(localAvatarName);
        ReloadControl(sender);
    }

    void OnRemoteCatAvatarClicked(object sender, EventArgs e)
    {
        remoteAvatarName = (remoteAvatarName != "cat.png") ? "cat.png" : "";
        SetRemoteAvatarSelection(remoteAvatarName);
        ReloadControl(sender);
    }

    void OnRemoteFoxAvatarClicked(object sender, EventArgs e)
    {
        remoteAvatarName = (remoteAvatarName != "fox.png") ? "fox.png" : "";
        SetRemoteAvatarSelection(remoteAvatarName);
        ReloadControl(sender);
    }

    void OnRemoteKoalaAvatarClicked(object sender, EventArgs e)
    {
        remoteAvatarName = (remoteAvatarName != "koala.png") ? "koala.png" : "";
        SetRemoteAvatarSelection(remoteAvatarName);
        ReloadControl(sender);
    }

    void OnRemoteMonkeyAvatarClicked(object sender, EventArgs e)
    {
        remoteAvatarName = (remoteAvatarName != "monkey.png") ? "monkey.png" : "";
        SetRemoteAvatarSelection(remoteAvatarName);
        ReloadControl(sender);
    }

    void OnRemoteMouseAvatarClicked(object sender, EventArgs e)
    {
        remoteAvatarName = (remoteAvatarName != "mouse.png") ? "mouse.png" : "";
        SetRemoteAvatarSelection(remoteAvatarName);
        ReloadControl(sender);
    }

    void OnRemoteOctopusAvatarClicked(object sender, EventArgs e)
    {
        remoteAvatarName = (remoteAvatarName != "octopus.png") ? "octopus.png" : "";
        SetRemoteAvatarSelection(remoteAvatarName);
        ReloadControl(sender);
    }

    void SetLocalAvatarSelection(string avatarName)
    {
       localAvatarName = avatarName;
       localCatBtn.Source = (localAvatarName == "cat.png") ? ImageSource.FromFile("cat_selected.png") : ImageSource.FromFile("cat.png");
       localFoxBtn.Source = (localAvatarName == "fox.png") ? ImageSource.FromFile("fox_selected.png") : ImageSource.FromFile("fox.png");
       localKoalaBtn.Source = (localAvatarName == "koala.png") ? ImageSource.FromFile("koala_selected.png") : ImageSource.FromFile("koala.png");
       localMonkeyBtn.Source = (localAvatarName == "monkey.png") ? ImageSource.FromFile("monkey_selected.png") : ImageSource.FromFile("monkey.png");
       localMouseBtn.Source = (localAvatarName == "mouse.png") ? ImageSource.FromFile("mouse_selected.png") : ImageSource.FromFile("mouse.png");
       localOctopusBtn.Source = (localAvatarName == "octopus.png") ? ImageSource.FromFile("octopus_selected.png") : ImageSource.FromFile("octopus.png");
    }

    void SetRemoteAvatarSelection(string avatarName)
    {
        remoteAvatarName = avatarName;
       remoteCatBtn.Source = (remoteAvatarName == "cat.png") ? ImageSource.FromFile("cat_selected.png") : ImageSource.FromFile("cat.png");
       remoteFoxBtn.Source = (remoteAvatarName == "fox.png") ? ImageSource.FromFile("fox_selected.png") : ImageSource.FromFile("fox.png");
       remoteKoalaBtn.Source = (remoteAvatarName == "koala.png") ? ImageSource.FromFile("koala_selected.png") : ImageSource.FromFile("koala.png");
       remoteMonkeyBtn.Source = (remoteAvatarName == "monkey.png") ? ImageSource.FromFile("monkey_selected.png") : ImageSource.FromFile("monkey.png");
       remoteMouseBtn.Source = (remoteAvatarName == "mouse.png") ? ImageSource.FromFile("mouse_selected.png") : ImageSource.FromFile("mouse.png");
       remoteOctopusBtn.Source = (remoteAvatarName == "octopus.png") ? ImageSource.FromFile("octopus_selected.png") : ImageSource.FromFile("octopus.png");
    }

    void ReloadControl(object sender)
    {
        (sender as ImageButton).RaiseImageSourcePropertyChanged();
    }
}

public struct LocalizationProps
{
    public string locale;
    public Boolean isLeftToRight;
}

public struct DataModelInjectionProps
{
    public string localAvatar;
    public string remoteAvatar;
}

public struct OrientationProps
{
    public string setupScreenOrientation;
    public string callScreenOrientation;
}

public struct CallControlProps
{
    public Boolean isSkipSetupON;
    public Boolean isMicrophoneON;
    public Boolean isCameraON;
    public Boolean isDisableLeaveCallConfirmation;
    public String title;
    public String subtitle;
    public Boolean updateSubtitleOnParticipantCountChange;
}
