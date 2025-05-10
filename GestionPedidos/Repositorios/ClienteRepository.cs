using GestionPedidos.Modelos;

namespace GestionPedidos.Repositorios;

public class ClienteRepository
{
    private List<Cliente> _clientes = new List<Cliente>();

    public List<Cliente> ObtenerTodos()
    {
        return _clientes;
    }

    public Cliente ObtenerPorId(int id)
    {
        if (id < 0 || id >= _clientes.Count)
            throw new ArgumentNullException(nameof(id));

        return _clientes.FirstOrDefault(c => c.Id == id);
    }

    public void Agregar(Cliente cliente)
    {
        
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));
        
        _clientes.Add(cliente);
    }
    
    public void AgregarDatosEjemplo()
    {
        Agregar(new Cliente 
            { Nombre = "Juan Perez", Email = "juan@ejemplo.com", Telefono = "555-1234" });
        Agregar(new Cliente 
            { Nombre = "Maria Lopez", Email = "maria@ejemplo.com", Telefono = "555-5678" });
    }
    
}