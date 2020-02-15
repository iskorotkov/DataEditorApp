using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                Height = 400,
                Width = 400
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
    }
}
