using System;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Registration.Postgres;
using DataEditorApp.Users;
using DbAuthApp.Passwords;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class AddPage : SubmitPage
    {
        private readonly ListView _usersList;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        public AddPage(ListView usersList) : base(null)
        {
            _usersList = usersList;
            InitializeComponent();
            Setup();
            LoginTb.IsAvailable = IsLoginAvailable;
        }

        protected override string SubmitButtonText => "Add";
        protected override string FormTitle => "Add user";
        protected override Visibility CreationDateEnabled => Visibility.Collapsed;
        protected override bool AllowEmptyPassword => false;

        protected override bool IsLoginAvailable(string login)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            return !new IsLoginPresentCommand(con, login).Execute();
        }

        protected override void SubmitChanges(User? oldUserData, string login, string password,
            DateTime? creationDate)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            var salt = _saltGenerator.Next();
            var hashedPassword = _passwordHasher.Hash(password, salt);
            var data = new AddUserReturningValuesCommand(con, login, hashedPassword, salt).Execute();
            AddUserToList(data.Id, login, data.CreationDate);
        }

        private void AddUserToList(int id, string login, DateTime creationDate)
        {
            var user = new User
            {
                Id = id,
                Login = login,
                CreationDate = creationDate
            };
            _usersList.Items.Add(user);
        }

        protected override string SuccessMessage(string login)
        {
            return $"User with login '{login}' was created";
        }
    }
}
