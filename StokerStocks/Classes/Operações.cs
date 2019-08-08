using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokerStocks
{

    public enum Operações
    {
        Compra,
        Venda,
        Provento,
        Agrupamento
    }

    public class OperaçõesClass
    {
        public static List<string> Listar()
        {
            return new List<string>()
            {
                "Compra",
                "Venda",
                "Provento",
                "Agrupamento"
            };
        }
    }
}
