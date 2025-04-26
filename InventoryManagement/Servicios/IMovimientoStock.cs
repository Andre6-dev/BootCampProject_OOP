namespace InventoryManagement.Interfaces;

public interface IMovimientoStock
{
    void AgregarEntrada(SistemaInventario sistema);
    void MostrarMovimientosPorProducto(SistemaInventario sistema);
}