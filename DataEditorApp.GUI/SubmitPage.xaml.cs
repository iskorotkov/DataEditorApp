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
        protected SubmitPage(User? oldUser)
        {
            InitializeComponent();
            User = oldUser;

            ChangeButtonState();
            LoginTb.IsCorrectChanged += ChangeButtonState;
            PasswordPb.IsCorrectChanged += ChangeButtonState;
        }

        protected abstract bool AllowEmptyPassword { get; }
        protected abstract Visibility CreationDateEnabled { get; }
        protected abstract string SubmitButtonText { get; }
        protected abstract string FormTitle { get; }
        protected User? User { get; set; }

        private void ChangeButtonState()
        {
            SubmitButton.IsEnabled = LoginTb.IsCorrect && PasswordPb.IsCorrect;
        }

        protected void Setup()
        {
            SubmitButton.Content = SubmitButtonText;
            WindowTitle = FormTitle;
            CreationDateBox.Visibility = CreationDateEnabled;
            PasswordPb.AllowEmpty = AllowEmptyPassword;
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            SubmitChanges(User, LoginTb.Text, PasswordPb.Text, CreationDatePicker.SelectedDate);
            MessageBox.Show(SuccessMessage(LoginTb.Text), "Success");
        }

        protected abstract string SuccessMessage(string login);

        protected abstract void SubmitChanges(User? user, string login, string password,
            DateTime? creationDate);
    }
}
