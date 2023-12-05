using CommunityToolkit.Maui;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Repositories;
using Projekt.Properties;
using Projekt.ViewModels;
using Projekt.Views;

namespace Projekt;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiCommunityToolkit()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<DatabaseProperties>();
        builder.Services.AddSingleton<UserRepository>();

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<RegistrationViewModel>();
        builder.Services.AddSingleton<RegistrationPage>();

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<AuthUtilities>();

        builder.Services.AddSingleton<DashBoardPage>();
        builder.Services.AddSingleton<DashboardViewModel>();

        return builder.Build();
	}
}
