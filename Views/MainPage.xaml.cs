using Projekt.ViewModels;

namespace Projekt;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.TranslateTo(0, this.TranslationY - loginFrame.Height, 500, Easing.CubicIn);
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        this.TranslateTo(0, 0, 500, Easing.CubicIn);
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        this.TranslateTo(0, this.TranslationY + registerFrame.Height, 500, Easing.CubicIn);
    }
}

