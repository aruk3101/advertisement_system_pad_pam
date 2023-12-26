using CommunityToolkit.Maui.Views;
using Projekt.Models.Common.Enumerated;
using Projekt.Models.Entities;
using Projekt.ViewModels;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Projekt.Views.PopUps;

public partial class AdvertisementFilterPopUp : Popup
{
    private AdvertisementFilterViewModel vm;

    public AdvertisementFilterPopUp()
    {
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        this.BindingContext = this.vm = this.Handler.MauiContext.Services.GetService<AdvertisementFilterViewModel>();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }

    // znowu podanie komendy powoduje crash ca³ej aplikacji,
    // dlatego musze robic obejœcie eventami,
    // EventToCommandBehaviour z MAUI communit toolkit nie pomaga
    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Check(sender, out CheckBox checkBox);

        this.vm.CheckJobType((JobType)checkBox.BindingContext);
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        Check(sender, out CheckBox checkBox);

        this.vm.CheckContractType((ContractType)checkBox.BindingContext);
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        Check(sender, out CheckBox checkBox);

        this.vm.CheckWorkingTimeType((WorkingTimeType)checkBox.BindingContext);
    }

    private void TapGestureRecognizer_Tapped_3(object sender, TappedEventArgs e)
    {
        Check(sender, out CheckBox checkBox);

        this.vm.CheckPositionLevel((PositionLevel)checkBox.BindingContext);
    }

    private void TapGestureRecognizer_Tapped_4(object sender, TappedEventArgs e)
    {
        Check(sender, out CheckBox checkBox);

        this.vm.CheckCategory((Category)checkBox.BindingContext);
    }

    private void TapGestureRecognizer_Tapped_5(object sender, TappedEventArgs e)
    {
        Check(sender, out CheckBox checkBox);

        this.vm.CheckCompany((Company)checkBox.BindingContext);
    }

    private void Check(object sender, out CheckBox checkBox)
    {
        checkBox = (((sender as StackLayout).Parent as Grid).Children[0] as StackLayout)
                .Where(c => c is CheckBox).First() as CheckBox;

        if (checkBox.IsChecked == true) checkBox.IsChecked = false;
        else checkBox.IsChecked = true;
    }
}
