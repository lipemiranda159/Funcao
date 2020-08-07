using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL
{
    internal class AcessoDados
    {
        private const string CONNECTION_NAME = "BancoDeDados";

        private string stringDeConexao
        {
            get
            {
                var conn = ConfigurationManager.ConnectionStrings[CONNECTION_NAME];
                var connectionString = conn != null ? conn.ConnectionString : string.Empty;
                return connectionString;
            }
        }

        internal string GetDatabaseValue(DataSet ds, int position)
        {
            return ds.Tables[position].Rows[0][0].ToString();
        }

        internal bool PossuiRegistros(DataSet ds, int position)
        {
            return ds.Tables.Count > 0 && ds.Tables[position].Rows.Count > 0;
        }


        internal void Executar(string NomeProcedure, List<SqlParameter> parametros)
        {
            using (var comando = new SqlCommand())
            {
                var conexao = new SqlConnection(stringDeConexao);
                comando.Connection = conexao;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = NomeProcedure;
                foreach (var item in parametros)
                    comando.Parameters.Add(item);

                conexao.Open();
                try
                {
                    comando.ExecuteNonQuery();
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        internal DataSet Consultar(string NomeProcedure, List<SqlParameter> parametros)
        {
            var comando = new SqlCommand();
            var conexao = new SqlConnection(stringDeConexao);

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
            {
                var ds = new DataSet();
                conexao.Open();

                try
                {
                    adapter.Fill(ds);
                }
                finally
                {
                    conexao.Close();
                }
                return ds;
            }

        }

    }
}