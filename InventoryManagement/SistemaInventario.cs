using InventoryManagement.Modelos;

namespace InventoryManagement;

public class SistemaInventario // Heredar esa interfaz para sobrescribir esos metodos
{
    // Propiedades 
    private List<TipoEmpleado> _tiposEmpleados = new List<TipoEmpleado>();
    private List<Empleado> _empleados = new List<Empleado>();
    private List<Producto> _productos = new List<Producto>();
    private List<MovimientoStock> _movimientos = new List<MovimientoStock>();
    
    // Metodo para agregar un tipo empleado
    public void AgregarTipoEmpleado(TipoEmpleado tipoEmpleado)
    {
        _tiposEmpleados.Add(tipoEmpleado);
    }
    
    // Metodo para listar todos los tipos de empleados
    public List<TipoEmpleado> ObtenerTipoEmpleados()
    {
        return _tiposEmpleados.ToList();
    }
    
    // Metodo para obtener un solo empleado y quiero que sea por Id
    public TipoEmpleado ObtenerTipoEmpleadoPorId(int id)
    {
        
        // (parametros) => expresion o bloque de codigo
        // t -> tipoEmpleado
        // expresion -> t.Id = id -> si el id que le pasas al metodo es igual a uno de los ids de los elementos de la lista
        // entonces tienes que retornar el elemento
        return _tiposEmpleados.FirstOrDefault(t => t.Id == id);
    }

    public void AgregarEmpleado(Empleado empleado)
    {
        _empleados.Add(empleado);
    }

    public List<Empleado> ObtenerEmpleados()
    {
        return _empleados.ToList();
        _empleados.Where(e => e.Edad > 27 && e.Edad < 35).ToList(); // Te trae todos los empleados que tengan mayor de 27
    }
    
    public Empleado ObtenerEmpleadoPorId(int id)
    {
        return _empleados.FirstOrDefault(t => t.Id == id);
    }
    
    public List<Empleado> ObtenerEmpleadosEnRetiro()
    {
        return _empleados.Where(e => e.Edad > 64).ToList();
    }
    
    // Agregar el resto de metodos
    public void AgregarProducto(Producto producto)
    {
        _productos.Add(producto);
    }

    public List<Producto> ListarProductos()
    {
        return _productos.ToList();
    }

    public Producto ObtenerProductoPorId(int id)
    {
        return _productos.FirstOrDefault(t => t.Id == id)!;
    }

    public Producto ObtenerProductoPorSku(string sku)
    {
        return _productos.FirstOrDefault(t => t.Sku == sku)!;
    }

    public void RegistrarEntrada(Producto producto, int cantidad, Empleado empleado)
    {
        // si la cantidad es menor que 0 retorna un error
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad es invalida");
        }
        
        // Actualizar el stock del producto
        producto.Stock += cantidad;
        
        // Crear y registrar el movimiento
        var movimientoStock = new MovimientoStock
        {
            Producto = producto,
            Empleado = empleado,
            cantidad = cantidad,
            TipoMovimiento = TipoMovimiento.ENTRADA,
            Fecha = DateTime.Now,
        };
        
        _movimientos.Add(movimientoStock);
    }
    
    public void RegistrarSalida(Producto producto, int cantidad, Empleado empleado)
    {
        // si la cantidad es menor que 0 retorna un error
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad es invalida");
        }
        
        // 
        if (producto.Stock < cantidad)
        {
            throw new ArgumentException("No hay suficiente stock");
        }
        
        // Actualizar el stock del producto
        producto.Stock -= cantidad;
        
        // Crear y registrar el movimiento
        var movimientoStock = new MovimientoStock
        {
            Producto = producto,
            Empleado = empleado,
            cantidad = cantidad,
            TipoMovimiento = TipoMovimiento.SALIDA,
            Fecha = DateTime.Now,
        };
        
        _movimientos.Add(movimientoStock);
    }

    public List<MovimientoStock> ObtenerMovimientosPorProductoId(int productoId)
    {
        return _movimientos.Where(m => m.Producto.Id == productoId).ToList();
    }
}