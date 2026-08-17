namespace NerdStore.Vendas.Domain.Tests;

public class PedidoTests
{
    [Fact(DisplayName = "Adicionar Item Pedido Vazio")]
    [Trait("Categoria", "Pedido Tests")]
    public void AdicionarItempedido_NovoPedido_DeveAtualizarValor()
    {
        // Arrange
        var pedido = new Pedido();
        var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

        // Act
        pedido.AdicionarItem(pedidoItem);
        
        // Assert
        Assert.Equal(200, pedido.ValorTotal);
    }

    // ChamadaDoMétodo_EstadoObjeto_Comportamento
    [Fact(DisplayName = "Adicionar Item Pedido Existente")]
    [Trait("Categoria", "Pedido Tests")]
    public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
    {
        // Arrange
        var pedido = new Pedido();
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
    
}