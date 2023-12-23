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
    public partial class ResponsibilitesViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Responsibility> itemsSource = new List<Responsibility>();

        [ObservableProperty]
        private Responsibility e = new Responsibility();

        [ObservableProperty]
        private int advertisementId;

        private ResponsiblityRepository repository;
        public ResponsibilitesViewModel(ResponsiblityRepository repository)
        {
            this.repository = repository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Responsibility();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.Name) + "Entry" , new List<string> {}}
            };
        }

        public async Task UpdateItemsSource()
        {
            ItemsSource = (await repository.GetResponsibility(AdvertisementId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Responsibility entity)
        {
            if (!await repository.DeleteById(entity.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Responsibility entity)
        {
            var popup = new AddEditResponsibilityPopUp(entity, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Responsibility entity)
        {
            var popup = new AddEditResponsibilityPopUp(entity, false);
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
                        await repository.UpdateResponsibility(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano obowiązek");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Responsibility();
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

        private bool Validate(out List<ValidationResult> violations, Responsibility entity)
        {
            ResetErrors();
            violations = entity.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditResponsibilityPopUp(null, false);
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
                        await repository.AddResponsibility(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nowy obowiązek");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Responsibility();
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
