using System;
using System.Windows;
using System.Windows.Controls;
using DbAuthApp.Login;
using DbAuthApp.Passwords;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class SubmitPage : Page
    {
        private readonly ISubmitFormContext _context;
        private readonly LoginProcessor _processor = new LoginProcessor();

        public SubmitPage(ISubmitFormContext context)
        {
            _context = context;
            InitializeComponent();
            SubmitButton.Content = context.SubmitButtonText;
            // TODO: Set window's title
        }

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var login = LoginTb.Text;
            login = _processor.RemoveWhitespaces(login);
            if (_context.CheckLogin(login))
            {
                var salt = _saltGenerator.Next();
                var password = _passwordHasher.Hash(PasswordPb.Password, salt);
                try
                {
                    _context.SubmitChanges(login, password, salt, null);
                    MessageBox.Show($"User with login '{login}' was created", "User created");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error occured");
                }
            }
            else
            {
                MessageBox.Show("Login doesn't match specified criteria.");
            }
        }
    }
}
