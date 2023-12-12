using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class SkillRepository : DatabaseContext
    {
        public SkillRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddSkill(Skill skill)
        {
            return await base.AddItemAsync<Skill>(skill);
        }

        public async Task<IEnumerable<Skill>> GetSkill(int userId)
        {
            return await base.GetFileteredAsync<Skill>(skill => skill.UserId == userId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Skill>(id);
        }

        public async Task<bool> UpdateSkill(Skill e)
        {
            return await base.UpdateItemAsync<Skill>(e);
        }
    }
}
