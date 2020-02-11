using System.Windows.Controls;

namespace DataEditorApp.GUI
{
    public partial class ViewPage : Page
    {
        private readonly ViewPagePresenter _presenter;
        
        public ViewPage()
        {
            InitializeComponent();
            _presenter = new ViewPagePresenter(this);
        }
    }
}

