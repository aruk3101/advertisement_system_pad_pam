using CommunityToolkit.Maui.Converters;
using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class CategoryRepository : DatabaseContext
    {
        public CategoryRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddCategory(Category category)
        {
            return await base.AddItemAsync<Category>(category);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await base.GetAllAsync<Category>();
        }
    }
}
