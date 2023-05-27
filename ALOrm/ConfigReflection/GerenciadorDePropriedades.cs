using System.Data.Common;

namespace ALOrm.ConfigReflection
{
    public static class GerenciadorDePropriedades
    {
        private static readonly Dictionary<Type, IReadOnlyList<Property>> _cache = new();

        public static IReadOnlyList<Property> RecuperarValorDasPropriedades<T>(T instancia) where T : class, new()
        {
            var values = new List<Property>();
            foreach (var property in instancia!.GetType().GetProperties())
            {
                var value = property!.GetValue(instancia, null);

                var isId = MapaEntidade<T>.PropriedadeChavePrimaria(property);
                var nomePropriedade = MapaEntidade<T>.ObterNomePropriedade(property);
                values.Add(new Property(property.Name, nomePropriedade, property.PropertyType, isId, value));
            }
            return values;
        }

        public static IReadOnlyList<Property> RecuperarPropriedades<T>() where T : class, new()
        {
            var type = typeof(T);
            if (_cache.TryGetValue(type, out var propriedades)) return propriedades;

            propriedades = typeof(T).GetProperties()
                .Select(x => new Property(x.Name, MapaEntidade<T>.ObterNomePropriedade(x), x.PropertyType, MapaEntidade<T>.PropriedadeChavePrimaria(x)))
                .ToList();
            _cache.Add(type, propriedades);
            return propriedades;
        }       
    }


}
