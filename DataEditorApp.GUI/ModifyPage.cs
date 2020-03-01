using System;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Modification.Postgres;
using DataEditorApp.Users;
using DbAuthApp.Passwords;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class ModifyPage : SubmitPage
    {
        private readonly ListView _usersList;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        public ModifyPage(User? oldUser, ListView usersList) : base(oldUser)
        {
            _usersList = usersList;
            InitializeComponent();
            Setup();
            LoginTb.IsAvailable = IsLoginAvailable;

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

        private bool IsLoginAvailable(string login)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            return new CanChangeLoginCommand(con, login, User.Value.Id).Execute();
        }

        protected override void SubmitChanges(User? oldUserData, string login, string password,
            DateTime? creationDate)
        {
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();

            if (oldUserData is { } user && creationDate is { } date)
            {
                if (password.Length > 0)
                {
                    // Change password too
                    var salt = _saltGenerator.Next();
                    var hashedPassword = _passwordHasher.Hash(password, salt);
                    new ModifyUserWithPasswordCommand(con, user.Id, login, date,
                        hashedPassword,
                        salt).Execute();
                }
                else
                {
                    // Password is the same
                    new ModifyUserCommand(con, oldUserData.Value.Id, login, creationDate.Value).Execute();
                }

                ModifyUserInList(user, login, date);
            }
        }

        private void ModifyUserInList(User oldUser, string newLogin, DateTime newCreationDate)
        {
            var index = _usersList.Items.IndexOf(oldUser);
            User = new User
            {
                Id = oldUser.Id,
                Login = newLogin,
                CreationDate = newCreationDate
            };
            _usersList.Items[index] = User;
        }

        protected override string SuccessMessage(string login)
        {
            return $"Changes to user '{login}' were applied";
        }
    }
}
