namespace MyMauiApp;

public partial class SettingsPage : ContentPage
{
    public delegate void ProcessSettingsCallback(LocalizationProps localization, DataModelInjectionProps dataModelInjection);
    public event ProcessSettingsCallback Callback;

    String localAvatarName = "";
    String remoteAvatarName = "";
    LocalizationProps _localizationProps;
    DataModelInjectionProps _dataModelInjectionProps;

    public SettingsPage(IComposite callComposite, LocalizationProps localizationProps, DataModelInjectionProps dataModelInjectionProps)
    {
        InitializeComponent();

        _localizationProps = localizationProps;
        _dataModelInjectionProps = dataModelInjectionProps;
        languagePicker.ItemsSource = callComposite.languages();
        languagePicker.SelectedItem = _localizationProps.locale;
        SetLocalAvatarSelection(_dataModelInjectionProps.localAvatar);
        SetRemoteAvatarSelection(_dataModelInjectionProps.remoteAvatar);
    }

    void OnLeftToRightToggled(object sender, ToggledEventArgs e)
    {
        leftToRightToggle.IsToggled = e.Value;
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

            Callback(localization, dataModelInjection);
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
