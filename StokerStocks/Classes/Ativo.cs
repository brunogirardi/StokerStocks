﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace StokerStocks
{
    public class Ativo : INotifyPropertyChanged
    {

        public Ativo()
        {

        }

        public int IdAtivos { get; set; }

        public string Ticket { get; set; }

        public string Empresa { get; set; }

        public string CodigoAPI { get; set; }

        public TipoAtivo TipoAtivo { get; set; }

        public string Regiao { get; set; }

        public string Moeda { get; set; }

        public string Corretora { get; set; }

        public List<Ordem> Ordems { get; set; }

        public List<Historico> Historicos { get; set; }

        public List<Posicao> Posicoes { get; set; }

        public List<Cotacao> Cotacoes { get; set; }

        public void ObterHistorico()
        {

            int Compras;
            int Vendas;
            DateTime InicioOp;
            DateTime FinalOp;
            List<Historico> Historico;

            if (Ordems.Count == 0) return;

            // Verifica a quantidade de compras e vendas para o ativo
            Compras = Ordems.Where(x => x.Operação == Operações.Compra).Sum(x => x.Quantidade);
            Vendas = Ordems.Where(x => x.Operação == Operações.Venda).Sum(x => x.Quantidade);

            // Verifica a primeira data de operação do ativo
            InicioOp = Ordems.Min(x => x.Data);

            // Verifica se ainda existe ações do ativo
            if ((Compras - Vendas) > 0)
            {
                // Carrega as cotações para o periodo 
                FinalOp = UltimoDiaMes(DateTime.Today);
                Cotacoes = CotacaoManager.HistoricoMensal(IdAtivos, InicioOp, DateTime.Today);
            }
            else
            {
                FinalOp = UltimoDiaMes(Ordems.Max(x => x.Data));
                Cotacoes = CotacaoManager.HistoricoMensal(IdAtivos, InicioOp, FinalOp);
            }

            // Inicia a variavel do historico do ativo
            Historico = new List<Historico>();

            // Iterar por todas as ordens e adicionar ao Historico
            foreach (Ordem item in Ordems)
            {

                // Adiciona todas as Ordens ao historico do ativo
                Historico.Add(new Historico()
                {
                    Data = item.Data,
                    Operação = item.Operação,
                    QuantidadeOperacao = item.Quantidade,
                    PrecoUnitarioOperacao = item.Preço,
                });

            }

            // Analisa as informações de quantidade acumulada, preço médio, total investido e etc.
            for (int i = 0; i < Historico.Count; i++)
            {
                // Verifica se o item é o primeiro da coleção
                if (i == 0)
                {
                    Historico[i].PrecoMedioCompra = Historico[i].PrecoUnitarioOperacao;
                    Historico[i].QuantidadeAcumulada = Historico[i].QuantidadeOperacao;
                    Historico[i].TotalInvestidoAcumulado = Historico[i].QuantidadeOperacao * Historico[i].PrecoUnitarioOperacao;
                    Historico[i].ResultadoAcumulado = 0;
                }
                else {

                    if (Historico[i].Operação == Operações.Compra)
                    {
                        Historico[i].QuantidadeAcumulada = Historico[i-1].QuantidadeAcumulada + Historico[i].QuantidadeOperacao;
                        Historico[i].TotalInvestidoAcumulado = Historico[i-1].TotalInvestidoAcumulado + Historico[i].TotalOperacao;
                        Historico[i].PrecoMedioCompra = Historico[i].TotalInvestidoAcumulado / Historico[i].QuantidadeAcumulada;
                        Historico[i].ResultadoAcumulado = Historico[i - 1].ResultadoAcumulado;
                    }
                    if (Historico[i].Operação == Operações.Venda)
                    {
                        Historico[i].QuantidadeAcumulada = Historico[i - 1].QuantidadeAcumulada - Historico[i].QuantidadeOperacao;
                        Historico[i].PrecoMedioCompra = Historico[i - 1].PrecoMedioCompra;
                        Historico[i].TotalInvestidoAcumulado = Historico[i - 1].TotalInvestidoAcumulado - (Historico[i].QuantidadeOperacao * Historico[i].PrecoMedioCompra);
                        Historico[i].ResultadoAcumulado = Historico[i - 1].ResultadoAcumulado + Historico[i].ResultadoOperacao;
                    }
                }
            }

            Historicos = Historico;

            // Posição ao longo dos meses
            List<Posicao> posicoes = new List<Posicao>();
            DateTime dataAtual = UltimoDiaMes(InicioOp);
            int Quantidade = 0;
            decimal PrecoMedio = 0;
            decimal Resultado = 0;
            Historico UltimaOrdem;

            while (dataAtual <= FinalOp)
            {

                UltimaOrdem = UltimaOrdemDoPeriodo(dataAtual);
                if (UltimaOrdem != null)
                {
                    Quantidade = UltimaOrdem.QuantidadeAcumulada;
                    PrecoMedio = UltimaOrdem.PrecoMedioCompra;
                    Resultado = UltimaOrdem.ResultadoAcumulado;
                }

                posicoes.Add(new Posicao()
                {
                    Data = dataAtual,
                    PrecoFechamento = Cotacoes.Where(x => (x.Data < dataAtual) && (x.Data >= PrimeiroDiaMes(dataAtual))).First().Fechamento,
                    Quantidade = Quantidade,
                    PrecoMedio = PrecoMedio,
                    ResultadoConsolidade = Resultado
                });

                dataAtual = ProximoMes(dataAtual);
            }

            Posicoes = posicoes;

        }

        private Historico UltimaOrdemDoPeriodo(DateTime data)
        {

            List<Historico> temp = new List<Historico>();
            foreach (Historico item in Historicos)
            {
                if ((item.Data < data) && (item.Data >= PrimeiroDiaMes(data)))
                {
                    temp.Add(item);
                }
            }

            if (temp.Count > 0) return temp.OrderBy(x => x.Data).Last();

            return null;
        }

        private DateTime UltimoDiaMes(DateTime data)
        {
            return new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month), 23, 59, 59);
        }

        private DateTime PrimeiroDiaMes(DateTime data)
        {
            return new DateTime(data.Year, data.Month, 1, 0, 0, 0);
        }

        private DateTime ProximoMes(DateTime data)
        {
            if (data.Month == 12) {
                return new DateTime(data.Year + 1, 1, DateTime.DaysInMonth(data.Year + 1, 1), 23, 59, 59);
            } else
            {
                return new DateTime(data.Year, data.Month + 1, DateTime.DaysInMonth(data.Year, data.Month + 1), 23, 59, 59);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
