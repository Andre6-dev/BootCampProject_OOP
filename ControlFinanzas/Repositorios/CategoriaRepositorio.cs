using ControlFinanzas.Modelos;

namespace ControlFinanzas.Repositorios;

public class CategoriaRepositorio : RepositorioBase<Categoria>
{
    public CategoriaRepositorio() : base("categorias")
    {
    }

    public bool ExistaNombre(string nombre)
    {
        return entidades.Any(x => x.Nombre == nombre);
    }

    public List<Categoria> obtenerCategoriasPorTipo(TipoTransaccion tipo)
    {
        return entidades.Where(c => c.TipoAsociado == tipo).ToList();
    }
    
    private void CrearCategoriasIniciales()
    {
        if (entidades.Count == 0)
        {
            entidades.Add(new Categoria { Nombre = "Salario", TipoAsociado = TipoTransaccion.Ingreso });
            entidades.Add(new Categoria { Nombre = "Alquiler", TipoAsociado = TipoTransaccion.Gasto });
            entidades.Add(new Categoria { Nombre = "Comida", TipoAsociado = TipoTransaccion.Gasto });
            entidades.Add(new Categoria { Nombre = "Transporte", TipoAsociado = TipoTransaccion.Gasto });
            entidades.Add(new Categoria { Nombre = "Entretenimiento", TipoAsociado = TipoTransaccion.Gasto });
            entidades.Add(new Categoria { Nombre = "Servicios", TipoAsociado = TipoTransaccion.Gasto });
        }
    }
}