using NerdStore.Vendas.Domain.DomainExceptions;

namespace NerdStore.Vendas.Domain.Tests;

public class PedidoItemTests
{
    [Fact(DisplayName = "Adicionar Item Pedido Abaixo do Permitido")]
    [Trait("Categoria", "Vendas - Pedido Item")]
    public void AdicionarItemPedido_UnidadesItemItemAbaixoDoPermitido_DeveRetornarException()
    {
        // Arrange & Act & Assert
        Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Produto Teste Exception", Pedido.MIN_UNIDADES_ITEM - 1, 100));
    }
}