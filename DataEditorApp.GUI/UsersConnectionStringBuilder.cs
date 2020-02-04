using Npgsql;

namespace DataEditorApp.GUI
{
    public class UsersConnectionStringBuilder
    {
        public string Build()
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = "localhost", Username = "postgres", Password = "1234", Database = "auth_app"
            }.ConnectionString;
        }
    }
}
