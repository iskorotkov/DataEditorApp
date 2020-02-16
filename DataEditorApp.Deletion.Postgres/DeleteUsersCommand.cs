using System.Collections.Generic;
using System.Linq;
using DataEditorApp.Users;
using Npgsql;

namespace DataEditorApp.Deletion.Postgres
{
    public class DeleteUsersCommand
    {
        private readonly NpgsqlCommand _command;
        
        public DeleteUsersCommand(NpgsqlConnection con, List<User> selectedUsers)
        {
            _command = new NpgsqlCommand
            {
                Connection = con,
                CommandText = @"DELETE
                                FROM users
                                WHERE users.id = ANY(@id_list)"
            };
            var ids = selectedUsers.Select(u => u.Id).ToArray();
            _command.Parameters.AddWithValue("@id_list", ids);
        }

        public int Execute() => _command.ExecuteNonQuery();
    }
}
