namespace ALOrm.Consumidor
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataDeNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}
