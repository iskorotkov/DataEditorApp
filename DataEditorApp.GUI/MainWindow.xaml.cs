using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Deletion.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewPage _viewPage;

        public MainWindow()
        {
            InitializeComponent();
            _viewPage = new ViewPage();
            MainFrame.Content = _viewPage;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            ShowWindowWithPage(new AddPage());
        }

        private static void ShowWindowWithPage(Page page)
        {
            new Window
            {
                Content = page,
                Width = 400,
                Height = 300,
            }.Show();
        }

        private void ModifyUser_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedUsers = _viewPage.GetSelectedUsers().ToList();
            if (selectedUsers.Count != 1)
            {
                // TODO: Handle selection of multiple users
                return;
            }

            ShowWindowWithPage(new ModifyPage(selectedUsers[0]));
        }

        private void DeleteUsers_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedUsers = _viewPage.GetSelectedUsers().ToList();
            if (selectedUsers.Count == 0)
            {
                // TODO: Handle case when no users were selected for deletion
                return;
            }

            _viewPage.RemoveSelectedUsers();
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            new DeleteUsersCommand(con, selectedUsers).Execute();
        }
    }
}
