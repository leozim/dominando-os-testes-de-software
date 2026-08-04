using Features.Clientes;

namespace DominandoTestesDeUnidades.Tests.Fixtures;

[Collection(nameof(ClienteCollection))]
public class ClienteTesteInvalido
{
    readonly ClienteTestsFixture _clienteTestsFixture;

    public ClienteTesteInvalido(ClienteTestsFixture clienteTestsFixture)
    {
        _clienteTestsFixture = clienteTestsFixture;
    }
    
    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Cliente Trait Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = _clienteTestsFixture.GerarClienteInvalido();
        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.False(result);
        Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        Assert.NotEmpty(cliente.ValidationResult.Errors);
    }
}