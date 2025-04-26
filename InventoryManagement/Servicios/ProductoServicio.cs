using InventoryManagement.Modelos;

namespace InventoryManagement.Interfaces;

public class ProductoServicio : IProductoServicio
{
    public void AgregarProducto(SistemaInventario sistema)
    {
        Console.WriteLine("\nAgregar un nuevo Producto: ");
        
        Console.WriteLine("Sku del Producto: ");
        string sku = Console.ReadLine();

        Console.WriteLine("Nombre del Producto: ");
        string nombre = Console.ReadLine();
        
        Console.WriteLine("Nombre del Producto: ");
        string descripcion = Console.ReadLine();
        
        Console.WriteLine("Precio del Producto: ");
        decimal precio = Convert.ToDecimal(Console.ReadLine());
        
        Console.WriteLine("Stock del Producto: ");
        int stock = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Estado del Producto: ");
        bool estado = Convert.ToBoolean(Console.ReadLine());
        
        Console.WriteLine("Estado del Producto: ");
        CategoriaProducto categoria = (CategoriaProducto)Convert.ToInt32(Console.ReadLine()); // 1 = Masculino
        
        var producto = new Producto()
        {
            Sku = sku,
            Nombre = nombre,
            Descripcion = descripcion,
            Precio = precio,
            Stock = stock,
            Activo = estado,
            Categoria = categoria,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
        
        sistema.AgregarProducto(producto);
        Console.WriteLine($"\n Empleado agregado con el ID: {producto.Id}");
    }

    public void MostrarProductos(SistemaInventario sistema)
    {
        var productos = sistema.ListarProductos();
        
        // Validar si no hay productos
        if (productos.Count == 0)
        {
            Console.WriteLine("No hay ningun Empleado");
            return;
        }

        Console.WriteLine("Lista de Productos");
        Console.WriteLine("ID\tSku\tNombre\tDescripcion\tPrecio\tStock\tActivo\tCategoria\tFechaCreacion");
        Console.WriteLine("-------------------------------------");
        foreach (var producto in productos)
        {
            Console.WriteLine($"{producto.Id}\t{producto.Sku}\t{producto.Nombre}\t{producto.Descripcion}\t{producto.Precio}\t{producto.Stock}\t{producto.Activo}\t{producto.Categoria}\t{producto.FechaCreacion}");
        }
    }
}