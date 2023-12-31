using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.ContentViews;

public partial class UserLinksContentView : ContentView
{
    private UserLinksViewModel vm;
    public UserLinksContentView()
    {
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = vm = this.Handler.MauiContext.Services.GetService<UserLinksViewModel>();
    }

    // nie moge skorzysta� w xamlu z komendy, poniewa� psuje si� ca�y ListView (???)
    // issue znany na githubie .net maui, ale nie rozwi�zany
    // collectionview natomiast tez nie moge uzyc, bo swipeView nie dziala na windowsie
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        await vm.Delete((sender as MenuItem).CommandParameter as Link);
    }

    private async void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        await vm.Edit((sender as MenuItem).CommandParameter as Link);
    }

    private async void MenuItem_Clicked_2(object sender, EventArgs e)
    {
        await vm.Info((sender as MenuItem).CommandParameter as Link);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await vm.Add();
    }
}