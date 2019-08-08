using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokerStocks
{
    public enum TipoMercado
    {
        Avista,
        Opcoes,
        Debentures,
        Atermo,
        TituloPublico
    }

    /// <summary>
    /// Classe responsavel por publicar os itens do enum TipoMecardo
    /// </summary>
    class TipoMercadoClass
    {
        public static List<string> Listar()
        {
            return new List<string>()
            {
                "Avista",
                "Opcoes",
                "Debentures",
                "Atermo",
                "TituloPublico"
            };
        }
    }
}
