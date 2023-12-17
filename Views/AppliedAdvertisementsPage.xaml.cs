using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views;

public partial class AppliedAdvertisementsPage : ContentPage
{
	private AdvertisementsViewModel advertisementsViewModel;
	public AppliedAdvertisementsPage(AppliedAdvertisementsViewModel vm,
		AdvertisementsViewModel advertisementsViewModel)
	{
		InitializeComponent();
		this.BindingContext = vm;
		this.advertisementsViewModel = advertisementsViewModel;

    }

    private async void MenuItem_Clicked_3(object sender, EventArgs e)
    {
		await advertisementsViewModel.Info((sender as MenuItem).CommandParameter as Advertisement);
    }
}