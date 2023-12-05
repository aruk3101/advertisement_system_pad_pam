using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class LinkRepository : DatabaseContext
    {
        public LinkRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddLink(Link link)
        {
            return await base.AddItemAsync<Link>(link);
        }

        public async Task<IEnumerable<Link>> GetLink(int userId)
        {
            return await base.GetFileteredAsync<Link>(link => link.UserId == userId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Link>(id);
        }

        public async Task<bool> UpdateLink(Link link)
        {
            return await base.UpdateItemAsync<Link>(link);
        }

    }
}
