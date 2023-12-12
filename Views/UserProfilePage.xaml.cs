using Projekt.ViewModels;

namespace Projekt.Views;

public partial class UserProfilePage : ContentPage
{
	private ContentView currentContentView = null;

	public UserProfilePage(UserProfileViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
		currentContentView = userDataContentView;
		currentContentView.IsVisible = true;
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		string content = (sender as RadioButton).Content as string;
        currentContentView.IsVisible = false;
        switch (content)
		{
            case "Edukacja":
                currentContentView = userEducationContentView;
                break;
            case "Do�wiadczenie":
                currentContentView = userExperienceContentView;
                break;
            case "Umiej�tno�ci":
                currentContentView = userSkillsContentView;
                break;
            case "Kursy":
                currentContentView = userCoursesContentView;
                break;
            case "J�zyki":
                currentContentView = userLanguagesContentView;
                break;
            case "Linki":
                currentContentView = userLinksContentView;
                break;
            default:
                currentContentView = userDataContentView;
                break;
        }
        currentContentView.IsVisible = true;
    }
}