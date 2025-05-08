using ControlFinanzas.Modelos;
using ControlFinanzas.Servicios;

namespace ControlFinanzas.UI;

public class InterfazUsuario
{
    private readonly GestorDatos _gestorDatos;

    public InterfazUsuario(GestorDatos gestor)
    {
        this._gestorDatos = gestor;
    }

    public void IniciarMenu()
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE FINANZAS PERSONALES ===");
            Console.WriteLine("1. Agregar Transacción");
            Console.WriteLine("2. Agregar Cuenta");
            Console.WriteLine("3. Agregar Categoría");
            Console.WriteLine("4. Crear Presupuesto");
            Console.WriteLine("5. Ver Transacciones");
            Console.WriteLine("6. Ver Cuentas");
            Console.WriteLine("7. Ver Presupuestos");
            Console.WriteLine("8. Resumen Financiero");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuAgregarTransaccion();
                    break;
                case "2":
                    MenuAgregarCuenta();
                    break;
                case "3":
                    MenuAgregarCategoria();
                    break;
                case "4":
                    MenuCrearPresupuesto();
                    break;
                case "5":
                    MostrarTransacciones();
                    break;
                case "6":
                    MostrarCuentas();
                    break;
                case "7":
                    MostrarPresupuestos();
                    break;
                case "8":
                    MostrarResumenFinanciero();
                    break;
                case "0":
                    salir = true;
                    break;
                default:
                    MostrarMensajeYEsperar("Opción inválida.");
                    break;
            }
        }
    }

    private void MenuAgregarTransaccion()
    {
        Console.Clear();
        Console.WriteLine("=== Agregar Transaccion ===");

        var cuentas = _gestorDatos.ObtenerCuentas();

        if (cuentas.Count == 0)
        {
            Console.WriteLine("No hay cuentas registradas");
            return;
        }
        
        // Seleccionar el tipo de transaccion
        TipoTransaccion tipo = SeleccionarTipoTransaccion();
        
        // Seleccionar la categoria
        var categoriaDisponibles = _gestorDatos.ObtenerCategoriasPorTipo(tipo).ToList();
        if (categoriaDisponibles.Count == 0)
        {
            Console.WriteLine($"No hay categorias para {tipo}. Primero debe crear una categoria");
        }

        string categoria = SeleccionarCategoria(categoriaDisponibles);
        // Solicitar cuenta
        string nombreCuenta = SeleccionarCuenta(cuentas);
        
        // Solicitar el monto
        decimal monto = SolicitarDecimalPositivo("Ingreso el monto: ");
        
        // solicitar la descripcion
        Console.WriteLine("Ingresa una descripcion");
        string descripcion = Console.ReadLine() ?? string.Empty;
        
        // Solicitar fechar
        DateTime fecha = SolicitarFecha();
        
        var transaccion = new Transaccion
        {
            Descripcion = descripcion,
            Monto = monto,
            Fecha = fecha,
            Tipo = tipo,
            Categoria = categoria,
            Cuenta = nombreCuenta
        };

        if (_gestorDatos.RegistrarTransaccion(transaccion))
        {
            Console.WriteLine("Transaccion agregada con exito");
        }
        else
        {
            Console.WriteLine("Error al registrar la transaccion");
        }
    }

    private TipoTransaccion SeleccionarTipoTransaccion()
    {
        Console.WriteLine("Tipo de transaccion: ");
        Console.WriteLine("1. Ingreso");
        Console.WriteLine("2. Gasto");
        Console.Write("Seleccione una opcion: ");

        int opcion = SolicitarEntero(1, 2);
        return (opcion == 1) ? TipoTransaccion.Ingreso : TipoTransaccion.Gasto;
    }

    private int SolicitarEntero(int min, int max)
    {
        int valor;
        while (!int.TryParse(Console.ReadLine(), out valor) || valor < min || valor > max)
        {
            Console.Write($"Opcion invalida. Ingrese un numero entre {min} y {max}");
        }

        return valor;
    }
    
    private string SeleccionarCategoria(List<Categoria> categorias)
    {
        Console.WriteLine("\nCategorías disponibles:");
        for (int i = 0; i < categorias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categorias[i].Nombre}");
        }
        Console.Write("Seleccione una categoría: ");
            
        int opcion = SolicitarEntero(1, categorias.Count);
        return categorias[opcion - 1].Nombre;
    }
    
    private string SeleccionarCuenta(List<Cuenta> cuentas)
    {
        Console.WriteLine("\nCuentas disponibles:");
        for (int i = 0; i < cuentas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cuentas[i].Nombre} (Saldo: {cuentas[i].Saldo:C})");
        }
        Console.Write("Seleccione una cuenta: ");
            
        int opcion = SolicitarEntero(1, cuentas.Count);
        return cuentas[opcion - 1].Nombre;
    }
    
    private DateTime SolicitarFecha()
    {
        Console.Write("Ingrese la fecha (DD/MM/AAAA) o deje en blanco para usar la fecha actual: ");
        string fechaStr = Console.ReadLine();
            
        if (string.IsNullOrWhiteSpace(fechaStr))
        {
            return DateTime.Now;
        }
            
        DateTime fecha;
        while (!DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha))
        {
            Console.Write("Formato de fecha inválido. Intente nuevamente (DD/MM/AAAA): ");
            fechaStr = Console.ReadLine();
        }
            
        return fecha;
    }

    private decimal SolicitarDecimalPositivo(string mensaje)
    {
        Console.Write(mensaje);
        decimal valor;
        while (!decimal.TryParse(Console.ReadLine(), out valor) || valor <= 0)
        {
            Console.Write("Valor inválido. Debe ser un número positivo: ");
        }
        return valor;
    }
}