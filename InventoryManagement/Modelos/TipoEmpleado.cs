namespace InventoryManagement.Modelos;

public class TipoEmpleado
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public TipoEmpleado()
    {
        Id = _nextId++;
    }

}