using Projekt.Models.Common.Utilities;
using Projekt.ViewModels;

namespace Projekt.Views;

public partial class DashBoardPage : ContentPage
{
	public DashBoardPage(DashboardViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if(AuthUtilities.LoggedInUserId == 0)
        {
            AppShell.Current.GoToAsync("mainpage", false);
        }
    }
}