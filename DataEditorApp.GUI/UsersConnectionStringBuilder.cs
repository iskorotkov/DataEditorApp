using System;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class UsersConnectionStringBuilder
    {
        public int MaxCommands { get; set; } = 10;
        public int MinUsages { get; set; } = 2;
        
        public string Build()
        {
            var builder = CreateBasicBuilder();
            builder.MaxAutoPrepare = MaxCommands;
            builder.AutoPrepareMinUsages = MinUsages;
            return builder.ConnectionString;
        }

        private static NpgsqlConnectionStringBuilder CreateBasicBuilder()
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = DatabaseSettings.Default.Host,
                Database = DatabaseSettings.Default.Database,
                Port = DatabaseSettings.Default.Port,
                Username = Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME"),
                Password = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD")
            };
        }
    }
}
