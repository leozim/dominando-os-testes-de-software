using NerdStore.Vendas.Domain.DomainExceptions;

namespace NerdStore.Vendas.Domain;

public class Pedido
{
    private readonly List<PedidoItem> _pedidoItems;

    protected Pedido()
    {
        _pedidoItems = new List<PedidoItem>();
    }

    public static int MAX_UNIDADES_ITEM => 15;
    public static int MIN_UNIDADES_ITEM => 1;

    public Guid ClienteId { get; private set; }
    public decimal ValorTotal { get; private set; }
    public PedidoStatus PedidoStatus { get; private set; }
    public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

    private void CalcularValorPedido()
    {
        ValorTotal = _pedidoItems.Sum(i => i.CalcularValor());
    }

    private bool PedidoItemExistente(PedidoItem item)
    {
        return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
    }

    private void ValidarPedidoItemInexistente(PedidoItem item)
    {
        if (!PedidoItemExistente(item)) throw new DomainException($"O item não existe no pedido");
    }

    private void ValidarQuantidadeItemPermitida(PedidoItem item)
    {
        var quantidadeItens = item.Quantidade;
        if (PedidoItemExistente(item))
        {
            var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
            quantidadeItens += itemExistente.Quantidade;
        }
        if (quantidadeItens > MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de {MAX_UNIDADES_ITEM} unidades por produto.");
    }

    public void AdicionarItem(PedidoItem pedidoItem)
    {
        ValidarQuantidadeItemPermitida(pedidoItem);

        if (PedidoItemExistente(pedidoItem))
        {
            var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);
            itemExistente.AdicionarUnidades(pedidoItem.Quantidade);
            pedidoItem = itemExistente;
            

            _pedidoItems.Remove(itemExistente);
        }

        _pedidoItems.Add(pedidoItem);
        CalcularValorPedido();
    }

    public void AtualizarItem(PedidoItem pedidoItem)
    {
        ValidarPedidoItemInexistente(pedidoItem);
        
        var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);
        
        _pedidoItems.Remove(itemExistente);
        _pedidoItems.Add(pedidoItem);
        
        ValidarQuantidadeItemPermitida(pedidoItem);
        CalcularValorPedido();
    }

    public void TornarRascunho()
    {
        PedidoStatus = PedidoStatus.Rascunho;
    }

    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascunho(Guid clienteId)
        {
            var pedido = new Pedido
            {
                ClienteId = clienteId
            };

            pedido.TornarRascunho();
            return pedido;
        }
    }
}