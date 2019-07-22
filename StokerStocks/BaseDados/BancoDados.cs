using System;
using System.Collections.ObjectModel;

namespace StokerStocks
{
    static class BancoDados
    {

        /// <summary>
        /// Carrega os ativos da carteira
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Ativo> CarregarAtivos()
        {
            ObservableCollection<Ativo> temp = new ObservableCollection<Ativo>();

            temp.Add(new Ativo("SPTW11"));

            return temp;
        }

        public static ObservableCollection<Ordem> CarregarOrdens(string Ticket)
        {
            return new ObservableCollection<Ordem>()
                    {
                        new Ordem(){ Date = new DateTime(2016, 09, 12), Operação = Operações.Compra, Quantidade = 27, ValorUnitário = 55.5m },
                        new Ordem(){ Date = new DateTime(2016, 10, 04), Operação = Operações.Compra, Quantidade = 10, ValorUnitário = 57.6m },
                        new Ordem(){ Date = new DateTime(2016, 10, 19), Operação = Operações.Compra, Quantidade = 3, ValorUnitário = 61.3m },
                        new Ordem(){ Date = new DateTime(2017, 02, 06), Operação = Operações.Venda, Quantidade = 2, ValorUnitário = 80.5m },
                        new Ordem(){ Date = new DateTime(2017, 03, 08), Operação = Operações.Compra, Quantidade = 10, ValorUnitário = 81.1m },
                        new Ordem(){ Date = new DateTime(2017, 03, 15), Operação = Operações.Venda, Quantidade = 38, ValorUnitário = 81.5m },
                        new Ordem(){ Date = new DateTime(2019, 06, 25), Operação = Operações.Venda, Quantidade = 10, ValorUnitário = 88.9m },
                    };
        }

