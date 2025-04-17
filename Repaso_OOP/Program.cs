namespace Repaso_OOP;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");
        // var song = new Song(
        //     "Something About Us", 
        //     "Daft Punk", 
        //     "Electronic", 
        //     "Discovery", 
        //     240
        //     );
        // Console.WriteLine(song.NamePublic);
        // Console.WriteLine(song._artist);
        // Console.WriteLine(song._genre);
        // Console.WriteLine(song._albumName);
        // Console.WriteLine(song._durationSeconds);
        //
        // var song2 = new Song("Around the World", "Daft Punk", "Electronic", "Discovery", 240);
        // Console.WriteLine(song2.NamePublic);
        // Console.WriteLine(song2._artist);
        // Console.WriteLine(song2._genre);
        // Console.WriteLine(song2._albumName);
        // Console.WriteLine(song2.mostrarNombreCancion());
        //
        // var song3 = new Song("Get Lucky", "Daft Punk", "Random Access Memories", 240);
        //
        // song3._genre = "Electronic";
        //
        // Console.WriteLine(song3._genre);
        
        var carro = new Carro();

        Console.WriteLine(carro.marca);
        carro.tocarClaxon();
        
        carro.encender();
        var moto = new Moto();
        moto.encender();

        var canela = new Perro("Canela");

        canela.animalSound();
        
        var gato = new Gato();
        gato.sonidoAnimal();
        int edadGato = gato.contarEdad(5);
        Console.WriteLine(edadGato);
    }
}