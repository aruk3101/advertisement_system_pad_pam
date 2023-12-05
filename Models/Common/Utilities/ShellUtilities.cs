using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Projekt.Models.Common.Utilities
{
    public static class ShellUtilities
    {
        public static async void DisplayUnexpectedErrorAlert()
        {
            if (Shell.Current != null)
            {
                await Shell.Current.DisplayAlert("Wystąpił nieoczekiwany błąd!",
               "Podczas wykonywania operacji wystąpił nieoczekiwany błąd. Spróbuj ponownie później lub skontaktuj się z administratorem.",
               "OK");
            }
        }

        public static async Task DisplayAlert(string title, string message)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.DisplayAlert(title,
                message,
                "OK");
            }
        }

        public static async void DisplayDeleteExceptionAlert() => await DisplayAlert("Wystąpił błąd", "Podczas usuwania obiektu wystąpił błąd");
    }
}
