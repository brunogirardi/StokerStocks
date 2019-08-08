using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StokerStocks
{
    /// <summary>
    /// Classe responsável por distribuir as informações basicas do sistema
    /// de forma statica
    /// </summary>
    public class MainRouter
    {

        /// <summary>
        /// Retorna as corretoras do sistema;
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Corretoras> Corretoras()
        {
            return ((App)Application.Current).Corretoras;
        }

    }
}
