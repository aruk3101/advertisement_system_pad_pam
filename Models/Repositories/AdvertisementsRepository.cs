using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class AdvertisementsRepository : DatabaseContext
    {
        public AdvertisementsRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<List<Advertisement>> GetAdvertisements()
        {
            var result = await base.GetAllAsync<Advertisement>();
            return result.ToList();
        }

        public async Task<bool> AddAdvertisement(Advertisement advertisement)
        {
            return await base.AddItemAsync(advertisement);
        }

        public async Task<bool> UpdateAdvertisement(Advertisement e)
        {
            return await base.UpdateItemAsync(e);
        }

        public async Task<bool> Delete(Advertisement advertisement)
        {
            return await base.DeleteItemAsync(advertisement);
        }
    }
}
