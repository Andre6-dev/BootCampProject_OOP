namespace GestionPedidos.Modelos;

public class Producto
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    
    public bool HayStockDisponible(int cantidad)
    {
        return Stock >= cantidad;
    }
        
    public void ReducirStock(int cantidad)
    {
        if (!HayStockDisponible(cantidad))
            throw new InvalidOperationException("No hay suficiente stock disponible.");
                
        Stock -= cantidad;
    }
        
    public override string ToString()
    {
        return $"{Nombre} - ${Precio} (Stock: {Stock})";
    }
}