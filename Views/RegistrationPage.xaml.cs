using Projekt.ViewModels;

namespace Projekt.Views;

public partial class RegistrationPage : ContentPage
{
    RegistrationViewModel vm = null;
    public RegistrationPage()
    {
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = vm = this.Handler?.MauiContext.Services.GetService<RegistrationViewModel>();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await vm.Submit(() =>
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new LoginPage());
        });
    }
}