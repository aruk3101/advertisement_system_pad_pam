using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Enumerated;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;

namespace Projekt.ViewModels
{
    public partial class CompaniesViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        List<Company> companies;
        [ObservableProperty]
        bool isRefreshing = false;

        private CompanyRepository companyRepository;
        private AddEditCompanyViewModel addEditCompanyViewModel;

        public CompaniesViewModel(CompanyRepository companyRepository,
            AddEditCompanyViewModel addEditCompanyViewModel)
        {
            this.companyRepository = companyRepository;
            this.addEditCompanyViewModel = addEditCompanyViewModel;
        }

        private bool IsAuthorized()
        {
            return AuthUtilities.LoggedInUserRole == Role.ADMIN;
        }

        [RelayCommand]
        public async Task Add()
        {
            if (!IsAuthorized()) return;
            addEditCompanyViewModel.E = new Company();
            addEditCompanyViewModel.PictureSource = null;
            await AppShell.Current.GoToAsync("addEditCompany", new Dictionary<string, object>()
            {
                {"company", null}
            });
        }

        [RelayCommand]
        public async Task Edit(Company company)
        {
            if (!IsAuthorized()) return;
            if (company == null) return;
            addEditCompanyViewModel.E = company;
            addEditCompanyViewModel.PictureSource = company.PictureSource;
            await AppShell.Current.GoToAsync("addEditCompany", new Dictionary<string, object>()
            {
                {"company", company}
            });
        }

        [RelayCommand]
        public async Task UpdateCompanies()
        {
            IsRefreshing = true;
            Companies = await companyRepository.GetCompanies();
            IsRefreshing = false;
        }
    }
}
