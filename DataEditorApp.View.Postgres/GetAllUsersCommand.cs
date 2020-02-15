using System;
using System.Collections.Generic;
using DataEditorApp.Users;
using Npgsql;

namespace DataEditorApp.View.Postgres
{
    public class GetAllUsersCommand
    {
        private readonly NpgsqlCommand _command;
        
        public GetAllUsersCommand(NpgsqlConnection con)
        {
            _command = new NpgsqlCommand
            {
                Connection = con,
                CommandText = @"SELECT users.id,
                                       users.login,
                                       users.creation_date
                                FROM users
                                ORDER BY id"
            };
        }

        public List<User> Execute()
        {
            var users = new List<User>();
            using (var reader = _command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = new User
                    {
                        Id = (int) reader["id"],
                        Login = (string) reader["login"],
                        CreationDate = (DateTime) reader["creation_date"]
                    };
                    users.Add(user);
                }
            }

            return users;
        }
    }
}
