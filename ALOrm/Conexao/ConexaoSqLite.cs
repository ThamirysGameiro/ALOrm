using Microsoft.Data.Sqlite;
using System.Data;

namespace ALOrm.Conexao
{
    public class ConexaoSqLite: IDisposable
    {
        
        private SqliteConnection _connection;
        public ConexaoSqLite(string stringDeConexao)
        {
            _connection = new SqliteConnection(stringDeConexao);
            _connection.Open();

        }

        public void ExecutarComandos(string query, Dictionary<string, object> parametros)
        {
            var command = _connection.CreateCommand();
            command.CommandText = query;

            foreach (var param in parametros)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            command.ExecuteNonQuery();            
        }

        public DataTable Consultar(string sql)
        {  
            var command = _connection.CreateCommand();
            command.CommandText = sql;

            using var reader = command.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);            

            return dt;
        }

        public DataTable Consultar(string sql, Dictionary<string, object> parametros)
        {   
            var command = _connection.CreateCommand();
            command.CommandText = sql;

            foreach (var param in parametros)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            using var reader = command.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }

        public void Dispose()
        {
            if(_connection.State != ConnectionState.Closed)
                _connection.Close();
        }
    }
}
