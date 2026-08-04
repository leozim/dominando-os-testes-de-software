namespace DominandoTestesDeUnidades.Tests;

public class AssertStrings
{
    [Fact]
    public void StringsTools_UnirMoes_RetornarNomeCompleto()
    {
        // Arrange
        var sut = new StringsTools();
        
        // Act
        var nomeCompleto = sut.Unir("Leonardo", "Mariz Bezerra");
        
        // Assert
        Assert.Equal("Leonardo Mariz Bezerra", nomeCompleto, true);
    }

    [Fact]
    public void StringsTools_UnirNomes_DeveConterTrecho()
    {
        // Arrange
        var sut = new StringsTools();
        
        // Act
        var nomCompleto = sut.Unir("Leonardo", "Mariz Bezerra");
        
        // Assert
        Assert.Contains("nardo", nomCompleto);
    }

    [Fact]
    public void StringsTools_UnirNome_DeveComecarCom()
    {
        // Arrange
        var sut = new StringsTools();
        
        // Act
        var nomeCompleto = sut.Unir("Leonardo", "Martin");
        
        // Assert
        Assert.StartsWith("Leon", nomeCompleto);
    }

    [Fact]
    public void StringsTools_UnirNomes_DeveAcabarCom()
    {
        // Arramge
        var sut = new StringsTools();
        
        // Act
        var nomeCompleto = sut.Unir("Leonardo", "Mariz Bezerra");
        
        //Assert
        Assert.EndsWith("erra", nomeCompleto);
        
    }

    [Fact]

    public void StringsTools_UnirNomes_ValidarExpressaoRegular()
    {
        // Arrange
        var sut = new StringsTools();
        
        // Act
        var nomeCompleto = sut.Unir("Leonardo", "Mariz Bezerra");
        
        // Assert
        Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
    }
}