using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class CompanyRepository : DatabaseContext
    {
        public CompanyRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddCompany(Company company)
        {
            return await base.AddItemAsync<Company>(company);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Company>(id);
        }

        public async Task<bool> UpdateCompany(Company e)
        {
            return await base.UpdateItemAsync<Company>(e);
        }

        public async Task<List<Company>> GetCompanies()
        {
            var result = await base.GetAllAsync<Company>();
            return result.ToList();
        }
    }
}
