using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Projekt.Models.Common.Utilities
{
    public static class ShellUtilities
    {
        public static async void DisplayUnexpectedErrorAlert()
        {
            await Shell.Current.DisplayAlert("Wystąpił nieoczekiwany błąd!",
                "Podczas wykonywania operacji wystąpił nieoczekiwany błąd. Spróbuj ponownie później lub skontaktuj się z administratorem.",
                "OK");
        }

        public static async void DisplayAlert(string title, string message)
        {
            await Shell.Current.DisplayAlert(title,
                message,
                "OK");
        }
    }
}
