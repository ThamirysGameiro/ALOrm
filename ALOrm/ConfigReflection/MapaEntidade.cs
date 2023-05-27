using ALOrm.Atributos;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace ALOrm.ConfigReflection
{
    public class MapaEntidade<T> where T : class, new()
    {
        public static IReadOnlyList<T> MapearEntidades(DataTable tabela)
        {
            var entidades = new List<T>();
            var propriedades = GerenciadorDePropriedades.RecuperarPropriedades<T>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                var entidade = new T();
                foreach (var prop in propriedades)
                {
                    var value = Convert.ChangeType(tabela.Rows[i][prop.NomeColuna], prop.Type);
                    var type = typeof(T);
                    var propertyInfo = type.GetProperty(prop.NomePropriedade);
                    propertyInfo!.SetValue(entidade, value);
                }
                entidades.Add(entidade);
            }            
           
            return entidades;
        }


        private static Dictionary<string, string> NomeTabelaCache = new();

        public static string ObterNomeTabela()
        {
            var tipo = typeof(T);
            if (NomeTabelaCache.ContainsKey(tipo.Name))
                return NomeTabelaCache[tipo.Name];

            var nome = tipo.Name;
            var atributo = tipo.GetCustomAttribute<NomeTabelaAttribute>();
            if (atributo != null)
                nome = atributo.Nome;


            NomeTabelaCache.Add(tipo.Name, nome);


            return nome;
        }

        public static string ObterColunaChavePrimaria()
        {
            var propriedades = GerenciadorDePropriedades.RecuperarPropriedades<T>();

            return propriedades.FirstOrDefault(x => x.IsId)?.NomeColuna ?? "id";
        }

        public static string ObterNomePropriedade(PropertyInfo property)
        {
            var nomePropriedade = property.Name;
            var nomePropriedadeAttribute = property.GetCustomAttribute<NomeColunaAttribute>();
            if (nomePropriedadeAttribute is not null)
                nomePropriedade = nomePropriedadeAttribute.Nome;
            return nomePropriedade;
        }

        public static bool PropriedadeChavePrimaria(PropertyInfo property)
        {
            var isId = false;
            var chavePrimariaAttribute = property.GetCustomAttribute<ChavePrimariaAttribute>();
            if (chavePrimariaAttribute is not null)
                isId = true;
            else
                isId = property.Name == "Id";


            return isId;
        }


    }
}
