namespace InventoryManagement.Interfaces;

public class MovimientoStock : IMovimientoStock
{
    public void AgregarEntrada(SistemaInventario sistema)
    {
        Console.WriteLine("Dame la cantidad del producto");
        int cantidad = Convert.ToInt32(Console.ReadLine());
        
        // Como llamariamos aqui a la instancia de Producto o Empleado

        Console.WriteLine("Dame el id del producto");
        int idProducto = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Dame el id del empleado");
        int idEmpleado = Convert.ToInt32(Console.ReadLine());
        
        var producto = sistema.ObtenerProductoPorId(idProducto);
        var empleado = sistema.ObtenerEmpleadoPorId(idEmpleado);
        
        sistema.RegistrarEntrada(producto, cantidad, empleado);

        Console.WriteLine(
            $"El movimiento de stock de entrada {cantidad} se ha agregado correctamente al producto {producto.Nombre} y el usuario que lo hizo fue {empleado.Nombres}");
    }

    public void MostrarMovimientosPorProducto(SistemaInventario sistema)
    {
        Console.WriteLine("Dame el id del producto: ");
        int idProducto = int.Parse(Console.ReadLine());

        if (idProducto < 0)
        {
            throw new ArgumentException("El id del producto debe ser mayor que 0");
        }

        var listaMovimientosStock = sistema.ObtenerMovimientosPorProductoId(idProducto);
        Console.WriteLine("Lista de movimientos del stock");
        Console.WriteLine("ID\t\tNombre del Producto\tStock del producto\tMovimiento del stock\tEmpleado\tFecha de movimiento");
        Console.WriteLine("-------------------------------------");
        foreach (var movimiento in listaMovimientosStock)
        {
            Console.WriteLine($"{movimiento.Id}\t{movimiento.Producto.Nombre}\t{movimiento.Producto.Stock}\t{movimiento.cantidad}\t{movimiento.Empleado.Nombres}\t{movimiento.Fecha}");
        }
    }
}