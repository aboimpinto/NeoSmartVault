using System.Windows;

namespace SmartVault.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();

            InitializeComponent();
        }
    }
}
