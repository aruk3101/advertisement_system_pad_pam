using Projekt.ViewModels;

namespace Projekt;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        currentPage.SizeChanged += CurrentPage_SizeChanged;
	}

    private void CurrentPage_SizeChanged(object sender, EventArgs e)
    {
        
    }
}

