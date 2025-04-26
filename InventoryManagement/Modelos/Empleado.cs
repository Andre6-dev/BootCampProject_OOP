using InventoryManagement.Modelos;

namespace InventoryManagement.Modelos;

public class Empleado : Auditoria
{
    private static int _nextId = 1;
    
    public int Id { get; private set; } 
    public string Nombres { get; set; }
    public TipoEmpleado TipoEmpleado { get; set; } // Yo puedo asignar otra clase como propiedad
    public EstadoEmpleado Estado { get; set; }
    public DateTime FechaIngreso { get; set; }
    public int Edad { get; set; }
    public Genero Genero { get; set; }
    
    public Empleado()
    {
        Id = _nextId++;
    }
}