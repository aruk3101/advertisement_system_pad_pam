using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.ContentViews;

public partial class UserExperienceContentView : ContentView
{
    private UserExperienceViewModel vm;
	public UserExperienceContentView()
	{
		InitializeComponent();
	}

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = vm = this.Handler.MauiContext.Services.GetService<UserExperienceViewModel>();
    }

    // nie moge skorzystaæ w xamlu z komendy, poniewa¿ psuje siê ca³y ListView (???)
    // issue znany na githubie .net maui, ale nie rozwi¹zany
    // collectionview natomiast tez nie moge uzyc, bo swipeView nie dziala na windowsie
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        await vm.Delete((sender as MenuItem).CommandParameter as Experience);
    }

    private async void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        await vm.Edit((sender as MenuItem).CommandParameter as Experience);
    }

    private async void MenuItem_Clicked_2(object sender, EventArgs e)
    {
        await vm.Info((sender as MenuItem).CommandParameter as Experience);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await vm.Add();
    }
}