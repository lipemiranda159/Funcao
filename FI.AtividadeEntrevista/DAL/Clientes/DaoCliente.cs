using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;
using FI.Utils.Exceptions;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Retorna lista de parametros com dados do cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        private List<SqlParameter> GetClientDataParameters(Cliente cliente)
        {
            return new List<SqlParameter>
            {
                new SqlParameter(Constants.PARAMETER_NAME, cliente.Nome),
                new SqlParameter(Constants.PARAMETER_SOBRENOME, cliente.Sobrenome),
                new SqlParameter(Constants.PARAMETER_CPF, cliente.CPF),
                new SqlParameter(Constants.PARAMETER_NACIONALIDADE, cliente.Nacionalidade),
                new SqlParameter(Constants.PARAMETER_CEP, cliente.CEP),
                new SqlParameter(Constants.PARAMETER_ESTADO, cliente.Estado),
                new SqlParameter(Constants.PARAMETER_CIDADE, cliente.Cidade),
                new SqlParameter(Constants.PARAMETER_LOGRADOURO, cliente.Logradouro),
                new SqlParameter(Constants.PARAMETER_EMAIL, cliente.Email),
                new SqlParameter(Constants.PARAMETER_TELEFONE, cliente.Telefone)
            };
        }
        /// <summary>
        /// Pega valor da tabela
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static string GetDatabaseValue(DataSet ds, int position)
        {
            return ds.Tables[position].Rows[0][0].ToString();
        }

        /// <summary>
        /// Verifica se existe registro
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static bool PossuiRegistros(DataSet ds, int position)
        {
            return ds.Tables.Count > 0 && ds.Tables[position].Rows.Count > 0;
        }


        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Cliente cliente)
        {
            long ret = 0;
            if (!VerificarExistencia(cliente.CPF))
            {
                List<SqlParameter> parametros = GetClientDataParameters(cliente);

                var ds = Consultar(Constants.INSERT_CLIENT_SP, parametros);
                if (PossuiRegistros(ds, default))
                    long.TryParse(GetDatabaseValue(ds, default), out ret);
            }
            else
            {
                throw new CpfJaExisteException("CPF já existe na base de dados!");
            }
            return ret;

        }



        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal Cliente Consultar(long Id)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.PARAMETER_ID, Id)
            };

            var ds = Consultar(Constants.SEARCH_CLIENT_SP, parametros);
            var cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.PARAMETER_CPF, CPF)
            };

            var ds = Consultar(Constants.VERIFY_CLIENT_SP, parametros);

            return PossuiRegistros(ds, default);
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.START_IN, iniciarEm),
                new SqlParameter(Constants.COUNT, quantidade),
                new SqlParameter(Constants.SORT_BY, campoOrdenacao),
                new SqlParameter(Constants.ORDER, crescente)
            };

            var ds = Consultar(Constants.GET_CLIENT_SP, parametros);
            var cli = Converter(ds);

            var iQtd = 0;

            if (PossuiRegistros(ds, 1))
                int.TryParse(GetDatabaseValue(ds, 1), out iQtd);

            qtd = iQtd;

            return cli;
        }


        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Cliente> Listar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.PARAMETER_ID, 0)
            };

            DataSet ds = Consultar(Constants.SEARCH_CLIENT_SP, parametros);
            List<Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(Cliente cliente)
        {
            if (!VerificarExistencia(cliente.CPF))
            {
                var parametros = GetClientDataParameters(cliente);
                parametros.Add(new SqlParameter(Constants.PARAMETER_ID, cliente.Id));
                Executar(Constants.CHANGE_CLIENT_SP, parametros);
            }
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.PARAMETER_ID, Id)
            };

            Executar(Constants.DELETE_CLIENT_SP, parametros);
        }

        private List<Cliente> Converter(DataSet ds)
        {
            var lista = new List<Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(row.RowToCliente());
                }
            }

            return lista;
        }
    }
}
