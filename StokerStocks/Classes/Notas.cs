using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokerStocks
{
    public class Notas : INotifyPropertyChanged
    {

        #region Propriedades

        /// <summary>
        /// id da nota de corretagem no banco de dados
        /// </summary>
        private int idNotas;
        public int idnotas
        {
            get { return idNotas; }
            set
            {
                idNotas = value;
                foreach (Ordem item in MySqlQueries.CarregarOrdens(value))
                {
                    AdicionarOrdem(item);
                }
            }
        }
        //public int idNotas { get; set; }

        /// <summary>
        /// Número da nota de corretagem
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Data do pregão
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Nome da corretora que emitiu a nota de corretagem
        /// </summary>
        public Corretoras Corretora { get; set; }

        /// <summary>
        /// Coleção de ordens que fazem parte da nota de corretagem
        /// </summary>
        public ObservableCollection<Ordem> Ordens { get; private set; }

        /// <summary>
        /// Totaliza as compras executadas
        /// </summary>
        public decimal TotalCompra { get; set; } = 0;

        /// <summary>
        /// Totaliza as vendas executadas
        /// </summary>
        public decimal TotalVenda { get; set; } = 0;

        /// <summary>
        /// Totaliza o valor operado entre compras e venda
        /// </summary>
        public decimal TotalOperacao => TotalVenda + TotalCompra;

        #endregion

        #region Construtores
        
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public Notas()
        {
            Ordens = new ObservableCollection<Ordem>();
            Ordens.CollectionChanged += Ordens_CollectionChanged;
        }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Adiciona uma nova ordem a coleção da nota de corretagem
        /// </summary>
        /// <param name="ordem"></param>
        public void AdicionarOrdem(Ordem ordem)
        {
            Ordens.Add(ordem);
        }

        /// <summary>
        /// Remove uma ordem da coleção da nota de corretagem
        /// </summary>
        /// <param name="ordem"></param>
        public void RemoverOrdem(Ordem ordem)
        {
            Ordens.Remove(ordem);
        }

        #endregion

        #region Metodos Privados

        /// <summary>
        /// Realiza os calculos totais da nota de corretagem
        /// </summary>
        private void CalculaNota()
        {
            TotalVenda =  0;
            TotalCompra = 0;

            foreach (var item in Ordens)
            {
                if (item.Operação == Operações.Venda) TotalVenda += item.Total;
                if (item.Operação == Operações.Compra) TotalCompra += item.Total;
            }
        }

        /// <summary>
        /// Monitora mudanças na coleção das ordens
        /// </summary>
        private void Ordens_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        ((Ordem)item).PropertyChanged += Item_PropertyChanged;
                    }
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    ((Ordem)item).PropertyChanged -= Item_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                foreach (var item in e.NewItems)
                {
                    ((Ordem)item).PropertyChanged += Item_PropertyChanged;
                }
                foreach (var item in e.OldItems)
                {
                    ((Ordem)item).PropertyChanged -= Item_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                foreach (var item in e.OldItems)
                {
                    ((Ordem)item).PropertyChanged -= Item_PropertyChanged;
                }
            }

            CalculaNota();

        }

        /// <summary>
        /// Dispara o calculo de totais da nota em caso de mudanças na coleção ou nos valores individuais da 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CalculaNota();
        }

        #endregion

        #region Eventos

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
