using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private UserRepository userRepository;
        private AuthUtilities authUtilities;
        public LoginViewModel(UserRepository userRepository, AuthUtilities authUtilities)
        {
            this.userRepository = userRepository;
            this.authUtilities = authUtilities;
        }

        [ObservableProperty]
        string email;
        [ObservableProperty]
        string passwordHash;

        [RelayCommand]
        public async Task Submit()
        {
            
            if(await authUtilities.LogIn(Email, PasswordHash))
            {
                ShellUtilities.DisplayAlert("Zalogowano!", "Zalogowano");
                await AppShell.Current.GoToAsync("../..", true);
            }
            else
            {
                ShellUtilities.DisplayAlert("Wystąpił błąd", "Użytkownik o takich danych nie istnieje");
            }
        }
    }
}
