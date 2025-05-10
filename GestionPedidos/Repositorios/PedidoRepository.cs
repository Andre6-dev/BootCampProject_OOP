using GestionPedidos.Modelos;

namespace GestionPedidos.Repositorios;

public class PedidoRepository
{
    private List<Pedido> _pedidos = new List<Pedido>();

    public List<Pedido> ObtenerPedidos()
    {
        return _pedidos;
    }

    public void Agregar(Pedido pedido)
    {
        if (pedido == null)
            throw new ArgumentNullException(nameof(pedido));
        
        _pedidos.Add(pedido);
    }
}