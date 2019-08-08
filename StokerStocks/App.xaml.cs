using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StokerStocks
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {

        public ObservableCollection<Corretoras> Corretoras { get; set; }

        public App()
        {

            Corretoras = MySqlQueries.CarregarCorretoras();

        }

    }
}
