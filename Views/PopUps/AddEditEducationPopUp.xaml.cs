using CommunityToolkit.Maui.Views;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.PopUps;

public partial class AddEditEducationPopUp : Popup
{
    private bool info;
    private Education education;
    private UserEducationViewModel vm;
    public AddEditEducationPopUp(Education education, bool info = false)
	{
		InitializeComponent();
        this.education = education;
        this.info = info;
        if (info)
        {
            SchoolNameEntry.Enabled
                = SchoolLocationEntry.Enabled
                = LevelEntry.IsEnabled
                = SpecializationEntry.Enabled
                = PeriodStartEntry.Enabled
                = PeriodEndEntry.Enabled
                = false;

            actionButton.IsVisible = false;
            actionButton.IsEnabled = false;
        }
        else if (education == null)
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
		this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<UserEducationViewModel>();
        if(info || education != null)
        {
            vm.E = education;
        }
        else
        {
            vm.E = new Education();
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
            if (isValid) {
                Close(true);
            }
        }
        else
        {
            Close(true);
        }
    }
}