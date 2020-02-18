using Npgsql;

namespace DataEditorApp.Modification.Postgres
{
    public class CanChangeLoginCommand
    {
        private readonly NpgsqlCommand _command;

        public CanChangeLoginCommand(NpgsqlConnection con, string newLogin, int userId)
        {
            _command = new NpgsqlCommand
            {
                Connection = con,
                CommandText = @"SELECT count(*)
                                FROM users
                                WHERE login = @login
                                  AND id <> @id"
            };
            _command.Parameters.AddWithValue("@login", newLogin);
            _command.Parameters.AddWithValue("@id", userId);
        }

        public bool Execute() => (long)_command.ExecuteScalar() == 0L;
    }
}
