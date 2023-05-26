namespace ALOrm.ConfigReflection
{
    public static class GerenciadorDePropriedades
    {
        private static readonly Dictionary<Type, IReadOnlyList<Property>> _cache = new();

        public static IReadOnlyList<Property> RecuperarValorDasPropriedades<T>(T instancia)
        {
            var values = new List<Property>();
            foreach (var property in instancia!.GetType().GetProperties())
            {
                var value = property!.GetValue(instancia, null);
                values.Add(new Property(property.Name, property.PropertyType, property.Name == "Id", value));
            }
            return values;
        }

        public static IReadOnlyList<Property> RecuperarPropriedades<T>()
        {
            var type = typeof(T);
            if (_cache.TryGetValue(type, out var propriedades)) return propriedades;

            propriedades = typeof(T).GetProperties()
                .Select(x => new Property(x.Name, x.PropertyType, x.Name == "Id"))
                .ToList();
            _cache.Add(type, propriedades);
            return propriedades;
        }
    }
}
