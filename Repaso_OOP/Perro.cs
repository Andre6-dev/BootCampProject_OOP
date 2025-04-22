namespace Repaso_OOP;

public class Perro : Animal, IHola, IAnimal
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

    public void metodoMultiple()
    {
        throw new NotImplementedException();
    }

    public void sonidoAnimal()
    {
        throw new NotImplementedException();
    }

    public int contarEdad(int edad)
    {
        throw new NotImplementedException();
    }

    public void maullar()
    {
        throw new NotImplementedException();
    }
}