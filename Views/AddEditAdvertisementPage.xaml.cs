using Projekt.Models.Entities;
using Projekt.ViewModels;
using Projekt.Views.ContentViews;

namespace Projekt.Views;

[QueryProperty(nameof(Advertisement), "advertisement")]
public partial class AddEditAdvertisementPage : ContentPage
{
    private List<string> addAdvertisementRadioButtonsContentList = new List<string>()
    {
        "Dane",
    };
    private List<string> editAdvertisementRadioButtonsContentList = new List<string>()
    {
        "Dane",
        "Obowi¹zki",
        "Wymagania",
        "Dodatki",
    };

    public static Advertisement SelectedAdvertisement { get; set; }
    public Advertisement Advertisement
    {
        set
        {
            SelectedAdvertisement = value;
            if (value == null) { 
                //dodawanie
                BindableLayout.SetItemsSource(flexLayout, addAdvertisementRadioButtonsContentList);
            }
            else
            {
                //edycja
                BindableLayout.SetItemsSource(flexLayout, editAdvertisementRadioButtonsContentList);
            }
            SelectContent((flexLayout.Children[0] as RadioButton).Content as string);
            (flexLayout.Children[0] as RadioButton).IsChecked = true;
        }
    }

    public AddEditAdvertisementPage(AddEditAdvertisementViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
        if (BindableLayout.GetItemsSource(flexLayout) == null)
        {
            BindableLayout.SetItemsSource(flexLayout, addAdvertisementRadioButtonsContentList);
        }
        SelectedAdvertisement = null;
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        string selectedValue = (sender as RadioButton).Content as string;
        SelectContent(selectedValue);
    }

    private void SelectContent(string selectedValue)
    {
        switch (selectedValue)
        {
            case "Obowi¹zki":
                content[0] = new AdvertisementResponsibilitiesContentView();
                break;
            case "Wymagania":
                content[0] = new AdvertisementRequirementsContentView();
                break;
            case "Dodatki":
                content[0] = new AdvertisementOpportunitiesContentView();
                break;
            case "Dane":
            default:
                if (content.Count() == 0) content.Children.Add(new AdvertisementDataContentView());
                else content[0] = new AdvertisementDataContentView();
                break;
        }
    }
}