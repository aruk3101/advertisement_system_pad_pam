using Projekt.Models.Entities;
using Projekt.Properties;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Repositories
{
    public class UserRepository : DatabaseContext
    {
        public UserRepository(DatabaseProperties databaseProperties) : base(databaseProperties)
        {

        }

        public async Task<bool> AddUserAsync(User user)
        {
            return await base.AddItemAsync<User>(user);
        }
        public async Task<bool> DeleteUserAsync(User user)
        {
            return await base.DeleteItemAsync<User>(user);
        }
        public async Task<bool> IsEmailUnique(string email)
        {
            var result = await base.GetFileteredAsync<User>((user) => user.Email.ToLower() == email.ToLower());
            return result.Count() > 0 ? false : true;
        }

        public async Task<User> FindByEmailAndPassword(string email, string password)
        {
            var result = await base.GetFileteredAsync<User>((user) => user.Email == email
                && user.PasswordHash == password);
            return result.Count() == 0 ? null : result.First();
        }

        public async Task<User> FindById(int id)
        {
            return await base.GetItemByKeyAsync<User>(id);
        }

        public async Task<User> FindByIdJoined(int id)
        {
            var table = await base.GetFileteredTableQueryAsync<User>(1, 1, u => u.Id == id);
            var result = await JoinTable(table);
            return result.FirstOrDefault(defaultValue: null);
        }

        private async Task<List<User>> JoinTable(TableQuery<User> table)
        {
            return table
                .GroupJoin(await base.GetTableQuery<Skill>(),
                    o => o.Id,
                    i => i.UserId,
                    (o, i) =>
                    {
                        o.Skills = i.ToList();
                        return o;
                    })
                 .GroupJoin(await base.GetTableQuery<Link>(),
                    o => o.Id,
                    i => i.UserId,
                    (o, i) =>
                    {
                        o.Links = i.ToList();
                        return o;
                    })
                 .GroupJoin(await base.GetTableQuery<Language>(),
                    o => o.Id,
                    i => i.UserId,
                    (o, i) =>
                    {
                        o.Languages = i.ToList();
                        return o;
                    })
                 .GroupJoin(await base.GetTableQuery<Experience>(),
                    o => o.Id,
                    i => i.UserId,
                    (o, i) =>
                    {
                        o.Experiences = i.ToList();
                        return o;
                    })
                 .GroupJoin(await base.GetTableQuery<Education>(),
                    o => o.Id,
                    i => i.UserId,
                    (o, i) =>
                    {
                        o.Educations = i.ToList();
                        return o;
                    })
                 .GroupJoin(await base.GetTableQuery<Course>(),
                    o => o.Id,
                    i => i.UserId,
                    (o, i) =>
                    {
                        o.Courses = i.ToList();
                        return o;
                    })
                .ToList();
        }

        public async Task<bool> Update(User user)
        {
            return await base.UpdateItemAsync<User>(user);
        }

        public async Task<User> FindAdminUser()
        {
            var result = await base.GetFileteredAsync<User>(u => u.Email.Equals("admin@admin.com"));
            return result.FirstOrDefault(defaultValue: null);
        }
    }
}
