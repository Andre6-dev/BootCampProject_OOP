namespace GestionPedidos.Modelos;

public class Cliente
{
    private static int _nextId = 1;
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }

    public Cliente()
    {
        Id = _nextId++;
    }

    public override string ToString()
    {
        return $"{Id}, {Nombre}, {Email}, {Telefono}";
    }
}