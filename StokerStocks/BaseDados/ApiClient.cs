using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace StokerStocks
{
    class ApiClient
    {

        #region Obter Cotações

        /// <summary>
        /// Consulta a API no site Alphavantage para a com sério diaria das cotações
        /// </summary>
        /// <param name="Texto">Ticket do Ativo</param>
        /// <param name="Compact">Retorna série compacta</param>
        public static async Task<List<Cotacao>> ConsultarCotacao(string TicketApi, bool Compact, TipoCotacao tipo)
        {
            HttpClient client = new HttpClient();
            List<Cotacao> cotacao = new List<Cotacao>();
            CotaçõesApiResposta resultado;
            string url;

            // Prepara o endereço de acordo com o tipo de cotação desejada
            switch (tipo)
            {
                case TipoCotacao.Diaria:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=" + TicketApi + "&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
                case TipoCotacao.Mensal:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=" + TicketApi + "&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
                case TipoCotacao.Intraday1Minuto:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + TicketApi + "&interval=1min&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
                case TipoCotacao.Intraday5Minuto:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + TicketApi + "&interval=5min&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
                case TipoCotacao.Intraday15Minuto:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + TicketApi + "&interval=15min&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
                case TipoCotacao.Intraday30Minuto:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + TicketApi + "&interval=30min&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
                case TipoCotacao.Intraday1Hora:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + TicketApi + "&interval=60min&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
                default:
                    url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=" + TicketApi + "&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
                    break;
            }

            string pesquisa;

            HttpResponseMessage response = await client.GetAsync(url);

            // Verifica se o status da consulta foi bem sucedida
            if (response.IsSuccessStatusCode)
            {
                // Deserializa o resoltado da consulta a API
                pesquisa = response.Content.ReadAsStringAsync().Result;
                resultado = JsonConvert.DeserializeObject<CotaçõesApiResposta>(pesquisa);
                
                // Processa o retorno da Api para o padrão do sistema
                foreach (var item in resultado.Cotacoes)
                {
                    cotacao.Add(new Cotacao()
                    {
                        Data = Convert.ToDateTime(item.Key),
                        Abertura = Convert.ToDecimal(item.Value.Open),
                        Maxima = Convert.ToDecimal(item.Value.High),
                        Minima = Convert.ToDecimal(item.Value.Low),
                        Fechamento = Convert.ToDecimal(item.Value.Close),
                        Volume = Convert.ToInt64(item.Value.Volume)
                    });
                }
            }

            return cotacao;

        }

        private class CotaçõesApiResposta
        {
            [JsonProperty("Meta Data")]
            public MetaData MetaData { get; set; }

            [JsonProperty("Time Series (Daily)")]
            public Dictionary<string, TimeSeriesDaily> Cotacoes { get; set; }
        }

        private class MetaData
        {
            [JsonProperty("1. Information")]
            public string Information { get; set; }

            [JsonProperty("2. Symbol")]
            public string Symbol { get; set; }

            [JsonProperty("3. Last Refreshed")]
            public string LastRefreshed { get; set; }

            [JsonProperty("4. Output Size")]
            public string OutputSize { get; set; }

            [JsonProperty("5. Time Zone")]
            public string TimeZone { get; set; }
        }

        public class TimeSeriesDaily
        {
            [JsonProperty("1. open")]
            public decimal Open { get; set; }

            [JsonProperty("2. high")]
            public decimal High { get; set; }

            [JsonProperty("3. low")]
            public decimal Low { get; set; }

            [JsonProperty("4. close")]
            public decimal Close { get; set; }

            [JsonProperty("5. volume")]
            public long Volume { get; set; }
        }

        #endregion

        #region Obter dados dos Ativos

        /// <summary>
        /// Pesquisar ativos com base no input do usuário
        /// </summary>
        /// <param name="Texto">Ticket do Ativo</param>
        public static async Task<List<Ativo>> PesquisarAtivo(string Texto)
        {
            HttpClient client = new HttpClient();
            List<Ativo> ativos = new List<Ativo>();
            ListaResultadosPesquisa resultado;
            string url = "https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=" + Texto + "&apikey=F6HSSNHPLC6IZEPL";
            string pesquisa;

            HttpResponseMessage response = await client.GetAsync(url);

            // Verifica se o status da consulta foi bem sucedida
            if (response.IsSuccessStatusCode)
            {
                // Deserializa o resoltado da consulta a API
                pesquisa = response.Content.ReadAsStringAsync().Result;
                resultado = JsonConvert.DeserializeObject<ListaResultadosPesquisa>(pesquisa);

                // Processa o retorno da Api para o padrão do sistema
                foreach (var item in resultado.Lista)
                {
                    ativos.Add(new Ativo()
                    {
                        CodigoAPI = item.Symbol,
                        Ticket = item.Symbol.Split('.')[0],
                        Empresa = item.Name,
                        Regiao = item.Region,
                        Moeda = item.Currency
                    });
                }
            }

            return ativos;

        }

        private class ResultadoPesquisa
        {
            [JsonProperty("1. symbol")]
            public string Symbol { get; set; }

            [JsonProperty("2. name")]
            public string Name { get; set; }

            [JsonProperty("3. type")]
            public string Type { get; set; }

            [JsonProperty("4. region")]
            public string Region { get; set; }

            [JsonProperty("5. marketOpen")]
            public string MarketOpen { get; set; }

            [JsonProperty("6. marketClose")]
            public string MarketClose { get; set; }

            [JsonProperty("7. timezone")]
            public string Timezone { get; set; }

            [JsonProperty("8. currency")]
            public string Currency { get; set; }

            [JsonProperty("9. matchScore")]
            public string MatchScore { get; set; }
        }

        private class ListaResultadosPesquisa
        {
            [JsonProperty("bestMatches")]
            public List<ResultadoPesquisa> Lista { get; set; }
        }


        #endregion

    }
}