        /// <summary>
        /// Carrega o historico de cotações para um periodo de tempo limitado
        /// </summary>
        /// <param name="Inicio"></param>
        /// <param name="Final"></param>
        /// <returns></returns>
        public static ObservableCollection<Cotacao> CarregarCotacoes(DateTime Inicio, DateTime Final)
        {
            ObservableCollection<Cotacao> DB = new ObservableCollection<Cotacao>()
            {
                new Cotacao() { Data = new DateTime(2019,7,19), Abertura=88.90m, Maxima=89.00m, Minima=87.07m, Fechamento=87.18m },
                new Cotacao() { Data = new DateTime(2019,6,28), Abertura=88.97m, Maxima=88.97m, Minima=86.30m, Fechamento=88.03m },
                new Cotacao() { Data = new DateTime(2019,5,31), Abertura=88.2m, Maxima=90m, Minima=86m, Fechamento=89m },
                new Cotacao() { Data = new DateTime(2019,5,31), Abertura=88.2m, Maxima=90m, Minima=86m, Fechamento=89m },
                new Cotacao() { Data = new DateTime(2019,4,30), Abertura=85.95m, Maxima=88.9m, Minima=84m, Fechamento=88.2m },
                new Cotacao() { Data = new DateTime(2019,3,29), Abertura=80.89m, Maxima=88m, Minima=79.5m, Fechamento=86m },
                new Cotacao() { Data = new DateTime(2019,2,28), Abertura=80.94m, Maxima=82.79m, Minima=78.75m, Fechamento=81m },
                new Cotacao() { Data = new DateTime(2019,1,31), Abertura=75.24m, Maxima=81.99m, Minima=74.8m, Fechamento=80.5m },
                new Cotacao() { Data = new DateTime(2018,12,28), Abertura=74.79m, Maxima=77.99m, Minima=72.26m, Fechamento=75.26m },
                new Cotacao() { Data = new DateTime(2018,11,30), Abertura=73.9m, Maxima=75.2m, Minima=72.02m, Fechamento=74.95m },
                new Cotacao() { Data = new DateTime(2018,10,31), Abertura=72.75m, Maxima=75m, Minima=70m, Fechamento=74.86m },
                new Cotacao() { Data = new DateTime(2018,9,28), Abertura=72.48m, Maxima=73.27m, Minima=71.16m, Fechamento=72.75m },
                new Cotacao() { Data = new DateTime(2018,8,31), Abertura=74m, Maxima=74.59m, Minima=71m, Fechamento=72.71m },
                new Cotacao() { Data = new DateTime(2018,7,31), Abertura=72.2m, Maxima=74.95m, Minima=70.99m, Fechamento=74.39m },
                new Cotacao() { Data = new DateTime(2018,6,29), Abertura=71.99m, Maxima=72.89m, Minima=69.13m, Fechamento=72.2m },
                new Cotacao() { Data = new DateTime(2018,5,30), Abertura=79.2m, Maxima=79.2m, Minima=67.16m, Fechamento=73m },
                new Cotacao() { Data = new DateTime(2018,4,30), Abertura=77.6m, Maxima=80m, Minima=76.81m, Fechamento=79m },
                new Cotacao() { Data = new DateTime(2018,3,29), Abertura=77.6m, Maxima=80m, Minima=74.4m, Fechamento=79.95m },
                new Cotacao() { Data = new DateTime(2018,2,28), Abertura=79m, Maxima=80.2m, Minima=74.5m, Fechamento=77.5m },
                new Cotacao() { Data = new DateTime(2018,1,31), Abertura=74.51m, Maxima=80m, Minima=74.05m, Fechamento=79.05m },
                new Cotacao() { Data = new DateTime(2017,12,29), Abertura=74.4m, Maxima=75m, Minima=72.03m, Fechamento=74.37m },
                new Cotacao() { Data = new DateTime(2017,11,30), Abertura=74.49m, Maxima=75.74m, Minima=71.2m, Fechamento=75m },
                new Cotacao() { Data = new DateTime(2017,10,31), Abertura=73.3m, Maxima=76m, Minima=71.2m, Fechamento=74.5m },
                new Cotacao() { Data = new DateTime(2017,9,29), Abertura=72.59m, Maxima=73.4m, Minima=70.11m, Fechamento=73.36m },
                new Cotacao() { Data = new DateTime(2017,8,31), Abertura=68.7m, Maxima=73m, Minima=68.7m, Fechamento=72.6m },
                new Cotacao() { Data = new DateTime(2017,7,31), Abertura=70.11m, Maxima=73.48m, Minima=68.9m, Fechamento=68.9m },
                new Cotacao() { Data = new DateTime(2017,6,30), Abertura=70m, Maxima=74.85m, Minima=68.5m, Fechamento=72m },
                new Cotacao() { Data = new DateTime(2017,5,31), Abertura=71.77m, Maxima=74m, Minima=67m, Fechamento=71.35m },
                new Cotacao() { Data = new DateTime(2017,4,28), Abertura=72.5m, Maxima=74.99m, Minima=67m, Fechamento=72.3m },
                new Cotacao() { Data = new DateTime(2017,3,31), Abertura=63.67m, Maxima=75m, Minima=63.65m, Fechamento=71.5m },
                new Cotacao() { Data = new DateTime(2017,2,24), Abertura=63m, Maxima=64.95m, Minima=62.5m, Fechamento=64m },
                new Cotacao() { Data = new DateTime(2017,1,31), Abertura=60.26m, Maxima=63.63m, Minima=58.36m, Fechamento=63.4m },
                new Cotacao() { Data = new DateTime(2016,12,29), Abertura=58m, Maxima=60.8m, Minima=55.3m, Fechamento=60.8m },
                new Cotacao() { Data = new DateTime(2016,11,30), Abertura=61m, Maxima=62m, Minima=53.01m, Fechamento=58m },
                new Cotacao() { Data = new DateTime(2016,10,31), Abertura=59m, Maxima=63.95m, Minima=55.51m, Fechamento=61m },
                new Cotacao() { Data = new DateTime(2016,9,30), Abertura=60m, Maxima=60m, Minima=54.2m, Fechamento=59.85m },
                new Cotacao() { Data = new DateTime(2016,8,31), Abertura=74.99m, Maxima=80m, Minima=57.3m, Fechamento=59.12m },
                new Cotacao() { Data = new DateTime(2016,7,29), Abertura=69.69m, Maxima=79.49m, Minima=68m, Fechamento=75.8m },
                new Cotacao() { Data = new DateTime(2016,6,30), Abertura=69.75m, Maxima=69.99m, Minima=67.8m, Fechamento=69.7m },
                new Cotacao() { Data = new DateTime(2016,5,31), Abertura=67m, Maxima=69.99m, Minima=64m, Fechamento=69.19m },
                new Cotacao() { Data = new DateTime(2016,4,29), Abertura=68.48m, Maxima=69m, Minima=65.1m, Fechamento=68.4m },
                new Cotacao() { Data = new DateTime(2016,3,31), Abertura=54.02m, Maxima=69.89m, Minima=53.1m, Fechamento=67.99m },
                new Cotacao() { Data = new DateTime(2016,2,29), Abertura=56.01m, Maxima=56.49m, Minima=51.62m, Fechamento=55.33m },
                new Cotacao() { Data = new DateTime(2016,1,29), Abertura=60.42m, Maxima=60.42m, Minima=55m, Fechamento=56.5m },
                new Cotacao() { Data = new DateTime(2015,12,30), Abertura=64.49m, Maxima=64.49m, Minima=58.57m, Fechamento=61.3m },
                new Cotacao() { Data = new DateTime(2015,11,30), Abertura=64.98m, Maxima=64.98m, Minima=62m, Fechamento=64.5m },
                new Cotacao() { Data = new DateTime(2015,10,30), Abertura=63.96m, Maxima=65m, Minima=61.61m, Fechamento=65m },
                new Cotacao() { Data = new DateTime(2015,9,30), Abertura=68m, Maxima=68.45m, Minima=62.5m, Fechamento=64.88m },
                new Cotacao() { Data = new DateTime(2015,8,31), Abertura=70.11m, Maxima=70.11m, Minima=65.22m, Fechamento=69m },
                new Cotacao() { Data = new DateTime(2015,7,31), Abertura=70.34m, Maxima=70.5m, Minima=69.31m, Fechamento=70.5m },
                new Cotacao() { Data = new DateTime(2015,6,30), Abertura=70.25m, Maxima=71m, Minima=69.13m, Fechamento=70.7m },
                new Cotacao() { Data = new DateTime(2015,5,29), Abertura=70.1m, Maxima=71.97m, Minima=68m, Fechamento=70.69m },
                new Cotacao() { Data = new DateTime(2015,4,30), Abertura=70.75m, Maxima=72.49m, Minima=68.83m, Fechamento=72.29m },
                new Cotacao() { Data = new DateTime(2015,3,31), Abertura=70.53m, Maxima=71.48m, Minima=68.01m, Fechamento=70m },
                new Cotacao() { Data = new DateTime(2015,2,27), Abertura=73.44m, Maxima=73.44m, Minima=71m, Fechamento=71m },
                new Cotacao() { Data = new DateTime(2015,1,30), Abertura=74.98m, Maxima=74.99m, Minima=71m, Fechamento=73.47m },
                new Cotacao() { Data = new DateTime(2014,12,30), Abertura=70.99m, Maxima=72.5m, Minima=68.68m, Fechamento=72.48m },
                new Cotacao() { Data = new DateTime(2014,11,28), Abertura=73m, Maxima=73.48m, Minima=67.25m, Fechamento=71.4m },
                new Cotacao() { Data = new DateTime(2014,10,31), Abertura=71.8m, Maxima=74.11m, Minima=67.05m, Fechamento=73.19m },
                new Cotacao() { Data = new DateTime(2014,9,30), Abertura=73.11m, Maxima=74.78m, Minima=70.01m, Fechamento=70.02m },
            };

            ObservableCollection<Cotacao> Resultado = new ObservableCollection<Cotacao>();

            DateTime FinalMes = new DateTime(Final.Year, Final.Month, DateTime.DaysInMonth(Final.Year, Final.Month), 23, 59, 59);
            DateTime InicioMes = new DateTime(Inicio.Year, Inicio.Month, 1, 0, 0, 0);

            foreach (Cotacao item in DB)
            {
                if ((item.Data > InicioMes) && (item.Data <= FinalMes))
                {
                    Resultado.Add(item);
                }
            }

            return Resultado;
        }

