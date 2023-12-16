using Projekt.Models.Common.Utilities;
using Projekt.ViewModels;
using Projekt.Views.ContentViews;
using System.Runtime.CompilerServices;

namespace Projekt;

public partial class App : Application
{
	public App(Projekt.Views.MainPage mainpage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(mainpage);
	
	}

    protected async override void OnStart()
    {
        base.OnStart();
		AuthUtilities authUtilities = this.Handler.MauiContext.Services.GetService<AuthUtilities>();
		await authUtilities.SetupAdminAccount();
    }
}
