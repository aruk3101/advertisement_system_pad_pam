using Projekt.ViewModels;

namespace Projekt.Views;

public partial class LoginPage : ContentPage
{
    LoginViewModel vm = null;
    public LoginPage()
    {
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = vm = this.Handler?.MauiContext.Services.GetService<LoginViewModel>();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await vm.Submit(async () =>
        {
            App.Current.MainPage = new AppShell();
        });
    }
}