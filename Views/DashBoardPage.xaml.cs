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

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        CheckAuth();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CheckAuth();
    }

    private void CheckAuth()
    {
        if (AuthUtilities.LoggedInUserId == 0)
        {
            AppShell.Current.GoToAsync("landing", false);
        }
    }
}