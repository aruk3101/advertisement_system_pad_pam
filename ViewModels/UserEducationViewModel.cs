using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Enumerated;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.Views.PopUps;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Projekt.ViewModels
{
    public partial class UserEducationViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Education> itemsSource = new List<Education>();

        [ObservableProperty]
        private Education e = new Education();

        [ObservableProperty]
        public ObservableCollection<EducationLevel> levels
            = new ObservableCollection<EducationLevel>(Enum.GetValues(typeof(EducationLevel)).OfType<EducationLevel>().ToList());

        private EducationRepository educationRepository;
        public UserEducationViewModel(EducationRepository educationRepository)
        {
            this.educationRepository = educationRepository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Education();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.SchoolLocation) + "Entry" , new List<string> {}},
                { nameof(E.SchoolName) + "Entry" , new List<string> {}},
                { nameof(E.Specialization) + "Entry" , new List<string> {}},
                { nameof(E.Level) + "Entry" , new List<string> {}},
                { nameof(E.PeriodStart) + "Entry" , new List<string> {}},
                { nameof(E.PeriodEnd) + "Entry" , new List<string> {}},
            };
        }

        private async Task UpdateItemsSource()
        {
            ItemsSource = (await educationRepository.GetEducation(AuthUtilities.LoggedInUserId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Education education)
        {
            if (!await educationRepository.DeleteById(education.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Education education)
        {
            var popup = new AddEditEducationPopUp(education, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Education education)
        {
            var popup = new AddEditEducationPopUp(education, false);
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            ResetErrors();
            if (result is bool && (bool)result == false) return;
            else
            {
                bool isValid = Validate(out List<ValidationResult> violations, E);
                if (isValid)
                {
                    try
                    {
                        await educationRepository.UpdateEducation(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano edukację");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Education();
                    await UpdateItemsSource();
                }
                else
                {
                    ShellUtilities.DisplayUnexpectedErrorAlert();
                }
            }
        }

        public bool Validate()
        {
            return Validate(out List<ValidationResult> violations, E);
        }

        private bool Validate(out List<ValidationResult> violations, Education education)
        {
            ResetErrors();
            violations = education.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditEducationPopUp(null, false);
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            ResetErrors();
            if (result is bool && (bool)result == false) return;
            else
            {
                bool isValid = Validate(out List<ValidationResult> violations, E);
                if(isValid)
                {
                    E.UserId = AuthUtilities.LoggedInUserId;
                    try
                    {
                        await educationRepository.AddEducation(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nową edukację");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Education();
                    await UpdateItemsSource();
                }
                else
                {
                    ShellUtilities.DisplayUnexpectedErrorAlert();
                }
            }
        }
    }
}
