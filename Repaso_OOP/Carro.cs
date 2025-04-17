namespace Repaso_OOP;

// Herencia: clase padre y una clase hijo: La clase hijo va a heredar de la clase padre
public class Carro : Vehiculo
{
    public string modelo = "Aventador";
    
    // Polimorfismo
    public override void encender()
    {
        Console.WriteLine("Encender desde carro");
    }

    public override void apagar()
    {
        Console.WriteLine("Apagar desde carro");
    }
}