using Client.ViewModelContent.ViewModels;
using System.Windows;

namespace Client.ViewContent
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;


        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }
    }
}
