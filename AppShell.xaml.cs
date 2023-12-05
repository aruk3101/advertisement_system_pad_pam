using Projekt.Models.Common.Utilities;
using Projekt.ViewModels;
using Projekt.Views;
using System.Diagnostics;

namespace Projekt;

public partial class AppShell : Shell
{
    public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("landing", typeof(MainPage));
        Routing.RegisterRoute("dashboard", typeof(DashBoardPage));
        Routing.RegisterRoute("login", typeof(LoginPage));
		Routing.RegisterRoute("registration", typeof(RegistrationPage));
        Routing.RegisterRoute("user", typeof(UserProfilePage));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        AuthUtilities.LogOut();
        App.Current.MainPage = new NavigationPage(this.Handler.MauiContext.Services.GetService<MainPage>());
    }
}
