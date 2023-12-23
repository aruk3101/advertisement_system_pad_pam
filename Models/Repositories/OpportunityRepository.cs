using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class OpportunityRepository : DatabaseContext
    {
        public OpportunityRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddOpportunity(Opportunity opportunity)
        {
            return await base.AddItemAsync<Opportunity>(opportunity);
        }

        public async Task<IEnumerable<Opportunity>> GetOpportunity(int advertisementId)
        {
            return await base.GetFileteredAsync<Opportunity>(opportunity => opportunity.AdvertisementId == advertisementId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Opportunity>(id);
        }

        public async Task<bool> UpdateOpportunity(Opportunity e)
        {
            return await base.UpdateItemAsync<Opportunity>(e);
        }
    }
}
