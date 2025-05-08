namespace ControlFinanzas.Modelos;

public class Transaccion
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public TipoTransaccion Tipo { get; set; }
    public string Categoria { get; set; }
    public string Cuenta { get; set; }
}