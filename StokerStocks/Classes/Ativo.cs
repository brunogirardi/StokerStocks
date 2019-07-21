using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace StokerStocks
{
    public class Ativo : INotifyPropertyChanged
    {

        public string Ticket { get; set; }

        public string CompanyName { get; set; }

        public string Corretora { get; set; }

        public ObservableCollection<Ordem> Ordems { get; set; }

        public ObservableCollection<Proventos> Proventos { get; set; }

        public void ObterHistorico()
        {

            int Compras;
            int Vendas;
            DateTime InicioOp;
            DateTime FinalOp;
            ObservableCollection<Cotacao> Cotacoes;
            ObservableCollection<Historico> Historico;

            // Verifica a quantidade de compras e vendas para o ativo
            Compras = Ordems.Where(x => x.Operação == Operações.Compra).Sum(x => x.Quantidade);
            Vendas = Ordems.Where(x => x.Operação == Operações.Venda).Sum(x => x.Quantidade);

            // Verifica a primeira data de operação do ativo
            InicioOp = Ordems.Min(x => x.Date);

            // Verifica se ainda existe ações do ativo
            if ((Compras - Vendas) > 0)
            {
                // Carrega as cotações para o periodo   
                Cotacoes = BancoDados.CarregarCotacoes(InicioOp);
            }
            else
            {
                FinalOp = Ordems.Max(x => x.Date);
                Cotacoes = BancoDados.CarregarCotacoes(InicioOp, FinalOp);
            }

            // Inicia a variavel do historico do ativo
            Historico = new ObservableCollection<Historico>();

            


        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
