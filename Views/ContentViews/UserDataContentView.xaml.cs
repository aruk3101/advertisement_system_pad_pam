using Projekt.ViewModels;

namespace Projekt.Views.ContentViews;

public partial class UserDataContentView : ContentView
{
    public UserDataContentView()
    {
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = this.Handler.MauiContext.Services.GetService<UserDataViewModel>();
    }
}