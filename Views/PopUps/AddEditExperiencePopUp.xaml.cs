using CommunityToolkit.Maui.Views;
using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.PopUps;

public partial class AddEditExperiencePopUp : Popup
{
    private bool info;
    private Experience experience;
    private UserExperienceViewModel vm;
    public AddEditExperiencePopUp(Experience experience, bool info = false)
    {
        InitializeComponent();
        this.experience = experience;
        this.info = info;
        if (info)
        {
            CompanyNameEntry.Enabled
                = DutiesEntry.Enabled
                = LocationEntry.IsEnabled
                = PeriodEndEntry.Enabled
                = PeriodStartEntry.Enabled
                = PositionEntry.Enabled
                = false;

            actionButton.IsVisible = false;
            actionButton.IsEnabled = false;
        }
        else if (experience == null)
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
        this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<UserExperienceViewModel>();
        if (info || experience != null)
        {
            vm.E = experience;
        }
        else
        {
            vm.E = new Experience();
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