using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace StokerStocks
{
    public class MainWindowVM : INotifyPropertyChanged
    {

        public ObservableCollection<Ativo> Ativos { get; set; }

        public ObservableCollection<Notas> Notas { get; set; }

        public ObservableCollection<Corretoras> Corretoras { get; set; }

        public Notas NotaSelecionada { get; set; }

        public Ordem OrdemSelecionada { get; set; }

        public bool ModoEdiçãoOrdem { get; set; } = true;

        public bool ModoEdiçãoNota { get; set; } = true;

        public MainWindowVM()
        {

            Corretoras = MySqlQueries.CarregarCorretoras();

            Notas = MySqlQueries.CarregarNotas();

            // Inicializar Comandos
            AdicionarOrdem = new RelayCommand(new Action<object>(ExecutarAdicionarOrdem), new Func<object, bool>(PodeExecutarAdicionarOrdem));
            ExcluirOrdem = new RelayCommand(new Action<object>(ExecutarExcluirOrdem), new Func<object, bool>(PodeExecutarExcluirOrdem));

            AdicionarNota = new RelayCommand(new Action<object>(ExecutarAdicionarNota));
            SalvarNota = new RelayCommand(new Action<object>(ExecutarSalvarNota));
            ExcluirNota = new RelayCommand(new Action<object>(ExecutarExcluirNota), new Func<object, bool>(PodeExecutarExcluirNota));

            CarregarAtivo = new RelayCommand(new Action<object>(ExecutarCarregarAtivo));

            TicketOrderLostFocus = new RelayCommand(new Action<object>(ExecutarTicketOrderLostFocus));
        }

        /// <summary>
        /// Executa o comando de adição de nova ordem
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarAdicionarOrdem(object obj)
        {
            if (ModoEdiçãoOrdem)
            {
                // Implementar adição do item
                Ordem ordem = new Ordem() { idNotas = NotaSelecionada.idnotas };
                NotaSelecionada.Ordens.Add(ordem);
                OrdemSelecionada = ordem;
                ModoEdiçãoOrdem = false;
            } else
            {
                // Após concluído a inserção dos itens insere a ordem no BD
                ModoEdiçãoOrdem = true;
                OrdemSelecionada.idOrdens = MySqlQueries.CreateOrdens(OrdemSelecionada);
            }
        }

        /// <summary>
        /// Verifica se existe alguma Nota de corretagem selecionada
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool PodeExecutarAdicionarOrdem(object obj)
        {
            if (NotaSelecionada != null) return true;

            return false;
        }

        /// <summary>
        /// Executa o comando de exclusão da ordem selecionada
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarExcluirOrdem(object obj)
        {
            MySqlQueries.DeleteOrdens(OrdemSelecionada);
            NotaSelecionada.RemoverOrdem(OrdemSelecionada);
        }

        /// <summary>
        /// Verifica se existe alguma ordem selecionada para exclusão
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool PodeExecutarExcluirOrdem(object obj)
        {
            if (OrdemSelecionada != null && OrdemSelecionada.idOrdens > 0) return true;

            return false;
        }
        
        /// <summary>
        /// Executa o comando de adição de uma nova nota
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarAdicionarNota(object obj)
        {
            if (ModoEdiçãoNota)
            {
                // Implementar adição do item
                Notas nota = new Notas();
                Notas.Add(nota);
                NotaSelecionada = nota;
                ModoEdiçãoNota = false;
            }
        }

        /// <summary>
        /// Executa o comando de adição de uma nova nota
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarSalvarNota(object obj)
        {
            if (!ModoEdiçãoNota)
            {
                // Implementar adição do item
                ModoEdiçãoNota = true;
                NotaSelecionada.idnotas = MySqlQueries.CreateNota(NotaSelecionada);
            }
        }

        /// <summary>
        /// Executar o comando de exclusão de uma nota existente
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarExcluirNota(object obj)
        {
            MySqlQueries.DeleteNota(NotaSelecionada.idnotas);
            Notas.Remove(NotaSelecionada);
        }
        
        /// <summary>
        /// Verifica se as condições mínimas foram atendidas para a execução do exluir nota
        /// </summary>
        private bool PodeExecutarExcluirNota(object obj)
        {
            if (NotaSelecionada != null && NotaSelecionada.idnotas > 0) return true;

            return false;
        }

        private void ExecutarCarregarAtivo(object obj)
        {
            CotacaoManager.HistoricoMensal(2, new DateTime(2019, 6, 1), DateTime.Today);
        }

        /// <summary>
        /// Função carrega as informações do ticket inserido
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarTicketOrderLostFocus(object obj)
        {
            if (!ModoEdiçãoOrdem)
            {
                Ativo ativo = MySqlQueries.CarregarAtivos(OrdemSelecionada.Ticket);
                if (ativo != null)
                {
                    OrdemSelecionada.Empresa = ativo.Empresa;
                    OrdemSelecionada.idAtivos = ativo.IdAtivos;
                } else
                {
                    MessageBox.Show("Nenhum ativo localizado!");
                }
            }
        }

        #region Comandos

        // Ordens
        public ICommand AdicionarOrdem { get; set; }
        public ICommand ExcluirOrdem { get; set; }

        // Notas
        public ICommand AdicionarNota { get; set; }
        public ICommand SalvarNota { get; set; }
        public ICommand ExcluirNota { get; set; }

        // Ativos
        public ICommand CarregarAtivo { get; set; }


        public ICommand TicketOrderLostFocus { get; set; }



        #endregion


        public event PropertyChangedEventHandler PropertyChanged;



    }
}
