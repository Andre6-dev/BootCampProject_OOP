using InventoryManagement.Modelos;

namespace InventoryManagement.Interfaces;

public interface IEmpleadoServicio
{
    
    void AgregarEmpleado(SistemaInventario sistema);
    void MostrarEmpleados(SistemaInventario sistema);
    void MostrarEmpleadoPorId(SistemaInventario sistema);
}