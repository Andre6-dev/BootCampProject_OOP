namespace ControlFinanzas.Modelos;

public class Presupuesto
{
    public string Categoria { get; set; }
    public decimal MontoAsignado { get; set; }
    public decimal MontoGastado { get; set; }
    public DateTime MesAño { get; set; }
}