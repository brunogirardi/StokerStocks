using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokerStocks
{
    public class Posicao
    {

        public DateTime Data { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoMedio { get; set; }

        public decimal TotalInvestido => Quantidade * PrecoMedio;

        public decimal PrecoFechamento { get; set; }

        public decimal TotalFechamento => Quantidade * PrecoFechamento;

        public decimal ResultadoVirtual => TotalFechamento - TotalInvestido;

        public decimal ResultadoConsolidade { get; set; }

        public decimal ResultadoAtual => ResultadoConsolidade + ResultadoVirtual;

    }
}
