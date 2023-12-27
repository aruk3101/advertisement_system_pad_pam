using Projekt.Models.Common.Enumerated;
using Projekt.Models.Entities;
using Projekt.Properties;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var table = await base.GetFileteredTableQueryAsync<Advertisement>(1, 99999, a => appliedAdsIds.Contains(a.Id));
            var result = await JoinTable(table);
            return result.ToList();
        }

        public async Task<List<Advertisement>> GetAdvertisements(int page, int pageSize, ViewModels.Filter filter)
        {
            List<Expression<Func<Advertisement, bool>>> list = new List<Expression<Func<Advertisement, bool>>>();
            list.Add(a => a.Position.ToLower().Contains(filter.Position.ToLower()));
            if (filter.JobTypes.Count > 0) list.Add(a => filter.JobTypes.Contains(a.JobType));
            if (filter.ContractTypes.Count > 0) list.Add(a => filter.ContractTypes.Contains(a.ContractType));
            if (filter.WorkingTimeTypes.Count > 0) list.Add(a => filter.WorkingTimeTypes.Contains(a.WorkingTimeType));
            if (filter.PositionLevels.Count > 0) list.Add(a => filter.PositionLevels.Contains(a.PositionLevel));
            if (filter.Companies.Count > 0)
            {
                List<int> companiesIds = filter.Companies.Select(c => c.Id).ToList();
                list.Add(a => companiesIds.Contains(a.CompanyId));
            }
            if (filter.Categories.Count > 0)
            {
                List<int> categoriesIds = filter.Categories.Select(c => c.Id).ToList();
                list.Add(a => categoriesIds.Contains(a.CategoryId));
            }

            var tableQuery = await base.GetFileteredTableQueryAsync<Advertisement>(page, pageSize, list.ToArray());
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

        public async Task<int> GetAdvertisementsCount(ViewModels.Filter filter)
        {
            List<Expression<Func<Advertisement, bool>>> list = new List<Expression<Func<Advertisement, bool>>>();
            list.Add(a => a.Position.ToLower().Contains(filter.Position.ToLower()));
            if (filter.JobTypes.Count > 0) list.Add(a => filter.JobTypes.Contains(a.JobType));
            if (filter.ContractTypes.Count > 0) list.Add(a => filter.ContractTypes.Contains(a.ContractType));
            if (filter.WorkingTimeTypes.Count > 0) list.Add(a => filter.WorkingTimeTypes.Contains(a.WorkingTimeType));
            if (filter.PositionLevels.Count > 0) list.Add(a => filter.PositionLevels.Contains(a.PositionLevel));
            if (filter.Companies.Count > 0)
            {
                List<int> companiesIds = filter.Companies.Select(c => c.Id).ToList();
                list.Add(a => companiesIds.Contains(a.CompanyId));
            }
            if (filter.Categories.Count > 0)
            {
                List<int> categoriesIds = filter.Categories.Select(c => c.Id).ToList();
                list.Add(a => categoriesIds.Contains(a.CategoryId));
            }
            return await base.GetFileteredCountAsync<Advertisement>(list.ToArray());
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
