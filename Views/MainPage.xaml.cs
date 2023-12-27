using Projekt.ViewModels;

namespace Projekt.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = this.Handler.MauiContext.Services.GetService<MainViewModel>();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LoginPage());
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistrationPage());
    }
}

