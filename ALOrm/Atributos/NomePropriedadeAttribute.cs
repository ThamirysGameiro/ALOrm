namespace ALOrm.Atributos
{
    public class NomePropriedadeAttribute : Attribute
    {
        public string Nome { get; set; }
        public NomePropriedadeAttribute(string nome)
        {
            Nome = nome;
        }
    }

}
