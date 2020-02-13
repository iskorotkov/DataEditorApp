using System;
using DbAuthApp.Login;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public interface ISubmitFormContext
    {
        public string SubmitButtonText { get; }
        public string FormTitle { get; }

        public bool CheckLogin(string login);
        public void SubmitChanges(string login, byte[] password, byte[] salt, DateTime? creationDate);
    }

    public class AddContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Add";
        public string FormTitle => "Add user";

        private readonly LoginChecker _checker = new LoginChecker();

        public bool CheckLogin(string login)
        {
            return _checker.IsCorrect(login);
        }

        public void SubmitChanges(string login, byte[] password, byte[] salt, DateTime? creationDate)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            new AddUserCommand(con, login, password, salt).Execute();
        }
    }

    public class ModifyContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Modify";
        public string FormTitle => "Modify user";

        public bool CheckLogin(string login)
        {
            throw new System.NotImplementedException();
        }

        public void SubmitChanges(string login, byte[] password, byte[] salt, DateTime? creationDate)
        {
            throw new System.NotImplementedException();
        }
    }
}
