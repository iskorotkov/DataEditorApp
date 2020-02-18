using System;
using System.Windows;
using DataEditorApp.Users;
using DbAuthApp.Passwords;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class AddPage : SubmitPage
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        public AddPage() : base(null)
        {
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

        protected override void SubmitChanges(User? oldUserData, string login, string password, DateTime? creationDate)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            var salt = _saltGenerator.Next();
            var hashedPassword = _passwordHasher.Hash(password, salt);
            new AddUserCommand(con, login, hashedPassword, salt).Execute();
        }

        protected override string SuccessMessage(string login)
        {
            return $"User with login '{login}' was created";
        }
    }
}
