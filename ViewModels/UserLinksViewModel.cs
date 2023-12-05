using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.Views.PopUps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class UserLinksViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Link> itemsSource = new List<Link>();

        [ObservableProperty]
        private Link e = new Link();

        private LinkRepository linkRepository;
        public UserLinksViewModel(LinkRepository linkRepository)
        {
            this.linkRepository = linkRepository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Link();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.Name) + "Entry" , new List<string> {}},
                { nameof(E.Hyperlink) + "Entry" , new List<string> {}},
            };
        }

        private async Task UpdateItemsSource()
        {
            ItemsSource = (await linkRepository.GetLink(AuthUtilities.LoggedInUserId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Link link)
        {
            if (!await linkRepository.DeleteById(link.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Link link)
        {
            var popup = new AddEditLinkPopUp(link, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Link link)
        {
            var popup = new AddEditLinkPopUp(link, false);
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            ResetErrors();
            if (result is bool && (bool)result == false) return;
            else
            {
                bool isValid = Validate(out List<ValidationResult> violations, E);
                if (isValid)
                {
                    try
                    {
                        await linkRepository.UpdateLink(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano link");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Link();
                    await UpdateItemsSource();
                }
                else
                {
                    ShellUtilities.DisplayUnexpectedErrorAlert();
                }
            }
        }

        public bool Validate()
        {
            return Validate(out List<ValidationResult> violations, E);
        }

        private bool Validate(out List<ValidationResult> violations, Link link)
        {
            ResetErrors();
            violations = link.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditLinkPopUp(null, false);
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            ResetErrors();
            if (result is bool && (bool)result == false) return;
            else
            {
                bool isValid = Validate(out List<ValidationResult> violations, E);
                if (isValid)
                {
                    E.UserId = AuthUtilities.LoggedInUserId;
                    try
                    {
                        await linkRepository.AddLink(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nowy link");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Link();
                    await UpdateItemsSource();
                }
                else
                {
                    ShellUtilities.DisplayUnexpectedErrorAlert();
                }
            }
        }
    }
}
