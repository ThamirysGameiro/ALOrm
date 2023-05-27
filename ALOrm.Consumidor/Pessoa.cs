using ALOrm.Atributos;

namespace ALOrm.Consumidor
{
    [NomeTabela("tb_pessoa")]
    public class Pessoa
    {
        [ChavePrimaria]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        [NomeColuna("dt_nasc")]
        public DateTime DataDeNascimento { get; set; }

        public bool Ativo { get; set; }
    }
}
