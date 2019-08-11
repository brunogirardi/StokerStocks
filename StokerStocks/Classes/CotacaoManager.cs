using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace StokerStocks
{
    public class CotacaoManager
    {

        /// <summary>
        /// Atualiza o banco de dados com as cotações faltantes
        /// </summary>
        public async static void UpdateCotacao(int IdAtivo)
        {

            List<Cotacao> cotacoesAPI;
            List<Cotacao> cotacoesInserir = new List<Cotacao>();
            Ativo ativo = MySqlQueries.CarregarAtivos(IdAtivo);
            Cotacao ultimaCotacao = MySqlQueries.CarregarUltimaCotacao(IdAtivo);

            if (ultimaCotacao == null)
            {
                ultimaCotacao = new Cotacao()
                {
                    Data = new DateTime(1900, 1, 1),
                    Abertura = 0,
                    Fechamento = 0,
                    Maxima = 0,
                    Minima = 0,
                    Volume = 0
                };
            }

            // Verifica se o periodo é menor que 120 dias e solicita da API o resultado compacto
            if (ultimaCotacao.Data.Subtract(DateTime.Today).TotalDays > 140)
            {
                cotacoesAPI = await ApiClient.ConsultarCotacao(ativo.CodigoAPI, true, TipoCotacao.Diaria);
            }
            // Solicita da API o resultado do histórico completo
            else
            {
                cotacoesAPI = await ApiClient.ConsultarCotacao(ativo.CodigoAPI, false, TipoCotacao.Diaria);
            }

            foreach (Cotacao item in cotacoesAPI)
            {

                // Verifica se a ultima cotação do BD está de acordo com o retorno da API
                if (ultimaCotacao.Data == item.Data)
                {
                    item.IdCotacoes = ultimaCotacao.IdCotacoes;
                    MySqlQueries.UpdateCotacoes(item);
                }
                // Se a data da cotação for mais nova que adiciona na coleção temporária
                else if (ultimaCotacao.Data < item.Data)
                {
                    cotacoesInserir.Add(item);
                }

            }

            // Insere os itens no banco de dados
            if (cotacoesInserir.Count > 0)  MySqlQueries.CreateCotacoes(cotacoesInserir, IdAtivo);

        }

        /// <summary>
        /// Gera o historico mensal das cotações
        /// </summary>
        /// <param name="IdAtivo"></param>
        /// <param name="inicio"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        public static List<Cotacao> HistoricoMensal(int IdAtivo, DateTime inicio, DateTime final)
        {
            // Coleção com a serie histórica
            List<Cotacao> cotacoes = MySqlQueries.CarregarCotacoes(IdAtivo, InicioMes(inicio), FinalMes(final));

            // Inicializa a coleção de retorno
            List<Cotacao> retorno = new List<Cotacao>();

            Cotacao processando = null;

            // Processa o resultado
            foreach (Cotacao item in cotacoes)
            {
                // Verifica se está no mesmo mês do item sendo processado
                if ((processando !=null) && (processando.Data.Month == item.Data.Month))
                {
                    processando.Fechamento = item.Fechamento;
                    processando.Data = item.Data;
                    processando.Volume += item.Volume;
                    if (processando.Maxima <= item.Maxima) processando.Maxima = item.Maxima; 
                    if (processando.Minima <= item.Minima) processando.Minima = item.Minima; 
                }

                // Caso não esteja no mesmo mês do processamento fecha o item anterior e adiciona a lista de resultados e 
                else
                {
                    if (processando != null) retorno.Add(processando);
                    processando = new Cotacao()
                    {
                        Data = item.Data,
                        Abertura = item.Abertura,
                        Maxima = item.Maxima,
                        Minima = item.Minima,
                        Fechamento = item.Fechamento,
                        Volume = item.Volume
                    };
                }
            }

            retorno.Add(processando);

            return retorno;

        }

        /// <summary>
        /// Calcula o primeiro dia do mês com base na data do paramentro
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static DateTime InicioMes(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        
        /// <summary>
        /// Calcula o ultimo dia do mês com base na data repassada como parametro
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static DateTime FinalMes(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

    }
}
