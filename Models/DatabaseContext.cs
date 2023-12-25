using Projekt.Properties;
using SQLite;
using System.Linq.Expressions;

namespace Projekt.Models
{
    public class DatabaseContext : IAsyncDisposable
    {
        private DatabaseProperties databaseProperties;

        private SQLiteAsyncConnection _connection;
        // AsyncTableQuery nie obsługuje niektórych metod, np. join czy groupjoin,
        // dlatego zrobiłem 2 połączenie, tym razem synchroniczne
        // nie chce zmieniać całego projektu przez 1 problem,
        // dlatego będe tam gdzie sie da korzystać z asynchronicznego połączenia, a czasami z synchronicznego
        private SQLiteConnection _synchronousConnectionn;
        private SQLiteAsyncConnection Database
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteAsyncConnection(databaseProperties.DbPath, databaseProperties.DbConnectionFlags);

                }
                return _connection;
            }
        }
        private SQLiteConnection SynchronousDatabase
        {
            get
            {
                if (_synchronousConnectionn == null)
                {
                    _synchronousConnectionn = new SQLiteConnection(databaseProperties.DbPath, databaseProperties.DbConnectionFlags);
                }
                return _synchronousConnectionn;
            }
        }

        public DatabaseContext(DatabaseProperties databaseProperties)
        {
            this.databaseProperties = databaseProperties;
        }

        public async ValueTask DisposeAsync()
        {
            await _connection?.CloseAsync();
        }
        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }

        private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await action();
        }

        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return Database.Table<TTable>();
        }

        protected async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        protected async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        protected async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate, int page, int pageSize) where TTable : class, new()
        {
            page--; //zeby pierwsza strona była 0, a nie 1
            var table = await GetTableAsync<TTable>();
            return await table
                .Where(predicate)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        protected async Task<TableQuery<TTable>> GetTableQuery<TTable>()
            where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Task.Run(() =>
            {
                return SynchronousDatabase.Table<TTable>();
            });
        }

        protected async Task<TableQuery<TTable>> GetFileteredTableQueryAsync<TTable>(Expression<Func<TTable, bool>> predicate,
            int page,
            int pageSize)
            where TTable : class, new()
        {
            var table = await GetTableQuery<TTable>();
            page--;
            return table.Where(predicate)
            .Skip(page * pageSize)
            .Take(pageSize);
        }

        protected async Task<int> GetFileteredCountAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).CountAsync();
        }

        protected async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            return await Execute<TTable, TTable>(async () => await Database.GetAsync<TTable>(primaryKey));
        }

        protected async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await Database.InsertAsync(item) > 0);
        }

        protected async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await Database.UpdateAsync(item) > 0);
        }

        protected async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await Database.DeleteAsync(item) > 0);
        }

        protected async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await Database.DeleteAsync<TTable>(primaryKey) > 0);
        }
    }
}
