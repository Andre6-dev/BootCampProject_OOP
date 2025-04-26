using InventoryManagement.Modelos;

namespace InventoryManagement.Utilidades;

public class CargaDatos
{
    public static void CargarDatosIniciales(SistemaInventario sistema)
    {
        // Agregar tipos de empleado
        var tipoAdmin = new TipoEmpleado { Nombre = "Administrador", Descripcion = "Acceso total al sistema"};
        var tipoAlmacen = new TipoEmpleado { Nombre = "Almacenero", Descripcion = "Gestion del almacen"};
        var tipoVendedor = new TipoEmpleado { Nombre = "Vendedor", Descripcion = "Reegistro de ventas"};
        
        sistema.AgregarTipoEmpleado(tipoAdmin);
        sistema.AgregarTipoEmpleado(tipoAlmacen);
        sistema.AgregarTipoEmpleado(tipoVendedor);
        
        // Agregar los empleados
        var empleado1 = new Empleado
        {
            Nombres = "Andre Antonio",
            TipoEmpleado = tipoAdmin,
            Estado = EstadoEmpleado.ACTIVO,
            FechaIngreso = new DateTime(1900, 1, 1),
            Edad = 29,
            Genero = Genero.MASCULINO,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
        
        var empleado2 = new Empleado
        {
            Nombres = "Carlos Ramos",
            TipoEmpleado = tipoVendedor,
            Estado = EstadoEmpleado.ACTIVO,
            FechaIngreso = new DateTime(1987, 1, 1),
            Edad = 24,
            Genero = Genero.MASCULINO,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
        
        sistema.AgregarEmpleado(empleado1);
        sistema.AgregarEmpleado(empleado2);
        
        // Agregar Productos
        var producto1 = new Producto
        {
            Sku = "LAP001",
            Nombre = "Laptop HP 15.6\"",
            Descripcion = "Laptop HP con procesador Intel i5, 8GB RAM, 256GB SSD",
            Precio = 799.99m,
            Stock = 25,
            Activo = true,
            Categoria = CategoriaProducto.LAPTOPS,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
        
        var producto2 = new Producto
        {
            Sku = "MON002",
            Nombre = "Monitor Dell 24\"",
            Descripcion = "Monitor Dell Full HD 1080p, 60Hz",
            Precio = 199.99m,
            Stock = 15,
            Activo = true,
            Categoria = CategoriaProducto.MONITORES,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
            
        var producto3 = new Producto
        {
            Sku = "IMP003",
            Nombre = "Impresora Láser HP",
            Descripcion = "Impresora Láser monocromática, 20 ppm",
            Precio = 149.99m,
            Stock = 10,
            Activo = true,
            Categoria = CategoriaProducto.IMPRESORAS,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
            
        var producto4 = new Producto
        {
            Sku = "MOU004",
            Nombre = "Mouse Logitech Inalámbrico",
            Descripcion = "Mouse óptico inalámbrico con 5 botones",
            Precio = 24.99m,
            Stock = 30,
            Activo = true,
            Categoria = CategoriaProducto.IMPRESORAS,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
            
        var producto5 = new Producto
        {
            Sku = "TEC005",
            Nombre = "Teclado Mecánico Gaming",
            Descripcion = "Teclado mecánico RGB con switches Blue",
            Precio = 59.99m,
            Stock = 8,
            Activo = true,
            Categoria = CategoriaProducto.IMPRESORAS,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };
        
        sistema.AgregarProducto(producto1);
        sistema.AgregarProducto(producto2);
        sistema.AgregarProducto(producto3);
        sistema.AgregarProducto(producto4);
        sistema.AgregarProducto(producto5);
        
        // Movimientos de stock por producto
        sistema.RegistrarEntrada(producto1, 5, empleado1);
        sistema.RegistrarEntrada(producto2, 5, empleado1);
        sistema.RegistrarSalida(producto3, 2, empleado1);
        sistema.RegistrarEntrada(producto4, 3, empleado1);
        sistema.RegistrarSalida(producto5, 4, empleado1);

    }
}