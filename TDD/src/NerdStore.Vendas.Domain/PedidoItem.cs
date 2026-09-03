using NerdStore.Vendas.Domain.DomainExceptions;

namespace NerdStore.Vendas.Domain;

public class PedidoItem
{
    public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
    {
        if (quantidade < Pedido.MIN_UNIDADES_ITEM) throw new DomainException($"Minímo de {Pedido.MIN_UNIDADES_ITEM} unidades por produto.");
        
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; }

    internal void AdicionarUnidades(int unidades)
    {
        Quantidade += unidades;
    }

    internal decimal CalcularValor()
    {
        return Quantidade * ValorUnitario;
    }
}