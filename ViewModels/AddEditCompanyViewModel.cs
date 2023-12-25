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
    public partial class AddEditCompanyViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private Company e;

        [ObservableProperty]
        private string pictureSource;

        private CompanyRepository companyRepository;

        public AddEditCompanyViewModel(CompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [RelayCommand]
        private async Task Submit()
        {
            if (E == null) E = new Company();
            if (E.Id == 0)
            {
                //TODO walidacja i cała reszta.
                //dodawanie
                await companyRepository.AddCompany(E);
                await AppShell.Current.GoToAsync("..");
                await ShellUtilities.DisplayAlert("Sukces", "Poprawnie dodano firmę!");
            }
            else
            {
                //edytowanie
                await companyRepository.UpdateCompany(E);
                await ShellUtilities.DisplayAlert("Sukces", "Poprawnie edytowano firmę!");
            }
        }

        [RelayCommand]
        private async Task ChoosePicture()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please a pick photo",

                });
                if (result == null) return;

                if (result.ContentType == "image/png" ||
                    result.ContentType == "image/jpeg" ||
                    result.ContentType == "image/jpg")
                {
                    await SavePicture(result);
                    return;
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error Type Image", "Please choose a new image with format png, jpg or jpeg", "Ok");

            }
            catch (Exception ex)
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
            }
        }

        private async Task<string> SavePicture(FileResult fileResult)
        {
            try
            {
                string destination = Path.Combine(FileSystem.AppDataDirectory,
                        "c_" + fileResult.FileName);
                File.Copy(fileResult.FullPath, destination, true);
                E.PictureSource = PictureSource = null;
                E.PictureSource = PictureSource = destination;
                return destination;
            }
            catch (Exception ex)
            {
                ShellUtilities.DisplayDeleteExceptionAlert();
                return null;
            }
        }
    }
}
