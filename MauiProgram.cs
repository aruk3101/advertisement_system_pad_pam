using CommunityToolkit.Maui;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Repositories;
using Projekt.Properties;
using Projekt.ViewModels;
using Projekt.Views;
using Projekt.Views.ContentViews;

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
        builder.Services.AddSingleton<EducationRepository>();
        builder.Services.AddSingleton<ExperienceRepository>();
        builder.Services.AddSingleton<SkillRepository>();
        builder.Services.AddSingleton<CoursesRepository>();
        builder.Services.AddSingleton<LanguageRepository>();
        builder.Services.AddSingleton<LinkRepository>();
        builder.Services.AddSingleton<AuthUtilities>();

        builder.Services.AddSingleton<AppShellViewModel>();

        builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<MainPage>();

        builder.Services.AddTransient<RegistrationViewModel>();
        builder.Services.AddTransient<RegistrationPage>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>();

        builder.Services.AddTransient<DashBoardPage>();
        builder.Services.AddTransient<DashboardViewModel>();

        builder.Services.AddTransient<UserProfilePage>();
        builder.Services.AddTransient<UserProfileViewModel>();

        builder.Services.AddTransient<UserDataContentView>();
        builder.Services.AddTransient<UserDataViewModel>();

        builder.Services.AddTransient<UserEducationContentView>();
        builder.Services.AddSingleton<UserEducationViewModel>();

        builder.Services.AddTransient<UserLanguagesContentView>();
        builder.Services.AddSingleton<UserExperienceViewModel>();

        builder.Services.AddTransient<UserSkillsContentView>();
        builder.Services.AddSingleton<UserSkillViewModel>();

        builder.Services.AddTransient<UserCoursesContentView>();
        builder.Services.AddSingleton<UserCoursesViewModel>();

        builder.Services.AddTransient<UserLanguagesContentView>();
        builder.Services.AddSingleton<UserLanguageViewModel>();

        builder.Services.AddTransient<UserLinksContentView>();
        builder.Services.AddSingleton<UserLinksViewModel>();


        return builder.Build();
	}
}
