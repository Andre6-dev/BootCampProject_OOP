namespace Repaso_OOP;

public class Vehiculo
{
    // Propiedad
    public string marca = "Lamborghini";

    // Metodo de la clase vehiculo
    public void tocarClaxon()
    {
        Console.WriteLine("Tuu, tuu, tuuu");
    }

    public virtual void encender()
    {
        Console.WriteLine("Encender");
    }
    
    public virtual void apagar()
    {
        Console.WriteLine("Encender");
    }
}