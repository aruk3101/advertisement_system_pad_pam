using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class ExperienceRepository : DatabaseContext
    {
        public ExperienceRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddExperience(Experience experience)
        {
            return await base.AddItemAsync<Experience>(experience);
        }

        public async Task<IEnumerable<Experience>> GetExperience(int userId)
        {
            return await base.GetFileteredAsync<Experience>(experience => experience.UserId == userId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Experience>(id);
        }

        public async Task<bool> UpdateExperience(Experience e)
        {
            return await base.UpdateItemAsync<Experience>(e);
        }
    }
}
