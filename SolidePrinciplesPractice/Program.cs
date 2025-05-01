namespace SolidePrinciplesPractice;

class Program
{
    static void Main(string[] args)
    {
        // Crear las implementaciones concretas

        var repositorio = new RepositorioProductos();
        var notificador = new EmailNotificador();
        var validador = new InventarioValidador();
        
        // Inyectar dependencias al gestor
        var gestorInventario = new GestorInventario(repositorio, notificador, validador);
        
        var producto1 = new Producto(1, "Laptop", 1200.00m, 10);
        var producto2 = new Producto(2, "Teclado", 200.00m, 5);
        var producto3 = new Producto(3, "Mouse", 50.00m, 8);
        
        // Agregar producto al repositorio
        repositorio.AgregarProducto(producto1);
        repositorio.AgregarProducto(producto2);
        repositorio.AgregarProducto(producto3);
        
        gestorInventario.ListarTodosLosProductos();
        
        // Reductir el stock
        
        gestorInventario.ReducirStock(1, 5);

        Console.WriteLine("Lista de productos luego de reducir stock");
        gestorInventario.ListarTodosLosProductos();
    }

    // Principio de Responsabilidad Unica
    // Clase solo representa un producto
    public class Producto
    {
        // Propiedades de la clase
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // Constructor
        public Producto(int id, string nombre, decimal precio, int stock)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
        }
    }

    // Principio de Abierto/Cerrado (O)
    // Interfaces para extender funcionalidad sin la necesidad de modificar codigo existente
    public interface IRepositorioProductos
    {
        void AgregarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        Producto ObtenerProducto(int id);
        List<Producto> ObtenerProductos();
        void EliminarProducto(int id);
    }

    // Implementacion concreta del repositorio
    public class RepositorioProductos : IRepositorioProductos
    {
        private readonly List<Producto> _productos = new List<Producto>();
        
        public void AgregarProducto(Producto producto)
        {
            _productos.Add(producto);
            Console.WriteLine($"Agregando producto con nombre: {producto.Nombre}");
        }

        public void ActualizarProducto(Producto producto)
        {
            var productoExistente = ObtenerProducto(producto.Id);
            if (productoExistente != null)
            {
                _productos.Remove(productoExistente);
                _productos.Add(producto);
                Console.WriteLine($"Actualizando producto con nombre: {producto.Nombre}");
            }
        }

        public Producto ObtenerProducto(int id)
        {
            return _productos.Find(p => p.Id == id);
        }

        public List<Producto> ObtenerProductos()
        {
            return _productos;
        }

        public void EliminarProducto(int id)
        {
            var producto = ObtenerProducto(id);

            if (producto != null)
            {
                _productos.Remove(producto);
                Console.WriteLine($"Eliminando producto con nombre: {producto.Nombre}");
            }
        }
    }
    
    // Principio de Segregacion de Interfaces (I)
    public interface INotificador
    {
        void Notificar(string mensaje);
    }

    public interface IInventarioValidador
    {
        bool ValidarStock(Producto producto, int stock);
    }


    public class EmailNotificador : INotificador
    {
        public void Notificar(string mensaje)
        {
            Console.WriteLine($"Notificacion por email: {mensaje}");
        }
    }
    
    public class InventarioValidador : IInventarioValidador
    {
        public bool ValidarStock(Producto producto, int stock)
        {
            return producto.Stock >= stock;
        }
    }
    
    // Principio de Inversion de Dependencias (D)
    // Debemos depender de abstraciones y no de implementaciones
    public class GestorInventario
    {
        private readonly IRepositorioProductos _repositorioProductos;
        private readonly INotificador _notificador;
        private readonly IInventarioValidador _inventarioValidador;

        // Inyeccion de dependencias
        public GestorInventario(
            IRepositorioProductos repositorioProductos,
            INotificador notificador,
            IInventarioValidador inventarioValidador)
        {
            _repositorioProductos = repositorioProductos;
            _notificador = notificador;
            _inventarioValidador = inventarioValidador;
        }

        // Principio de Susticion de Liskov (L)
        // Cualquier implementacion de IRepositorioProductos va a funcionar aqui
        public void ReducirStock(int productoId, int cantidad)
        {
            var producto = _repositorioProductos.ObtenerProducto(productoId);

            if (producto == null)
            {
                _notificador.Notificar($"Producto no encontrado");
            }

            if (_inventarioValidador.ValidarStock(producto, cantidad))
            {
                producto.Stock -= cantidad;
                _repositorioProductos.AgregarProducto(producto);
                _notificador.Notificar($"Stock agregado: {producto.Stock}");
            }
            else
            {
                _notificador.Notificar($"Stock insuficiente para {producto.Nombre}");
            }
        }

        public void ListarTodosLosProductos()
        {
            Console.WriteLine("Lista de productos:");
            var productos = _repositorioProductos.ObtenerProductos();
            foreach (var producto in productos)
            {
                Console.WriteLine($"{producto.Nombre} - {producto.Stock}");
            }
        }
    }
}