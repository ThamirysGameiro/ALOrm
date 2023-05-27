namespace ALOrm.ConfigReflection
{
    public record Property(string NomePropriedade, string NomeColuna, Type Type, bool IsId, object? Value = null);
}
