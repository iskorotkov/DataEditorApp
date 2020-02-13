using System.Windows;
using System.Windows.Controls;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            ShowWindowWithPage(new SubmitPage(new AddContext()));
        }

        private static void ShowWindowWithPage(Page page)
        {
            new Window
            {
                Content = new Frame
                {
                    Content = page
                }
            }.Show();
        }

        private void ModifyUser_OnClick(object sender, RoutedEventArgs e)
        {
            ShowWindowWithPage(new SubmitPage(new ModifyContext()));
        }
    }
}
