using Projekt.ViewModels;
using Projekt.Views.ContentViews;

namespace Projekt;

public partial class App : Application
{
	public App(Projekt.Views.MainPage mainpage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(mainpage);
	
	}

    protected override void OnStart()
    {
        base.OnStart();
    }
}
