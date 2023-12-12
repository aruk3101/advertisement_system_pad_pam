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
    public partial class UserSkillViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Skill> itemsSource = new List<Skill>();

        [ObservableProperty]
        private Skill e = new Skill();

        private SkillRepository skillRepository;
        public UserSkillViewModel(SkillRepository skillRepository)
        {
            this.skillRepository = skillRepository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Skill();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.Name) + "Entry" , new List<string> {}}
            };
        }

        private async Task UpdateItemsSource()
        {
            ItemsSource = (await skillRepository.GetSkill(AuthUtilities.LoggedInUserId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Skill skill)
        {
            if (!await skillRepository.DeleteById(skill.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Skill skill)
        {
            var popup = new AddEditSkillPopUp(skill, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Skill skill)
        {
            var popup = new AddEditSkillPopUp(skill, false);
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
                        await skillRepository.UpdateSkill(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano umiejętność");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Skill();
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

        private bool Validate(out List<ValidationResult> violations, Skill skill)
        {
            ResetErrors();
            violations = skill.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditSkillPopUp(null, false);
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
                        await skillRepository.AddSkill(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nową umiejętność");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Skill();
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
