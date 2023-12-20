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
    public partial class RequirementsViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Requirement> itemsSource = new List<Requirement>();

        [ObservableProperty]
        private Requirement e = new Requirement();

        [ObservableProperty]
        private int advertisementId;

        private RequirementsRepository repository;
        public RequirementsViewModel(RequirementsRepository repository)
        {
            this.repository = repository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Requirement();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.Name) + "Entry" , new List<string> {}}
            };
        }

        public async Task UpdateItemsSource()
        {
            ItemsSource = (await repository.GetRequirement(AdvertisementId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Requirement entity)
        {
            if (!await repository.DeleteById(entity.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Requirement entity)
        {
            var popup = new AddEditRequirementPopUp(entity, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Requirement entity)
        {
            var popup = new AddEditRequirementPopUp(entity, false);
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
                        await repository.UpdateRequirement(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano wymaganie");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Requirement();
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

        private bool Validate(out List<ValidationResult> violations, Requirement entity)
        {
            ResetErrors();
            violations = entity.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditRequirementPopUp(null, false);
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            ResetErrors();
            if (result is bool && (bool)result == false) return;
            else
            {
                bool isValid = Validate(out List<ValidationResult> violations, E);
                if (isValid)
                {
                    E.AdvertisementId = AdvertisementId;
                    try
                    {
                        await repository.AddRequirement(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nowe wymaganie");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Requirement();
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
