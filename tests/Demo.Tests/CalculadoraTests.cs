namespace DominandoTestesDeUnidades.Tests;

public class CalculadoraTests
{
    [Fact]
    public void Calculadora_Somar_RetornarValorSoma()
    {
        // Arrange
        var calculadora = new Calculadora();
        
        // Act
        var resultado = calculadora.Somar(1, 2);

        // Assert
        Assert.Equal(3, resultado);
    }
}