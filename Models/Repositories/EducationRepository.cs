using Projekt.Models.Entities;
using Projekt.Properties;

namespace Projekt.Models.Repositories
{
    public class EducationRepository : DatabaseContext
    {
        public EducationRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddEducation(Education education)
        {
            return await base.AddItemAsync<Education>(education);
        }

        public async Task<IEnumerable<Education>> GetEducation(int userId)
        {
            return await base.GetFileteredAsync<Education>(education => education.UserId == userId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Education>(id);
        }

        public async Task<bool> UpdateEducation(Education e)
        {
            return await base.UpdateItemAsync<Education>(e);
        }
    }
}
