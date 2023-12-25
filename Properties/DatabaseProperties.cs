using SQLite;

namespace Projekt.Properties
{
    public class DatabaseProperties
    {
        public string DbName = "db_V3.db3";
        public string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);
        public SQLiteOpenFlags DbConnectionFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
    }
}
