﻿using System;

namespace CommunicationCallingSampleMauiApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnJoinCallButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new JoinCallPage());
    }
}


