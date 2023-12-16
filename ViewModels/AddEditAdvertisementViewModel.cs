using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Enumerated;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
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
        private Advertisement e;

        private AdvertisementsRepository advertisementsRepository;
        private CategoryRepository categoryRepository;

        public AddEditAdvertisementViewModel(CategoryRepository categoryRepository,
            AdvertisementsRepository advertisementsRepository)
        {
            this.advertisementsRepository = advertisementsRepository;
            this.categoryRepository = categoryRepository;
            Task.Run(async () =>
            {
                categories = new ObservableCollection<Category>(await categoryRepository.GetCategories());
            });
        }

        public void SetAdvertisementCategory()
        {
            if (E == null) return;
            E.Category = Categories.Where(c => c.Id == E.CategoryId).First();
        }

        [RelayCommand]
        private async Task Submit()
        {
            if(E == null) E = new Advertisement();
            if(E.Id == 0)
            {
                //TODO walidacja i cała reszta.
                //dodawanie
                E.CategoryId = E.Category.Id;
                await advertisementsRepository.AddAdvertisement(E);
                await AppShell.Current.GoToAsync("..");
                await ShellUtilities.DisplayAlert("Sukces", "Poprawnie dodano ogłoszenie!");
            }
            else
            {
                //edytowanie
                E.CategoryId = E.Category.Id;
                await advertisementsRepository.UpdateAdvertisement(E);
                await AppShell.Current.GoToAsync("..");
                await ShellUtilities.DisplayAlert("Sukces", "Poprawnie edytowano ogłoszenie!");
            }
        }
    }
}
