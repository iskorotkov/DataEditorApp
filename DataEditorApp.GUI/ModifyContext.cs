using System;
using System.Windows;
using DataEditorApp.Users;
using DbAuthApp.Passwords;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class ModifyContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Modify";
        public string FormTitle => "Modify user";
        public Visibility CreationDateEnabled => Visibility.Visible;

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
            }
            else
            {
                // Password is the same
            }
        }
    }
}