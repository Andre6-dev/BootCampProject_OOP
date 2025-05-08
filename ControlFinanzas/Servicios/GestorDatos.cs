using ControlFinanzas.Modelos;
using ControlFinanzas.Repositorios;

namespace ControlFinanzas.Servicios;

public class GestorDatos
{
    private readonly TransaccionRepositorio transaccionRepositorio;
    private readonly CuentaRepositorio cuentaRepositorio;
    private readonly CategoriaRepositorio categoriaRepositorio;
    private readonly PresupuestoRepositorio presupuestoRepositorio;

    public GestorDatos()
    {
        transaccionRepositorio = new TransaccionRepositorio();
        cuentaRepositorio = new CuentaRepositorio();
        categoriaRepositorio = new CategoriaRepositorio();
        presupuestoRepositorio = new PresupuestoRepositorio();
    }

    public bool RegistrarTransaccion(Transaccion transaccion)
    {
        // Obtener la cuenta
        var cuenta = cuentaRepositorio.ObtenerPorNombre(transaccion.Cuenta);
        if (cuenta == null)
            return false;
        
        // Asignar el ID a la transaccion
        transaccion.Id = transaccionRepositorio.GenerarNuevoId();
        
        // Actualizar el saldo
        if (transaccion.Tipo == TipoTransaccion.Ingreso)
        {
            cuenta.Saldo += transaccion.Monto;
        }
        else
        {
            cuenta.Saldo -= transaccion.Monto;
        }
        
        // Guardar transaccion
        transaccionRepositorio.Agregar(transaccion);
        
        // Actualizar el presupuesto si es un gasto
        if (transaccion.Tipo == TipoTransaccion.Gasto)
        {
            ActualizarPresupuesto(transaccion.Categoria, transaccion.Monto, transaccion.Fecha);
        }

        return true;
    }

    public void ActualizarPresupuesto(string categoria, decimal monto, DateTime fecha)
    {
        DateTime inicioMes = new DateTime(fecha.Year, fecha.Month, 1);
        Presupuesto presupuestoExistente = presupuestoRepositorio.ObtenerPresupuestoPorCategoriaMes(categoria, inicioMes);

        if (presupuestoExistente != null)
        {
            presupuestoExistente.MontoGastado += monto;
        }
    }

    public bool CrearCuenta(Cuenta cuenta)
    {
        if (cuentaRepositorio.ExisteNombre(cuenta.Nombre))
            return false;
        
        cuentaRepositorio.Agregar(cuenta);

        return true;
    }
    
    public bool CrearCategoria(Categoria categoria)
    {
        if (categoriaRepositorio.ExistaNombre(categoria.Nombre))
            return false;

        categoriaRepositorio.Agregar(categoria);
        return true;
    }

    public bool CrearPresupuesto(Presupuesto presupuesto)
    {
        if (presupuestoRepositorio.ExistePresupuesto(
                presupuesto.Categoria, 
                presupuesto.MesAño.Month, 
                presupuesto.MesAño.Year))
            return false;

        presupuestoRepositorio.Agregar(presupuesto);
        return true;
    }

    public List<Transaccion> ObtenerTransacciones() => transaccionRepositorio.ObtenerTodos();

    public List<Cuenta> ObtenerCuentas()
    {
        return cuentaRepositorio.ObtenerTodos();
    }

    public List<Categoria> ObtenerCategorias() => categoriaRepositorio.ObtenerTodos();

    public List<Categoria> ObtenerCategoriasPorTipo(TipoTransaccion tipo)
    {
        return categoriaRepositorio.obtenerCategoriasPorTipo(tipo);
    }

    public List<Presupuesto> ObtenerPresupuestos() => presupuestoRepositorio.ObtenerTodos();

    public decimal ObtenerSaldoTotal()
    {
        return cuentaRepositorio.ObtenerSaldoTotal();
    }

}