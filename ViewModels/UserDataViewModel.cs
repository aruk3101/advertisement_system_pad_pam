using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace Projekt.ViewModels
{
    public partial class UserDataViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        string firstName;
        [ObservableProperty]
        string lastName;
        [ObservableProperty]
        string birthDate;
        [ObservableProperty]
        string phoneNumber;
        [ObservableProperty]
        string city;
        [ObservableProperty]
        string postalName;
        [ObservableProperty]
        string postalCode;
        [ObservableProperty]
        string street;
        [ObservableProperty]
        string streetNumber;
        [ObservableProperty]
        string apartmentNumber;
        [ObservableProperty]
        string country;

        private UserRepository userRepository;
        private AuthUtilities authUtilities;

        private User loggedInUser = null;

        public UserDataViewModel(UserRepository userRepository,
            AuthUtilities authUtilities)
        {
            this.authUtilities = authUtilities;
            this.userRepository = userRepository;
            errors = new Dictionary<string, List<string>>()
            {
                { nameof(FirstName) + "Entry" , new List<string> {}},
                { nameof(LastName) + "Entry" , new List<string> {}},
                { nameof(BirthDate) + "Entry" , new List<string> {}},
                { nameof(PhoneNumber) + "Entry" , new List<string> {}},
                { nameof(City) + "Entry" , new List<string> {}},
                { nameof(PostalName) + "Entry" , new List<string> {}},
                { nameof(PostalCode) + "Entry" , new List<string> {}},
                { nameof(Street) + "Entry" , new List<string> {}},
                { nameof(StreetNumber) + "Entry" , new List<string> {}},
                { nameof(ApartmentNumber) + "Entry" , new List<string> {}},
                { nameof(Country) + "Entry" , new List<string> {}},
            };
            Task t = new Task(SetProperties);
            t.Start();
        }

        public void TryUpdate()
        {
            if(loggedInUser != null && loggedInUser.Id != AuthUtilities.LoggedInUserId)
            {
                SetProperties();
            }
        }

        private void SetProperties()
        {
            loggedInUser = authUtilities.GetLoggedInUser().Result;
            FirstName = loggedInUser.FirstName;
            LastName = loggedInUser.LastName;
            BirthDate = loggedInUser.BirthDate == null ? null : loggedInUser.BirthDate.ToString();
            PhoneNumber = loggedInUser.PhoneNumber;
            City = loggedInUser.City;
            PostalName = loggedInUser.PostalName;
            PostalCode = loggedInUser.PostalCode;
            Street = loggedInUser.Street;
            StreetNumber = loggedInUser.StreetNumber;
            ApartmentNumber = loggedInUser.ApartmentNumber;
            Country = loggedInUser.Country;
        }

        [RelayCommand]
        public async Task Submit()
        {
            // walidacja typów pól, np. konwersja string na DateTime
            List<ValidationResult> typeValidations = TypeValidation(out DateTime date);
            ResetErrors();
            CreateUserEntity(out User user);
            Validate(out List<ValidationResult> violations, user, typeValidations);

            if (violations != null && violations.Count == 0)
            {
                user.BirthDate = date;
                bool result = await EditUser(user, loggedInUser.Id);
                if (!result)
                {
                    ShellUtilities.DisplayUnexpectedErrorAlert();
                    SetProperties();
                }
                else
                {
                    await ShellUtilities.DisplayAlert("Sukces!",
                        "Pomyślnie edytowano profil");
                }
            }
        }

        private void CreateUserEntity(out User user)
        {
            user = new User()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                PhoneNumber = this.PhoneNumber,
                ApartmentNumber = this.ApartmentNumber,
                Country = this.Country,
                PostalName = this.PostalName,
                PostalCode = this.PostalCode,
                Street = this.Street,
                StreetNumber = this.StreetNumber,
            };
        }

        private List<ValidationResult> TypeValidation(out DateTime date)
        {
            date = DateTime.MinValue;
            List<ValidationResult> typeValidations = new List<ValidationResult>();
            if (!String.IsNullOrEmpty(BirthDate) && !DateTime.TryParse(BirthDate, out date))
            {
                typeValidations.Add(new ValidationResult("Podana data nie jest poprawna", new List<string> { nameof(BirthDate) }));
            }
            return typeValidations;
        }

        private List<ValidationResult> Validate(out List<ValidationResult> violations,
            User user,
            List<ValidationResult> typeViolations)
        {
            
            // walidacja na tylko niektóre wartości
            var validationContext = new ValidationContext(user, serviceProvider: null, items: null);
            var propertiesToValidate = validationContext.ObjectType.GetProperties().Where(
                    prop => new List<string>
                    {
                        nameof(FirstName),
                        nameof(LastName),
                        nameof(BirthDate),
                        nameof(PhoneNumber),
                        nameof(City),
                        nameof(PostalName),
                        nameof(PostalCode),
                        nameof(Country),
                        nameof(Street),
                        nameof(StreetNumber),
                        nameof(ApartmentNumber),

                    }.Contains(prop.Name)
                );


            violations = user.validate(propertiesToValidate, validationContext);
            violations.AddRange(typeViolations);
            UpdateErorrs(violations);
            return violations;
        }

        private async Task<bool> EditUser(User user, int id)
        {
            loggedInUser.Street = user.Street;
            loggedInUser.StreetNumber = user.StreetNumber;
            loggedInUser.ApartmentNumber = user.ApartmentNumber;
            loggedInUser.PhoneNumber = user.PhoneNumber;
            loggedInUser.FirstName = user.FirstName;
            loggedInUser.LastName = user.LastName;
            loggedInUser.BirthDate = user.BirthDate;
            loggedInUser.City = user.City;
            loggedInUser.Country = user.Country;
            loggedInUser.PostalCode = user.PostalCode;
            loggedInUser.PostalName = user.PostalName;
            return await userRepository.Update(loggedInUser);
        }
    }
}
    