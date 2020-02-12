using System.Windows;
using System.Windows.Controls;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var w = new Window();
            var f = new Frame {Content = new AddPage()};
            w.Content = f;
            w.Show();
        }
    }
}
