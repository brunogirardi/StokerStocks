using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StokerStocks
{
    class IncluirAtivosMV
    {

        /// <summary>
        /// Lista de ativos recuperados da API
        /// </summary>
        public ObservableCollection<Ativo> Ativos { get; set; } = new ObservableCollection<Ativo>();

        /// <summary>
        /// Ativo selecionado na listview
        /// </summary>
        public Ativo AtivoSelecionado { get; set; }

        /// <summary>
        /// Texto preenchido no campo de pesquisa
        /// </summary>
        public string Pesquisa { get; set; } = "";

        /// <summary>
        /// Comando do botão de buscar ativo na API
        /// </summary>
        public ICommand Buscar { get; set; }

        /// <summary>
        /// Comando de aceite item selecionado
        /// Responsável por fechar a janela
        /// </summary>
        public ICommand Selecionar { get; set; }

        public delegate void DgItemSelecionado(Ativo ativo);

        public event DgItemSelecionado OnItemSelecionado;

        public IncluirAtivosMV()
        {

            // Inicializa comandos
            Buscar = new RelayCommand(new Action<object>(ExecutarBuscaAtivos), new Func<object,bool>(PodeExecutarBuscaAtivos));
            Selecionar = new RelayCommand(new Action<object>(ExecutarSelecionar), new Func<object, bool>(PodeExecutarSelecionar));

        }

        /// <summary>
        /// Execução do comando Buscar ativo na API
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecutarBuscaAtivos(object obj)
        {

            Ativos.Clear();

            List<Ativo> ativos = await ApiClient.PesquisarAtivo(Pesquisa);

            foreach (Ativo item in ativos)
            {
                Ativos.Add(item);
            }

        }

        /// <summary>
        /// Verifica a disponibilidade do botão Buscar
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool PodeExecutarBuscaAtivos(object obj)
        {
            if (Pesquisa.Length > 0) return true;

            return false;
        }

        /// <summary>
        /// Executa a ação de fechar a janela, incluir o ativo ao DB e retornar o ativo selecionado
        /// </summary>
        /// <param name="obj"></param>
        private void ExecutarSelecionar(object obj)
        {
            OnItemSelecionado(AtivoSelecionado);
        }

        /// <summary>
        /// Verifica se existe item selecionado na lista e libera o botão de selecionar
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool PodeExecutarSelecionar(object obj)
        {
            if (AtivoSelecionado != null) return true;

            return false;
        }



    }

    public class ItemSelecionadoEventArgs : EventArgs
    {
        public Ativo AtivoSelecionado { get; set; }
    }

}
