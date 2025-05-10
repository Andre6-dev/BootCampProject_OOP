namespace GestionPedidos.Modelos;

public class ProductoPedido
{
    public string ProductoId { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    
    public decimal Subtotal => Precio * Cantidad;
    
    public override string ToString()
    {
        return $"{Cantidad} x {Nombre} (${Precio}) = ${Subtotal}";
    }
}