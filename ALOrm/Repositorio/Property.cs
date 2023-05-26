namespace ALOrm.Repositorio
{
    public record Property(string Name, Type Type, bool IsId, object? Value = null);
}
