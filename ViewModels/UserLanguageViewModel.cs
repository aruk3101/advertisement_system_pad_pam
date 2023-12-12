using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Enumerated;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.Views.PopUps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class UserLanguageViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Language> itemsSource = new List<Language>();

        [ObservableProperty]
        private Language e = new Language();

        [ObservableProperty]
        public ObservableCollection<LanguageLevel> levels
            = new ObservableCollection<LanguageLevel>(Enum.GetValues(typeof(LanguageLevel)).OfType<LanguageLevel>().ToList());

        private LanguageRepository languageRepository;

        public UserLanguageViewModel(LanguageRepository repository)
        {
            this.languageRepository = repository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Language();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.Name) + "Entry" , new List<string> {}},
                { nameof(E.Level) + "Entry" , new List<string> {}},
            };
        }

        private async Task UpdateItemsSource()
        {
            ItemsSource = (await languageRepository.GetLanguage(AuthUtilities.LoggedInUserId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Language language)
        {
            if (!await languageRepository.DeleteById(language.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Language language)
        {
            var popup = new AddEditLanguagePopUp(language, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Language language)
        {
            var popup = new AddEditLanguagePopUp(language, false);
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
                        await languageRepository.UpdateLanguage(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano język");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Language();
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

        private bool Validate(out List<ValidationResult> violations, Language language)
        {
            ResetErrors();
            violations = language.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditLanguagePopUp(null, false);
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
                        await languageRepository.AddLanguage(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nowy język");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Language();
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
