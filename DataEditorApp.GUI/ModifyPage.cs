using System;
using System.Windows;
using DataEditorApp.Modification.Postgres;
using DataEditorApp.Users;
using DbAuthApp.Passwords;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class ModifyPage : SubmitPage
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();
        
        public ModifyPage(User? oldUser) : base(oldUser)
        {
            InitializeComponent();
            Setup();

            if (oldUser is { } user)
            {
                LoginTb.Text = user.Login;
                CreationDatePicker.SelectedDate = user.CreationDate;
            }
        }

        protected override string SubmitButtonText => "Modify";
        protected override string FormTitle => "Modify user";
        protected override Visibility CreationDateEnabled => Visibility.Visible;
        protected override bool AllowEmptyPassword => true;

        protected override bool IsLoginValid(string login) => true;

        protected override void SubmitChanges(User? oldUserData, string login, string password, DateTime? creationDate)
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

        protected override string SuccessMessage(string login)
        {
            return $"Changes to user '{login}' were applied";
        }
    }
}
