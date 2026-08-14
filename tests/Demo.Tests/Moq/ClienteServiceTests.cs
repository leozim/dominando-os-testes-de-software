using DominandoTestesDeUnidades.Tests.DadosHumanos;
using Features.Clientes;
using MediatR;
using Moq;

namespace DominandoTestesDeUnidades.Tests.Moq;

[Collection(nameof(ClienteBogusCollection))]
public class ClienteServiceTests
{
    private readonly ClienteTestsBogusFixture _clienteTestsBogus;

    public ClienteServiceTests(ClienteTestsBogusFixture clienteTestsBogus)
    {
        _clienteTestsBogus = clienteTestsBogus;
    }

    [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var cliente = _clienteTestsBogus.GerarClienteValido();
        var clienteRepo = new Mock<IClienteRepository>();
        var mediatr = new Mock<IMediator>();
        
        var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);
        // Act
        clienteService.Adicionar(cliente);
        // Assert
        Assert.True(cliente.EhValido());
        clienteRepo.Verify(x => x.Adicionar(cliente), Times.Once);
        mediatr.Verify(m => 
            m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar Cliente com Falha")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
    {
        // Arrange
        var cliente = _clienteTestsBogus.GerarClienteInvalido();
        var clienteRepo = new Mock<IClienteRepository>();
        var mediatr = new Mock<IMediator>();
        
        var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);
        
        // Act
        clienteService.Adicionar(cliente);
        
        // Assert
        Assert.False(cliente.EhValido());
        clienteRepo.Verify(x => x.Adicionar(cliente), Times.Never);
        mediatr.Verify(m => 
            m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Adicionar Clientes Ativos")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtvos()
    {
        // Arrange
        var clienteRepo = new Mock<IClienteRepository>();
        var mediatr = new Mock<IMediator>();

        clienteRepo.Setup(c => c.ObterTodos())
            .Returns(_clienteTestsBogus.ObterClientesVariados());
        
        var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);
        
        // Act
        var clientes = clienteService.ObterTodosAtivos();
        
        // Assert
        clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
        Assert.True(clientes.Any());
        Assert.False(clientes.Count(c => !c.Ativo) > 0);
    }
}