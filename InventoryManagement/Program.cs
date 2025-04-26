using InventoryManagement.Interfaces;
using InventoryManagement.Modelos;
using InventoryManagement.Utilidades;
using MovimientoStock = InventoryManagement.Interfaces.MovimientoStock;

namespace InventoryManagement;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Inventory Management System ===\n");
        SistemaInventario sistema = new SistemaInventario();
        CargaDatos.CargarDatosIniciales(sistema);
        MenuUI.CargarMenuPrincipal(sistema);
    }
}