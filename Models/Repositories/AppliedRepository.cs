using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
     public class AppliedRepository : DatabaseContext
    {
        public AppliedRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddApplied(Applied applied)
        {
            return await base.AddItemAsync<Applied>(applied);
        }

        public async Task<IEnumerable<Applied>> GetApplied(int userId)
        {
            return await base.GetFileteredAsync<Applied>(a => a.UserId == userId);
        }
    }
}
