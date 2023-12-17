using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Enumerated;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class AdvertisementsViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Advertisement> advertisements;
        [ObservableProperty]
        Binding isAdminBinding;
        [ObservableProperty]
        int page = 1;
        [ObservableProperty]
        int maxPage;
        [ObservableProperty]
        bool isRefreshing = false;

        int advertisementsCount = 0;

        const int pageSize = 10;

        private AdvertisementsRepository advertisementsRepository;
        private AddEditAdvertisementViewModel addEditAdvertisementViewModel;

        public AdvertisementsViewModel(AdvertisementsRepository advertisementsRepository,
            AddEditAdvertisementViewModel addEditAdvertisementViewModel,
            AppShellViewModel appShellViewModel)
        {
            this.advertisementsRepository = advertisementsRepository;
            this.addEditAdvertisementViewModel = addEditAdvertisementViewModel;
        }

        private bool IsAuthorized()
        {
            return AuthUtilities.LoggedInUserRole == Role.ADMIN;
        }

        [RelayCommand]
        public async Task Add()
        {
            if (!IsAuthorized()) return;
            addEditAdvertisementViewModel.E = new Advertisement();
            await AppShell.Current.GoToAsync("addEditAdvertisement");
        }

        [RelayCommand]
        public async Task Edit(Advertisement advertisement)
        {
            if (!IsAuthorized()) return;
            if (advertisement == null) return;
            addEditAdvertisementViewModel.E = advertisement;
            addEditAdvertisementViewModel.SetAdvertisementCategory();
            await AppShell.Current.GoToAsync("addEditAdvertisement");
        }

        [RelayCommand]
        public async Task Delete(Advertisement advertisement)
        {
            if (!IsAuthorized()) return;
            await advertisementsRepository.Delete(advertisement);
            await UpdateAdvertisements();
        }

        [RelayCommand]
        public async Task UpdateAdvertisements()
        {
            IsRefreshing = true;
            Advertisements = await advertisementsRepository.GetAdvertisements(Page, pageSize);
            advertisementsCount = await advertisementsRepository.GetAdvertisementsCount();
            MaxPage = Convert.ToInt32(Math.Ceiling(advertisementsCount / (double)pageSize));
            if (MaxPage == 0) MaxPage = 1;
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task NextPage()
        {
            if (Page + 1 <= MaxPage)
            {
                Page++;
                await UpdateAdvertisements();
            }

        }

        [RelayCommand]
        public async Task PreviousPage()
        {
            if (Page > 1)
            {
                Page--;
                await UpdateAdvertisements();
            }
        }

        [RelayCommand]
        public async Task Info(Advertisement advertisement)
        {
            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                {"advertisement",advertisement}
            };
            await AppShell.Current.GoToAsync("advertisement", true, args);
        }
    }
}
