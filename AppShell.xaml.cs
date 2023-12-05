using Projekt.Models.Common.Utilities;
using Projekt.ViewModels;
using Projekt.Views;

namespace Projekt;

public partial class AppShell : Shell
{
    public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("mainpage", typeof(MainPage));
		Routing.RegisterRoute("login", typeof(LoginPage));
		Routing.RegisterRoute("registration", typeof(RegistrationPage));
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
        AppShell.Current.GoToAsync("mainpage", true);
    }
}
