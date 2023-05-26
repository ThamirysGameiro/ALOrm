namespace ALOrm.ConfigReflection
{
    public record Property(string Name, Type Type, bool IsId, object? Value = null);
}
