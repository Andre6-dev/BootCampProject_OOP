namespace InventoryManagement.Modelos;

public class Producto
{
    private static int _nextId = 1;
    
    public int Id { get; private set; }
    public string Sku { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public bool Activo { get; set; }
    public CategoriaProducto Categoria { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    
    public Producto()
    {
        Id = _nextId++;
    }
}