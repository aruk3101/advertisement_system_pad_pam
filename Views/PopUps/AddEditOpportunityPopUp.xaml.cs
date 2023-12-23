using CommunityToolkit.Maui.Views;
using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.PopUps;

public partial class AddEditOpportunityPopUp : Popup
{
    private bool info;
    private Opportunity entity;
    private OpportunitiesViewModel vm;
    public AddEditOpportunityPopUp(Opportunity entity, bool info = false)
    {
        InitializeComponent();
        this.entity = entity;
        this.info = info;
        if (info)
        {
            NameEntry.Enabled = false;

            actionButton.IsVisible = false;
            actionButton.IsEnabled = false;
        }
        else if (entity == null)
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
        this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<OpportunitiesViewModel>();
        if (info || entity != null)
        {
            vm.E = entity;
        }
        else
        {
            vm.E = new Opportunity();
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