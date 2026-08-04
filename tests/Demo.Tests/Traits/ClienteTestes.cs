using Features.Clientes;

namespace DominandoTestesDeUnidades.Tests.Traits;

public class ClienteTestes
{
    [Fact(DisplayName = "Novo Clientre Valido")]
    [Trait("Categoria", "Cliente Trait Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // Arrange
        var cliente = new Cliente(
            Guid.NewGuid(),
            "Leoardo",
            "Mariz",
            DateTime.Now.AddYears(-31),
            "leonardo@hotmail.com",
            true,
            DateTime.Now);
        
        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.True(result);
        Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        Assert.Empty(cliente.ValidationResult.Errors);
    }

    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Cliente Trait Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = new Cliente(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now.AddYears(-11),
            "leonardo#hotmail.com",
            true,
            DateTime.Now);
        // Act
        var result = cliente.EhValido();
        
        // Assert
        Assert.False(result);
        Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        Assert.NotEmpty(cliente.ValidationResult.Errors);
    }
}