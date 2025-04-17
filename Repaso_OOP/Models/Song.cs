using System.Runtime.CompilerServices;

namespace Repaso_OOP;

public class Song
{
    // Properties of the class
    private string _name;
    public string NamePublic
    {
        get => _name;
        set => _name = value;
    }
    public string _artist { get; set; }
    public string _genre { get; set; }
    public string _albumName { get; set; }
    public int _durationSeconds { get; set; }
        
    // Constructor -> sigue siendo un metodo
    public Song(string name, string artist, string genre, string albumName, int durationSeconds)
    {
        // Setting the values of the method in the properties of the class
        this._name = name;
        this._artist = artist;
        this._genre = genre;
        this._albumName = albumName;
        this._durationSeconds = durationSeconds;
    }

    public Song(string name, string artist, string albumName, int durationSeconds)
    {
        this._name = name;
        this._genre = artist;
        this._albumName = albumName;
        this._durationSeconds = durationSeconds;
    }

    public string mostrarNombreCancion()
    {
        return _name + " " + NamePublic;
    }
}