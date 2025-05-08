using ControlFinanzas.Modelos;

namespace ControlFinanzas.Repositorios;

public class TransaccionRepositorio : RepositorioBase<Transaccion>
{
    public TransaccionRepositorio() : base("transacciones")
    {
    }

    public int GenerarNuevoId()
    {
        return entidades.Count() > 0 ? entidades.Max(x => x.Id) + 1 : 1;
    }
}