using Projekt.Models.Entities;
using Projekt.Properties;
using SQLite;
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
            var result = await base.GetFileteredAsync<Advertisement>(a => appliedAdsIds.Contains(a.Id));
            return result.ToList();
        }

        public async Task<List<Advertisement>> GetAdvertisements(int page, int pageSize)
        {
            var tableQuery = await base.GetFileteredTableQueryAsync<Advertisement>(_ => true, page, pageSize);
            return await JoinTable(tableQuery);
        }

        private async Task<List<Advertisement>> JoinTable(TableQuery<Advertisement> table)
        {
            return await Task.Run(async () =>
            {
                return table
                .Join(await base.GetTableQuery<Category>(),
                    a => a.CategoryId,
                    c => c.Id,
                    (a, c) =>
                    {
                        a.Category = c;
                        return a;
                    })
                .Join(await base.GetTableQuery<Company>(),
                    a => a.CompanyId,
                    c => c.Id,
                    (a, c) =>
                    {
                        a.Company = c;
                        return a;
                    })
                .GroupJoin(await base.GetTableQuery<Requirement>(),
                    a => a.Id,
                    r => r.AdvertisementId,
                    (a, r) =>
                    {
                        a.Requirements = r.ToList();
                        return a;
                    })
                 .GroupJoin(await base.GetTableQuery<Responsibility>(),
                    a => a.Id,
                    r => r.AdvertisementId,
                    (a, r) =>
                    {
                        a.Responsibilities = r.ToList();
                        return a;
                    })
                 .GroupJoin(await base.GetTableQuery<Opportunity>(),
                    a => a.Id,
                    o => o.AdvertisementId,
                    (a, o) =>
                    {
                        a.Opportunities = o.ToList();
                        return a;
                    })
                .ToList();
            });
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
