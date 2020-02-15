using System;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class UsersConnectionStringBuilder
    {
        public string Build()
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = DatabaseSettings.Default.Host,
                Database = DatabaseSettings.Default.Database,
                Port = DatabaseSettings.Default.Port,
                Username = Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME"),
                Password = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD")
            }.ConnectionString;
        }
    }
}
