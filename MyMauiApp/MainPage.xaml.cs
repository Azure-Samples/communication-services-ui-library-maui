#if ANDROID
using MyMauiApp.Platforms.Android;
#elif IOS
using MyMauiApp.Platforms.iOS;
#endif
using System;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		Composite composite = new Composite();
		string name = "";
		string acsToken = "";
		string callId = "";
		bool isTeamsCall = false;
		composite.JoinCall(name, acsToken, callId, isTeamsCall);
	}
}


