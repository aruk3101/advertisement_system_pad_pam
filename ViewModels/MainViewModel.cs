using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Projekt.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        public async Task GoToLoginPage()
        {
            await AppShell.Current.GoToAsync("login", true);
        }

        [RelayCommand]
        public async Task GoToRegistrationPage()
        {
            await AppShell.Current.GoToAsync("registration", true);
        }
    }
}
