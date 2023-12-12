using CommunityToolkit.Maui.Views;
using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.PopUps;

public partial class AddEditLinkPopUp : Popup
{
    private bool info;
    private Link link;
    private UserLinksViewModel vm;
    public AddEditLinkPopUp(Link link, bool info = false)
    {
        InitializeComponent();
        this.link = link;
        this.info = info;
        if (info)
        {
            NameEntry.Enabled
                = HyperlinkEntry.Enabled
                = false;

            actionButton.IsVisible = false;
            actionButton.IsEnabled = false;
        }
        else if (link == null)
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
        this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<UserLinksViewModel>();
        if (info || link != null)
        {
            vm.E = link;
        }
        else
        {
            vm.E = new Link();
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