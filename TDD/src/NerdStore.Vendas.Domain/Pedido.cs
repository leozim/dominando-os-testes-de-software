using System.Collections.ObjectModel;

namespace NerdStore.Vendas.Domain;

public class Pedido
{
    private readonly List<PedidoItem> _pedidoItems;
    
    public decimal ValorTotal { get; private set; }
    public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
    
    public Pedido()
    {
        _pedidoItems = new List<PedidoItem>();
    }
    public void AdicionarItem(PedidoItem pedidoItem)
    {
        _pedidoItems.Add(pedidoItem);
        ValorTotal = PedidoItems.Sum(i => i.Quantidade * i.ValorUnitario);
    }
}