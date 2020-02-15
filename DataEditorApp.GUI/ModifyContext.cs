using System;
using System.Windows;
using DataEditorApp.Modification.Postgres;
using DataEditorApp.Users;
using DbAuthApp.Passwords;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class ModifyContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Modify";
        public string FormTitle => "Modify user";
        public Visibility CreationDateEnabled => Visibility.Visible;
        public bool AllowEmptyPassword => true;

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        public bool IsLoginValid(string login) => true;

        public void SubmitChanges(User? oldUserData, string login, string password, DateTime? creationDate)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();

            if (password.Length > 0)
            {
                // Change password too
                var salt = _saltGenerator.Next();
                var hashedPassword = _passwordHasher.Hash(password, salt);
                // TODO: Too many parameters
                new ModifyUserWithPasswordCommand(con, oldUserData.Value.Id, login, creationDate.Value, hashedPassword,
                    salt).Execute();
            }
            else
            {
                // Password is the same
                // TODO: Too many parameters
                new ModifyUserCommand(con, oldUserData.Value.Id, login, creationDate.Value).Execute();
            }
        }

        public string SuccessMessage(string login)
        {
            return $"Changes to user '{login}' were applied";
        }
    }
}
