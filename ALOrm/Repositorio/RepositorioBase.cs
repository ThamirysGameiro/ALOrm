using ALOrm.Conexao;


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

    }
}
