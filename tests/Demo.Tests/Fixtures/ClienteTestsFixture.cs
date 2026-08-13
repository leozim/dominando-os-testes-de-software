using Bogus;
using Bogus.DataSets;
using Features.Clientes;

namespace DominandoTestesDeUnidades.Tests.Fixtures;

public class ClienteTestsFixture : IDisposable
{
    public Cliente GerarClienteValido()
    {
        var genero = new Faker().PickRandom<Name.Gender>();
        /*var email = new Faker().Internet.Email("Leonardo", "Mariz", "gmail");
        var clienteFaker = new Faker<Cliente>();
        clienteFaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());*/

        var cliente2 = new Faker<Cliente>("pt_BR")
            .CustomInstantiator(f => new Cliente(
                Guid.NewGuid(),
                f.Name.FirstName(genero),
                f.Name.LastName(genero),
                f.Date.Past(80, DateTime.Now.AddYears(-18)),
                "",
                true,
                DateTime.Now
                ))
            .RuleFor(c => c.Email, (f,c) => 
                    f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower())
                    );
        
        var cliente = new Cliente(Guid.NewGuid(),
            "Leonardo",
            "Mariz",
            DateTime.Now.AddYears(-31),
            "leonardo@hotmail.com",
            true,
            DateTime.Now);

        return cliente2;
    }

    public Cliente GerarClienteInvalido() => new Cliente(
        Guid.NewGuid(),
        "",
        "",
        DateTime.Now.AddYears(-11),
        "leonardo#hotmail.com",
        true,
        DateTime.Now);

    public void Dispose()
    {
        // TODO release managed resources here
    }
}