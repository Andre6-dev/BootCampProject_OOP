using ControlFinanzas.Modelos;

namespace ControlFinanzas.Repositorios;

public class PresupuestoRepositorio : RepositorioBase<Presupuesto>
{
    public PresupuestoRepositorio() : base("presupuestos") { }

    public Presupuesto? ObtenerPresupuestoPorCategoriaMes(string categoria, DateTime fecha)
    {
        return entidades.FirstOrDefault(
            p => p.Categoria == categoria && p.MesAño.Year == fecha.Year && p.MesAño.Month == fecha.Month
            );
    }

    public bool ExistePresupuesto(string categoria, int mes, int year)
    {
        return entidades.Any(
            p => p.Categoria == categoria && p.MesAño.Year == year && p.MesAño.Month == mes
        );
    }
}