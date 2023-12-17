using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views;

public partial class AppliedAdvertisementsPage : ContentPage
{
    private AdvertisementsViewModel advertisementsViewModel;
    private AppliedAdvertisementsViewModel vm;
    public AppliedAdvertisementsPage(AppliedAdvertisementsViewModel vm,
        AdvertisementsViewModel advertisementsViewModel)
    {
        InitializeComponent();
        this.BindingContext = this.vm = vm;
        this.advertisementsViewModel = advertisementsViewModel;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await vm.UpdateAdvertisements();
    }

    private async void MenuItem_Clicked_3(object sender, EventArgs e)
    {
        await advertisementsViewModel.Info((sender as MenuItem).CommandParameter as Advertisement);
    }
}