using GestionPedidos.Modelos;
using GestionPedidos.Repositorios;

namespace GestionPedidos.Servicios;

public class PedidoService
{
    private readonly ProductoRepository _productoRepository;
    private readonly PedidoRepository _pedidoRepository;
    
    public PedidoService(ProductoRepository productoRepository, PedidoRepository pedidoRepository)
    {
        _productoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
        _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
    }

    public Pedido CrearPedido(Cliente cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        return new Pedido
        {
            ClienteId = cliente.Id,
            NombreCliente = cliente.Nombre,
            Fecha = DateTime.Now,
        };
    }

    public void GuardarPedido(Pedido pedido)
    {
        if (pedido == null)
            throw new ArgumentNullException(nameof(pedido));
        
        if (pedido.Productos.Count == 0)
            throw new InvalidOperationException("El pedido no tiene productos");
        
        _pedidoRepository.Agregar(pedido);
    }
}