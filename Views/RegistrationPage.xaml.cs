using Projekt.ViewModels;

namespace Projekt.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}