


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
