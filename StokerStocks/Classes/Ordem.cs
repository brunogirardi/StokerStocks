using System;
using System.ComponentModel;

namespace StokerStocks
{
    public class Ordem : INotifyPropertyChanged
    {

        public int idOrdens { get; set; }

        public int idNotas { get; set; }

        public int idAtivos { get; set; }

        public string Ticket { get; set; }

        public string Empresa { get; set; }

        public DateTime Data { get; set; }

        public Operações Operação { get; set; }
 
        public TipoMercado TipoMercado { get; set; }

        public decimal Preço { get; set; }

        public decimal Total
        {
            get { return Math.Round(Quantidade * Preço, 2); }
        }

        public int Quantidade { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }




}
