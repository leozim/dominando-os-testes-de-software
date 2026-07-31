namespace DominandoTestesDeUnidades.Tests;

public class AssertingObjectTypestests
{
    [Fact]
    public void FuncionarioFactory_Criar_DeveRetornarTipoFuncionario()
    {
        // Arrange & Act
        var funcionario = FuncionarioFactory.Criar("Leonardo", 10000);

        // Assert
        Assert.IsType<Funcionario>(funcionario);
    }

    [Fact]
    public void FuncionarioFactory_Criar_DeveRetornarTipoDerivadoPessoa()
    {
        // Arrange & Act
        var funcionario = FuncionarioFactory.Criar("Leonardo", 10000);
        
        // Assert
        Assert.IsAssignableFrom<Pessoa>(funcionario);
    }
}