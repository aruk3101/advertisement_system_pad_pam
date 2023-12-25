using Projekt.Models.Common;
using Projekt.ViewModels;

namespace Projekt.Views;

public partial class AddEditCompanyPage : ContentPage
{
	public AddEditCompanyPage(AddEditCompanyViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
        bannerColorPicker.ItemsSource = new List<ColorObject>
        {
            new ColorObject(){ Name = "White", Color = Colors.White},
            new ColorObject(){ Name = "Black", Color = Colors.Black},
            new ColorObject(){ Name = "Blue", Color = Colors.Blue},
            new ColorObject(){ Name = "Red", Color = Colors.Red},
            new ColorObject(){ Name = "Green", Color =  Colors.Green},
            new ColorObject(){ Name = "Yellow", Color = Colors.Yellow},
            new ColorObject(){ Name = "Orange", Color = Colors.Orange},
            new ColorObject(){ Name = "Purple", Color = Colors.Purple},
            new ColorObject(){ Name = "Pink", Color = Colors.Pink},
            new ColorObject(){ Name = "LightBlue", Color =Colors.LightBlue },
            new ColorObject(){ Name = "LightGreen", Color = Colors.LightGreen},
            new ColorObject(){ Name = "Gray", Color = Colors.Gray},
        };
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		Console.WriteLine(((AddEditCompanyViewModel)this.BindingContext).E.PictureSource);
    }
}