using ALOrm.Conexao;
using ALOrm.ConfigReflection;

namespace ALOrm.Repositorio
{
    public class RepositorioBase<T>: IDisposable where T : class, new()
    {
        private readonly string _nomeDaTabela;
        private readonly string _colunaChavePrimaria;
        private readonly ConexaoSqLite _conexao;
        public RepositorioBase(string stringDeConexao)
        {
            _nomeDaTabela = MapaEntidade<T>.ObterNomeTabela();
            _colunaChavePrimaria = MapaEntidade<T>.ObterColunaChavePrimaria();
            _conexao = new ConexaoSqLite(stringDeConexao);

        }
        

        public T Incluir(T entity)
        {
            var valorDasPropriedades = GerenciadorDePropriedades.RecuperarValorDasPropriedades(entity);            
            
            var colunas = string.Empty;
            var parametrosQuery = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
           
            for (var i = 0; i < valorDasPropriedades.Count; i++)
            {
                var propertyValue = valorDasPropriedades[i];
                parametros.Add($"${propertyValue.NomeColuna}", propertyValue.Value);
                
                colunas += propertyValue.NomeColuna;
                parametrosQuery += $"${propertyValue.NomeColuna}";
                if (i < 0 || i >= valorDasPropriedades.Count - 1) continue;
                colunas += ",";
                parametrosQuery += ",";
            }
            var query = $"INSERT INTO {_nomeDaTabela} ({colunas}) VALUES ({parametrosQuery})";

            _conexao.ExecutarComandos(query, parametros);

            return entity;
        }

        public T Alterar(T entity)
        {
            var valorDasPropriedades = GerenciadorDePropriedades.RecuperarValorDasPropriedades(entity);
            var colunas = string.Empty;            
            Dictionary<string, object> parametros = new Dictionary<string, object>();
                        
            for (var i = 0; i < valorDasPropriedades.Count; i++)
            {
                var propertyValue = valorDasPropriedades[i];

                parametros.Add($"${propertyValue.NomeColuna}", propertyValue.Value);


                if (!propertyValue.IsId)
                {
                    colunas += $"{propertyValue.NomeColuna} = ${propertyValue.NomeColuna}";
                    if (i < 0 || i >= valorDasPropriedades.Count - 1) continue;
                    colunas += ",";
                }
               
            }
            var query = $"UPDATE {_nomeDaTabela} SET {colunas} WHERE {_colunaChavePrimaria} = ${_colunaChavePrimaria}";

            _conexao.ExecutarComandos(query, parametros);

            return entity;
        }

        public IReadOnlyList<T> ConsultarTudo()
        {            
            var query = $"SELECT * FROM {_nomeDaTabela}";

            var dados = _conexao.Consultar(query);
            
            return MapaEntidade<T>.MapearEntidades(dados);
        }

        public T? ConsultarPorChavePrimaria<K>(K valorChavePrimaria)
        {            
            var query = $"SELECT * FROM {_nomeDaTabela} WHERE {_colunaChavePrimaria} = ${_colunaChavePrimaria}";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add($"${_colunaChavePrimaria}", valorChavePrimaria);

            var dados = _conexao.Consultar(query, parametros);

            return MapaEntidade<T>.MapearEntidades(dados).FirstOrDefault();
        }


        public void ExcluirPorChavePrimaria<K>(K valorChavePrimaria)
        {
            var query = $"DELETE FROM {_nomeDaTabela} WHERE {_colunaChavePrimaria} = ${_colunaChavePrimaria}";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add($"${_colunaChavePrimaria}", valorChavePrimaria);


            _conexao.ExecutarComandos(query, parametros);
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
