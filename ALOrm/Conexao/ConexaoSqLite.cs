using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace ALOrm.Conexao
{
    public class ConexaoSqLite
    {
        private readonly string _stringDeConexao;

        public ConexaoSqLite()
        {
            _stringDeConexao = "Data Source=alura.db";

        }

        public void ExecutarComandos(string query, Dictionary<string, object> parametros)
        {
            using var connection = new SqliteConnection(_stringDeConexao);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = query;

            foreach (var param in parametros)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            command.ExecuteNonQuery();
            connection.Close();
        }

        private DbDataReader Consultar(string sql)
        {   
            using var connection = new SqliteConnection(_stringDeConexao);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sql;

            using var reader = command.ExecuteReader();
            return reader;
        }        
    }
}
