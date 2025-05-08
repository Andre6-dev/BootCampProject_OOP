using ControlFinanzas.Modelos;

namespace ControlFinanzas.Repositorios;

public class CuentaRepositorio : RepositorioBase<Cuenta>
{
    public CuentaRepositorio() : base("cuentas") {}

    public Cuenta ObtenerPorNombre(string nombre)
    {
        return entidades.FirstOrDefault(x => x.Nombre == nombre);
    }

    public bool ExisteNombre(string nombre)
    {
        return entidades.Any(x => x.Nombre == nombre);
    }

    public decimal ObtenerSaldoTotal()
    {
        return entidades.Sum(x => x.Saldo);
    }
}