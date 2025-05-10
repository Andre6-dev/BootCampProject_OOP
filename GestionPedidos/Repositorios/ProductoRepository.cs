using GestionPedidos.Modelos;

namespace GestionPedidos.Repositorios;

public class ProductoRepository
{
    private List<Producto> _productos = new List<Producto>();

    public List<Producto> ObtenerProductos()
    {
        return _productos;
    }

    public Producto ObtenerProducto(int id)
    {
        return _productos.FirstOrDefault(p => p.Id == id);
    }

    public void Agregar(Producto producto)
    {
        if (producto == null)
            throw new ArgumentNullException(nameof(producto));
        _productos.Add(producto);
    }
    
    public void AgregarDatosEjemplo()
    {
        Agregar(new Producto { Nombre = "Laptop", Precio = 1200.00m, Stock = 10 });
        Agregar(new Producto { Nombre = "Mouse", Precio = 25.00m, Stock = 50 });
        Agregar(new Producto { Nombre = "Teclado", Precio = 45.00m, Stock = 30 });
    }
}