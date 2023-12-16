using Projekt.ViewModels;

namespace Projekt.Views;

public partial class AddEditAdvertisementPage : ContentPage
{
	public AddEditAdvertisementPage(AddEditAdvertisementViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}