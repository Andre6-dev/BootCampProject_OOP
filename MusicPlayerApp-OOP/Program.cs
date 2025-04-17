namespace MusicPlayerApp_OOP;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Console Music Player!");
        Console.WriteLine("Demonstrating OOP Concepts...");

        // --- Object Creation ---
        // We are creating instances ('objects') of our defined classes.
        // Each object encapsulates its own data.

        // Create some Song objects
        // OOP Concept: Instantiation. Creating objects from the Song class blueprint.
        var song1 = new Song("Bohemian Rhapsody", "Queen", "A Night at the Opera", TimeSpan.FromMinutes(5.9));
        var song2 = new Song("Stairway to Heaven", "Led Zeppelin", "Led Zeppelin IV", TimeSpan.FromMinutes(8.0));
        var song3 = new Song("Hotel California", "Eagles", "Hotel California", TimeSpan.FromMinutes(6.5));
        var song4 = new Song("Like a Rolling Stone", "Bob Dylan", "Highway 61 Revisited", TimeSpan.FromMinutes(6.2));

        // Create an AudioBook object
        var audioBook1 = new AudioBook("The Hobbit", "J.R.R. Tolkien", "Rob Inglis", TimeSpan.FromHours(11.1));


        // --- Using the Player ---
        // Create an instance of the MusicPlayer
        // OOP Concept: Encapsulation. We interact with the player through its public methods
        // (LoadPlaylist, Play, Stop, etc.), without needing to know the internal details
        // of how it manages the playlist or playback state.
        var player = new MusicPlayer();

        // Create a list of playable items (demonstrates Polymorphism storage)
        // OOP Concept: Polymorphism. This list holds objects of different types
        // (Song, AudioBook) because they all implement the IPlayable interface
        // or inherit from the MediaItem base class which implements IPlayable.
        var myPlaylist = new List<IPlayable> // List of the common interface/base type
        {
            song1,
            song2,
            audioBook1, // We can mix types!
            song3,
            song4
        };

        // Load the list into the player
        player.LoadPlaylist(myPlaylist, "My Mixed Favorites");

        // --- User Interaction Loop ---
        string command = "";
        while (command?.ToLower() != "exit")
        {
            Console.WriteLine("\n--------------------");
            Console.WriteLine("Enter command: play, stop, next, prev, info, list, exit");
            Console.Write("> ");
            command = Console.ReadLine()?.ToLower(); // Read user input

            try // Basic error handling for player actions
            {
                switch (command)
                {
                    case "play":
                        player.Play();
                        break;
                    case "stop":
                        player.Stop();
                        break;
                    case "next":
                        player.Next();
                        break;
                    case "prev": // 'prev' for previous
                        player.Previous();
                        break;
                    case "info":
                        player.DisplayCurrentItemInfo();
                        break;
                    case "list":
                        player.DisplayPlaylist();
                        break;
                    case "exit":
                        Console.WriteLine("Exiting Music Player. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Catch potential errors from our classes (like null arguments)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        Console.WriteLine("\n--- OOP Concepts Demonstrated ---");
        Console.WriteLine(
            "1. Encapsulation: MusicPlayer hides its internal state (_playlist, _currentIndex, _isPlaying) and exposes controlled actions (Play, Stop, LoadPlaylist). Song/AudioBook bundle data (title, artist/author) with behavior (Play, DisplayDetails).");
        Console.WriteLine(
            "2. Abstraction: The IPlayable interface defines a contract for 'what' playable items can do (Play, DisplayDetails, Title) without specifying 'how'. MediaItem provides an abstract base, hiding some common details.");
        Console.WriteLine(
            "3. Inheritance: Song and AudioBook 'inherit' common properties (Title, Duration) and potentially behavior (default DisplayDetails) from the MediaItem base class, promoting code reuse.");
        Console.WriteLine(
            "4. Polymorphism: The List<IPlayable> holds different types (Song, AudioBook). When player.Play() is called, the correct Play() method (Song.Play or AudioBook.Play) is executed based on the actual object type (runtime polymorphism). The player interacts with them uniformly via the IPlayable contract/MediaItem base.");
        Console.WriteLine("-----------------------------------");
    }
}