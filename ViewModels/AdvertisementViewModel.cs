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
    public partial class AdvertisementViewModel : ObservableObject
    {
        [ObservableProperty]
        Advertisement advertisement;

        private AppliedRepository appliedRepository;
        public AdvertisementViewModel(AppliedRepository appliedRepository)  
        {
            this.appliedRepository = appliedRepository;
        }

        [RelayCommand]
        private async Task Apply()
        {
            await appliedRepository.AddApplied(new Applied()
            {
                UserId = AuthUtilities.LoggedInUserId,
                AdvertisementId = Advertisement.Id
            });
            await ShellUtilities.DisplayAlert("Sukces", "Zaaplikowano do ogłoszenia!");
        }
    }
}
