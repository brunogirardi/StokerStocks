using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StokerStocks
{
    /// <summary>
    /// Lógica interna para IncluirAtivos.xaml
    /// </summary>
    public partial class IncluirAtivos : Window
    {

        public Ativo AtivoSelecionado { get; set; }

        public IncluirAtivos()
        {
            InitializeComponent();

            IncluirAtivosMV DContext = new IncluirAtivosMV();

            DataContext = DContext;

            DContext.OnItemSelecionado += DContext_OnItemSelecionado;

        }

        private void DContext_OnItemSelecionado(Ativo ativo)
        {

            AtivoSelecionado = ativo;
            Close();
        }
    }
}
