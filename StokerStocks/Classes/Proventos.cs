using System;
using System.ComponentModel;

namespace StokerStocks
{
    public class Proventos : INotifyPropertyChanged
    {

        DateTime Data { get; set; }

        decimal Valor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}