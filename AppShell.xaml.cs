using Projekt.Views;

namespace Projekt;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("login", typeof(LoginPage));
		Routing.RegisterRoute("registration", typeof(RegistrationPage));
	}
}
