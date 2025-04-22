namespace Repaso_OOP;

// Una clase abstracta no se puede usar para crear objetos- no se puede instanciar
// var animal = new Animal();
public abstract class Animal
{
    public abstract void animalSound(); // metodo abstracto

    // Metodos no abstractos
    public static void dormir()
    {
        Console.WriteLine("zzzz");
    }
}