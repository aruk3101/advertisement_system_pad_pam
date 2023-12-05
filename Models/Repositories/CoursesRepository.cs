using Projekt.Models.Entities;
using Projekt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class CoursesRepository : DatabaseContext
    {
        public CoursesRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }
        public async Task<bool> AddCourse(Course course)
        {
            return await base.AddItemAsync<Course>(course);
        }

        public async Task<IEnumerable<Course>> GetCourse(int userId)
        {
            return await base.GetFileteredAsync<Course>(course => course.UserId == userId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteItemByKeyAsync<Course>(id);
        }

        public async Task<bool> UpdateCourse(Course e)
        {
            return await base.UpdateItemAsync<Course>(e);
        }
    }
}
