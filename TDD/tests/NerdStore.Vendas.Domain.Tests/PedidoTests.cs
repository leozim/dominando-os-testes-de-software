using NerdStore.Vendas.Domain.DomainExceptions;

namespace NerdStore.Vendas.Domain.Tests;

public class PedidoTests
{
    [Fact(DisplayName = "Adicionar Item Pedido Vazio")]
    [Trait("Categoria", "Vendas - Pedido")]
    public void AdicionarItempedido_NovoPedido_DeveAtualizarValor()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

        // Act
        pedido.AdicionarItem(pedidoItem);
        
        // Assert
        Assert.Equal(200, pedido.ValorTotal);
    }

    // ChamadaDoMétodo_EstadoObjeto_Comportamento
    [Fact(DisplayName = "Adicionar Item Pedido Existente")]
    [Trait("Categoria", "Vendas - Pedido")]
    public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var produtoId = Guid.NewGuid();
        var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
        pedido.AdicionarItem(pedidoItem);

        var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", 1, 100);
        
        // Act
        pedido.AdicionarItem(pedidoItem2);
        // Assert
        Assert.Equal(300, pedido.ValorTotal);
        Assert.Single(pedido.PedidoItems);
        Assert.Equal(1, pedido.PedidoItems.Count);
        Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
        
    }

    // ChamadaDoMétodo_EstadoObjeto_Comportamento
    [Fact(DisplayName = "Adicionar Item pedido Acima do permitido")]
    [Trait("Categoria", "Vendas - Pedido")]
    public void AdicionarItemPedido_UnidadesItemItemAcimaDoPermitido_DeveRetornarException()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var produtoId = Guid.NewGuid();
        var pedidoItem = new PedidoItem(produtoId, "Produto Teste Exception", Pedido.MAX_UNIDADES_ITEM + 1, 100);
        
        // Act & Assert
        Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
    }
    
    // ChamadaDoMétodo_EstadoObjeto_Comportamento
    [Fact(DisplayName = "Adicionar Item Pedido Existente Acima do Permitido")]
    [Trait("Categoria", "Vendas - Pedido")]
    public void AdicionarItemPedido_ItemExistenteSomaUnidadesAcimaDoPermitido_DeveRetornarException()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var produtoId = Guid.NewGuid();
        var pedidoItem = new PedidoItem(produtoId, "Produto Teste Exception", Pedido.MAX_UNIDADES_ITEM, 100);
        var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste Exception 2", 1, 100);
        pedido.AdicionarItem(pedidoItem);
        
        // Act & Assert
        Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));
    }
}