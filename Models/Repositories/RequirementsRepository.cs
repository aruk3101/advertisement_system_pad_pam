using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class RequirementsRepository : DatabaseContext
    {
        public RequirementsRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddRequirement(Requirement requirement)
        {
            return await base.AddItemAsync<Requirement>(requirement);
        }

        public async Task<IEnumerable<Requirement>> GetRequirement(int advertisementId)
        {
            return await base.GetFileteredAsync<Requirement>(requirement => requirement.AdvertisementId == advertisementId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Requirement>(id);
        }

        public async Task<bool> UpdateRequirement(Requirement e)
        {
            return await base.UpdateItemAsync<Requirement>(e);
        }
    }
}
