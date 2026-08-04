using Features.Clientes;

namespace DominandoTestesDeUnidades.Tests.Fixtures;

public class ClienteTestsFixture : IDisposable
{
    public Cliente GerarClienteValido() =>
        new Cliente(Guid.NewGuid(),
            "Leonardo",
            "Mariz",
            DateTime.Now.AddYears(-31),
            "leonardo@hotmail.com",
            true,
            DateTime.Now);

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