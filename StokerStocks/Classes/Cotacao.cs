    using System;
using System.ComponentModel;

namespace StokerStocks
{
    public class Cotacao : INotifyPropertyChanged
    {

        public int IdCotacoes { get; set; }

        public DateTime Data { get; set; }

        public decimal Maxima { get; set; }

        public decimal Minima { get; set; }

        public decimal Abertura { get; set; }

        public decimal Fechamento { get; set; }

        public long Volume { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
