namespace GestionPedidos.Modelos;

public class Cliente
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }

    public override string ToString()
    {
        return $"{Id}, {Nombre}, {Email}, {Telefono}";
    }
}