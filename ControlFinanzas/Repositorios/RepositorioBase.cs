namespace ControlFinanzas.Repositorios;

public class RepositorioBase<T> : IRepositorio<T>
{
    protected List<T> entidades;
    protected readonly string rutaArchivo;

    protected RepositorioBase(string nombreArchivo)
    {
        rutaArchivo = $"{nombreArchivo}.json";
        CargarDatos();
    }
    
    public List<T> ObtenerTodos()
    {
        return entidades;
    }

    public void Agregar(T entidad)
    {
       entidades.Add(entidad);
    }
    
    protected void CargarDatos()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            // entidades = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
        else
        {
            entidades = new List<T>();
        }
    }
}