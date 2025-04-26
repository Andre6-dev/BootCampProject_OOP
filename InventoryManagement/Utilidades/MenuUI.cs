using InventoryManagement.Interfaces;

namespace InventoryManagement.Utilidades;

public class MenuUI
{

    public static void CargarMenuPrincipal(SistemaInventario sistema)
    {
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\nMenu Principal:");
            Console.WriteLine("1. Gestion de empleados");
            Console.WriteLine("2. Gestion de Productos");
            Console.WriteLine("3. Movimientos de stock");
            Console.WriteLine("4. Reportes");
            Console.WriteLine("0. Salir");

            Console.WriteLine("\n Seleccione una opcion: ");
            
            try
            {
                int opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        GestionEmpleados(sistema);
                        break;
                    case 2:
                        GestionProductos(sistema);
                        break;
                    case 3:
                        MovimientosStock(sistema);
                        break;
                    case 4:
                        // MostrarReportes(sistema);
                        break;
                    case 0:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }
    }
    
    static void MovimientosStock(SistemaInventario sistema)
    {
        bool regresar = false;
        while (!regresar)
        {
            Console.WriteLine("\nGestion de Movimientos de stock: ");
            Console.WriteLine("\nSeleccione una opcion: ");
            Console.WriteLine("1. Agregar movimiento stock - Entrada");
            Console.WriteLine("2. Agregar movimiento stock - Salida");
            Console.WriteLine("3. Mostrar movimientos por producto id");
            Console.WriteLine("0. Volver atras");
            
            string opcion = Console.ReadLine();
            IMovimientoStock servicio = new MovimientoStock();

            switch (opcion)
            {
                case "1":
                    servicio.AgregarEntrada(sistema);
                    break;
                case "2":
                    // AgregarSalida(sistema);
                    break;
                case "3":
                    servicio.MostrarMovimientosPorProducto(sistema);
                    break;
                case "4":
                    // MostrarTipoEmpleados(sistema);
                    break;
                case "0":
                    regresar = true;
                    break;
                default:
                    Console.WriteLine("Opcion invalida.");
                    break;
            }
        }
    }
    
    static void GestionEmpleados(SistemaInventario sistema)
    {
        bool regresar = false;
        IEmpleadoServicio servicio = new EmpleadoServicio();
        while (!regresar)
        {
            Console.WriteLine("\nGestion de Empleados: ");
            Console.WriteLine("\nSeleccione una opcion: ");
            Console.WriteLine("1. Agregar un empleado");
            Console.WriteLine("2. Ver lista de empleados");
            Console.WriteLine("3. Ver empleado por Id");
            Console.WriteLine("4. Mostrar lista de tipos de empleados");
            Console.WriteLine("0. Regresar al menu principal");
            
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    servicio.AgregarEmpleado(sistema);
                    break;
                case "2":
                    servicio.MostrarEmpleados(sistema);
                    break;
                case "3":
                    servicio.MostrarEmpleadoPorId(sistema);
                    break;
                case "4":
                    // MostrarTipoEmpleados(sistema);
                    break;
                case "0":
                    regresar = true;
                    break;
                default:
                    Console.WriteLine("Opcion invalida.");
                    break;
            }
        }
    }

    static void GestionProductos(SistemaInventario sistema)
    {
        bool regresar = false;
        IProductoServicio servicio = new ProductoServicio();
        while (!regresar)
        {
            Console.WriteLine("\nGestion de Productos: ");
            Console.WriteLine("\nSeleccione una opcion: ");
            Console.WriteLine("1. Agregar un producto");
            Console.WriteLine("2. Ver lista de productos");
            
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    servicio.AgregarProducto(sistema);
                    break;
                case "2":
                    servicio.MostrarProductos(sistema);
                    break;
                case "0":
                    regresar = true;
                    break;
                default:
                    Console.WriteLine("Opcion invalida.");
                    break;
            }
        }
    }
}