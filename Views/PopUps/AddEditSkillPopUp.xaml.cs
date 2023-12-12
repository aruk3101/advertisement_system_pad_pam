using CommunityToolkit.Maui.Views;
using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.PopUps;

public partial class AddEditSkillPopUp : Popup
{
    private bool info;
    private Skill skill;
    private UserSkillViewModel vm;
    public AddEditSkillPopUp(Skill skill, bool info = false)
    {
        InitializeComponent();
        this.skill = skill;
        this.info = info;
        if (info)
        {
            NameEntry.Enabled = false;

            actionButton.IsVisible = false;
            actionButton.IsEnabled = false;
        }
        else if (skill == null)
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
        this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<UserSkillViewModel>();
        if (info || skill != null)
        {
            vm.E = skill;
        }
        else
        {
            vm.E = new Skill();
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