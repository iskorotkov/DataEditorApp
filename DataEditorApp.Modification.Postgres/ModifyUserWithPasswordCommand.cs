using System;
using Npgsql;

namespace DataEditorApp.Modification.Postgres
{
    public class ModifyUserWithPasswordCommand
    {
        private readonly NpgsqlCommand _command;

        public ModifyUserWithPasswordCommand(NpgsqlConnection con, int id, string login, DateTime creationDate,
            byte[] password, byte[] salt)
        {
            _command = new NpgsqlCommand
            {
                Connection = con,
                CommandText = @"UPDATE users
                                SET login         = @login,
                                    creation_date = @creation_date,
                                    salt          = @salt,
                                    password      = @password
                                WHERE id = @id;"
            };
            _command.Parameters.AddWithValue("@id", id);
            _command.Parameters.AddWithValue("@login", login);
            _command.Parameters.AddWithValue("@creation_date", creationDate);
            _command.Parameters.AddWithValue("@password", password);
            _command.Parameters.AddWithValue("@salt", salt);
        }

        public void Execute() => _command.ExecuteNonQuery();
    }
}
