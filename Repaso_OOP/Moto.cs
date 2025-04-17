namespace Repaso_OOP;

public class Moto : Vehiculo
{
    public string asientoDoble;
    
    public override void encender()
    {
        Console.WriteLine("Encender desde moto");
    }
    
    public override void apagar()
    {
        Console.WriteLine("Apagar desde moto");
    }
}