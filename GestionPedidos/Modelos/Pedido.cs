namespace GestionPedidos.Modelos;

public class Pedido
{
    public string Id { get; set; }
    public string ClienteId { get; set; }
    public string NombreCliente { get; set; }
    public DateTime Fecha { get; set; }
    public List<ProductoPedido> Productos { get; set; }

    public decimal Total => Productos.Sum(p => p.Subtotal);

    public void AgregarProducto(Producto producto, int cantidad)
    {
        if (producto == null)
            throw new ArgumentNullException(nameof(producto));

        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad es invalida", nameof(cantidad));
        }
        
        producto.ReducirStock(cantidad);
        
        Productos.Add(new ProductoPedido
        {
            ProductoId = producto.Id,
            Nombre = producto.Nombre,
            Precio = producto.Precio,
            Cantidad = cantidad,
        });
    }
}