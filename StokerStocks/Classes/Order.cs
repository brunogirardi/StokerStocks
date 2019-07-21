using System;
using System.ComponentModel;

namespace StokerStocks
{
    public class Ordem : INotifyPropertyChanged
    {

        public DateTime Date { get; set; }

        public Operações Operação { get; set; }

        public Decimal ValorUnitário { get; set; }

        public decimal ValorTotal
        {
            get { return Math.Round(Quantidade * ValorUnitário, 2); }
        }

        public int Quantidade { get; set; }

        public int NotaCorretagem{ get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum Operações
    {
        Compra,

        Venda,

        Provento,

        Agrupamento
    }
}
