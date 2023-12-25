using Projekt.Models.Common.Enumerated;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.ViewModels;
using Projekt.Views.ContentViews;
using SQLite;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Projekt;

public partial class App : Application
{
    public App(Projekt.Views.MainPage mainpage)
    {
        InitializeComponent();

        MainPage = new NavigationPage(mainpage);

    }

    protected async override void OnStart()
    {
        base.OnStart();
        AuthUtilities authUtilities = this.Handler.MauiContext.Services.GetService<AuthUtilities>();
        CategoryRepository categoryRepository = this.Handler.MauiContext.Services.GetService<CategoryRepository>();
        List<Category> categories = new List<Category>(await categoryRepository.GetCategories());
        if (categories.Count == 0)
        {
            await categoryRepository.AddCategory(new Category()
            {
                Name = "Kategoria 1"
            });
            await categoryRepository.AddCategory(new Category()
            {
                Name = "Kategoria 2"
            });
            await categoryRepository.AddCategory(new Category()
            {
                Name = "Kategoria 3"
            });
            await categoryRepository.AddCategory(new Category()
            {
                Name = "Kategoria 4"
            });
        }
        await authUtilities.SetupAdminAccount();
    }
}
