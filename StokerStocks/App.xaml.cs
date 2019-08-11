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

        public List<Corretoras> Corretoras { get; set; }

        public List<Ativo> Ativos { get; set; }

        public App()
        {

            Corretoras = MySqlQueries.CarregarCorretoras();

            Ativos = MySqlQueries.CarregarAtivos();

        }

    }
}
