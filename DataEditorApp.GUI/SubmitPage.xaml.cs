using System;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Users;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class SubmitPage : Page
    {
        private readonly ISubmitFormContext _context;
        private readonly User? _user;

        public SubmitPage(ISubmitFormContext context, User? oldUser)
        {
            InitializeComponent();

            _context = context;
            _user = oldUser;
            SubmitButton.Content = context.SubmitButtonText;
            WindowTitle = context.FormTitle;
            CreationDateBox.Visibility = context.CreationDateEnabled;
            PasswordPb.AllowEmpty = context.AllowEmptyPassword;

            if (oldUser is { } user)
            {
                LoginTb.Text = user.Login;
                CreationDatePicker.SelectedDate = user.CreationDate;
            }
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!LoginTb.IsCorrect)
            {
                MessageBox.Show("Login doesn't match specified criteria.");
                return;
            }

            if (!_context.IsLoginValid(LoginTb.Text))
            {
                MessageBox.Show("User with the same login already exists");
                return;
            }

            if (!PasswordPb.IsCorrect)
            {
                MessageBox.Show("Password isn't strong enough");
                return;
            }

            try
            {
                _context.SubmitChanges(_user, LoginTb.Text, PasswordPb.Text.Trim(), CreationDatePicker.SelectedDate);
                MessageBox.Show(_context.SuccessMessage(LoginTb.Text), "Success");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error occured");
            }
        }
    }
}
