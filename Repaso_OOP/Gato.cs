namespace Repaso_OOP;

public class Gato : IAnimal, IHola
{
    public void sonidoAnimal()
    {
        Console.WriteLine("Miau miau miau");
    }

    public int contarEdad(int edad)
    {
        // condicionales
        // validar la edad
        // comparar el nombre
        return edad;
    }

    public void maullar()
    {
        Console.WriteLine(" asdasdas");
    }

    public void metodoMultiple()
    {
        Console.WriteLine("Hola desde la interfaz 2");
    }
}