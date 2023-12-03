using Projekt.ViewModels;

namespace Projekt.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}