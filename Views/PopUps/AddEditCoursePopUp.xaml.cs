using CommunityToolkit.Maui.Views;
using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.PopUps;

public partial class AddEditCoursePopUp : Popup
{
    private bool info;
    private Course course;
    private UserCoursesViewModel vm;
    public AddEditCoursePopUp(Course course, bool info = false)
    {
        InitializeComponent();
        this.course = course;
        this.info = info;
        if (info)
        {
            NameEntry.Enabled
                = HostEntry.Enabled
                = DateEntry.Enabled
                = false;

            actionButton.IsVisible = false;
            actionButton.IsEnabled = false;
        }
        else if (course == null)
        {
            actionButton.Text = "Dodaj";
            actionButton.IsEnabled = true;
            actionButton.IsVisible = true;
        }
        else
        {
            actionButton.Text = "Edytuj";
            actionButton.IsEnabled = true;
            actionButton.IsVisible = true;
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<UserCoursesViewModel>();
        if (info || course != null)
        {
            vm.E = course;
        }
        else
        {
            vm.E = new Course();
        }

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Close(false);
    }

    private void actionButton_Clicked_1(object sender, EventArgs e)
    {
        if (!info)
        {
            bool isValid = vm.Validate();
            if (isValid)
            {
                Close(true);
            }
        }
        else
        {
            Close(true);
        }

    }
}