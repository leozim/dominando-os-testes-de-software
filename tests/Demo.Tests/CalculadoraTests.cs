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

    [Theory]
    [InlineData(1, 3, 4)]
    [InlineData(2, 3, 5)]
    [InlineData(3, 3, 6)]
    public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
    {
        // Arrange
        var calculadora = new Calculadora();
        
        // Act
        var resultado = calculadora.Somar(v1, v2);

        // Assert
        Assert.Equal(total, resultado);
    }
}