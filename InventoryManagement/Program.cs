using InventoryManagement.Modelos;

namespace InventoryManagement;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Inventory Management System ===\n");

        SistemaInventario sistema = new SistemaInventario();
        CargarDatosIniciales(sistema); // Yo ya tengo cargado los datos de las listas que estan en Sistema Inventario
        
        // Menu de entrada
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
                // Entra aqui -> 
                switch (opcion)
                {
                    case 1:
                        GestionEmpleados(sistema);
                        break;
                    case 2:
                        // GestionProductos(sistema);
                        break;
                    case 3:
                        // MovimientosStock(sistema);
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
            } catch (Exception ex) // throw an error
            {
                Console.WriteLine(ex.Message); // IndexOutOfRangeException // En internet
            }
        }
    }

    static void GestionEmpleados(SistemaInventario sistema)
    {
        bool regresar = false;
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
                    AgregarEmpleado(sistema);
                    break;
                case "2":
                    MostrarEmpleados(sistema);
                    break;
                case "3":
                    MostrarEmpleadoPorId(sistema);
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

    static void MostrarEmpleadoPorId(SistemaInventario sistema)
    {
        Console.WriteLine("\nMostrar Empleados por Id: ");
        Console.WriteLine("Ingresa el Id del Empleado: ");
        
        int id = int.Parse(Console.ReadLine());
        
        // Validar si el id existe

        var empleado = sistema.ObtenerEmpleadoPorId(id);
        
        Console.WriteLine("ID\tNombres\tTipo");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"{empleado.Id}\t{empleado.Nombres}\t{empleado.TipoEmpleado.Nombre}\t{empleado.FechaIngreso}\t{empleado.Edad}\t{empleado.Genero}");
    }

    static void AgregarEmpleado(SistemaInventario sistema)
    {
        Console.WriteLine("\nAgregar un nuevo Empleado: ");

        Console.WriteLine("Nombre del empleado: ");
        string nombres = Console.ReadLine();
        
        Console.WriteLine("Tipo de Empleado: ");
        var tiposEmpleados = sistema.ObtenerTipoEmpleados();

        if (tiposEmpleados.Count == 0)
        {
            Console.WriteLine("No hay ningun tipo de Empleado");
            return;
        }

        Console.WriteLine("Tipos de empleados disponibles: ");

        for (int i = 0; i < tiposEmpleados.Count; i++)
        {
            Console.WriteLine("{0}. {1}", i + 1, tiposEmpleados[i].Nombre);
        }

        Console.WriteLine("Seleccionar el tipo de empleado:");
        if (!int.TryParse(Console.ReadLine(), out int tipoIndice) || tipoIndice < 0)
        {
            Console.WriteLine("Seleccion invalida.");
            return;
        }

        var tipoSeleccionado = tiposEmpleados[tipoIndice]; // Estoy agarrando una instancia de TipoEmpleado
        
        int edad = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Ingrese su genero con la palabra MASCULINO o FEMENINO");
        Genero genero = (Genero)Convert.ToInt32(Console.ReadLine()); // 1 = Masculino
        
        // Leeerlo con el tryParse
        
        // Agregar año, mes y fecha
        // Agregar usando el formato DD/MM/AAAA

        var empleado = new Empleado
        {
            Nombres = nombres,
            TipoEmpleado = tipoSeleccionado,
            Estado = EstadoEmpleado.ACTIVO,
            FechaIngreso = new DateTime(1900, 1, 1),
            Edad = edad,
            Genero = genero,
        };
        
        sistema.AgregarEmpleado(empleado);
        Console.WriteLine($"\n Empleado agregado con el ID: {empleado.Id}");
    }

    static void MostrarEmpleados(SistemaInventario sistema)
    {
        var empleados = sistema.ObtenerEmpleados();
        
        // Validar si no hay empleados
        if (empleados.Count == 0)
        {
            Console.WriteLine("No hay ningun Empleado");
            return;
        }

        Console.WriteLine("Lista de empleados");
        Console.WriteLine("ID\t\tNombres\t\tTipo\t");
        Console.WriteLine("-------------------------------------");
        foreach (var empleado in empleados)
        {
            Console.WriteLine($"{empleado.Id}\t{empleado.Nombres}\t{empleado.TipoEmpleado.Nombre}\t{empleado.FechaIngreso}\t{empleado.Edad}\t{empleado.Genero}");
        }
    }

    static void CargarDatosIniciales(SistemaInventario sistema)
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


    }
}