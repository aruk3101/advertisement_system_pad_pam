using Projekt.ViewModels;

namespace Projekt;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }
}

