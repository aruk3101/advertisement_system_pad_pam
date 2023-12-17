using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views;

[QueryProperty(nameof(Advertisement), "advertisement")]
public partial class AdvertisementPage : ContentPage
{
    public Advertisement Advertisement
    {
        set
        {
            this.vm.Advertisement = value;
        }
    }

    private AdvertisementViewModel vm;

    public AdvertisementPage(AdvertisementViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = this.vm = vm;
    }
}