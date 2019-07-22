using System;
using System.ComponentModel;

namespace StokerStocks
{
    public class Historico : INotifyPropertyChanged
    {

        public DateTime Data { get; set; }

        public int Nota { get; set; }

        public Operações Operação { get; set; }

        public int QuantidadeOperacao { get; set; }

        public int QuantidadeAcumulada { get; set; }

        public decimal PrecoUnitarioOperacao { get; set; }

        public decimal PrecoMedioCompra { get; set; }

        public decimal TotalOperacao => QuantidadeOperacao * PrecoUnitarioOperacao;

        public decimal TotalInvestidoAcumulado { get; set; }

        public decimal ResultadoOperacao {
            get
            {
                if (Operação == Operações.Venda) return TotalOperacao - QuantidadeOperacao * PrecoMedioCompra;
                else return 0;
            }
        }

        public decimal ResultadoAcumulado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
