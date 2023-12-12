using CommunityToolkit.Maui.Views;
using Projekt.Models.Entities;
using Projekt.ViewModels;

namespace Projekt.Views.PopUps;

public partial class AddEditLanguagePopUp : Popup
{
    private bool info;
    private Language language;
    private UserLanguageViewModel vm;
    public AddEditLanguagePopUp(Language language, bool info = false)
    {
        InitializeComponent();
        this.language = language;
        this.info = info;
        if (info)
        {
            NameEntry.Enabled
                = LevelEntry.IsEnabled
                = false;

            actionButton.IsVisible = false;
            actionButton.IsEnabled = false;
        }
        else if (language == null)
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
        this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<UserLanguageViewModel>();
        if (info || language != null)
        {
            vm.E = language;
        }
        else
        {
            vm.E = new Language();
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