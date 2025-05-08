namespace ControlFinanzas.Repositorios;

public interface IRepositorio<T>
{
    List<T> ObtenerTodos();
    void Agregar(T entidad);
}