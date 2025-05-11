using GestionPedidos.Repositorios;
using GestionPedidos.UI;

namespace GestionPedidos;

class Program
{
    static void Main(string[] args)
    {
        // Inicializar los repositorioes
        var clienteRepository = new ClienteRepository();
        var productoRepository = new ProductoRepository();
        var pedidoRepository = new PedidoRepository();
        
        // Agregar datos de ejemplo
        clienteRepository.AgregarDatosEjemplo();
        productoRepository.AgregarDatosEjemplo();
        
        // Crear el servicio de UI
        var uiManager = new UIManager(clienteRepository, productoRepository, pedidoRepository);
        
        // Ejecutar la aplicacion
        uiManager.EjecutarMenuPrincipal();
    }
}