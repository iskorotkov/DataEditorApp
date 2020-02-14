using System;
using System.Windows;
using System.Windows.Controls;
using DbAuthApp.Login;

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
            // TODO: Enable/disable creation date control
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var login = LoginTb.Text;
            login = _processor.RemoveWhitespaces(login);
            if (_context.IsLoginValid(login))
            {
                try
                {
                    // TODO: Pass creation date
                    _context.SubmitChanges(login, PasswordPb.Password.Trim(), null);
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
