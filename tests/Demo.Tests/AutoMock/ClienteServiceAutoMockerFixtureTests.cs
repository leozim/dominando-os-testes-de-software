using DominandoTestesDeUnidades.Tests.DadosHumanos;
using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace DominandoTestesDeUnidades.Tests.AutoMock;

[Collection(nameof(ClienteAutoMockerCollection))]
public class ClienteServiceAutoMockerFixtureTests
{
    private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;
    private readonly ClienteService? _clienteService;

    public ClienteServiceAutoMockerFixtureTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture)
    {
        _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
        _clienteService = _clienteTestsAutoMockerFixture?.ObterClienteService();
    }
    
    

    [Fact(DisplayName = "Adicionar Cliente com Sucesso AutoMockFixture")]
    [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
    public void ClienteService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();
        // precisa ser instancia da classe concreta e não interface!
        var clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
        
        // Act
        clienteService.Adicionar(cliente);
        // Assert
        Assert.True(cliente.EhValido());
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(x => x.Adicionar(cliente), Times.Once);
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => 
            m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar Cliente com Falha AutoMockFixture")]
    [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
    public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
    {
        // Arrange
        var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();
        // precisa ser instancia da classe concreta e não interface!
        
        // Act
        _clienteService?.Adicionar(cliente);
        
        // Assert
        Assert.False(cliente.EhValido());
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(x => x.Adicionar(cliente), Times.Never);
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => 
            m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Adicionar Clientes Ativos AutoMockFixture")]
    [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
    public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtvos()
    {
        // Arrange
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
            .Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());
        
        // Act
        var clientes = _clienteService?.ObterTodosAtivos();
        
        // Assert
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
        Assert.True(clientes.Any());
        Assert.False(clientes.Count(c => !c.Ativo) > 0);
    }
}