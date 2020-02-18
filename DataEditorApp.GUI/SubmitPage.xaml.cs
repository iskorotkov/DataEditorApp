using System;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Users;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public abstract partial class SubmitPage : Page
    {
        protected ListView UsersList { get; }
        
        protected SubmitPage(User? oldUser, ListView usersList)
        {
            InitializeComponent();
            User = oldUser;
            UsersList = usersList;
        }

        protected abstract bool AllowEmptyPassword { get; }
        protected abstract Visibility CreationDateEnabled { get; }
        protected abstract string SubmitButtonText { get; }
        protected abstract string FormTitle { get; }
        protected User? User { get; set; }

        protected void Setup()
        {
            SubmitButton.Content = SubmitButtonText;
            WindowTitle = FormTitle;
            CreationDateBox.Visibility = CreationDateEnabled;
            PasswordPb.AllowEmpty = AllowEmptyPassword;
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!LoginTb.IsCorrect)
            {
                MessageBox.Show("Login doesn't match specified criteria.");
                return;
            }

            if (!IsLoginAvailable(LoginTb.Text))
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
                SubmitChanges(User, LoginTb.Text, PasswordPb.Text, CreationDatePicker.SelectedDate);
                MessageBox.Show(SuccessMessage(LoginTb.Text), "Success");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error occured");
            }
        }

        protected abstract string SuccessMessage(string login);

        protected abstract void SubmitChanges(User? user, string login, string password, DateTime? creationDate);

        protected abstract bool IsLoginAvailable(string loginTbText);
    }
}
