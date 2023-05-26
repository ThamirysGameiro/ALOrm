using ALOrm.Conexao;
using ALOrm.ConfigReflection;
using System.Data.Common;

namespace ALOrm.Repositorio
{
    public class RepositorioBase<T> where T : class, new()
    {

        public T Incluir(T entity)
        {
            var valorDasPropriedades = GerenciadorDePropriedades.RecuperarValorDasPropriedades(entity);
             
            var nomeDaTabela = typeof(T).Name;
            var colunas = string.Empty;
            var parametrosQuery = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
           
            for (var i = 0; i < valorDasPropriedades.Count; i++)
            {
                var propertyValue = valorDasPropriedades[i];
                parametros.Add($"${propertyValue.Name}", propertyValue.Value);
                
                colunas += propertyValue.Name;
                parametrosQuery += $"${propertyValue.Name}";
                if (i < 0 || i >= valorDasPropriedades.Count - 1) continue;
                colunas += ",";
                parametrosQuery += ",";
            }
            var query = $"INSERT INTO {nomeDaTabela} ({colunas}) VALUES ({parametrosQuery})";

            new ConexaoSqLite().ExecutarComandos(query, parametros);

            return entity;
        }

        public T Alterar(T entity)
        {
            var valorDasPropriedades = GerenciadorDePropriedades.RecuperarValorDasPropriedades(entity);

            var nomeDaTabela = typeof(T).Name;
            var colunas = string.Empty;
            var parametrosQuery = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            object? id = null;


            for (var i = 0; i < valorDasPropriedades.Count; i++)
            {
                var propertyValue = valorDasPropriedades[i];
                if (propertyValue.IsId)
                    id = propertyValue.Value;

                parametros.Add($"${propertyValue.Name}", propertyValue.Value);
                colunas += $"{propertyValue.Name} = ${propertyValue.Name}";
                if (i < 0 || i >= valorDasPropriedades.Count - 1) continue;
                colunas += ",";
            }
            var query = $"UPDATE {nomeDaTabela} SET {colunas} WHERE ID = {id}";

            new ConexaoSqLite().ExecutarComandos(query, parametros);

            return entity;
        }

        public IReadOnlyList<T> ConsultarTudo()
        {
            var nomeDaTabela = typeof(T).Name;
            var query = $"SELECT * FROM {nomeDaTabela}";

            var dados = new ConexaoSqLite().Consultar(query);
            
            return MapaEntidade<T>.MapearEntidades(dados);
        }

        public T? ConsultarPorId(int id)
        {
            var nomeDaTabela = typeof(T).Name;
            var query = $"SELECT * FROM {nomeDaTabela} WHERE Id = {id}";

            var dados = new ConexaoSqLite().Consultar(query);

            return MapaEntidade<T>.MapearEntidades(dados).FirstOrDefault();
        }


        public void ExcluirPorId(int id)
        {
            var nomeDaTabela = typeof(T).Name;
            
            var query = $"DELETE FROM {nomeDaTabela} WHERE Id = {id}";
            new ConexaoSqLite().ExecutarComandos(query, new Dictionary<string, object>());
        }

        

    }
}
