namespace GestionPedidos;

class Program
{
    static void Main(string[] args)
    {
        List<string[]> clientes = new List<string[]>();
        List<string[]> productos = new List<string[]>();
        List<string[]> pedidos = new List<string[]>();
        
        // Agregar clientes de ejemplo
        clientes.Add(new string[] 
            { "C001", "Juan Perez", "juan@ejemplo.com", "555-1234" }
        );
        clientes.Add(new string[] 
            { "C002", "Maria Lopez", "maria@ejemplo.com", "555-5678" }
        );
            
        // Agregar productos de ejemplo
        productos.Add(new string[] 
            { "P001", "Laptop", "1200.00", "10" }
        );
        productos.Add(new string[] 
            { "P002", "Mouse", "25.00", "50" }
        );
        productos.Add(new string[] 
            { "P003", "Teclado", "45.00", "30" }
        );

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
                    Console.Clear();
                    Console.WriteLine("=== LISTA DE CLIENTES ===");
                    Console.WriteLine("ID\tNombre\t\tEmail\t\tTeléfono");
                    foreach (string[] cliente in clientes)
                    {
                        Console.WriteLine($"{cliente[0]}\t{cliente[1]}\t{cliente[2]}\t{cliente[3]}");
                    }
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                
                case "2":
                    Console.Clear();
                    Console.WriteLine("=== LISTA DE PRODUCTOS ===");
                    Console.WriteLine("ID\tNombre\t\tPrecio\t\tStock");
                    foreach (string[] producto in productos)
                    {
                        Console.WriteLine($"{producto[0]}\t{producto[1]}\t{producto[2]}\t{producto[3]}");
                    }
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                
                case "3":
                    Console.Clear();
                    Console.WriteLine("=== CREAR NUEVO PEDIDO ===");
                    
                    // Seleccionar un cliente
                    Console.WriteLine("Clientes disponibles: ");
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        Console.WriteLine($"{i+1}. {clientes[i][1]} ({clientes[i][0]})");
                    }
                    Console.Write("Seleccione un cliente (número): ");
                    int clienteIndex = int.Parse(Console.ReadLine()) - 1;
                    
                    // Crear pedido
                    string pedidoId = "0" + (pedidos.Count + 1).ToString("000");
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    
                    // Agregar productos al pedido
                    List<string[]> itemsPedido = new List<string[]>();
                    bool agregarProductos = true;
                    double total = 0;

                    while (agregarProductos)
                    {
                        Console.WriteLine("\nProductos disponibles: ");
                        for (int i = 0; i < productos.Count; i++)
                        {
                            Console.WriteLine($"{i+1}. {productos[i][1]} - ${productos[i][2]} (Stock: {productos[i][3]})");
                        }

                        Console.WriteLine("Seleccione un producto(numero) o 0 para finalizar: ");
                        int productoIndex = int.Parse(Console.ReadLine()) - 1;

                        if (productoIndex == -1)
                        {
                            agregarProductos = false;
                        }
                        else
                        {
                            Console.WriteLine("Cantidad: ");
                            int cantidad = int.Parse(Console.ReadLine());

                            if (cantidad > int.Parse(productos[productoIndex][3]))
                            {
                                Console.WriteLine("Error: No hay suficiente stock disponible");
                            }
                            else
                            {
                                // Actualizar el stock
                                int nuevoStock = int.Parse(productos[productoIndex][3]) - cantidad; // TODO: Debuggear
                                productos[productoIndex][3] = nuevoStock.ToString();
                                
                                // Calcular el subtotal
                                double precio = double.Parse(productos[productoIndex][2]);
                                double subtotal = precio * cantidad;
                                total += subtotal;
                                
                                // Agregar el item al pedido
                                itemsPedido.Add(new string[]
                                {
                                  productos[productoIndex][0],
                                  productos[productoIndex][1],
                                  precio.ToString(),
                                  cantidad.ToString(),
                                  subtotal.ToString(),
                                });
                                
                                Console.WriteLine($"Producto agregado: {cantidad} x {productos[productoIndex][1]} = ${subtotal}");
                            }
                        }
                    }
                    
                    // Guardado del pedido
                    if (itemsPedido.Count > 0)
                    {
                        // Convertir los items a un formato serializable para guardar en el array
                        string itemsSerializados = "";
                        foreach (string[] item in itemsPedido)
                        {
                            itemsSerializados += $"{item[0]}|{item[1]}|{item[2]}|{item[3]}|{item[4]}#";
                        }
                            
                        pedidos.Add(new string[] { 
                            pedidoId, 
                            clientes[clienteIndex][0], 
                            clientes[clienteIndex][1], 
                            fecha, 
                            total.ToString(), 
                            itemsSerializados 
                        });
                            
                        Console.WriteLine($"\nPedido {pedidoId} creado con éxito. Total: ${total}");
                    }
                    else
                    {
                        Console.WriteLine("\nEl pedido no tiene productos. Operacion cancelada.");
                    }

                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("=== LISTA DE PEDIDOS ===");
                    if (pedidos.Count == 0)
                    {
                        Console.WriteLine("No hay pedidos registrados.");
                    }
                    else
                    {
                        foreach (string[] pedido in pedidos)
                        {
                            Console.WriteLine($"Pedido: {pedido[0]}");
                            Console.WriteLine($"Cliente: {pedido[2]} ({pedido[1]})");
                            Console.WriteLine($"Fecha: {pedido[3]}");
                            Console.WriteLine($"Total: ${pedido[4]}");
                            Console.WriteLine("Productos:");
                                
                            string[] items = pedido[5].Split('#');
                            foreach (string item in items)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    string[] detalles = item.Split('|');
                                    Console.WriteLine($"  {detalles[3]} x {detalles[1]} (${detalles[2]}) = ${detalles[4]}");
                                }
                            }
                            Console.WriteLine("-------------------------");
                        }
                    }

                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "5":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }

        Console.WriteLine("!Gracias por usar el Sistema de Gestion de pedidos");
    }
}