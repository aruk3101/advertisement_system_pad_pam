using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Enumerated;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class AddEditAdvertisementViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        public ObservableCollection<ContractType> contractTypes
          = new ObservableCollection<ContractType>(Enum.GetValues(typeof(ContractType)).OfType<ContractType>().ToList());

        [ObservableProperty]
        public ObservableCollection<WorkingTimeType> workingTimeTypes
         = new ObservableCollection<WorkingTimeType>(Enum.GetValues(typeof(WorkingTimeType)).OfType<WorkingTimeType>().ToList());

        [ObservableProperty]
        public ObservableCollection<JobType> jobTypes
         = new ObservableCollection<JobType>(Enum.GetValues(typeof(JobType)).OfType<JobType>().ToList());

        [ObservableProperty]
        public ObservableCollection<Category> categories;

        [ObservableProperty]
        public ObservableCollection<Company> companies;

        [ObservableProperty]
        private Advertisement e;

        private AdvertisementsRepository advertisementsRepository;
        private CategoryRepository categoryRepository;
        private OpportunitiesViewModel opportunitiesViewModel;
        private ResponsibilitesViewModel responsibilitesViewModel;
        private RequirementsViewModel requirementsViewModel;
        private CompanyRepository companyRepository;

        public AddEditAdvertisementViewModel(CategoryRepository categoryRepository,
            AdvertisementsRepository advertisementsRepository,
            OpportunitiesViewModel opportunitiesViewModel,
            ResponsibilitesViewModel responsibilitesViewModel,
            RequirementsViewModel requirementsViewModel,
            CompanyRepository companyRepository)
        {
            this.requirementsViewModel = requirementsViewModel;
            this.responsibilitesViewModel = responsibilitesViewModel;
            this.opportunitiesViewModel = opportunitiesViewModel;
            this.advertisementsRepository = advertisementsRepository;
            this.categoryRepository = categoryRepository;
            this.companyRepository = companyRepository;

            Task.Run(async () =>
            {
                categories = new ObservableCollection<Category>(await categoryRepository.GetCategories());
                await SetUpCompanies();
            });
        }

        public async Task SetupAdvertisement(Advertisement advertisement)
        {
            E = advertisement;
            opportunitiesViewModel.AdvertisementId = advertisement.Id;
            await opportunitiesViewModel.UpdateItemsSource();
            requirementsViewModel.AdvertisementId = advertisement.Id;
            await requirementsViewModel.UpdateItemsSource();
            responsibilitesViewModel.AdvertisementId = advertisement.Id;
            await responsibilitesViewModel.UpdateItemsSource();

            await SetUpCompanies();
        }

        private async Task SetUpCompanies()
        {
            // jakiś dziwny błąd - po pobraniu list firm, firma E.Company staje się null ????
            // pewnie coś z sqlite-net-pcl i referencjami
            Company copy = null;
            if (E != null) {
                copy = E.Company;
            }
            
            Companies = new ObservableCollection<Company>(await companyRepository.GetCompanies());
            if (E != null)
            {
                E.Company = copy;
            }
        }

        [RelayCommand]
        private async Task Submit()
        {
            if (E == null) E = new Advertisement();
            if (E.Id == 0)
            {
                //TODO walidacja i cała reszta.
                //dodawanie
                E.CategoryId = E.Category.Id;
                E.CompanyId = E.Company.Id;
                await advertisementsRepository.AddAdvertisement(E);
                await AppShell.Current.GoToAsync("..");
                await ShellUtilities.DisplayAlert("Sukces", "Poprawnie dodano ogłoszenie!");
            }
            else
            {
                //edytowanie
                E.CategoryId = E.Category.Id;
                await advertisementsRepository.UpdateAdvertisement(E);
                await ShellUtilities.DisplayAlert("Sukces", "Poprawnie edytowano ogłoszenie!");
            }
        }
    }
}
