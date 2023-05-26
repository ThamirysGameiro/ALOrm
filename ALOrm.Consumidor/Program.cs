


using ALOrm.Consumidor;
using ALOrm.Repositorio;

var repositorio = new RepositorioBase<Pessoa>();


var x = repositorio.Incluir(new Pessoa
{
    Id = 1,
    Nome = "Thamirys",
    DataDeNascimento = new DateTime(1988,09,30),
    Ativo = true
});

repositorio.Incluir(new Pessoa
{
    Id = 2,
    Nome = "Diego",
    DataDeNascimento = new DateTime(1986, 06, 30),
    Ativo = true
});

repositorio.Incluir(new Pessoa
{
    Id = 3,
    Nome = "Diana",
    DataDeNascimento = new DateTime(2022, 08, 01),
    Ativo = true
});




var pessoa = repositorio.ConsultarPorId(1);
if (pessoa is not null)
{
    Console.WriteLine($"{pessoa.Id} - {pessoa.Nome} - {pessoa.DataDeNascimento} - {pessoa.Ativo}");
    pessoa.Nome += " EDITADO";
    repositorio.Alterar(pessoa);
}


repositorio.ExcluirPorId(2);


var todosOsDados = repositorio.ConsultarTudo();
foreach (var p in todosOsDados)
{
    Console.WriteLine($"{p.Id} - {p.Nome} - {p.DataDeNascimento} - {p.Ativo}");
}