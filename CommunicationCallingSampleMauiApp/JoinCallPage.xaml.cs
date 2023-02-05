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

    LocalizationProps _localization;
    DataModelInjectionProps _dataModelInjection;

    public JoinCallPage()
    {
        InitializeComponent();

        _localization = new LocalizationProps();
        _localization.locale = "en";
        _localization.isLeftToRight = true;

        _dataModelInjection = new DataModelInjectionProps();
        _dataModelInjection.localAvatar = "";
        _dataModelInjection.remoteAvatar = "";
        groupCallFrame.IsVisible = true;
        teamsCallFrame.IsVisible = false;
    }

    async void OnToolbarClicked(object sender, EventArgs e)
    {
        SettingsPage settingsPage = new SettingsPage(callComposite, _localization, _dataModelInjection);
        settingsPage.Callback += new SettingsPage.ProcessSettingsCallback(ProcessSettings);
        await Navigation.PushModalAsync(settingsPage);
    }

    void ProcessSettings(LocalizationProps localization, DataModelInjectionProps dataModelInjection)
    {
        _localization = localization;
        _dataModelInjection = dataModelInjection;
        Console.WriteLine("locale is " + localization.locale + " isLeftToRight is " + localization.isLeftToRight);
    }

    void OnGroupCallClicked(object sender, EventArgs e)
    {
        if (isTeamsCall)
        {
            isTeamsCall = false;
        }
        groupCallFrame.IsVisible = true;
        teamsCallFrame.IsVisible = false;
        teamsMeetingPivot.TextColor = Color.FromHex("#6E6E6E");
        groupCallPivot.TextColor = Colors.White;
        meetingTitleLabel.Text = groupCallTitle;
        meetingEntry.Placeholder = groupCallEntryPlaceholder;
        meetingSubtitleLabel.Text = groupCallSubtitle;
    }

    void OnTeamsMeetingClicked(object sender, EventArgs e)
    {
        if (!isTeamsCall)
        {
            isTeamsCall = true;
        }
        groupCallFrame.IsVisible = false;
        teamsCallFrame.IsVisible = true;
        groupCallPivot.TextColor = Color.FromHex("#6E6E6E");
        teamsMeetingPivot.TextColor = Colors.White;
        meetingTitleLabel.Text = teamsMeetingTitle;
        meetingEntry.Placeholder = teamsMeetingEntryPlaceholder;
        meetingSubtitleLabel.Text = teamsMeetingSubtitle;
    }

    void OnButtonClicked(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(tokenEntry.Text) && !String.IsNullOrEmpty(meetingEntry.Text))
        {
            callComposite.joinCall(name.Text, tokenEntry.Text, meetingEntry.Text, isTeamsCall, _localization, _dataModelInjection);
        }
    }

}