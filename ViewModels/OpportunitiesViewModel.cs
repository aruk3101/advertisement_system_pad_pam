using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.Views;
using Projekt.Views.PopUps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class OpportunitiesViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Opportunity> itemsSource = new List<Opportunity>();

        [ObservableProperty]
        private Opportunity e = new Opportunity();

        [ObservableProperty]
        private int advertisementId;

        private OpportunityRepository repository;

        public OpportunitiesViewModel(OpportunityRepository repository)
        {
            this.repository = repository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Opportunity();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.Name) + "Entry" , new List<string> {}}
            };
        }

        public async Task UpdateItemsSource()
        {
            ItemsSource = (await repository.GetOpportunity(AdvertisementId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Opportunity entity)
        {
            if (!await repository.DeleteById(entity.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Opportunity entity)
        {
            var popup = new AddEditOpportunityPopUp(entity, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Opportunity entity)
        {
            var popup = new AddEditOpportunityPopUp(entity, false);
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
                        await repository.UpdateOpportunity(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano dodatek");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Opportunity();
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

        private bool Validate(out List<ValidationResult> violations, Opportunity entity)
        {
            ResetErrors();
            violations = entity.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditOpportunityPopUp(null, false);
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            ResetErrors();
            if (result is bool && (bool)result == false) return;
            else
            {
                E.AdvertisementId = AdvertisementId;
                bool isValid = Validate(out List<ValidationResult> violations, E);
                if (isValid)
                {
                    try
                    {
                        await repository.AddOpportunity(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nowy dodatek");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Opportunity();
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
