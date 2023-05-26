using System.Data;
using System.Data.Common;

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
                    var value = Convert.ChangeType(tabela.Rows[i][prop.Name], prop.Type);
                    var type = typeof(T);
                    var propertyInfo = type.GetProperty(prop.Name);
                    propertyInfo!.SetValue(entidade, value);
                }
                entidades.Add(entidade);
            }            
           
            return entidades;
        }
    }
}
