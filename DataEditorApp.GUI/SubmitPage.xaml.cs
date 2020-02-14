using System;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Users;
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
        private readonly PasswordChecker _passwordChecker = new PasswordChecker();
        private readonly User? _user;

        public SubmitPage(ISubmitFormContext context, User? oldUser)
        {
            InitializeComponent();

            _context = context;
            _user = oldUser;
            SubmitButton.Content = context.SubmitButtonText;
            WindowTitle = context.FormTitle;
            // TODO: Enable/disable creation date control

            if (oldUser is { } user)
            {
                LoginTb.Text = user.Login;
                CreationDatePicker.SelectedDate = user.CreationDate;
            }
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var login = LoginTb.Text;
            login = _processor.RemoveWhitespaces(login);
            if (!_context.IsLoginValid(login))
            {
                MessageBox.Show("Login doesn't match specified criteria.");
                return;
            }

            var password = PasswordPb.Password.Trim();
            if (!_passwordChecker.IsStrong(password))
            {
                MessageBox.Show("Password isn't strong enough");
                return;
            }

            try
            {
                // TODO: Pass creation date
                _context.SubmitChanges(_user, login, password, null);
                MessageBox.Show($"User with login '{login}' was created", "User created");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error occured");
            }
        }
    }
}
