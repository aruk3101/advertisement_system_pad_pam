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
    public partial class UserCoursesViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private List<Course> itemsSource = new List<Course>();

        [ObservableProperty]
        private Course e = new Course();

        private CoursesRepository courseRepository;
        public UserCoursesViewModel(CoursesRepository repository)
        {
            this.courseRepository = repository;
            Task t = new Task(async () =>
            {
                await UpdateItemsSource();
            });
            t.Start();

            E = new Course();

            errors = new Dictionary<string, List<string>>()
            {
                { nameof(E.Name) + "Entry" , new List<string> {}},
                { nameof(E.Host) + "Entry" , new List<string> {}},
                { nameof(E.Date) + "Entry" , new List<string> {}}
            };
        }

        private async Task UpdateItemsSource()
        {
            ItemsSource = (await courseRepository.GetCourse(AuthUtilities.LoggedInUserId)).ToList();
        }

        [RelayCommand]
        public async Task Delete(Course course)
        {
            if (!await courseRepository.DeleteById(course.Id))
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
                return;
            }
            await UpdateItemsSource();
        }

        [RelayCommand]
        public async Task Info(Course course)
        {
            var popup = new AddEditCoursePopUp(course, true);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task Edit(Course course)
        {
            var popup = new AddEditCoursePopUp(course, false);
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
                        await courseRepository.UpdateCourse(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie edytowano");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Course();
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

        private bool Validate(out List<ValidationResult> violations, Course course)
        {
            ResetErrors();
            violations = course.validate();
            UpdateErorrs(violations);
            return violations.Count() == 0 ? true : false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var popup = new AddEditCoursePopUp(null, false);
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
                        await courseRepository.AddCourse(E);
                        await ShellUtilities.DisplayAlert("Sukces!", "Poprawnie dodano nowy kurs");
                    }
                    catch (Exception)
                    {
                        ShellUtilities.DisplayUnexpectedErrorAlert();
                    }
                    E = new Course();
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
