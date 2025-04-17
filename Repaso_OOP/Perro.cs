namespace Repaso_OOP;

public class Perro : Animal
{
    public string nombre { get; set; }
    // Sobreescritura o overriding del metodo abstracto
    public override void animalSound()
    {
        Console.WriteLine($"El perro de nombre {nombre} hace guau guau guay");
    }
    
    public Perro(string nombrePerro)
    {
        nombre = nombrePerro;
    }
}