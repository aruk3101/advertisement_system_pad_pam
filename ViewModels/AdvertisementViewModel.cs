using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Entities;
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
        public AdvertisementViewModel()
        {
            
        }

        [RelayCommand]
        private async void Apply()
        {

        }
    }
}
