using System;
using System.Windows;
using DataEditorApp.Users;
using DbAuthApp.Passwords;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class AddContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Add";
        public string FormTitle => "Add user";
        public Visibility CreationDateEnabled => Visibility.Collapsed;

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        public bool IsLoginValid(string login)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            return !new IsLoginPresentCommand(con, login).Execute();
        }

        public void SubmitChanges(User? oldUserData, string login, string password, DateTime? creationDate)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            var salt = _saltGenerator.Next();
            var hashedPassword = _passwordHasher.Hash(password, salt);
            new AddUserCommand(con, login, hashedPassword, salt).Execute();
        }
    }
}