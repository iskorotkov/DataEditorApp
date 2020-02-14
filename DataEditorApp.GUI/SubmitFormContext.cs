using System;
using DbAuthApp.Login;
using DbAuthApp.Passwords;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public interface ISubmitFormContext
    {
        public string SubmitButtonText { get; }
        public string FormTitle { get; }
        public bool CreationDateEnabled { get; }

        public bool IsLoginValid(string login);
        public void SubmitChanges(string login, string password, DateTime? creationDate);
    }

    public class AddContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Add";
        public string FormTitle => "Add user";
        public bool CreationDateEnabled => false;

        private readonly LoginChecker _checker = new LoginChecker();
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        public bool IsLoginValid(string login)
        {
            if (!_checker.IsCorrect(login))
            {
                return false;
            }

            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            return !new IsLoginPresentCommand(con, login).Execute();
        }

        public void SubmitChanges(string login, string password, DateTime? creationDate)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            var salt = _saltGenerator.Next();
            var hashedPassword = _passwordHasher.Hash(password, salt);
            new AddUserCommand(con, login, hashedPassword, salt).Execute();
        }
    }

    public class ModifyContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Modify";
        public string FormTitle => "Modify user";
        public bool CreationDateEnabled => true;

        public bool IsLoginValid(string login)
        {
            throw new System.NotImplementedException();
        }

        public void SubmitChanges(string login, string password, DateTime? creationDate)
        {
            throw new System.NotImplementedException();
        }
    }
}
