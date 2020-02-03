using System.Windows.Controls;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private readonly AddPagePresenter _presenter;

        public AddPage()
        {
            InitializeComponent();
            _presenter = new AddPagePresenter(this);
        }
    }
}
