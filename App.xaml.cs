﻿namespace Projekt;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	
	}
    protected override void OnStart()
    {
        base.OnStart();
		Shell.Current.GoToAsync("mainpage", false);
    }
}
