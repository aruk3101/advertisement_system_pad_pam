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
        private AdvertisementFilterViewModel advertisementFilterViewModel;

        public AdvertisementsViewModel(AdvertisementsRepository advertisementsRepository,
            AddEditAdvertisementViewModel addEditAdvertisementViewModel,
            AppShellViewModel appShellViewModel,
            AdvertisementFilterViewModel advertisementFilterViewModel)
        {
            this.advertisementsRepository = advertisementsRepository;
            this.addEditAdvertisementViewModel = addEditAdvertisementViewModel;
            this.advertisementFilterViewModel = advertisementFilterViewModel;
        }

        private bool IsAuthorized()
        {
            return AuthUtilities.LoggedInUserRole == Role.ADMIN;
        }

        [RelayCommand]
        public async Task Add()
        {
            if (!IsAuthorized()) return;
            await addEditAdvertisementViewModel.SetupAdvertisement(new Advertisement());
            await AppShell.Current.GoToAsync("addEditAdvertisement", new Dictionary<string, object>()
            {
                {"advertisement", null}
            });
        }

        [RelayCommand]
        public async Task Edit(Advertisement advertisement)
        {
            if (!IsAuthorized()) return;
            if (advertisement == null) return;
            await addEditAdvertisementViewModel.SetupAdvertisement(advertisement);
            await AppShell.Current.GoToAsync("addEditAdvertisement", new Dictionary<string, object>()
            {
                {"advertisement", advertisement}
            });
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
            Filter filter = advertisementFilterViewModel.Filter;
            Advertisements = await advertisementsRepository.GetAdvertisements(Page, pageSize, filter);
            advertisementsCount = await advertisementsRepository.GetAdvertisementsCount(filter);
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

        [RelayCommand]
        public async Task Filter()
        {
            var popup = new AdvertisementFilterPopUp();
            await advertisementFilterViewModel.Setup(); // aktualizuje liste kategorii i firm
            await AppShell.Current.ShowPopupAsync(popup);
            await UpdateAdvertisements();
        }
    }
}
