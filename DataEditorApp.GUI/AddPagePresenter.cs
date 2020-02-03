using System;
using System.Windows;
using DbAuthApp.Passwords;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class AddPagePresenter
    {
        private readonly AddPage _page;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        public AddPagePresenter(AddPage page)
        {
            _page = page;
            page.AddBt.Click += AddBtOnClick;
            _connectionString = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost", Username = "postgres", Password = "1234", Database = "auth_app"
            }.ConnectionString;
        }

        private readonly string _connectionString;

        private void AddBtOnClick(object sender, RoutedEventArgs e)
        {
            var login = _page.LoginTb.Text;
            try
            {
                using var con = new NpgsqlConnection(_connectionString);
                con.Open();
                
                var salt = _saltGenerator.Next();
                var password = _passwordHasher.Hash(_page.PasswordPb.Password, salt);
                new AddUserCommand(con, login, password, salt).Execute();
                
                MessageBox.Show($"User with login '{login}' was created", "User created");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error occured");
            }
        }
    }
}
