namespace ALOrm.Atributos
{
    public class NomeColunaAttribute : Attribute
    {
        public string Nome { get; set; }
        public NomeColunaAttribute(string nome)
        {
            Nome = nome;
        }
    }

}