        /// <summary>
        /// Carrega o historico de cotações de uma data de inicio até hoje
        /// </summary>
        /// <param name="Inicio"></param>
        /// <returns></returns>
        public static ObservableCollection<Cotacao> CarregarCotacoes(DateTime Inicio)
        {
            ObservableCollection<Cotacao> DB = new ObservableCollection<Cotacao>()
            {
                new Cotacao() { Data = new DateTime(2019,7,19), Abertura=88.90m, Maxima=89.00m, Minima=87.07m, Fechamento=87.18m },
                new Cotacao() { Data = new DateTime(2019,6,28), Abertura=88.97m, Maxima=88.97m, Minima=86.30m, Fechamento=88.03m },
                new Cotacao() { Data = new DateTime(2019,5,31), Abertura=88.2m, Maxima=90m, Minima=86m, Fechamento=89m },
                new Cotacao() { Data = new DateTime(2019,5,31), Abertura=88.2m, Maxima=90m, Minima=86m, Fechamento=89m },
                new Cotacao() { Data = new DateTime(2019,4,30), Abertura=85.95m, Maxima=88.9m, Minima=84m, Fechamento=88.2m },
                new Cotacao() { Data = new DateTime(2019,3,29), Abertura=80.89m, Maxima=88m, Minima=79.5m, Fechamento=86m },
                new Cotacao() { Data = new DateTime(2019,2,28), Abertura=80.94m, Maxima=82.79m, Minima=78.75m, Fechamento=81m },
                new Cotacao() { Data = new DateTime(2019,1,31), Abertura=75.24m, Maxima=81.99m, Minima=74.8m, Fechamento=80.5m },
                new Cotacao() { Data = new DateTime(2018,12,28), Abertura=74.79m, Maxima=77.99m, Minima=72.26m, Fechamento=75.26m },
                new Cotacao() { Data = new DateTime(2018,11,30), Abertura=73.9m, Maxima=75.2m, Minima=72.02m, Fechamento=74.95m },
                new Cotacao() { Data = new DateTime(2018,10,31), Abertura=72.75m, Maxima=75m, Minima=70m, Fechamento=74.86m },
                new Cotacao() { Data = new DateTime(2018,9,28), Abertura=72.48m, Maxima=73.27m, Minima=71.16m, Fechamento=72.75m },
                new Cotacao() { Data = new DateTime(2018,8,31), Abertura=74m, Maxima=74.59m, Minima=71m, Fechamento=72.71m },
                new Cotacao() { Data = new DateTime(2018,7,31), Abertura=72.2m, Maxima=74.95m, Minima=70.99m, Fechamento=74.39m },
                new Cotacao() { Data = new DateTime(2018,6,29), Abertura=71.99m, Maxima=72.89m, Minima=69.13m, Fechamento=72.2m },
                new Cotacao() { Data = new DateTime(2018,5,30), Abertura=79.2m, Maxima=79.2m, Minima=67.16m, Fechamento=73m },
                new Cotacao() { Data = new DateTime(2018,4,30), Abertura=77.6m, Maxima=80m, Minima=76.81m, Fechamento=79m },
                new Cotacao() { Data = new DateTime(2018,3,29), Abertura=77.6m, Maxima=80m, Minima=74.4m, Fechamento=79.95m },
                new Cotacao() { Data = new DateTime(2018,2,28), Abertura=79m, Maxima=80.2m, Minima=74.5m, Fechamento=77.5m },
                new Cotacao() { Data = new DateTime(2018,1,31), Abertura=74.51m, Maxima=80m, Minima=74.05m, Fechamento=79.05m },
                new Cotacao() { Data = new DateTime(2017,12,29), Abertura=74.4m, Maxima=75m, Minima=72.03m, Fechamento=74.37m },
                new Cotacao() { Data = new DateTime(2017,11,30), Abertura=74.49m, Maxima=75.74m, Minima=71.2m, Fechamento=75m },
                new Cotacao() { Data = new DateTime(2017,10,31), Abertura=73.3m, Maxima=76m, Minima=71.2m, Fechamento=74.5m },
                new Cotacao() { Data = new DateTime(2017,9,29), Abertura=72.59m, Maxima=73.4m, Minima=70.11m, Fechamento=73.36m },
                new Cotacao() { Data = new DateTime(2017,8,31), Abertura=68.7m, Maxima=73m, Minima=68.7m, Fechamento=72.6m },
                new Cotacao() { Data = new DateTime(2017,7,31), Abertura=70.11m, Maxima=73.48m, Minima=68.9m, Fechamento=68.9m },
                new Cotacao() { Data = new DateTime(2017,6,30), Abertura=70m, Maxima=74.85m, Minima=68.5m, Fechamento=72m },
                new Cotacao() { Data = new DateTime(2017,5,31), Abertura=71.77m, Maxima=74m, Minima=67m, Fechamento=71.35m },
                new Cotacao() { Data = new DateTime(2017,4,28), Abertura=72.5m, Maxima=74.99m, Minima=67m, Fechamento=72.3m },
                new Cotacao() { Data = new DateTime(2017,3,31), Abertura=63.67m, Maxima=75m, Minima=63.65m, Fechamento=71.5m },
                new Cotacao() { Data = new DateTime(2017,2,24), Abertura=63m, Maxima=64.95m, Minima=62.5m, Fechamento=64m },
                new Cotacao() { Data = new DateTime(2017,1,31), Abertura=60.26m, Maxima=63.63m, Minima=58.36m, Fechamento=63.4m },
                new Cotacao() { Data = new DateTime(2016,12,29), Abertura=58m, Maxima=60.8m, Minima=55.3m, Fechamento=60.8m },
                new Cotacao() { Data = new DateTime(2016,11,30), Abertura=61m, Maxima=62m, Minima=53.01m, Fechamento=58m },
                new Cotacao() { Data = new DateTime(2016,10,31), Abertura=59m, Maxima=63.95m, Minima=55.51m, Fechamento=61m },
                new Cotacao() { Data = new DateTime(2016,9,30), Abertura=60m, Maxima=60m, Minima=54.2m, Fechamento=59.85m },
                new Cotacao() { Data = new DateTime(2016,8,31), Abertura=74.99m, Maxima=80m, Minima=57.3m, Fechamento=59.12m },
                new Cotacao() { Data = new DateTime(2016,7,29), Abertura=69.69m, Maxima=79.49m, Minima=68m, Fechamento=75.8m },
                new Cotacao() { Data = new DateTime(2016,6,30), Abertura=69.75m, Maxima=69.99m, Minima=67.8m, Fechamento=69.7m },
                new Cotacao() { Data = new DateTime(2016,5,31), Abertura=67m, Maxima=69.99m, Minima=64m, Fechamento=69.19m },
                new Cotacao() { Data = new DateTime(2016,4,29), Abertura=68.48m, Maxima=69m, Minima=65.1m, Fechamento=68.4m },
                new Cotacao() { Data = new DateTime(2016,3,31), Abertura=54.02m, Maxima=69.89m, Minima=53.1m, Fechamento=67.99m },
                new Cotacao() { Data = new DateTime(2016,2,29), Abertura=56.01m, Maxima=56.49m, Minima=51.62m, Fechamento=55.33m },
                new Cotacao() { Data = new DateTime(2016,1,29), Abertura=60.42m, Maxima=60.42m, Minima=55m, Fechamento=56.5m },
                new Cotacao() { Data = new DateTime(2015,12,30), Abertura=64.49m, Maxima=64.49m, Minima=58.57m, Fechamento=61.3m },
                new Cotacao() { Data = new DateTime(2015,11,30), Abertura=64.98m, Maxima=64.98m, Minima=62m, Fechamento=64.5m },
                new Cotacao() { Data = new DateTime(2015,10,30), Abertura=63.96m, Maxima=65m, Minima=61.61m, Fechamento=65m },
                new Cotacao() { Data = new DateTime(2015,9,30), Abertura=68m, Maxima=68.45m, Minima=62.5m, Fechamento=64.88m },
                new Cotacao() { Data = new DateTime(2015,8,31), Abertura=70.11m, Maxima=70.11m, Minima=65.22m, Fechamento=69m },
                new Cotacao() { Data = new DateTime(2015,7,31), Abertura=70.34m, Maxima=70.5m, Minima=69.31m, Fechamento=70.5m },
                new Cotacao() { Data = new DateTime(2015,6,30), Abertura=70.25m, Maxima=71m, Minima=69.13m, Fechamento=70.7m },
                new Cotacao() { Data = new DateTime(2015,5,29), Abertura=70.1m, Maxima=71.97m, Minima=68m, Fechamento=70.69m },
                new Cotacao() { Data = new DateTime(2015,4,30), Abertura=70.75m, Maxima=72.49m, Minima=68.83m, Fechamento=72.29m },
                new Cotacao() { Data = new DateTime(2015,3,31), Abertura=70.53m, Maxima=71.48m, Minima=68.01m, Fechamento=70m },
                new Cotacao() { Data = new DateTime(2015,2,27), Abertura=73.44m, Maxima=73.44m, Minima=71m, Fechamento=71m },
                new Cotacao() { Data = new DateTime(2015,1,30), Abertura=74.98m, Maxima=74.99m, Minima=71m, Fechamento=73.47m },
                new Cotacao() { Data = new DateTime(2014,12,30), Abertura=70.99m, Maxima=72.5m, Minima=68.68m, Fechamento=72.48m },
                new Cotacao() { Data = new DateTime(2014,11,28), Abertura=73m, Maxima=73.48m, Minima=67.25m, Fechamento=71.4m },
                new Cotacao() { Data = new DateTime(2014,10,31), Abertura=71.8m, Maxima=74.11m, Minima=67.05m, Fechamento=73.19m },
                new Cotacao() { Data = new DateTime(2014,9,30), Abertura=73.11m, Maxima=74.78m, Minima=70.01m, Fechamento=70.02m },
            };

            ObservableCollection<Cotacao> Resultado = new ObservableCollection<Cotacao>();

            DateTime FinalMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month), 23, 59, 59);
            DateTime InicioMes = new DateTime(Inicio.Year, Inicio.Month, 1, 0, 0, 0);

            foreach (Cotacao item in DB)
            {
                if ((item.Data > InicioMes) && (item.Data <= FinalMes))
                {
                    Resultado.Add(item);
                }
            }

            return Resultado;
        }

    }
}
