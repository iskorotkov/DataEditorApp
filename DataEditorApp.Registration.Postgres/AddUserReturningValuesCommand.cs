using System;
using Npgsql;

namespace DataEditorApp.Registration.Postgres
{
    public class AddUserReturningValuesCommand
    {
        private NpgsqlCommand _command;

        public AddUserReturningValuesCommand(NpgsqlConnection con, string login, byte[] password, byte[] salt)
        {
            _command = new NpgsqlCommand
            {
                Connection = con,
                CommandText = @"INSERT INTO users(login, password, salt)
                                VALUES (@login, @password, @salt)
                                RETURNING users.id, users.creation_date"
            };
            _command.Parameters.AddWithValue("@login", login);
            _command.Parameters.AddWithValue("@password", password);
            _command.Parameters.AddWithValue("@salt", salt);
        }

        public UserInsertData Execute()
        {
            var data = new UserInsertData();
            using var reader = _command.ExecuteReader();
            reader.Read();
            data.Id = (int) reader["id"];
            data.CreationDate = (DateTime) reader["creation_date"];
            return data;
        }

        public struct UserInsertData
        {
            public int Id { get; set; }
            public DateTime CreationDate { get; set; }
        }
    }
}
