namespace InventoryManagement.Modelos;

public abstract class Auditoria
{
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
}