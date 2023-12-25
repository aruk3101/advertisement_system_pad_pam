using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views;

public partial class CompaniesPage : ContentPage
{
    CompaniesViewModel vm;
    public CompaniesPage(CompaniesViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = this.vm = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await vm.UpdateCompanies();
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
    // nie da siê daæ komendy, bo siê psuje ca³y listview
    private async void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        await vm.Edit((sender as MenuItem).CommandParameter as Company);
    }

    private async void FloatingAddButton_Clicked_1(object sender, EventArgs e)
    {
        await vm.Add();
    }
}