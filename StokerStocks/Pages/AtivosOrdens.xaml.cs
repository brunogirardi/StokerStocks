using System.Windows.Controls;

namespace StokerStocks
{
    /// <summary>
    /// Interação lógica para AtivosOrdens.xam
    /// </summary>
    public partial class AtivosOrdens : UserControl
    {
        public AtivosOrdens()
        {
            InitializeComponent();

            DataContext = new AtivosOrdensMV();

        }
    }
}
