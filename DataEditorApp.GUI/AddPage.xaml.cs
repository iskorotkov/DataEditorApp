using System;
using System.Windows;
using System.Windows.Controls;
using DbAuthApp.Passwords;
using DbAuthApp.Registration.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();


        private void AddBtOnClick(object sender, RoutedEventArgs e)
        {
            var login = LoginTb.Text;
            try
            {
                var conStr = new UsersConnectionStringBuilder().Build();
                using var con = new NpgsqlConnection(conStr);
                con.Open();

                var salt = _saltGenerator.Next();
                var password = _passwordHasher.Hash(PasswordPb.Password, salt);
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
