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
        [ObservableProperty]
        string currentJob;
        [ObservableProperty]
        string currentJobDescription;
        [ObservableProperty]
        string careerSummary;
        [ObservableProperty]
        string profilePictureSource;

        private UserRepository userRepository;
        private AuthUtilities authUtilities;
        private AppShellViewModel appShellViewModel;

        private User loggedInUser = null;

        public UserDataViewModel(UserRepository userRepository,
            AuthUtilities authUtilities,
            AppShellViewModel appShellViewModel)
        {
            this.appShellViewModel = appShellViewModel;
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
                { nameof(CurrentJob) + "Entry" , new List<string> {}},
                { nameof(CurrentJobDescription) + "Entry" , new List<string> {}},
                { nameof(CareerSummary) + "Entry" , new List<string> {}},
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
            CurrentJob = loggedInUser.CurrentJob;
            CurrentJobDescription = loggedInUser.CurrentJobDescription;
            CareerSummary = loggedInUser.CareerSummary;
            ProfilePictureSource = loggedInUser.ProfilePictureSource;
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
                    appShellViewModel.User = user;
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
                CareerSummary = this.CareerSummary,
                CurrentJob = this.CurrentJob,
                CurrentJobDescription = this.CurrentJobDescription,
                ProfilePictureSource = this.ProfilePictureSource,
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
                        nameof(CurrentJob),
                        nameof(CurrentJobDescription),
                        nameof(CareerSummary),

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
            loggedInUser.CareerSummary = user.CareerSummary;
            loggedInUser.CurrentJob = user.CurrentJob;
            loggedInUser.CurrentJobDescription = user.CurrentJobDescription;
            loggedInUser.ProfilePictureSource = user.ProfilePictureSource;
            return await userRepository.Update(loggedInUser);
        }

        [RelayCommand]
        private async Task ChoosePicture()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please a pick photo",

                });
                if (result == null) return;

                if (result.ContentType == "image/png" ||
                    result.ContentType == "image/jpeg" ||
                    result.ContentType == "image/jpg")
                {
                    await SavePicture(result);
                    return;
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error Type Image", "Please choose a new image with format png, jpg or jpeg", "Ok");

            }
            catch (Exception ex)
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
            }
        }

        private async Task<string> SavePicture(FileResult fileResult)
        {
            try
            {
                string extension;
                switch (fileResult.ContentType)
                {
                    case "image/png":
                        extension = ".png";
                        break;
                    case "image/jpeg":
                        extension = ".jpeg";
                        break;
                    case "image/jpg":
                    default:
                        extension = ".jpg";
                        break;
                }
                string destination = Path.Combine(FileSystem.AppDataDirectory,
                        "user_" + AuthUtilities.LoggedInUserId + extension);
                File.Copy(fileResult.FullPath, destination, true);
                ProfilePictureSource = null;
                ProfilePictureSource = destination;
                return destination;
            }
            catch (Exception ex)
            {
                ShellUtilities.DisplayDeleteExceptionAlert();
                return null;
            }
        }
    }
}
    