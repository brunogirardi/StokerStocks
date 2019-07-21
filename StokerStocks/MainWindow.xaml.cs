using System.Windows;

namespace StokerStocks
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowVM vm;

        public MainWindow()
        {

            InitializeComponent();

            vm = new MainWindowVM();

            DataContext = vm;
            
        }
    }
}
