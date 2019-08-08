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

        /// <summary>
        /// Consulta a API no site Alphavantage para a com sério diaria das cotações
        /// </summary>
        /// <param name="TicketApi">Ticket do Ativo</param>
        /// <param name="Compact">Retorna série compacta</param>
        public static async Task<List<Cotacao>> ConsultarCotacao(string TicketApi, bool Compact)
        {
            HttpClient client = new HttpClient();
            List<Cotacao> cotacao = new List<Cotacao>();
            CotaçõesApiResposta resultado;
            string url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=" + TicketApi + "&outputsize=" + (Compact == true ? "compact" : "full") + "&apikey=F6HSSNHPLC6IZEPL";
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
                        Volume = Convert.ToInt32(item.Value.Volume)
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

    }
}
