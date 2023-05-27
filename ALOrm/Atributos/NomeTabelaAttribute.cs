namespace ALOrm.Atributos
{
    public class NomeTabelaAttribute : Attribute
    {
        public string Nome { get; set; }
        public NomeTabelaAttribute(string nome)
        {
            Nome = nome;
        }
    }

}
