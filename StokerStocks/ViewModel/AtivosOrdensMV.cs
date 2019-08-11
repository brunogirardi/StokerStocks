using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StokerStocks
{ 
    class AtivosOrdensMV
    {

        #region Construtor

        public AtivosOrdensMV()
        {

            Ativos = new ObservableCollection<Ativo>(MainRouter.Ativos());

            CarregarAtivo = new RelayCommand(new Action<object>(ExecutarCarregarAtivo));

        }
        #endregion

        #region Propriedades

        public ObservableCollection<Ativo> Ativos { get; set; }

        private Ativo ativoSelecionado;
        public Ativo AtivoSelecionado
        {
            get { return ativoSelecionado; }
            set { ativoSelecionado = value; }
        }

        #endregion

        #region Comandos

        // Ativos
        public ICommand CarregarAtivo { get; set; }

        // Comandos para ações da UI
        public ICommand TicketOrderLostFocus { get; set; }

        #endregion

        #region Comandos (Funções de Execução e Validação)

                /// <summary>
        /// Adiciona nova ativo a base de Ativos
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarCarregarAtivo(object obj)
        {
            IncluirAtivos dlg = new IncluirAtivos();
            dlg.ShowDialog();
            if (dlg.AtivoSelecionado != null)
            {
                Ativo ativo = dlg.AtivoSelecionado;

                ativo.IdAtivos = MySqlQueries.CreateAtivo(ativo);

                CotacaoManager.UpdateCotacao(ativo.IdAtivos);

                Ativos.Add(ativo);
            }
        }

        #endregion

    }
}
