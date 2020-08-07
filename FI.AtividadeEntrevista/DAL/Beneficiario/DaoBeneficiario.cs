using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FI.AtividadeEntrevista.DML;
using FI.Utils.Exceptions;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Retorna lista de parametros com dados do beneficiario
        /// </summary>
        /// <param name="Beneficiario"></param>
        /// <returns></returns>
        private List<SqlParameter> GetBeneficiarioDataParameters(Beneficiario beneficiario)
        {
            return new List<SqlParameter>
            {
                new SqlParameter(Constants.BENEFICIARIO_PARAMETER_NAME, beneficiario.Nome),
                new SqlParameter(Constants.BENEFICIARIO_PARAMETER_CPF, beneficiario.CPF),
            };
        }


        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            long ret = 0;
            if (!VerificarExistencia(beneficiario.CPF))
            {
                List<SqlParameter> parametros = GetBeneficiarioDataParameters(beneficiario);

                var ds = Consultar(Constants.BENEFICIARIO_INSERT_SP, parametros);
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
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal Beneficiario Consultar(long Id)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.BENEFICIARIO_PARAMETER_ID, Id)
            };

            var ds = Consultar(Constants.BENEFICIARIO_SEARCH_SP, parametros);
            var ben = Converter(ds);

            return ben.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.BENEFICIARIO_PARAMETER_CPF, CPF)
            };

            var ds = Consultar(Constants.BENEFICIARIO_VERIFY_SP, parametros);

            return PossuiRegistros(ds, default);
        }

        internal List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.START_IN, iniciarEm),
                new SqlParameter(Constants.COUNT, quantidade),
                new SqlParameter(Constants.SORT_BY, campoOrdenacao),
                new SqlParameter(Constants.ORDER, crescente)
            };

            var ds = Consultar(Constants.GET_BENEFICIARIO_SP, parametros);
            var cli = Converter(ds);

            var iQtd = 0;

            if (PossuiRegistros(ds, 1))
                int.TryParse(GetDatabaseValue(ds, 1), out iQtd);

            qtd = iQtd;

            return cli;
        }


        /// <summary>
        /// Lista todos os beneficiarios
        /// </summary>
        internal List<Beneficiario> Listar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.BENEFICIARIO_PARAMETER_ID, 0)
            };

            var ds = Consultar(Constants.BENEFICIARIO_SEARCH_SP, parametros);

            return Converter(ds);
        }

        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        internal void Alterar(Beneficiario Beneficiario)
        {
            if (!VerificarExistencia(Beneficiario.CPF))
            {
                var parametros = GetBeneficiarioDataParameters(Beneficiario);
                parametros.Add(new SqlParameter(Constants.BENEFICIARIO_PARAMETER_ID, Beneficiario.Id));
                Executar(Constants.CHANGE_BENEFICIARIO_SP, parametros);
            }
        }


        /// <summary>
        /// Excluir Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        internal void Excluir(long Id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(Constants.BENEFICIARIO_PARAMETER_ID, Id)
            };

            Executar(Constants.DELETE_BENEFICIARIO_SP, parametros);
        }

        internal List<Beneficiario> Converter(DataSet ds)
        {
            var lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(row.RowToBeneficiario());
                }
            }

            return lista;
        }
    }
}

