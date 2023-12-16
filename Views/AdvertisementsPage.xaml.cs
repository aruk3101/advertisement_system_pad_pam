using CommunityToolkit.Mvvm.ComponentModel;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.ViewModels;
using Projekt.Views.UserControls;

namespace Projekt.Views;

public partial class AdvertisementsPage : ContentPage
{
    AdvertisementsViewModel vm;
	public AdvertisementsPage(AdvertisementsViewModel vm,
        AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
		this.BindingContext = this.vm = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await vm.UpdateAdvertisements();
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
    // nie da siê daæ komendy, bo siê psuje ca³y listview
    private async void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        await vm.Edit((sender as MenuItem).CommandParameter as Advertisement);
    }

    private async void MenuItem_Clicked_2(object sender, EventArgs e)
    {
        await vm.Delete((sender as MenuItem).CommandParameter as Advertisement);
    }

    private async void FloatingAddButton_Clicked_1(object sender, EventArgs e)
    {
        await vm.Add();
    }
}