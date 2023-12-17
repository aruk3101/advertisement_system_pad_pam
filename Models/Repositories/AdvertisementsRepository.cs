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

        public async Task<List<Advertisement>> GetAdvertisements(IEnumerable<Applied> applied)
        {
            var appliedAdsIds = applied.Select(a => a.AdvertisementId);
            var result = await base.GetFileteredAsync<Advertisement>(a=>appliedAdsIds.Contains(a.Id));
            return result.ToList();
        }

        public async Task<List<Advertisement>> GetAdvertisements(int page, int pageSize)
        {
            var result = await base.GetFileteredAsync<Advertisement>(_ => true, page, pageSize);
            return result.ToList();
        }

        public async Task<int> GetAdvertisementsCount()
        {
            return await base.GetFileteredCountAsync<Advertisement>(_ => true);
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
