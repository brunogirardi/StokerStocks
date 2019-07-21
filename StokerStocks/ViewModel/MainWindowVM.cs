using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StokerStocks
{
    public class MainWindowVM : INotifyPropertyChanged
    {

        public ObservableCollection<Ativo> Ativos { get; set; }

        public MainWindowVM()
        {

            Ativos = BancoDados.CarregarAtivos();

            Ativos[0].ObterHistorico();

        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
