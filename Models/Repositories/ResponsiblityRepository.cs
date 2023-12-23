using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class ResponsiblityRepository : DatabaseContext
    {
        public ResponsiblityRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddResponsibility(Responsibility responsibility)
        {
            return await base.AddItemAsync<Responsibility>(responsibility);
        }

        public async Task<IEnumerable<Responsibility>> GetResponsibility(int advertisementId)
        {
            return await base.GetFileteredAsync<Responsibility>(responsibility =>
            responsibility.AdvertisementId == advertisementId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Responsibility>(id);
        }

        public async Task<bool> UpdateResponsibility(Responsibility e)
        {
            return await base.UpdateItemAsync<Responsibility>(e);
        }
    }
}
