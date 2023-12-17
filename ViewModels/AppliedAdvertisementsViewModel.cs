using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class AppliedAdvertisementsViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Advertisement> advertisements;
        [ObservableProperty]
        bool isRefreshing;

        private AppliedRepository appliedRepository;
        private AdvertisementsRepository advertisementsRepository;

        public AppliedAdvertisementsViewModel(AppliedRepository appliedRepository,
            AdvertisementsRepository advertisementsRepository)
        {
            this.advertisementsRepository = advertisementsRepository;
            this.appliedRepository = appliedRepository;
            Task.Run(UpdateAdvertisements);
        }

        [RelayCommand]
        public async Task UpdateAdvertisements()
        {
            IsRefreshing = true;
            var applied = await appliedRepository.GetApplied(AuthUtilities.LoggedInUserId);
            Advertisements = await advertisementsRepository.GetAdvertisements(applied);
            IsRefreshing = false;
        }


    }
}
