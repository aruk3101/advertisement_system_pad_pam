using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.ContentViews;

public partial class AdvertisementDataContentView : ContentView
{
	public AdvertisementDataContentView()
	{
		InitializeComponent();
	}

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = this.Handler.MauiContext.Services.GetService<AddEditAdvertisementViewModel>();
    }
}