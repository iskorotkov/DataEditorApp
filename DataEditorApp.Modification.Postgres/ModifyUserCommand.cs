using System;
using Npgsql;

namespace DataEditorApp.Modification.Postgres
{
    public class ModifyUserCommand
    {
        private readonly NpgsqlCommand _command;

        public ModifyUserCommand(NpgsqlConnection con, int id, string login, DateTime creationDate)
        {
            _command = new NpgsqlCommand
            {
                Connection = con,
                CommandText = @"UPDATE users
                                SET login         = @login,
                                    creation_date = @creation_date
                                WHERE id = @id;"
            };
            _command.Parameters.AddWithValue("@id", id);
            _command.Parameters.AddWithValue("@login", login);
            _command.Parameters.AddWithValue("@creation_date", creationDate);
        }

        public void Execute() => _command.ExecuteNonQuery();
    }
}
