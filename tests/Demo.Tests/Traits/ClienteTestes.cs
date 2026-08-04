using Features.Clientes;

namespace DominandoTestesDeUnidades.Tests.Traits;

public class ClienteTestes
{
    [Fact]
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
}