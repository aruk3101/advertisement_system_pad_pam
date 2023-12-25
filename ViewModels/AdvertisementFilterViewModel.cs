using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Enumerated;
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
    public class Filter : ValidatableEntity
    {
        public string Position { get; set; } = string.Empty;
        public List<Company> Companies { get; set; } = new List<Company>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<PositionLevel> PositionLevels { get; set; } = new List<PositionLevel>();
        public List<ContractType> ContractTypes { get; set; } = new List<ContractType>();
        public List<WorkingTimeType> WorkingTimeTypes { get; set; } = new List<WorkingTimeType>();
        public List<JobType> JobTypes { get; set; } = new List<JobType>();
    }

    public partial class AdvertisementFilterViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private Filter filter = new Filter();

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
        public ObservableCollection<PositionLevel> positionLevels
            = new ObservableCollection<PositionLevel>(Enum.GetValues(typeof(PositionLevel)).OfType<PositionLevel>().ToList());

        [ObservableProperty]
        public ObservableCollection<Category> categories;

        [ObservableProperty]
        public ObservableCollection<Company> companies;

        private CompanyRepository companyRepository;
        private CategoryRepository categoryRepository;

        public AdvertisementFilterViewModel(CategoryRepository categoryRepository,
            CompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task Setup()
        {
            // dziwny bug - po pobraniu listy z bazy, lista z filtra się nulluje ??????
            // pewnie coś z związanego z referencjami
            // totalnie niezrozumiałe
            var companiesCopy = Filter.Companies;
            Companies = new ObservableCollection<Company>(await companyRepository.GetCompanies() ?? new List<Company>()) ;
            Filter.Companies = companiesCopy;
            var categoriesCopy = Filter.Categories;
            Categories = new ObservableCollection<Category>(await categoryRepository.GetCategories() ?? new List<Category>());
            Filter.Categories = categoriesCopy;
        }

        [RelayCommand]
        public void CheckJobType(JobType jobType)
        {
            if (Filter.JobTypes.Contains(jobType))
            {
                Filter.JobTypes.Remove(jobType);
            }
            else
            {
                Filter.JobTypes.Add(jobType);
            }
        }

        [RelayCommand]
        public void CheckContractType(ContractType contractType)
        {
            if (Filter.ContractTypes.Contains(contractType))
            {
                Filter.ContractTypes.Remove(contractType);
            }
            else
            {
                Filter.ContractTypes.Add(contractType);
            }
        }

        [RelayCommand]
        public void CheckWorkingTimeType(WorkingTimeType workingTimeType)
        {
            if (Filter.WorkingTimeTypes.Contains(workingTimeType))
            {
                Filter.WorkingTimeTypes.Remove(workingTimeType);
            }
            else
            {
                Filter.WorkingTimeTypes.Add(workingTimeType);
            }
        }

        [RelayCommand]
        public void CheckPositionLevel(PositionLevel positionLevel)
        {
            if (Filter.PositionLevels.Contains(positionLevel))
            {
                Filter.PositionLevels.Remove(positionLevel);
            }
            else
            {
                Filter.PositionLevels.Add(positionLevel);
            }
        }

        [RelayCommand]
        public void CheckCategory(Category category)
        {
            if (Filter.Categories.Contains(category))
            {
                Filter.Categories.Remove(category);
            }
            else
            {
                Filter.Categories.Add(category);
            }
        }
        [RelayCommand]
        public void CheckCompany(Company company)
        {
            if (Filter.Companies.Contains(company))
            {
                Filter.Companies.Remove(company);
            }
            else
            {
                Filter.Companies.Add(company);
            }
        }
    }
}
