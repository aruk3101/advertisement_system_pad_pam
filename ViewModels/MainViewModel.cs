using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Projekt.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        void GoToLoginPage()
        {
            Shell.Current.DisplayAlert("Go to login page", "Not implemented yet.", "OK");
        }

        [RelayCommand]
        void GoToRegisterPage()
        {
            Shell.Current.DisplayAlert("Go to register page", "Not implemented yet.", "OK");
        }
    }
}
