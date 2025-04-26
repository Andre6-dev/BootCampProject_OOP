using InventoryManagement.Modelos;

namespace InventoryManagement.Interfaces;

public class EmpleadoServicio : IEmpleadoServicio
{
    public void AgregarEmpleado(SistemaInventario sistema)
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

    public void MostrarEmpleados(SistemaInventario sistema)
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

    public void MostrarEmpleadoPorId(SistemaInventario sistema)
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
}