using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class UserRepository : DatabaseContext
    {
        public UserRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {
            
        }
    }
}
