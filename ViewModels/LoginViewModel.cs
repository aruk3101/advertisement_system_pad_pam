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
        public LoginViewModel(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [ObservableProperty]
        string email;
        [ObservableProperty]
        string passwordHash;

        [RelayCommand]
        public async Task Submit()
        {
            User user = await userRepository.FindByEmailAndPassword(Email, PasswordHash);
            if(user != null)
            {
                ShellUtilities.DisplayAlert("Zalogowano!", "Zalogowano");
                //TODO zmienic na przypisanie do jakiejś klasy Auth
            }
            else
            {
                ShellUtilities.DisplayAlert("Wystąpił błąd", "Użytkownik o takich danych nie istnieje");
            }
        }
    }
}
