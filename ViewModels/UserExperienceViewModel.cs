using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.Views.PopUps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class UserExperienceViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Experience> itemsSource = new List<Experience>();

        [ObservableProperty]
        private Experience e = new Experience();

        private ExperienceRepository experienceRepository;
        public UserExperienceViewModel(ExperienceRepository experienceRepository)
        {
            this.experienceRepository = experienceRepository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Experience();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.CompanyName) + "Entry" , new List<string> {}},
                { nameof(E.Position) + "Entry" , new List<string> {}},
                { nameof(E.Location) + "Entry" , new List<string> {}},
                { nameof(E.Duties) + "Entry" , new List<string> {}},
                { nameof(E.PeriodStart) + "Entry" , new List<string> {}},
                { nameof(E.PeriodEnd) + "Entry" , new List<string> {}},
            };
        }

        private async Task UpdateItemsSource()
        {
            ItemsSource = (await experienceRepository.GetExperience(AuthUtilities.LoggedInUserId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Experience experience)
        {
            if (!await experienceRepository.DeleteById(experience.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Experience experience)
        {
            var popup = new AddEditExperiencePopUp(experience, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Experience experience)
        {
            var popup = new AddEditExperiencePopUp(experience, false);
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
                        await experienceRepository.UpdateExperience(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano doświadczenie");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Experience();
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

        private bool Validate(out List<ValidationResult> violations, Experience experience)
        {
            ResetErrors();
            violations = experience.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditExperiencePopUp(null, false);
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            ResetErrors();
            if (result is bool && (bool)result == false) return;
            else
            {
                bool isValid = Validate(out List<ValidationResult> violations, E);
                if (isValid)
                {
                    E.UserId = AuthUtilities.LoggedInUserId;
                    try
                    {
                        await experienceRepository.AddExperience(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nowe doświadczenie");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Experience();
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
