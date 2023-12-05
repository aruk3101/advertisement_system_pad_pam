using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class LanguageRepository : DatabaseContext
    {
        public LanguageRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddLanguage(Language language)
        {
            return await base.AddItemAsync<Language>(language);
        }

        public async Task<IEnumerable<Language>> GetLanguage(int userId)
        {
            return await base.GetFileteredAsync<Language>(language => language.UserId == userId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Language>(id);
        }

        public async Task<bool> UpdateLanguage(Language e)
        {
            return await base.UpdateItemAsync<Language>(e);
        }
    }
}
