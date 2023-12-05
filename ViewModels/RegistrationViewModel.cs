using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.Models.Common.Enumerated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.Models.Common.Utilities;

namespace Projekt.ViewModels
{
    public partial class RegistrationViewModel : ValidatableViewModel
    {
        private UserRepository userRepository;
        public RegistrationViewModel(UserRepository userRepository)
        {
            this.userRepository = userRepository;
            errors = new Dictionary<string, List<string>>()
            {
                { nameof(Email) + "Entry" , new List<string> {}},
                { nameof(PasswordHash) + "Entry" , new List<string> {}},
                { nameof(FirstName) + "Entry" , new List<string> {}},
                { nameof(LastName) + "Entry" , new List<string> {}},
            };
        }

        [ObservableProperty]
        string email;
        [ObservableProperty]
        string passwordHash;
        [ObservableProperty]
        string firstName;
        [ObservableProperty]
        string lastName;

        [RelayCommand]
        public async Task Submit()
        {
            ResetErrors();
            User user = new User()
            {
                Email = this.Email,
                PasswordHash = this.PasswordHash,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Role = Role.USER
            };
            List<ValidationResult> violations = user.validate();
            bool serverSideValid = true;
            if (!await userRepository.IsEmailUnique(Email))
            {
                Errors[nameof(Email) + "Entry"].Add("Podany e-mail jest już zajęty");
                serverSideValid = false;
            }
            UpdateErorrs(violations);

            if (violations != null && violations.Count == 0 && serverSideValid)
            {
                bool result = await registerUser(user);
                if (!result)
                {
                    ShellUtilities.DisplayUnexpectedErrorAlert();
                }
                else
                {
                    ShellUtilities.DisplayAlert("Sukces!",
                        "Zarejestrowano pomyślnie! Możesz teraz się zalogować.");
                    await AppShell.Current.GoToAsync("../login", true);
                }
            }
        }

        private async Task<bool> registerUser(User user)
        {
            try
            {
                await userRepository.AddUserAsync(user);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
