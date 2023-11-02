using SQLite;

namespace Projekt.Properties
{
    public class DatabaseProperties
    {
        public string DbName = "db.db3";
        public string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);
        public SQLiteOpenFlags DbConnectionFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
    }
}
