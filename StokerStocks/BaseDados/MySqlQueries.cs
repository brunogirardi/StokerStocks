using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StokerStocks
{
    class MySqlQueries
    {

        private static string LoadConnectionString(string id = "MySql")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        #region Ativos

        /// <summary>
        /// Carrega os Ativos do Banco de Dados
        /// </summary>
        /// <returns></returns>
        public static List<Ativo> CarregarAtivos()
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Ativo>("SELECT * FROM ativos;", new DynamicParameters());

                List<Ativo> lista = output.ToList();

                return new List<Ativo>(lista);
            }

        }

        /// <summary>
        /// Carrega o ativo a partir do ticket
        /// </summary>
        /// <returns></returns>
        public static Ativo CarregarAtivos(string Ticket)
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                try
                {
                    var output = cnn.QuerySingleOrDefault<Ativo>("SELECT * FROM ativos WHERE ticket = @Ticket;", new { Ticket });
                    return output;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// Carrega o ativo a partir do Id
        /// </summary>
        /// <param name="IdAtivo"></param>
        /// <returns></returns>
        public static Ativo CarregarAtivos(int IdAtivo)
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {
                var output = cnn.QuerySingleOrDefault<Ativo>("SELECT * FROM ativos WHERE idAtivos = @Id;", new { Id = IdAtivo });

                return output;
            }

        }

        public static int CreateAtivo(Ativo ativo)
        {

            string sql = "INSERT INTO ativos (Ticket, Empresa, CodigoAPI, Tipo) VALUES (@Ticket, @Empresa, @CodigoAPI, 0); SELECT LAST_INSERT_ID();";

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                cnn.Open();

                return cnn.ExecuteScalar<int>(sql, ativo);

            }

        }

        #endregion

        #region Notas de corretagem

        /// <summary>
        /// Carrega as notas de corretagem do banco de dados
        /// </summary>
        /// <returns></returns>
        public static List<Notas> CarregarNotas()
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {
                string sql = "SELECT notas.*, corretoras.idCorretoras, corretoras.nome, corretoras.cnpj FROM notas INNER JOIN corretoras ON corretoras.idcorretoras = notas.idcorretora;";

                var output = cnn.Query<Notas, Corretoras, Notas>(
                    sql, 
                    (nota, corretora) => {
                        nota.Corretora = corretora;
                        return nota;
                    }, 
                    splitOn: "idCorretoras");

                List <Notas> lista = output.ToList();

                return new List<Notas>(lista);
            }
        }

        /// <summary>
        /// Insere em uma nova ordem no banco de dados
        /// </summary>
        /// <param name="ordem"></param>
        public static int CreateNota(Notas nota)
        {
            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                int id = Convert.ToInt32(cnn.ExecuteScalar("INSERT INTO notas (idcorretora, data, numero) VALUES (@idcorretora, @data, @numero); SELECT LAST_INSERT_ID();",
                            new
                            {
                                idcorretora = nota.Corretora.idCorretoras,
                                data = nota.Data,
                                numero = nota.Numero
                            }
                         ));

                return id;

                
            }
        }

        /// <summary>
        /// Deleta a nota de corretagem selecionada
        /// </summary>
        /// <param name="Id"></param>
        public static void DeleteNota(int Id)
        {
            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM notas WHERE idnotas=@id", new { id = Id });
            }
        }

        #endregion

        #region Ordens da Nota

        /// <summary>
        /// Carrega a lista de ordens de uma nota de corretagem
        /// </summary>
        /// <param name="idNotaCorretagem"></param>
        /// <returns></returns>
        public static List<Ordem> CarregarOrdens(int idNotaCorretagem)
        {
            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Ordem>("SELECT ordens.*, ativos.Empresa, ativos.Ticket FROM ordens INNER JOIN ativos ON ativos.idAtivos = ordens.idAtivos WHERE idnotas = @id;", new { id = idNotaCorretagem });

                List<Ordem> lista = output.ToList();

                return new List<Ordem>(lista);
            }

        }

        /// <summary>
        /// Retorna todas as ordens feitas para um ativo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static List<Ordem> CarregarOrdensAtivo(int Id)
        {
            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Ordem>("SELECT * FROM ordens INNER JOIN notas ON notas.idnotas = ordens.idnotas WHERE idativos = @id ORDER BY data;", new { id = Id });

                List<Ordem> lista = output.ToList();

                return new List<Ordem>(lista);
            }
        }

        /// <summary>
        /// Insere em uma nova ordem no banco de dados
        /// </summary>
        /// <param name="ordem"></param>
        public static int CreateOrdens(Ordem ordem)
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                int id = Convert.ToInt32(cnn.ExecuteScalar("INSERT INTO ordens (quantidade, preço, idnotas, idativos, tipomercado, operação) VALUES (@quantidade, @preço, @idnotas, @idativos, 0, @operação); SELECT LAST_INSERT_ID();",
                                new {
                                    quantidade = ordem.Quantidade,
                                    preço = ordem.Preço,
                                    idnotas = ordem.idNotas,
                                    idativos = ordem.idAtivos,
                                    operação = ordem.Operação
                                }
                            ));

                return id;
            }

        }

        /// <summary>
        /// Remove do banco de dados a ordem selecionada
        /// </summary>
        /// <param name="ordem"></param>
        public static void DeleteOrdens(Ordem ordem)
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                cnn.Execute("DELETE FROM ordens WHERE idOrdens = @Id", new {  Id = ordem.idOrdens });


            }

        }

        #endregion

        #region Corretoras

        /// <summary>
        /// Carrega as corretoras cadastradas no sistema
        /// </summary>
        /// <returns></returns>
        public static List<Corretoras> CarregarCorretoras()
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Corretoras>("SELECT * FROM investimentos.corretoras;", new DynamicParameters());

                List<Corretoras> lista = output.ToList();

                return new List<Corretoras>(lista);
            }

        }

        #endregion

        #region Cotações
        
        /// <summary>
        /// Carrega as cotações do ativo com todas as cotações do historico
        /// </summary>
        /// <param name="IdAtivo"></param>
        /// <returns></returns>
        public static List<Cotacao> CarregarCotacoes(int IdAtivo)
        {
            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<Cotacao>("SELECT * FROM cotacoes WHERE idativos = @Id;", new { Id = IdAtivo });

                return new List<Cotacao>(output.ToList());

            }
        }

        /// <summary>
        /// Carrega as cotações do ativo para o periodo selecionado
        /// </summary>
        /// <param name="IdAtivo">Codigo do insumo</param>
        /// <param name="Inicio">Data de inicio da serie desejada</param>
        /// <param name="Final">Data final da serie desejada</param>
        /// <returns></returns>
        public static List<Cotacao> CarregarCotacoes(int IdAtivo, DateTime Inicio, DateTime Final)
        {

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<Cotacao>("SELECT * FROM investimentos.cotacoes WHERE idativos=@Id AND data >= @Inicio AND data <= @Final ORDER BY data;", new { Id = IdAtivo, Inicio = Inicio, Final = Final });

                return new List<Cotacao>(output.ToList());

            }
        }

        public static Cotacao CarregarUltimaCotacao(int IdAtivo)
        {
            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                try
                {
                    var output = cnn.QuerySingle<Cotacao>("SELECT * FROM cotacoes WHERE idativos=@Id ORDER BY data DESC LIMIT 1;", new { Id = IdAtivo });
                    return (Cotacao)output;
                }

                // Se o revoltado for nulo ele devolve null
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Função responsável por receber as cotações da consulta a API e salvar no banco de dados
        /// </summary>
        /// <param name="cotacao"></param>
        public static void CreateCotacoes(IEnumerable<Cotacao> cotacao, int IdAtivo)
        {

            string sql = "INSERT INTO cotacoes (idativos, data, abertura, maxima, minima, fechamento, volume, tipocotacao) VALUES (" + IdAtivo + ", @Data, @Abertura, @Maxima, @Minima, @Fechamento, @Volume, 1)";

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                cnn.Open();

                cnn.Execute(sql, cotacao);

            }
        }

        /// <summary>
        /// Função responsável por atualizar as cotações
        /// </summary>
        /// <param name="cotacao"></param>
        /// <param name="IdAtivo"></param>
        public static void UpdateCotacoes(Cotacao cotacao)
        {

            string sql = "UPDATE cotacoes SET abertura = @Abertura, fechamento = @Fechamento, minima = @Minima, maxima = @Maxima, volume = @Volume WHERE idcotacoes = @IdCotacoes;";

            using (IDbConnection cnn = new MySqlConnection(LoadConnectionString()))
            {

                cnn.Open();

                cnn.Execute(sql, cotacao);

            }
        }

        #endregion

    }
}

