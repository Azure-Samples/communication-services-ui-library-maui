#if ANDROID
using CommunicationCallingSampleMauiApp.Platforms.Android;
#elif IOS
using CommunicationCallingSampleMauiApp.Platforms.iOS;
#endif
namespace CommunicationCallingSampleMauiApp;

public partial class JoinCallPage : ContentPage
{
    IComposite callComposite = new Composite();
    bool isTeamsCall = false;

    const String groupCallTitle = "Group call ID";
    const String groupCallEntryPlaceholder = "Enter call ID";
    const String groupCallSubtitle = "Start a call to get a call ID.";

    const String teamsMeetingTitle = "Teams meeting";
    const String teamsMeetingEntryPlaceholder = "Enter invite link";
    const String teamsMeetingSubtitle = "Get link from the meeting invite or anyone in the call.";

    const String onetoNCallTitle = "1 to N call";
    const String onetoNCallPlaceholder = "Enter comma separated MRIs";
    const String onetoNCallSubtitle = "Get CommunicationIdentifiers MRIs to dial.";

    LocalizationProps _localization;
    DataModelInjectionProps _dataModelInjection;
    OrientationProps _orientationProps;
    CallControlProps _callControlProps;
    private CallType callType;

    public JoinCallPage()
    {
        InitializeComponent();

        _localization = new LocalizationProps();
        _localization.locale = "en";
        _localization.isLeftToRight = true;

        _orientationProps = new OrientationProps();
        _orientationProps.setupScreenOrientation = "PORTRAIT";
        _orientationProps.callScreenOrientation = "USER";

        _dataModelInjection = new DataModelInjectionProps();
        _dataModelInjection.localAvatar = "";
        _dataModelInjection.remoteAvatar = "";

        _callControlProps = new CallControlProps();
        _callControlProps.isSkipSetupON = false;
        _callControlProps.isMicrophoneON = false;
        _callControlProps.isCameraON = false;

        groupCallFrame.IsVisible = true;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = false;
    }

    async void OnToolbarClicked(object sender, EventArgs e)
    {
        SettingsPage settingsPage = new SettingsPage(callComposite, _localization, _dataModelInjection, _orientationProps);
        settingsPage.Callback += new SettingsPage.ProcessSettingsCallback(ProcessSettings);
        await Navigation.PushModalAsync(settingsPage);
    }

    void ProcessSettings(LocalizationProps localization, DataModelInjectionProps dataModelInjection, OrientationProps orientationProps, CallControlProps callControlProps)
    {
        _localization = localization;
        _dataModelInjection = dataModelInjection;
        _orientationProps = orientationProps;
        _callControlProps = callControlProps;
        Console.WriteLine("locale is " + localization.locale + " isLeftToRight is " + localization.isLeftToRight);
    }

    void OnGroupCallClicked(object sender, EventArgs e)
    {
        callType = CallType.GroupCall;
        groupCallFrame.IsVisible = true;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = false;
        teamsMeetingPivot.TextColor = Color.FromHex("#6E6E6E");
        onetoNCallPivot.TextColor = Color.FromHex("#6E6E6E");
        groupCallPivot.TextColor = Colors.White;
        meetingTitleLabel.Text = groupCallTitle;
        meetingEntry.Placeholder = groupCallEntryPlaceholder;
        meetingSubtitleLabel.Text = groupCallSubtitle;
    }

    void OnTeamsMeetingClicked(object sender, EventArgs e)
    {
        callType = CallType.TeamsCall;
        groupCallFrame.IsVisible = false;
        teamsCallFrame.IsVisible = true;
        onetoNCallFrame.IsVisible = false;
        groupCallPivot.TextColor = Color.FromHex("#6E6E6E");
        onetoNCallPivot.TextColor = Color.FromHex("#6E6E6E");
        teamsMeetingPivot.TextColor = Colors.White;
        meetingTitleLabel.Text = teamsMeetingTitle;
        meetingEntry.Placeholder = teamsMeetingEntryPlaceholder;
        meetingSubtitleLabel.Text = teamsMeetingSubtitle;
    }

    void On1ToNCallClicked(object sender, EventArgs e)
    {
        callType = CallType.OneToN;
        groupCallFrame.IsVisible = false;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = true;
        groupCallPivot.TextColor = Color.FromHex("#6E6E6E");
        teamsMeetingPivot.TextColor = Color.FromHex("#6E6E6E");
        onetoNCallPivot.TextColor = Colors.White; ;
        meetingTitleLabel.Text = onetoNCallTitle;
        meetingEntry.Placeholder = onetoNCallPlaceholder;
        meetingSubtitleLabel.Text = onetoNCallSubtitle;
    }

    void OnButtonClicked(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(tokenEntry.Text) && !String.IsNullOrEmpty(meetingEntry.Text))
        {
            callComposite.joinCall(name.Text, tokenEntry.Text, meetingEntry.Text, callType, _localization, _dataModelInjection, _orientationProps, _callControlProps);
        }
    }
}

public enum CallType
{
    TeamsCall,
    GroupCall,
    OneToN
}