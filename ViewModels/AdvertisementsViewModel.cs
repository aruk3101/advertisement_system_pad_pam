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

        private AdvertisementsRepository advertisementsRepository;
        private AddEditAdvertisementViewModel addEditAdvertisementViewModel;

        public AdvertisementsViewModel(AdvertisementsRepository advertisementsRepository,
            AddEditAdvertisementViewModel addEditAdvertisementViewModel,
            AppShellViewModel appShellViewModel)
        {
            this.advertisementsRepository = advertisementsRepository;
            this.addEditAdvertisementViewModel = addEditAdvertisementViewModel;
        }

        private bool IsAuthorized() {
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

        public async Task UpdateAdvertisements()
        {
            Advertisements = await advertisementsRepository.GetAdvertisements();
        }


    }
}
