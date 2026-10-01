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

    [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
    [Trait("Categoria", "Vendas - Pedido")]
    public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);
        
        // Act & Assert
        Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
    }
    // ChamadaDoMétodo_EstadoObjeto_Comportamento
    [Fact(DisplayName = "Atualizar item Pedido Valido")]
    [Trait("Categoria", "Vendas - Pedido")]
    public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var produtoId = Guid.NewGuid();
        var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
        
        pedido.AdicionarItem(pedidoItem);
        
        var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);
        var novaQuantidade = pedidoItemAtualizado.Quantidade;
        
        // Act
        pedido.AtualizarItem(pedidoItemAtualizado);
        
        // Assert
        Assert.Equal(novaQuantidade, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId)?.Quantidade);
    }

    [Fact(DisplayName = "Atualizar Item Pedido Quantidade acima do permitido")]
    [Trait("Categpria", "Vendas - Pedido")]
    public void AtualizarItemPedido_ItemUnidadesAcimaDoPermitido_DeveRetornarException()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var produtoId = Guid.NewGuid();
        var pedidoItemExistente1 = new PedidoItem(produtoId, "Produto Teste", 3, 15);
        pedido.AdicionarItem(pedidoItemExistente1);
        
        var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 15);
        
        // Act & Assert
        Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
    }
    
    // ChamadaDoMétodo_EstadoObjeto_Comportamento
    [Fact(DisplayName = "Remover Item Pedido Inexistente")]
    [Trait("Categoria", "Vendas - Pedido")]
    public void RemoverItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
    {
        // Arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

        // Act & Assert
        Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItemRemover));
    }
}