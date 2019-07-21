using System;
using System.ComponentModel;

namespace StokerStocks
{
    class Historico : INotifyPropertyChanged
    {

        public DateTime Data { get; set; }

        public int Nota { get; set; }

        public Operações Operação { get; set; }

        public int Quantidade { get; set; }

        public int QuantidadeAcumulada { get; set; }

        public decimal PrecoMedioCompra { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
