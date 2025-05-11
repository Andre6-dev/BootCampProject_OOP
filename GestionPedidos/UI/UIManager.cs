using GestionPedidos.Repositorios;
using GestionPedidos.Servicios;

namespace GestionPedidos.UI;

public class UIManager
{
    private readonly ClienteRepository _clienteRepository;
    private readonly ProductoRepository _productoRepository;
    private readonly PedidoRepository _pedidoRepository;
    private readonly PedidoService _pedidoService;

    public UIManager(
        ClienteRepository clienteRepository,
        ProductoRepository productoRepository,
        PedidoRepository pedidoRepository)
    {
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        _productoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
        _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
        _pedidoService = new PedidoService(productoRepository, pedidoRepository);
    }

    public void EjecutarMenuPrincipal()
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE PEDIDOS ===");
            Console.WriteLine("1. Ver clientes");
            Console.WriteLine("2. Ver productos");
            Console.WriteLine("3. Crear pedido");
            Console.WriteLine("4. Ver pedidos");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MostrarClientes();
                    break;

                case "2":
                    MostrarProductos();
                    break;

                case "3":
                    CrearPedido();
                    break;

                case "4":
                    MostrarPedidos();
                    break;

                case "5":
                    salir = true;
                    break;

                default:
                    MostrarMensaje("Opción no válida.");
                    break;
            }
        }

        Console.WriteLine("¡Gracias por usar el Sistema de Gestión de Pedidos!");
    }

    private void MostrarClientes()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE CLIENTES ===");
        Console.WriteLine("ID\tNombre\t\tEmail\t\tTeléfono");

        foreach (var cliente in _clienteRepository.ObtenerTodos())
        {
            Console.WriteLine($"{cliente.Id}\t{cliente.Nombre}\t{cliente.Email}\t{cliente.Telefono}");
        }

        EsperarTecla();
    }

    private void MostrarProductos()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE PRODUCTOS ===");
        Console.WriteLine("ID\tNombre\t\tPrecio\t\tStock");

        foreach (var producto in _productoRepository.ObtenerProductos())
        { 
            Console.WriteLine($"{producto.Id}\t{producto.Nombre}\t{producto.Precio}\t{producto.Stock}");
        }
        
        EsperarTecla();
    }

    private void CrearPedido()
    {
        Console.Clear();
        Console.WriteLine("=== CREAR NUEVO PEDIDO ===");
        
        // Seleccionar el cliente
        var clientes = _clienteRepository.ObtenerTodos();
        Console.WriteLine("Clientes disponibles:");

        for (int i = 0; i < clientes.Count; i++)
        {
            Console.WriteLine($"{i+1}. {clientes[i]}");
        }
        
        Console.Write("Seleccione un cliente(numero)");
        
        if (!int.TryParse(Console.ReadLine(), out int clienteIndex) || clienteIndex < 1 || clienteIndex > clientes.Count)
        {
            MostrarMensaje("Selección de cliente inválida.");
            return;
        }
        
        var cliente = clientes[clienteIndex - 1];
        var pedido = _pedidoService.CrearPedido(cliente);
        
        // Agregar los productos al pedido
        bool agregarProductos = true;

        while (agregarProductos)
        {
            var productos = _productoRepository.ObtenerProductos();
            Console.WriteLine("\nProductos disponibles:");
            
            for (int i = 0; i < productos.Count; i++)
            {
                Console.WriteLine($"{i+1}. {productos[i]}");
            }

            Console.Write("Seleccione un producto(numero) o 0 para finalizar:");
            if (!int.TryParse(Console.ReadLine(), out int productoIndex))
            {
                Console.WriteLine("Entrada invalida. ");
                return;
            }

            if (productoIndex == 0)
            {
                agregarProductos = false;
            }
            else if (productoIndex < 1 || productoIndex > productos.Count)
            {
                Console.WriteLine("Seleccion de producto invalida.");
            }
            else
            {
                var producto = productos[productoIndex - 1];

                Console.Write("Cantidad: ");
                if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad < 1)
                {
                    Console.WriteLine("Cantidad invalida.");
                    continue;
                }

                try
                {
                    pedido.AgregarProducto(producto, cantidad);
                    Console.WriteLine($"Producto agregado: {cantidad} x {producto.Nombre} = ${producto.Precio * cantidad}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    throw;
                }
            }
        }
        
        // Guardar pedido
        if (pedido.Productos.Count > 0)
        {
            try
            {
                _pedidoService.GuardarPedido(pedido);
                Console.WriteLine($"\nPedido con {pedido.Id} se ha creado con exito. Total ${pedido.Total}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nError al guardar pedido: {e.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nEl pedido no tiene productos. Operacion invalida");
        }
        
        EsperarTecla();
    }

    private void MostrarPedidos()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE PEDIDOS ===");
        
        var pedidos = _pedidoRepository.ObtenerPedidos().ToList();

        if (pedidos.Count == 0)
        {
            Console.WriteLine("No hay pedidos registrados.");
        }
        else
        {
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"Pedido: {pedido.Id}");
                Console.WriteLine($"Cliente: {pedido.NombreCliente} ({pedido.ClienteId})");
                Console.WriteLine($"Fecha: {pedido.Fecha:yyyy-MM-dd}");
                Console.WriteLine($"Total: ${pedido.Total}");

                foreach (var producto in pedido.Productos)
                {
                    Console.WriteLine($"Producto: {producto.Nombre} ({producto.Precio})");
                }
                Console.WriteLine("-------------------------");
            }
        }
        
        EsperarTecla();
    }

    private void MostrarMensaje(string mensaje)
    {
        Console.WriteLine(mensaje);
        EsperarTecla();
    }

    private void EsperarTecla()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}